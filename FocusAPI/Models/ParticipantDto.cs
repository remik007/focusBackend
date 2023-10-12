using FocusAPI.Data;

namespace FocusAPI.Models
{
    public class ParticipantDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthday { get; set; }
        public string DocumentNumber { get; set; }
        public string PhoneNumber { get; set; }
        public int ReservationId { get; set; }
    }
}
