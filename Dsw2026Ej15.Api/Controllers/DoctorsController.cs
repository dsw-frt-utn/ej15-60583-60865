using Dsw2026Ej15.Api.Models;
using Dsw2026Ej15.Domain.Entities;
using Dsw2026Ej15.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Dsw2026Ej15.Domain.Exceptions;

namespace Dsw2026Ej15.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorsController : ControllerBase
    {
        private readonly IPersistence _persistence;

        public DoctorsController(IPersistence persistence)
        {
            _persistence = persistence;
        }

        // i. Primer endpoint: POST
        [HttpPost]
        public IActionResult CreateDoctor([FromBody] CreateDoctorRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Name))
                throw new ValidationException("El Name es requerido.");

            if (string.IsNullOrWhiteSpace(request.LicenseNumber))
                throw new ValidationException("El LicenseNumber es requerido.");

            var speciality = _persistence.GetSpecialityById(request.SpecialityId);

            if (speciality == null)
                throw new ValidationException("La especialidad indicada no existe.");

            var newDoctor = new Doctor
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                LicenseNumber = request.LicenseNumber,
                IsActive = true,
                Speciality = speciality
            };

            _persistence.AddDoctor(newDoctor);

            return StatusCode(201, newDoctor);
        }

        // ii. Segundo endpoint: GET
        [HttpGet]
        public IActionResult GetActiveDoctors()
        {
            var allDoctors = _persistence.GetDoctors();
            var activeDoctors = allDoctors.Where(d => d.IsActive).ToList();
            
            return Ok(activeDoctors);
        }
    
        // iii Tercer endpoint: GET por ID
    [HttpGet("{id}")]
        public IActionResult GetDoctorById(Guid id)
        {
            var doctor = _persistence.GetDoctorById(id);

            if (doctor == null || !doctor.IsActive)
            {
                return NotFound("Medico no encontrado o inactivo.");
            }
            var response = new DoctorDetailResponse
            {
                Name = doctor.Name,
                LicenseNumber = doctor.LicenseNumber,
                SpecialityName = doctor.Speciality?.Name
            };
            return Ok(response);
        }
        // iv Cuarto endpoint: DELETE
        [HttpDelete("{id}")]
        public IActionResult DeleteDoctor(Guid id)
        {
            
            var doctor = _persistence.GetDoctorById(id);

            
            if (doctor == null || !doctor.IsActive)
            {
                return NotFound("Medico no encontrado o ya se encuentra inactivo.");
            }
   
            doctor.IsActive = false;
            return NoContent();
        }
    }
}