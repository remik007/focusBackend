namespace FocusAPI.Data
{
    public class Reservation
    {
        public int Id { get; set; }
        public int OwnerId { get; set; }
        public virtual AppUser Owner { get; set; }
        public int TripId { get; set; } 
        public virtual Trip Trip { get; set; }
        public virtual List<Participant> Participants { get; set; }
        public virtual DateTime From { get { return Trip.From; } }
        public virtual DateTime To { get { return Trip.To; } }

        public DateTime DateCreated { get; set; }
        public Boolean IsConfirmed { get; set; }
        public Boolean IsPaid { get; set; }

    }
}
