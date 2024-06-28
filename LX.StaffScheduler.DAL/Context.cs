using Microsoft.EntityFrameworkCore;

namespace LX.StaffScheduler.DAL
{
    public class Context : DbContext
    {
        public DbSet<Cafe> Cafes { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<UserContract> UserContracts { get; set; }
        public DbSet<WorkShift> WorkShifts { get; set; }

        public Context()
        {

        }
        public Context(DbContextOptions<Context> options)
            : base(options)
        {
        }
    
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WorkShift>()
            .HasOne(c => c.Cafe)
            .WithMany(w => w.Workshifts)
            .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<WorkShift>()
            .HasOne(e => e.Employee)
            .WithMany(w => w.Workshifts)
            .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Employee>()
                .HasIndex(l => l.Login)
                .IsUnique();

            modelBuilder.Entity<Employee>()
                .HasIndex(p => p.Phone)
                .IsUnique();

            base.OnModelCreating(modelBuilder);

        }
    }

}
