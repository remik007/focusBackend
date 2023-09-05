namespace FocusAPI.Data
{
    public class AppUser
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Roles { get; set; }
        public List<string> RoleList
        {
            get
            {
                return Roles.Split(',').ToList();
            }
        }
    }
}
