using Microsoft.EntityFrameworkCore;
using FleetFuelManagementAPI.Models;

namespace FleetFuelManagementAPI.Data
{
    public class FleetDbContext : DbContext
    {
        public FleetDbContext(DbContextOptions<FleetDbContext> options) : base(options) { }

        public DbSet<Aircraft> Aircrafts { get; set; }
    }
}
