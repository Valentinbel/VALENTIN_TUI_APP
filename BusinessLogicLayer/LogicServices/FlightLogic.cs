using DataAccessLayer.DataServices;
using BOLDB = BusinessObjectLayer.DataBaseEntities;
using GeoCoordinatePortable;

namespace BusinessLogicLayer.LogicServices
{
    public class FlightLogic : IFlightLogic
    {
        private readonly IFlightDataDAL _flightDataDAL;
        private readonly IAirportDataDAL _airportDataDAL;
        public FlightLogic(IFlightDataDAL flightDataDAL, IAirportDataDAL airportDataDAL)
        {
            _flightDataDAL = flightDataDAL;
            _airportDataDAL = airportDataDAL;
        }

        public async Task<List<BOLDB::FlightBo>> GetFlights()
        {
            List <BOLDB::FlightBo> flights = new List<BOLDB::FlightBo>();
            flights = await _flightDataDAL.GetFlights();
            
            foreach (BOLDB::FlightBo flight in flights)
            {
                if (flight != null && 
                    (flight.DepartureAirportName == null || flight.ArrivalAirportName == null || flight.Distance == null))
                {
                    BOLDB::AirportBo airportDeparture = await _airportDataDAL.GetAirportById(flight.DepartureAirportId);
                    BOLDB::AirportBo airportArrival = await _airportDataDAL.GetAirportById(flight.ArrivalAirportId);

                    if (flight.DepartureAirportName == null)
                        flight.DepartureAirportName = airportDeparture.Name;
                    if (flight.ArrivalAirportName == null)
                        flight.ArrivalAirportName = airportArrival.Name;
                    if (flight.Distance == null)
                    { 
                        flight.Distance = await ComputedDistance(flight.DepartureAirportId, flight.ArrivalAirportId);
                        await _flightDataDAL.Save(flight);
                    }                      
                }
            }
            return flights;
        }

        public async Task<BOLDB::FlightBo> GetFlightById(int id)
        {
            return await _flightDataDAL.GetFlightById(id);
        }

        public async Task<string> Save(BOLDB::FlightBo flight)
        {
            flight.Distance = await ComputedDistance(flight.DepartureAirportId, flight.ArrivalAirportId);
            if (flight.Distance == 0)
                return "There is no flight for that";
            return await _flightDataDAL.Save(flight);
        }
        public async Task<string> RemoveFlight(int id)
        {
            return await _flightDataDAL.RemoveFlight(id);
        }

        public async Task<int> ComputedDistance(int airportDepartureId, int airportArrivalId)
        {
            BOLDB::AirportBo airportDeparture = await _airportDataDAL.GetAirportById(airportDepartureId);
            BOLDB::AirportBo airportArrival = await _airportDataDAL.GetAirportById(airportArrivalId);
            var departureCoord = new GeoCoordinate(airportDeparture.Latitude, airportDeparture.Longitude);
            var arrivalCoord = new GeoCoordinate(airportArrival.Latitude, airportArrival.Longitude);

            return (int)Math.Round(departureCoord.GetDistanceTo(arrivalCoord)) / 1000;
        }
    }
}
