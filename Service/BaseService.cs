using Cabinet.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Cabinet.Service
{
    public class BaseService
    {
        public CabinetContext Context
        {
            get
            {
                return this.context;
            }
        }

        private readonly CabinetContext context;
        private readonly NavigationManager navigationManager;
        private IConfiguration _configuration;
        private NavigationManager Navigation;
        public BaseService(CabinetContext context, NavigationManager navigationManager, IConfiguration configuration, NavigationManager navigation)
        {
            this.context = context;
            this.navigationManager = navigationManager;
            _configuration = configuration;
            Navigation = navigation;
        }

        public void Reset() => Context.ChangeTracker.Entries().Where(e => e.Entity != null).ToList().ForEach(e => e.State = EntityState.Detached);

        public void ReloadChanges() => Context.ChangeTracker.Entries().Where(e => e.Entity != null).ToList().ForEach(entry => entry.Reload());
    }
}
