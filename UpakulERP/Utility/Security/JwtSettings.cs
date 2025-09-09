namespace Utility.Security
{
    public class JwtSettings
    {
        public const string SecretKey = "9AF624ED-0D1A-49E9-9A03-0C0DEB126716";
        public const string Issuer = "COAST Foundation";
        public const string Audience = "ApiUser";
        public const int ExpirationTokenMinutes = 30;
        public const int RefreshTokenExpirationMinutes = 30;
    }
}
