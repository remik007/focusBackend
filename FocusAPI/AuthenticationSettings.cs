namespace FocusAPI
{
    public class AuthenticationSettings
    {
        public string JwtKey { get; set; }
        public int JwtValidityInMinutes { get; set; }
        public int RefreshTokenValidityInDays { get; set; }
        public string JwtIssuer { get; set; }
    }
}
