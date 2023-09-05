namespace FocusAPI.Data
{
    public class Trip
    {
        public int Id { get; set; }
        public string ShortName { get; set; }
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set;}
        public string Prize { get; set; }
        public string OldPrize { get; set; }
        public int AvailableSeats { get; set; }
        public int TransportTypeId { get; set; }
        public virtual TransportType TransportType { get; set; }
        public string ImageUrl { get; set; }
        public int TripTypeId { get; set; }
        public virtual TripType TripType { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public virtual List<Reservation> Reservations { get; set; }
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
