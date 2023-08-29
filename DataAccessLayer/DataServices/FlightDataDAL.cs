using DataAccessLayer.Models;
using BOLDB = BusinessObjectLayer.DataBaseEntities;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.DataServices
{
    public class FlightDataDAL : IFlightDataDAL
    {
        private readonly ValentinTuiDbContext _valentinTuiDbContext;

        public FlightDataDAL(ValentinTuiDbContext valentinTuiDbContext)
        {
            _valentinTuiDbContext = valentinTuiDbContext;
        }

        public async Task<List<BOLDB::FlightBo>> GetFlights()
        {
            var _data = await _valentinTuiDbContext.Flights.ToListAsync();
            
            List<BOLDB::FlightBo> _flights = new();
            List<BOLDB::AirportBo> _airports = new();
            
            if (_data != null && _data.Count > 0)
            {
                _data.ForEach(item =>
                {
                    _flights.Add(new BOLDB.FlightBo()
                    {
                        Id = item.Id,
                        DepartureAirportId = item.DepartureAirportId,
                        ArrivalAirportId = item.ArrivalAirportId,
                        Distance = item.Distance
                    });
                });
            }
            return _flights;
        }

        public async Task<BOLDB::FlightBo> GetFlightById(int id)
        {
            var _data = await _valentinTuiDbContext.Flights.FirstOrDefaultAsync(item => item.Id == id);
            BOLDB::FlightBo _flight = new BOLDB::FlightBo();

            if (_data != null)
            {
                _flight = (new BOLDB::FlightBo()
                {
                    Id = _data.Id,
                    DepartureAirportId = _data.DepartureAirportId,
                    ArrivalAirportId = _data.ArrivalAirportId,
                    Distance = _data.Distance
                });
            }
            return _flight;
        }

        public async Task<string> Save(BOLDB::FlightBo flight)
        {
            string Response = string.Empty;
            try
            {
                if (flight.Id > 0)
                {
                    var _exist = await _valentinTuiDbContext.Flights.FirstOrDefaultAsync(item => item.Id == flight.Id);
                    if (_exist != null)
                    {
                        _exist.DepartureAirportId = flight.DepartureAirportId;
                        _exist.ArrivalAirportId = flight.ArrivalAirportId;
                        _exist.Distance = flight.Distance;
                    }
                }
                else
                {
                    Flight _flight = new()
                    {
                        DepartureAirportId = flight.DepartureAirportId,
                        ArrivalAirportId = flight.ArrivalAirportId,
                        Distance = flight.Distance,
                    };
                    await _valentinTuiDbContext.Flights.AddAsync(_flight);
                }
                await _valentinTuiDbContext.SaveChangesAsync();
                Response = "pass";
            }
            catch (Exception ex)
            {
                Response = ex.Message;
            }
            return Response;
        }

        public async Task<string> RemoveFlight(int id)
        {
            var _data = await _valentinTuiDbContext.Flights.FirstOrDefaultAsync(item => item.Id == id); 
            string Response = string.Empty;

            if (_data != null)
            {
                try
                {
                    _valentinTuiDbContext.Flights.Remove(_data);
                    await _valentinTuiDbContext.SaveChangesAsync();
                    Response = "pass";
                }
                catch (Exception ex)
                {
                    Response = ex.Message;
                }
            }
            return Response;
        }
    }
}
