namespace DataAccessLayer.Models;

public partial class Airport
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public double Latitude { get; set; }

    public double Longitude { get; set; }

    public virtual ICollection<Flight> FlightArrivalAirports { get; set; } = new List<Flight>();

    public virtual ICollection<Flight> FlightDepartureAirports { get; set; } = new List<Flight>();
}
