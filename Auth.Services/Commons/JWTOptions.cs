namespace Auth.Services.Commons
{
    public class JWTOptions
    {
        public string Secret { get; set; } = default!;
        public string Audience { get; set; } = default!;
        public string Issuer { get; set; } = default!;
    }
}
