using Microsoft.EntityFrameworkCore;

namespace Cabinet.Data
{
    public class CabinetContext:DbContext
    {
        public CabinetContext(DbContextOptions<CabinetContext> options) : base(options) { }

        
    }
}
