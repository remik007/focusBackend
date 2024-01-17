namespace FocusAPI.Models
{
    public class TripCategoryDetailsDto
    {
        public string Name { get; set; }
        public virtual List<TripDto> Trips { get; set; }
    }
}
