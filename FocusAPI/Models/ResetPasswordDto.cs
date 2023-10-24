namespace FocusAPI.Models
{
    public class ResetPasswordDto
    {
        public string Login { get; set; }
        public string Token { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
