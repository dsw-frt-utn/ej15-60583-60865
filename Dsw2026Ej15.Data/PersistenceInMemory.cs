using Dsw2026Ej15.Domain.Entities;
using Dsw2026Ej15.Domain.Interfaces;

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

        }
        public void AddDoctor(Doctor doctor)
        {
            
        }

        public void AddSpeciality(Speciality speciality)
        {
            throw new NotImplementedException();
        }

        public Doctor? GetDoctorById(Guid id)
        {
            throw new NotImplementedException();
        }

        public List<Doctor> GetDoctors()
        {
            throw new NotImplementedException();
        }

        public List<Speciality> GetSpecialities()
        {
            throw new NotImplementedException();
        }

        public Speciality? GetSpecialityById(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
