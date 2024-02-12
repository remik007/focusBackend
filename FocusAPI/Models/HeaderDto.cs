using FocusAPI.Data;

namespace FocusAPI.Models
{
    public class HeaderDto
    {
        public List<string> Countries { get; set; }
        public List<string> DepartureCities { get; set; }
        public List<TripCategoryDto> TripCategories { get; set; }
        public List<TransportTypeDto> TransportTypes { get; set; }
        public List<SubPageDto> SubPages { get; set; }
        public ContactDto ContactDetails { get; set; }
    }
}
