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

                var options = new JsonSerializerOptions{ PropertyNameCaseInsensitive = true};

                var specialitiesFromFile = JsonSerializer.Deserialize<List<Speciality>>(json, options);

                if(specialitiesFromFile != null)
                {
                    _specialities.AddRange(specialitiesFromFile);
                }
            }
        }
        public void AddDoctor(Doctor doctor)
        {
            _doctors.Add(doctor);
        }

        public Doctor? GetDoctorById(Guid id)
        {
            return _doctors.FirstOrDefault(d => d.Id == id);
        }

        public List<Doctor> GetDoctors()
        {
            return _doctors;
        }

        public void AddSpeciality(Speciality speciality)
        {
            _specialities.Add(speciality);
        }
        public List<Speciality> GetSpecialities()
        {
            return _specialities;
        }

        public Speciality? GetSpecialityById(Guid id)
        {
            return _specialities.FirstOrDefault(s => s.Id == id);
        }
    }
}
