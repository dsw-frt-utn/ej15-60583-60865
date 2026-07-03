using Dsw2026Ej15.Domain.Entities;
using Dsw2026Ej15.Domain.Interfaces;
using System.Text.Json;

namespace Dsw2026Ej15.Data
{
    public class PersistenceInMemory : IPersistence
    {
        private readonly List<Doctor> _doctors;
        private readonly List<Speciality> _specialities;

        public PersistenceInMemory()
        {
            _doctors = new List<Doctor>();
            _specialities = new List<Speciality>();

            LoadSpecialities();
        }

        private void LoadSpecialities()
        {
            string filePath = "specialities.json";

            if (File.Exists(filePath))
            {
                var json = File.ReadAllText(filePath);

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                var specialitiesFromFile = JsonSerializer.Deserialize<List<Speciality>>(json, options);

                if (specialitiesFromFile != null)
                {
                    _specialities.AddRange(specialitiesFromFile);
                }
            }
        }

        public Task<List<Doctor>> GetDoctorsAsync()
        {
            return Task.FromResult(_doctors);
        }

        public Task<Doctor?> GetDoctorByIdAsync(Guid id)
        {
            var doctor = _doctors.FirstOrDefault(d => d.Id == id);
            return Task.FromResult(doctor);
        }

        public Task AddDoctorAsync(Doctor doctor)
        {
            _doctors.Add(doctor);
            return Task.CompletedTask;
        }

        public Task UpdateDoctorAsync(Doctor doctor)
        {
            var existingDoctor = _doctors.FirstOrDefault(d => d.Id == doctor.Id);

            if (existingDoctor != null)
            {
                _doctors.Remove(existingDoctor);
                _doctors.Add(doctor);
            }

            return Task.CompletedTask;
        }

        public Task<List<Speciality>> GetSpecialitiesAsync()
        {
            return Task.FromResult(_specialities);
        }

        public Task<Speciality?> GetSpecialityByIdAsync(Guid id)
        {
            var speciality = _specialities.FirstOrDefault(s => s.Id == id);
            return Task.FromResult(speciality);
        }

        public Task AddSpecialityAsync(Speciality speciality)
        {
            _specialities.Add(speciality);
            return Task.CompletedTask;
        }
    }
}