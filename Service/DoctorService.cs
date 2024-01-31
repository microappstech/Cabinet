using Cabinet.Data;
using Cabinet.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
namespace Cabinet.Service
{
    public class DoctorService : BaseService
    {
        
        public DoctorService(CabinetContext context, NavigationManager navigationManager, IConfiguration configuration, NavigationManager navigation) : base(context, navigationManager, configuration, navigation)
        {
        }

        public  async Task<IEnumerable<Doctor>> GetDoctors()
        {
            var items = Context.Doctors.ToList();
            return await Task.FromResult(items);
        }
        public async Task<Doctor> GetDoctorById(int doctorId)
        {
            return await Context.Doctors.FindAsync(doctorId);
        }
        public async Task<bool> UpdateDoctor(Doctor updatedDoctor)
        {
            try
            {
                Context.Doctors.Update(updatedDoctor);
                await Context.SaveChangesAsync();
                return true;
            }
            catch
            {
                // Handle exceptions accordingly
                return false;
            }
        }
        public async Task<Doctor> CreateDoctor(Doctor doctor)
        {
            try
            {
                Context.Doctors.Add(doctor);
                Context.SaveChanges();
                return doctor;
            }catch(Exception ex)
            {
                throw;
            }
        }
        public async Task<bool> DeleteDoctor(int doctorId)
        {
            var doctorToDelete = await Context.Doctors.FindAsync(doctorId);

            if (doctorToDelete == null)
            {
                return false; // Doctor not found
            }

            try
            {
                Context.Doctors.Remove(doctorToDelete);
                await Context.SaveChangesAsync();
                return true;
            }
            catch
            {
                // Handle exceptions accordingly
                return false;
            }
        }
    }
}
