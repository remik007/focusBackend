using FocusAPI.Data;

namespace FocusAPI.Models
{
    public class GetImageDto
    {
        public int Id { get; set; }
        public string? ImageName { get; set; }
        public string ImageContent { get; set; }
    }
}
