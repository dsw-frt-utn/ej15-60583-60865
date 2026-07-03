using Dsw2026Ej15.Api.Models;
using Dsw2026Ej15.Domain.Entities;
using Dsw2026Ej15.Domain.Exceptions;
using Dsw2026Ej15.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> CreateDoctor([FromBody] CreateDoctorRequest request)
        {
            if (request == null)
                throw new ValidationException("El cuerpo de la petición no puede estar vacío.");

            if (string.IsNullOrWhiteSpace(request.Name))
                throw new ValidationException("El Name es requerido.");

            if (string.IsNullOrWhiteSpace(request.LicenseNumber))
                throw new ValidationException("El LicenseNumber es requerido.");

            var speciality = await _persistence.GetSpecialityByIdAsync(request.SpecialityId);

            if (speciality == null)
                throw new ValidationException("La especialidad indicada no existe.");

            var newDoctor = new Doctor
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                LicenseNumber = request.LicenseNumber,
                IsActive = true,
                SpecialityId = request.SpecialityId,
                Speciality = speciality
            };

            await _persistence.AddDoctorAsync(newDoctor);

            return StatusCode(201, newDoctor);
        }

        // ii. Segundo endpoint: GET
        [HttpGet]
        public async Task<IActionResult> GetActiveDoctors()
        {
            var allDoctors = await _persistence.GetDoctorsAsync();

            var activeDoctors = allDoctors
                .Where(d => d.IsActive)
                .ToList();

            return Ok(activeDoctors);
        }

        // iii. Tercer endpoint: GET por ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDoctorById(Guid id)
        {
            var doctor = await _persistence.GetDoctorByIdAsync(id);

            if (doctor == null || !doctor.IsActive)
            {
                return NotFound("Médico no encontrado o inactivo.");
            }

            var response = new DoctorDetailResponse
            {
                Name = doctor.Name,
                LicenseNumber = doctor.LicenseNumber,
                SpecialityName = doctor.Speciality?.Name ?? string.Empty
            };

            return Ok(response);
        }

        // iv. Cuarto endpoint: DELETE
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDoctor(Guid id)
        {
            var doctor = await _persistence.GetDoctorByIdAsync(id);

            if (doctor == null || !doctor.IsActive)
            {
                return NotFound("Médico no encontrado o ya se encuentra inactivo.");
            }

            doctor.IsActive = false;

            await _persistence.UpdateDoctorAsync(doctor);

            return NoContent();
        }
    }
}