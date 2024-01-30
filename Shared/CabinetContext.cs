using Cabinet.Models;
using Microsoft.EntityFrameworkCore;

namespace Cabinet.Data
{
    public class CabinetContext:DbContext
    {
        public CabinetContext(DbContextOptions<CabinetContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            
            builder.Entity<Patient>()
                .HasMany(i => i.Appointments)
                .WithOne(i => i.Patient)
                .HasForeignKey(i => i.PatientId)
                .HasPrincipalKey(i => i.Id);

            //builder.Entity<Doctor>()
            //    .HasMany(i => i.Appointments)
            //    .WithOne(i => i.Doctor)
            //    .HasForeignKey(i => i.DoctorId)
            //    .HasPrincipalKey(i => i.Id);
                
            
        }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Assisstant> Assisstants { get; set;}
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Infirmier> Infirmiers { get; set;}
        public DbSet<Patient> Patients { get; set; }
    }
}
