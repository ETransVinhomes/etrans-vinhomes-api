using Services.Services.Interfaces;

namespace Services.Services
{
    public class CurrentTime : ICurrentTime
    {
        public DateTime GetCurrentTime() => DateTime.UtcNow;


    }
}
