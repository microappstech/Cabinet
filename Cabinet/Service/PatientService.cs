using Cabinet.Data;
using Microsoft.AspNetCore.Components;

namespace Cabinet.Service
{
    public class PatientService : BaseService
    {
        public PatientService(CabinetContext context, NavigationManager navigationManager, IConfiguration configuration, NavigationManager navigation) : base(context, navigationManager, configuration, navigation)
        {
        }

        public async Task<IEnumerable<Models.Patient>> GetAll()
        {
            var items = Context.Patients.ToList();
            return await Task.FromResult(items);
        }
        public async Task<Models.Patient> GetItemById(int id)
        {
            return await Context.Patients.FindAsync(id);
        }
        public async Task<bool> UpdateItem(Models.Patient updated)
        {
            try
            {
                Context.Patients.Update(updated);
                await Context.SaveChangesAsync();
                return true;
            }
            catch
            {
                // Handle exceptions accordingly
                return false;
            }
        }
        public async Task<Models.Patient> CreateItem(Models.Patient item)
        {
            try
            {
                Context.Patients.Add(item);
                Context.SaveChanges();
                return item;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<bool> DeleteItem(int Id)
        {
            var itemToDelete = await Context.Patients.FindAsync(Id);

            if (itemToDelete == null)
            {
                return false;
            }

            try
            {
                Context.Patients.Remove(itemToDelete);
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
