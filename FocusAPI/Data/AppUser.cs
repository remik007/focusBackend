namespace FocusAPI.Data
{
    public class AppUser
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public int UserRoleId { get; set; }
        public virtual UserRole UserRole { get; set; }
        public Boolean IsConfirmed { get; set; } = false;

    }
}
