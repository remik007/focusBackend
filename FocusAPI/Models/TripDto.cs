using FocusAPI.Data;

namespace FocusAPI.Models
{
    public class TripDto
    {
        public string ShortName { get; set; }
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public string Prize { get; set; }
        public string OldPrize { get; set; }
        public int AvailableSeats { get; set; }
        public int TransportTypeId { get; set; }
        public virtual TransportTypeDto TransportType { get; set; }
        public string ImageUrl { get; set; }
        public int TripCategoryId { get; set; }
        public virtual TripCategoryDto TripCategory { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public virtual List<ReservationDto> Reservations { get; set; }
        public Boolean IsEnabled { get; set; } = false;
        public int CurrentAvailableSeats { get; set; }
        public Boolean IsDeleted { get; set; }
    }
}
