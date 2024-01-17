namespace FocusAPI.Data
{
    public class TripCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual List<Trip> Trips { get; set; }
    }
}
