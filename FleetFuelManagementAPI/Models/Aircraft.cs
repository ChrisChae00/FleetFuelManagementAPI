namespace FleetFuelManagementAPI.Models
{
    public class Aircraft
    {
        public int Id { get; set; }
        public string UniqueId { get; set; }
        public DateTime Timestamp { get; set; }
        public double FuelRemaining { get; set; }
        public double AverageFuelConsumption { get; set; }
    }
}
