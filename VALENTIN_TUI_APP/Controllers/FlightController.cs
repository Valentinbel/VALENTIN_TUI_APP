using Microsoft.AspNetCore.Mvc;
using BOLDB = BusinessObjectLayer.DataBaseEntities;
using BusinessLogicLayer.LogicServices;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace VALENTIN_TUI_APP.Controllers
{
    public class FlightController : Controller
    {
        private readonly IFlightLogic _flighLogic;
        private readonly IAirportLogic _airportLogic;
        public FlightController(IFlightLogic flighLogic, IAirportLogic airportLogic) 
        {
            _flighLogic = flighLogic;
            _airportLogic = airportLogic;
        }

        public async Task<IActionResult> Index()
        {
            List<BOLDB::FlightBo> flights = await _flighLogic.GetFlights();
            return View(flights);
        }
        public async Task<IActionResult> Create()
        {
            var flight = new BOLDB::FlightBo();
            flight.Airports = await PopulateAirports();
            return View(flight);
        }

        public async Task<IActionResult> Edit(string id) 
        {
            BOLDB::FlightBo flight = await _flighLogic.GetFlightById(Convert.ToInt32(id));
            flight.Airports = await PopulateAirports();
            return View("Create", flight);
        }

        public async Task<IActionResult> Save(BOLDB::FlightBo flight)
        {
            string Response = await _flighLogic.Save(flight);
            return Json(Response);
        }

        public async Task<IActionResult> Remove(string id)
        {
            string Response = await _flighLogic.RemoveFlight(Convert.ToInt32(id));
            return Json(Response);
        }

        private async Task<SelectList> PopulateAirports()
        {
            List<BOLDB::AirportBo> airports = await _airportLogic.GetAirportsDDL();
            IEnumerable<SelectListItem> airportslist = new SelectList(airports, "Id", "Name");
            return (SelectList)airportslist;
        }
    }
}
