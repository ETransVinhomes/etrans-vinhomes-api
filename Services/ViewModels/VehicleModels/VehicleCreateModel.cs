namespace Services.ViewModels.VehicleModels
{
    public class VehicleCreateModel
    {
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public int TotalSeat { get; set; } = 0;
        public string LicensePlate { get; set; } = default!;
        public Guid? DriverId { get; set; } = default!;
        //public Guid ProviderId { get; set; } = default!;
    }
}
