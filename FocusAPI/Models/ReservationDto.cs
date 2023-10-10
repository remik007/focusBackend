using FocusAPI.Data;

namespace FocusAPI.Models
{
    public class ReservationDto
    {
        public int OwnerId { get; set; }
        public virtual AppUser Owner { get; set; }
        public int TripId { get; set; }
        public virtual Trip Trip { get; set; }
        public virtual List<Participant> Participants { get; set; }
    }
}
