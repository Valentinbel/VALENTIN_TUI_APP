using BOLDB = BusinessObjectLayer.DataBaseEntities;

namespace DataAccessLayer.DataServices
{
    public interface IFlightDataDAL
    {
        Task<List<BOLDB::FlightBo>> GetFlights();
        Task<BOLDB::FlightBo> GetFlightById(int id);
        Task<string> Save(BOLDB::FlightBo flight);
        Task<string> RemoveFlight(int id);
    }
}
