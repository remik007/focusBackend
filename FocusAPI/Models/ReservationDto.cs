using FocusAPI.Data;

namespace FocusAPI.Models
{
    public class ReservationDto
    {
        public int OwnerId { get; set; }
        public int TripId { get; set; }
        public string TripName { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public virtual List<ParticipantDto> Participants { get; set; }
    }
}
