using BOLDB = BusinessObjectLayer.DataBaseEntities;

namespace DataAccessLayer.DataServices
{
    public interface IAirportDataDAL
    {
        Task<List<BOLDB::AirportBo>> GetAirportsDDL();
        Task<BOLDB::AirportBo> GetAirportById(int id);
    }
}
