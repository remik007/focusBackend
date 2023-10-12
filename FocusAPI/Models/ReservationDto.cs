using FocusAPI.Data;

namespace FocusAPI.Models
{
    public class ReservationDto
    {
        public int OwnerId { get; set; }
        public int TripId { get; set; }
        public virtual List<ParticipantDto> Participants { get; set; }
    }
}
