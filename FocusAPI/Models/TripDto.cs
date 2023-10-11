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
        public int TripTypeId { get; set; }
        public virtual TripCategoryDto TripCategory { get; set; }
        public DateOnly From { get; set; }
        public DateOnly To { get; set; }
        public virtual List<ReservationDto> Reservations { get; set; }
        public Boolean IsEnabled { get; set; } = false;
        public virtual int CurrentAvailableSeats
        {
            get
            {
                int reservationsCount = Reservations.Sum(x => x.Participants.Count);
                return AvailableSeats - reservationsCount;
            }
        }
    }
}
