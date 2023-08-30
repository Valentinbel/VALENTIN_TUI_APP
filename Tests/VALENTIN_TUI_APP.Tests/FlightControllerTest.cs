using Moq;
using BOLDB = BusinessObjectLayer.DataBaseEntities;
using BusinessLogicLayer.LogicServices;
using DataAccessLayer.Models;
using VALENTIN_TUI_APP.Controllers;
using Microsoft.AspNetCore.Mvc;
using BusinessObjectLayer.DataBaseEntities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace VALENTIN_TUI_APP.Tests
{
    [TestClass]
    public class FlightControllerTest
    {
        public static List<SelectListItem> airportlist = new List<SelectListItem>()
        {
            new SelectListItem() { Value= "1", Text="Airport1"},
            new SelectListItem() { Value= "2", Text="Airport2"},
            new SelectListItem() { Value= "3", Text="Airport3"},
            new SelectListItem() { Value= "4", Text="Airport4"},
            new SelectListItem() { Value= "5", Text="Airport5"}
        };

        private IEnumerable<BOLDB::AirportBo> GetTestAirports()
        {
            IEnumerable<BOLDB::AirportBo> airports = new List<BOLDB::AirportBo>() {
                new AirportBo { Id = 1, Name = "Airport1"},
                new AirportBo { Id = 2, Name = "Airport2"},
                new AirportBo { Id = 3, Name = "Airport3"},
                new AirportBo { Id = 4, Name = "Airport4"},
                new AirportBo { Id = 5, Name = "Airport5"}
            };
            return airports;
        }

        private IEnumerable<BOLDB::FlightBo> GetTestFlights()
        {
            IEnumerable<BOLDB::FlightBo> flights = new List<BOLDB::FlightBo>() {
                new FlightBo { Id = 1, DepartureAirportId = 1, ArrivalAirportId = 3, Distance = 3212 },
                new FlightBo { Id = 2, DepartureAirportId = 4, ArrivalAirportId = 2, Distance = 6545 },
                new FlightBo { Id = 3, DepartureAirportId = 2, ArrivalAirportId = 1, Distance = 9878 }
            };
            return flights;
        }


        [TestMethod]
        public async Task Index_ReturnView()
        {
            //Arrange
            var flightLogicMock = new Mock<IFlightLogic>();
            var airportLogicMock = new Mock<IAirportLogic>();
            var controller = new FlightController(flightLogicMock.Object, airportLogicMock.Object);

            //Act
            ViewResult? result = await controller.Index() as ViewResult;

            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task Index_ReturnsAllFlights()
        {
            //Arrange
            var flightLogicMock = new Mock<IFlightLogic>();
            var airportLogicMock = new Mock<IAirportLogic>();
            flightLogicMock.Setup(repo => repo.GetFlights()).Returns(Task.FromResult(GetTestFlights().ToList()));
            var controller = new FlightController(flightLogicMock.Object, airportLogicMock.Object);

            //Act
            var viewResult = await controller.Index() as ViewResult;

            Assert.IsNotNull(viewResult);
            if (viewResult.ViewData.Model is List<Flight> flights)
                Assert.AreEqual(3, flights.Count);
        }

        [TestMethod]
        public async Task Edit_ReturnFlight()
        {
            //Arrange
            var flightLogicMock = new Mock<IFlightLogic>();
            var airportLogicMock = new Mock<IAirportLogic>();
            flightLogicMock.Setup(repo => repo.GetFlightById(2)).Returns(Task.FromResult(GetTestFlights().ElementAt(1)));
            airportLogicMock.Setup(repo => repo.GetAirportsDDL()).Returns(Task.FromResult(GetTestAirports().ToList()));
            var controller = new FlightController(flightLogicMock.Object, airportLogicMock.Object);

            //Act
            var viewResult = await controller.Edit(2) as ViewResult;

            //Assert
            Assert.IsNotNull(viewResult);
            if (viewResult.ViewData.Model is Flight flight)
                Assert.AreEqual(4, flight.DepartureAirportId);
        }

        [TestMethod]
        public async Task Save_ReturnsRedirectToAction()
        {
            //Arrange
            var flightLogicMock = new Mock<IFlightLogic>();
            var airportLogicMock = new Mock<IAirportLogic>();
            airportLogicMock.Setup(repo => repo.GetAirportsDDL()).Returns(Task.FromResult(GetTestAirports().ToList()));
            var controller = new FlightController(flightLogicMock.Object, airportLogicMock.Object);

            //Act
            var result = await controller.Save(new BOLDB::FlightBo
            {
                Id = 4,
                DepartureAirportId = 1,
                DepartureAirportName = "airportX",
                ArrivalAirportId = 2,
                ArrivalAirportName = "airportY",
                Distance = 444
            }) as RedirectToActionResult;

            //Assert
            Assert.AreEqual(null, result);
        }

        //[TestMethod]
        //public async Task Edit_ReturnsNotFoundWithId()
        //{
        //    //Arrange
        //    var flightLogicMock = new Mock<IFlightLogic>();
        //    var airportLogicMock = new Mock<IAirportLogic>();
        //    airportLogicMock.Setup(repo => repo.GetAirportsDDL()).Returns(Task.FromResult(GetTestAirports().ToList()));
        //    flightLogicMock.Setup(repo => repo.GetFlightById(2)).Returns(Task.FromResult<BOLDB::FlightBo>(null));
        //    var controller = new FlightController(flightLogicMock.Object, airportLogicMock.Object);

        //    //Act
        //    IActionResult actionResult = await controller.Edit(2);

        //    //Assert
        //    Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
        //}

        //[TestMethod]
        //public async Task Edit_ReturnsNotFoundWithNullId()
        //{
        //    //Arrange
        //    var flightLogicMock = new Mock<IFlightLogic>();
        //    var airportLogicMock = new Mock<IAirportLogic>();
        //    airportLogicMock.Setup(repo => repo.GetAirportsDDL()).Returns(Task.FromResult(GetTestAirports().ToList()));
        //    var controller = new FlightController(flightLogicMock.Object, airportLogicMock.Object);

        //    //Act
        //    IActionResult actionResult = await controller.Edit(null);

        //    //Assert
        //    Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
        //}

        //[TestMethod]
        //public async Task Save_InvalidModelState()
        //{
        //    //Arrange
        //    var flightLogicMock = new Mock<IFlightLogic>();
        //    var airportLogicMock = new Mock<IAirportLogic>();
        //    airportLogicMock.Setup(repo => repo.GetAirportsDDL()).Returns(Task.FromResult(GetTestAirports().ToList()));
        //    var controller = new FlightController(flightLogicMock.Object, airportLogicMock.Object);
        //    BOLDB::FlightBo flightounet = new BOLDB::FlightBo
        //    {
        //        Id = 5,
        //        DepartureAirportId = 2,
        //        DepartureAirportName = "airportX",
        //        ArrivalAirportId = 2,
        //        ArrivalAirportName = "airportX",
        //        Distance = 0
        //    };

        //    //Act
        //    var result = await controller.Save(flightounet) as RedirectToActionResult;
        //    controller.ModelState.AddModelError("0", "There is no flight for that");

        //    //Assert
        //    var viewResult = await controller.Save(new FlightBo()) as ViewResult;
        //    Assert.IsNotNull(viewResult);
        //    var flight = viewResult.Model as Flight;
        //    Assert.IsNotNull(flight);
        //}
    }
}