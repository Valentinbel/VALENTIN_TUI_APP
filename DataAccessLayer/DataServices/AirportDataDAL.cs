using DataAccessLayer.Models;
using BOLDB = BusinessObjectLayer.DataBaseEntities;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.DataServices
{
    public class AirportDataDAL : IAirportDataDAL
    {
        private readonly ValentinTuiDbContext _valentinTuiDbContext;
        public AirportDataDAL(ValentinTuiDbContext valentinTuiDbContext)
        {
            _valentinTuiDbContext = valentinTuiDbContext;
        }

        public async Task<List<BOLDB::AirportBo>> GetAirportsDDL()
        {
            var _data = await _valentinTuiDbContext.Airports.ToListAsync();

            List<BOLDB::AirportBo> _airports = new();

            if (_data != null && _data.Count > 0)
            {
                _data.ForEach(item =>
                {
                    _airports.Add(new BOLDB.AirportBo()
                    {
                        Id = item.Id,
                        Name = item.Name
                    });
                });
            }
            return _airports.OrderBy(item => item.Name).ToList();
        }
        public async Task<BOLDB::AirportBo> GetAirportById(int id)
        {
            var _data = await _valentinTuiDbContext.Airports.FirstOrDefaultAsync(item => item.Id == id);
            BOLDB::AirportBo _airport = new();

            if (_data != null)
            {
                _airport = (new BOLDB::AirportBo()
                {
                    Id = _data.Id,
                    Name = _data.Name,
                    Latitude = _data.Latitude,
                    Longitude = _data.Longitude
                });
            }
            return _airport;
        }
    }
}
