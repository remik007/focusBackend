using Microsoft.AspNetCore.Mvc;

namespace FocusAPI.Models
{
    public class SearchDto
    {
        [FromQuery]
        public string? Country { get; set; }
        [FromQuery]
        public DateTime? From { get; set; }
        [FromQuery]
        public DateTime? To { get; set; }
        [FromQuery]
        public string? DepartureCity { get; set; }
        [FromQuery]
        public string? TransportType { get; set; }
    }
}
