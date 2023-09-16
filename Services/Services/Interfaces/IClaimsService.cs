namespace Services.Services.Interfaces
{
    public interface IClaimsService
    {
        public Guid GetCurrentUser { get; }
        public string GetEmail { get; }
        public string GetName { get; }
        public string GetPhoneNumber { get; }
    }
}
