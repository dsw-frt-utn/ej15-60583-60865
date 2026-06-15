using Dsw2026Ej15.Domain.Entities;

namespace Dsw2026Ej15.Domain.Interfaces
{
    public interface IPersistence
    {
        // Métodos para gestionar Doctores
        List<Doctor> GetDoctors();
        Doctor? GetDoctorById(Guid id);
        void AddDoctor(Doctor doctor);

        // Métodos para gestionar Especialidades
        List<Speciality> GetSpecialities();
        Speciality? GetSpecialityById(Guid id);
        void AddSpeciality(Speciality speciality);
    }
}