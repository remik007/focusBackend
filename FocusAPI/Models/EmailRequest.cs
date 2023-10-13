namespace FocusAPI.Models
{
    public class EmailRequest
    {
        public string To { get; }
   
        public string Subject { get; }

        public string? Body { get; }
    }
}
