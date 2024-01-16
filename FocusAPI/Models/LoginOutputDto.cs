namespace FocusAPI.Models
{
    public class LoginOutputDto
    {
        public string Email { get; set; }
        public bool IsAdmin { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public DateTime Expiration { get; set; }
    }
}
