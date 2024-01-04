using Cabinet.Data;
using Cabinet.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using System;
namespace Cabinet.Service
{
    public class AppointmentService : BaseService
    {
        public AppointmentService(CabinetContext context, NavigationManager navigationManager, IConfiguration configuration, NavigationManager navigation) : base(context, navigationManager, configuration, navigation)
        {
        }

        public async Task<IEnumerable<Models.Appointment>> GetAll()
        {
            var items = Context.Appointments.Include(i=>i.Patient).AsNoTracking();
            return await Task.FromResult(items);
        }
        public async Task<Models.Appointment> GetById(int id)
        {
            var item = Context.Appointments.Where(i => i.Id == id).Include(i => i.Patient).FirstOrDefault();
            
            if (item == null)
            {
                throw new CabinetException("cet rendez-vous ni plus exsists");
            }
            return await Task.FromResult(item);
        }

        public async Task<bool> DeleteItem(int id)
        {
            var item = Context.Appointments.Where(i => i.Id == id).FirstOrDefault();
            if (item == null)
            {
                throw new CabinetException("ce rendez-vous déjâ supprimé");
            }
            Context.Appointments.Remove(item);
            try
            {
                Context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> UpdateItem(Models.Appointment appointment)
        {
            //var item = Context.Assisstants.Where(i=>i.Id== assisstant.Id).FirstOrDefault();
            Context.Appointments.Update(appointment);
            try
            {
                Context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public async Task<Models.Appointment> CreateItem(Appointment appointment)
        {
            Context.Appointments.Add(appointment);
            try
            {
                Context.SaveChanges();
            }
            catch (Exception ex)
            {
                return null;
            }
            return await Task.FromResult(appointment);
        }

    }

}

