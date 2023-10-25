using Microsoft.AspNetCore.Identity;

namespace Auth.Domains.Entities
{
    public class AppUser : IdentityUser<Guid>
    {
        public Guid ExternalId { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string? Role { get; set; } = default!;

    }
}
