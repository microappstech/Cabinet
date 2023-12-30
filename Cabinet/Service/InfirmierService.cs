using Cabinet.Data;
using Cabinet.Models;
using Microsoft.AspNetCore.Components;

namespace Cabinet.Service
{
    public class InfirmierService:BaseService
    {
        public InfirmierService(CabinetContext context, NavigationManager navigationManager, IConfiguration configuration, NavigationManager navigation) : base(context, navigationManager, configuration, navigation)
        {
        }

        public async Task<IEnumerable<Models.Infirmier>> GetAll()
        {
            var items = Context.Infirmiers.ToList();
            return await Task.FromResult(items);
        }
        public async Task<Models.Infirmier> GetItemById(int id)
        {
            return await Context.Infirmiers.FindAsync(id);
        }
        public async Task<bool> UpdateItem(Infirmier updatedInfirmier)
        {
            try
            {
                Context.Infirmiers.Update(updatedInfirmier);
                await Context.SaveChangesAsync();
                return true;
            }
            catch
            {
                // Handle exceptions accordingly
                return false;
            }
        }
        public async Task<Models.Infirmier> CreateItem(Models.Infirmier Infirmier)
        {
            try
            {
                Context.Infirmiers.Add(Infirmier);
                Context.SaveChanges();
                return Infirmier;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<bool> DeleteItem(int Id)
        {
            var itemToDelete = await Context.Infirmiers.FindAsync(Id);

            if (itemToDelete == null)
            {
                return false; 
            }

            try
            {
                Context.Infirmiers.Remove(itemToDelete);
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
