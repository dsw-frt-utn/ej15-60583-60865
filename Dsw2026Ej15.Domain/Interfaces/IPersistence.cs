using Dsw2026Ej15.Domain.Entities;

namespace Dsw2026Ej15.Domain.Interfaces
{
    public interface IPersistence
    {
        List<Doctor> GetDoctors();
        Doctor? GetDoctorById(Guid id);
        void AddDoctor(Doctor doctor);

        List<Speciality> GetSpecialities();
        Speciality? GetSpecialityById(Guid id);
        void AddSpeciality(Speciality speciality);
    }
}