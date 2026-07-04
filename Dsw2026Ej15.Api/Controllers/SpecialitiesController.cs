using Dsw2026Ej15.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Dsw2026Ej15.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpecialitiesController : ControllerBase
    {
        private readonly IPersistence _persistence;

        public SpecialitiesController(IPersistence persistence)
        {
            _persistence = persistence;
        }

        [HttpGet]
        public async Task<IActionResult> GetSpecialities()
        {
            var specialities = await _persistence.GetSpecialitiesAsync();

            return Ok(specialities);
        }
    }
}