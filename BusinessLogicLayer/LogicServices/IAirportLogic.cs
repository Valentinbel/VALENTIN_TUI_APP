using BOLDB = BusinessObjectLayer.DataBaseEntities;

namespace BusinessLogicLayer.LogicServices
{
    public interface IAirportLogic
    {
        Task<List<BOLDB::AirportBo>> GetAirportsDDL();
    }
}
