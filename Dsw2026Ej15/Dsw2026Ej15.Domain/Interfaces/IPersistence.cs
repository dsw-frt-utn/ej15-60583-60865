using System;
using System.Collections.Generic;
using System.Text;
using Dsw2026Ej15.Domain.Entities;

namespace Dsw2026Ej15.Domain.Interfaces
{
    public interface IPersistence
    {
        Task<IEnumerable<Doctor>> GetAllDoctos();
        Task<Doctor?> GetDoctorById(Guid id);
        Task<Speciality?> GetSpecialityById(Guid id);
        Task SaveDoctor(Doctor doctor);
        Task UpdateDoctor(Doctor doctor);
    }
}
