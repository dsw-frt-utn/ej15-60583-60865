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

            modelBuilder.Entity<Speciality>().HasData(
                new Speciality
                {
                    Id = new Guid("11111111-1111-1111-1111-111111111111"),
                    Name = "Cardiología",
                    Description = "Especialidad médica relacionada con el corazón"
                },
                new Speciality
                {
                    Id = new Guid("22222222-2222-2222-2222-222222222222"),
                    Name = "Pediatría",
                    Description = "Especialidad médica relacionada con la atención de niños"
                },
                new Speciality
                {
                    Id = new Guid("33333333-3333-3333-3333-333333333333"),
                    Name = "Traumatología",
                    Description = "Especialidad médica relacionada con lesiones óseas"
                }
            );

            modelBuilder.Entity<Doctor>()
                .HasOne(d => d.Speciality)
                .WithMany()
                .HasForeignKey(d => d.SpecialityId)
                .IsRequired();
        }
    }
}