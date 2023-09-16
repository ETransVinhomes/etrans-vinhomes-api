using Auth.Domains.Entities;

namespace Auth.Services.Services.Interfaces
{
    public interface IJWTTokenGenerator
    {
        string GenerateToken(AppUser user, IEnumerable<string> roles);
    }
}
