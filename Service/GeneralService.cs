﻿using Cabinet.Data;
using Cabinet.Models;
using Cabinet.Pages;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;

namespace Cabinet.Service
{
    public class GeneralService : BaseService
    {
        public GeneralService(CabinetContext context, NavigationManager navigationManager, IConfiguration configuration, NavigationManager navigation) : base(context, navigationManager, configuration, navigation)
        {
        }

        public async Task<int> GetNbHelpers()
        {
            var res = Context.Assisstants.AsNoTracking().Count();
            return await Task.FromResult(res);
        }
        public async Task<int> GetNbInfirmiers()
        {
            var res = Context.Infirmiers.AsNoTracking().Count();
            return await Task.FromResult(res);
        }
        public async Task<int> GetNbDoctors()
        {
            var res = Context.Doctors.AsNoTracking().Count();
            return await Task.FromResult(res);
        }
        public async Task<int> GetNbAppoitemet()
        {
            var res = Context.Appointments.AsNoTracking().Count();
            return await Task.FromResult(res);
        }

        public async Task<int> GetNbAppoitementPassed()
        {
            var res = Context.Appointments.Where(i => i.Passed == true).AsNoTracking().Count();
            return await Task.FromResult(res);
        }
        public async Task<int> GetNbAppoitementCanceled()
        {
            var res = Context.Appointments.Where(i => i.Annuled == true).AsNoTracking().Count();
            return await Task.FromResult(res);
        }
        public async Task<int> GetNbPatient()
        {
            var res = Context.Patients.AsNoTracking().Count();
            return await Task.FromResult(res);
        }
        public async Task<List<StatisticPoitement>> StatisticPoitements(int PatientId)
        {
            var result = Context.Appointments
                .Where(a => a.PatientId == PatientId)
                .GroupBy(a => new { a.PatientId, Month = a.DateAppointement.Value.Month })
                .Select(group => new StatisticPoitement
                {
                    NbVisits = group.Count(),
                    Months = group.Key.Month,
                })
                .OrderBy(resultItem => resultItem.NbVisits)
                .ToList();
            return await Task.FromResult(result);
        }
    }
}
