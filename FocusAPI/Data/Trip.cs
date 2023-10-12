﻿namespace FocusAPI.Data
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
        public int TripCategoryId { get; set; }
        public virtual TripCategory TripCategory { get; set; }
        public DateOnly From { get; set; }
        public DateOnly To { get; set; }
        public virtual List<Reservation> Reservations { get; set; }
        public Boolean IsEnabled { get; set; } = false;
    }
}
