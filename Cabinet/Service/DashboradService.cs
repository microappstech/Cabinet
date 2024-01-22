using Cabinet.Data;
using Microsoft.AspNetCore.Components;
using Cabinet.Models;
using Microsoft.EntityFrameworkCore;

namespace Cabinet.Service
{
    public class DashboradService : GeneralService
    {
        public DashboradService(CabinetContext context, NavigationManager navigationManager, IConfiguration configuration, NavigationManager navigation) : base(context, navigationManager, configuration, navigation)
        {
        }

        
    }
}
