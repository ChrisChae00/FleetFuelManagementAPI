using FleetFuelManagementAPI.Models;
using FleetFuelManagementAPI.Repositories;

namespace FleetFuelManagementAPI.Services
{
    public class AircraftService
    {
        private readonly IAircraftRepository _repository;

        public AircraftService(IAircraftRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Aircraft>> GetAllAircraftsAsync()
        {
            return await _repository.GetAllAircraftsAsync();
        }

        public async Task<Aircraft> GetAircraftByIdAsync(string uniqueId)
        {
            return await _repository.GetAircraftByIdAsync(uniqueId);
        }

        public async Task AddAircraftDataAsync(Aircraft aircraft)
        {
            aircraft.AverageFuelConsumption = CalculateFuelConsumption(aircraft);
            await _repository.AddAircraftDataAsync(aircraft);
        }

        private double CalculateFuelConsumption(Aircraft aircraft)
        {
            return aircraft.FuelRemaining > 0 ? aircraft.FuelRemaining / (DateTime.UtcNow - aircraft.Timestamp).TotalHours : 0;
        }
    }
}
