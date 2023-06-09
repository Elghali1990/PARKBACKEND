using chrep.core.park.Models;
using Microsoft.EntityFrameworkCore;

namespace chrep.data.park.SqlServer
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasMany(e => e.Roles).WithMany(e => e.Users).UsingEntity<UserRole>();
            modelBuilder.Entity<Demande>().HasMany(d => d.Users).WithMany(d => d.Demandes).UsingEntity<UserDemande>();
            modelBuilder.Entity<Mission>().HasMany(d => d.Users).WithMany(d => d.Missions).UsingEntity<UserMission>();
            modelBuilder.Entity<Vehicle>().Property(v => v.Type_Matricule).HasComputedColumnSql("[Matricule] + '-' + [TypeVehicule]");
            modelBuilder.Entity<Mission>().HasMany(a => a.Absences).WithOne(m => m.Mission).HasForeignKey(a=>a.MissionId);
            modelBuilder.Entity<User>().HasMany(a => a.Absences).WithOne(m => m.User).HasForeignKey(a=>a.UserId);
            
        }

        public DbSet<Demande> Demandes { get; set; }
        public DbSet<Mission> Missions { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Absence> Absences { get; set; }


    }
}
