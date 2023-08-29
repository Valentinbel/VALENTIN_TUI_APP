using Microsoft.AspNetCore.Mvc.Rendering;

namespace BusinessObjectLayer.DataBaseEntities
{
    public class FlightBo
    {
        public int Id { get; set; }

        public int DepartureAirportId { get; set; }
        public string? DepartureAirportName { get; set; }
        public int ArrivalAirportId { get; set; }
        public string? ArrivalAirportName { get; set; }
        public int? Distance { get; set; }
        public IEnumerable<SelectListItem>? Airports { get; set; }
    }
}
