using FleetFuelManagementAPI.Models;
using FleetFuelManagementAPI.Repositories;
using System.Globalization;

namespace FleetFuelManagementAPI.Services
{
    public class AircraftService
    {
        private readonly IAircraftRepository _repository;
        private readonly string _dataFilesPath;

        public AircraftService(IAircraftRepository repository)
        {
            _repository = repository;
            _dataFilesPath = Path.Combine(Directory.GetCurrentDirectory(), "DataFiles");
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

        public List<Aircraft> LoadAircraftDataFromFiles()
        {
            List<Aircraft> aircraftList = new List<Aircraft>();

            if (!Directory.Exists(_dataFilesPath))
                return aircraftList;

            var files = Directory.GetFiles(_dataFilesPath, "*.txt");

            foreach (var file in files)
            {
                var lines = File.ReadAllLines(file);

                foreach (var line in lines.Skip(1)) // Skip header
                {
                    var columns = line.Split(',');

                    if (columns.Length >= 3 && DateTime.TryParseExact(columns[1], "HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime timestamp))
                    {
                        aircraftList.Add(new Aircraft
                        {
                            UniqueId = columns[0].Trim(),
                            Timestamp = timestamp,
                            FuelRemaining = double.TryParse(columns[2], out double fuel) ? fuel : 0
                        });
                    }
                }
            }

            return aircraftList;
        }
    }
}
