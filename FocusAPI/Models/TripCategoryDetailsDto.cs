﻿namespace FocusAPI.Models
{
    public class TripCategoryDetailsDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual List<GetTripDto> Trips { get; set; }
    }
}
