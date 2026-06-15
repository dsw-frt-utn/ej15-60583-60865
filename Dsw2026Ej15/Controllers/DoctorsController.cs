using Dsw2026Ej15.Api.Models;
using Dsw2026Ej15.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Numerics;

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

        [HttpPost]
        public IActionResult CreateDoctor([FromBody] CreateDoctorRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Name))
                return BadRequest("El Name es requerido.");

            if (string.IsNullOrWhiteSpace(request.LicenseNumber))
                return BadRequest("El LicenseNumber es requerido.");
            // Validar que la especialidad exista en la base de datos
            var speciality = _persistence.GetSpecialityById(request.SpecialityId);
            if (speciality == null)
                return BadRequest("La especialidad indicada no existe.");

            // 2. Crear la entidad de dominio
            var newDoctor = new Doctor
            {
                Id = Guid.NewGuid(), 
                Name = request.Name,
                LicenseNumber = request.LicenseNumber,
                IsActive = true,    
                Speciality = speciality
            };

            // 3. Guardar en la base de datos
            _persistence.AddDoctor(newDoctor);

            // 4. Respuesta exitosa: 201 Created
            return StatusCode(201, newDoctor);
        }
    }
}