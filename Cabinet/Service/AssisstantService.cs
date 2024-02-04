using Cabinet.Data;
using Cabinet.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace Cabinet.Service
{
    public class AssisstantService : BaseService
    {
        public AssisstantService(CabinetContext context, NavigationManager navigationManager, IConfiguration configuration, NavigationManager navigation) : base(context, navigationManager, configuration, navigation)
        {
        }
        public async Task<IEnumerable<Models.Assisstant>> GetAll()
        {
            var items= Context.Assisstants.AsNoTracking();
            return await Task.FromResult(items);
        }
        public async Task<Models.Assisstant> GetById(int id)
        {
            var item = Context.Assisstants.Where(i=>i.Id==id).FirstOrDefault();
            if(item==null)
            {
                throw new CabinetException("cet utilisateur ni plus exsisté");
            }
            return await Task.FromResult(item);
        }

        public async Task<bool> DeleteItem(int id)
        {
            var item = Context.Assisstants.Where(i=>i.Id==id).FirstOrDefault();
            if(item==null)
            {
                throw new CabinetException("cetutilsisateur déjâ supprimé");
            }
            Context.Assisstants.Remove(item);
            try
            {
                Context.SaveChanges();
                return true;
            }catch (Exception ex) 
            {
                return false;
            }
        }

        public async Task<bool> UpdateItem(Assisstant assisstant)
        {
            //var item = Context.Assisstants.Where(i=>i.Id== assisstant.Id).FirstOrDefault();
            Context.Assisstants.Update(assisstant);
            try
            {
                Context.SaveChanges();
                return true;
            }catch(Exception ex)
            {
                return false;
            }
        }
        public async Task<Assisstant> CreateItem(Assisstant assisstant)
        {
            Context.Assisstants.Add(assisstant);
            try
            {
                Context.SaveChanges();
            }
            catch(Exception ex)
            {
                return null;
            }
            return await Task.FromResult(assisstant);
        }

    }
}
