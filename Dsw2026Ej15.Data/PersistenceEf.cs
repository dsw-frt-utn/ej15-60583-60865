using Dsw2026Ej15.Domain.Entities;
using Dsw2026Ej15.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Dsw2026Ej15.Data
{
    public class PersistenceEf : IPersistence
    {
        private readonly Dsw2026Ej15DbContext _context;

        public PersistenceEf(Dsw2026Ej15DbContext context)
        {
            _context = context;
        }

        public async Task<List<Doctor>> GetDoctorsAsync()
        {
            return await _context.Doctors
                .Include(d => d.Speciality)
                .ToListAsync();
        }

        public async Task<Doctor?> GetDoctorByIdAsync(Guid id)
        {
            return await _context.Doctors
                .Include(d => d.Speciality)
                .FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task AddDoctorAsync(Doctor doctor)
        {
            await _context.Doctors.AddAsync(doctor);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateDoctorAsync(Doctor doctor)
        {
            var existingDoctor = await _context.Doctors
                .Include(d => d.Speciality)
                .FirstOrDefaultAsync(d => d.Id == doctor.Id);

            if (existingDoctor == null)
            {
                return;
            }

            existingDoctor.Name = doctor.Name;
            existingDoctor.LicenseNumber = doctor.LicenseNumber;
            existingDoctor.IsActive = doctor.IsActive;
            existingDoctor.Speciality = doctor.Speciality;

            await _context.SaveChangesAsync();
        }

        public async Task<List<Speciality>> GetSpecialitiesAsync()
        {
            return await _context.Specialities.ToListAsync();
        }

        public async Task<Speciality?> GetSpecialityByIdAsync(Guid id)
        {
            return await _context.Specialities
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task AddSpecialityAsync(Speciality speciality)
        {
            await _context.Specialities.AddAsync(speciality);
            await _context.SaveChangesAsync();
        }
    }
}