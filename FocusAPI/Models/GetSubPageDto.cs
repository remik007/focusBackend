namespace FocusAPI.Models
{
    public class GetSubPageDto
    {
        public int Id { get; set; }
        public string ShortName { get; set; }
        public string? Name { get; set; }
        public string? ShortDescription { get; set; }
        public string? Description { get; set; }
    }
}
