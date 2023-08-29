using DataAccessLayer.DataServices;
using BOLDB = BusinessObjectLayer.DataBaseEntities;

namespace BusinessLogicLayer.LogicServices
{
    public class AirportLogic : IAirportLogic
    {

        private readonly IAirportDataDAL _airportDataDAL;

        public AirportLogic(IAirportDataDAL airportDataDAL)
        {
            _airportDataDAL = airportDataDAL;
        }

        public async Task<List<BOLDB::AirportBo>> GetAirportsDDL()
        {
            return await _airportDataDAL.GetAirportsDDL();
        }
    }
}
