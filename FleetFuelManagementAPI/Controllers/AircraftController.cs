using Microsoft.AspNetCore.Mvc;
using FleetFuelManagementAPI.Models;
using FleetFuelManagementAPI.Services;

namespace FleetFuelManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AircraftController : ControllerBase
    {
        private readonly AircraftService _aircraftService;

        public AircraftController(AircraftService aircraftService)
        {
            _aircraftService = aircraftService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAircrafts()
        {
            var aircrafts = await _aircraftService.GetAllAircraftsAsync();
            return Ok(aircrafts);
        }

        [HttpGet("{uniqueId}")]
        public async Task<IActionResult> GetAircraftById(string uniqueId)
        {
            var aircraft = await _aircraftService.GetAircraftByIdAsync(uniqueId);
            if (aircraft == null) return NotFound();
            return Ok(aircraft);
        }

        [HttpPost]
        public async Task<IActionResult> AddAircraft([FromBody] Aircraft aircraft)
        {
            if (aircraft == null) return BadRequest("Invalid data");

            await _aircraftService.AddAircraftDataAsync(aircraft);
            return CreatedAtAction(nameof(GetAircraftById), new { uniqueId = aircraft.UniqueId }, aircraft);
        }
    }
}
