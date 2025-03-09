using FleetFuelManagementAPI.Models;
using FleetFuelManagementAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace FleetFuelManagementAPI.Repositories
{
    public class AircraftRepository : IAircraftRepository
    {
        private readonly FleetDbContext _context;

        public AircraftRepository(FleetDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Aircraft>> GetAllAircraftsAsync()
        {
            return await _context.Aircrafts.ToListAsync();
        }

        public async Task<Aircraft> GetAircraftByIdAsync(string uniqueId)
        {
            return await _context.Aircrafts.FirstOrDefaultAsync(a => a.UniqueId == uniqueId);
        }

        public async Task AddAircraftDataAsync(Aircraft aircraft)
        {
            _context.Aircrafts.Add(aircraft);
            await _context.SaveChangesAsync();
        }
    }
}
