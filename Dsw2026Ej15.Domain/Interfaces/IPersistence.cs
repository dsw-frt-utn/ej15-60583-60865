using Dsw2026Ej15.Domain.Entities;

namespace Dsw2026Ej15.Domain.Interfaces
{
    public interface IPersistence
    {
        Task<List<Doctor>> GetDoctorsAsync();
        Task<Doctor?> GetDoctorByIdAsync(Guid id);
        Task AddDoctorAsync(Doctor doctor);
        Task UpdateDoctorAsync(Doctor doctor);

        Task<List<Speciality>> GetSpecialitiesAsync();
        Task<Speciality?> GetSpecialityByIdAsync(Guid id);
        Task AddSpecialityAsync(Speciality speciality);
    }
}