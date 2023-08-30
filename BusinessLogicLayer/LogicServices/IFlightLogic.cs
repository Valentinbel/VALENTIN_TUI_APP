using BOLDB = BusinessObjectLayer.DataBaseEntities;

namespace BusinessLogicLayer.LogicServices
{
    public interface IFlightLogic
    {
        Task<List<BOLDB::FlightBo>> GetFlights();
        Task<BOLDB::FlightBo> GetFlightById(int id);
        Task<string> Save(BOLDB::FlightBo flight);
        Task<string> RemoveFlight(int id);
    }
}
