using Dsw2026Ej15.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Dsw2026Ej15.Data
{
    public class Dsw2026Ej15DbContext : DbContext
    {
        public Dsw2026Ej15DbContext(DbContextOptions<Dsw2026Ej15DbContext> options)
            : base(options)
        {
        }

        public DbSet<Doctor> Doctors { get; set; } = null!;
        public DbSet<Speciality> Specialities { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Doctor>()
                .HasOne(d => d.Speciality)
                .WithMany()
                .HasForeignKey(d => d.SpecialityId)
                .IsRequired();
        }
    }
}