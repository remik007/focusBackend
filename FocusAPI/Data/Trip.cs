namespace FocusAPI.Data
{
    public class Trip
    {
        public int Id { get; set; }
        public string ShortName { get; set; }
        public string Country { get; set; }
        public string DepartureCity { get; set; }
        public string? Name { get; set; }
        public string? ShortDescription { get; set; }
        public string Description { get; set;}
        public string Prize { get; set; }
        public string? OldPrize { get; set; }
        public int AvailableSeats { get; set; }
        public int TransportTypeId { get; set; }
        public virtual TransportType? TransportType { get; set; }
        public string? ImageName { get; set; }
        public string? ImageContent { get; set; }
        public int TripCategoryId { get; set; }
        public virtual TripCategory? TripCategory { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public virtual List<Reservation>? Reservations { get; set; }
        public Boolean IsEnabled { get; set; } = false;
        public Boolean? IsDeleted { get; set; }
    }
}
