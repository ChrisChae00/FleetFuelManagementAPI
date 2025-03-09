using FleetFuelManagementAPI.Models;

namespace FleetFuelManagementAPI.Repositories
{
    public interface IAircraftRepository
    {
        Task<IEnumerable<Aircraft>> GetAllAircraftsAsync();
        Task<Aircraft> GetAircraftByIdAsync(string uniqueId);
        Task AddAircraftDataAsync(Aircraft aircraft);
    }
}
