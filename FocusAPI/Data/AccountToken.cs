namespace FocusAPI.Data
{
    public class AccountToken
    {
        public int Id { get; set; }
        public string Token { get; set; }
        public int UserId { get; set; }
        public virtual AppUser User { get; set; }
        public DateTime Created { get; set; }
    }
}
