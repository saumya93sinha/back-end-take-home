using Microsoft.VisualStudio.TestTools.UnitTesting;
using Guestlogix.Data;
using Guestlogix.Data.IRepository;
using Guestlogix.Data.Repository;
using Guestlogix.Models;
using System.Collections.Generic;

namespace Guestlogix.WebAPI.Tests.Controllers
{
    [TestClass]
    public class RouteRepositoryTest
    {
        private IRouteRepository _routeRepository;

        [TestInitialize]
        public void TestInitialize()
        {
            List<AirportModel> airports = new List<AirportModel>();
            airports.Add(new AirportModel { IATA3 = "A1" });
            airports.Add(new AirportModel { IATA3 = "A2" });
            airports.Add(new AirportModel { IATA3 = "A3" });
            airports.Add(new AirportModel { IATA3 = "A4" });

            List<AirlineModel> airlines = new List<AirlineModel>();
            airlines.Add(new AirlineModel { TwoDigitCode = "A1" });
            airlines.Add(new AirlineModel { TwoDigitCode = "A2" });
            airlines.Add(new AirlineModel { TwoDigitCode = "A3" });
            airlines.Add(new AirlineModel { TwoDigitCode = "A4" });

            List<RouteModel> routes = new List<RouteModel>();
            routes.Add(new RouteModel { Origin = "A1", Destination = "A2" });
            routes.Add(new RouteModel { Origin = "A1", Destination = "A3" });
            routes.Add(new RouteModel { Origin = "A2", Destination = "A3" });
            routes.Add(new RouteModel { Origin = "A2", Destination = "A4" });
            _routeRepository = new RouteRepository(airports, airlines, routes); 
        }
        
        [TestMethod]
        public void GetShortestRoute_SameOriginDestination()
        {
            // Arrange
            string origin = "ABJ";
            string destination = "ABJ";

            // Act
           var result = _routeRepository.GetShortestRoute(origin,destination);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(ApplicationConstants.SameOriginDestination, result);
            
        }

        [TestMethod]
        public void GetShortestRoute_InvalidOriginDestination()
        {
            // Arrange
            string origin = "ABJ";
            string destination = "AoJ";

            // Act
            var result = _routeRepository.GetShortestRoute(origin, destination);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(ApplicationConstants.InvalidOriginDestination, result);

        }

        [TestMethod]
        public void GetShortestRoute_NullRoutes()
        {
            // Arrange
            string origin = null;
            string destination = null;

            // Act
            var result = _routeRepository.GetShortestRoute(origin, destination);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(ApplicationConstants.InvalidOriginDestination, result);

        }

        [TestMethod]
        public void GetShortestRoute_Case1()
        {
            // Arrange
            string origin = "A1";
            string destination = "A3";

            // Act
            var result = _routeRepository.GetShortestRoute(origin, destination);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreNotEqual(ApplicationConstants.InvalidOriginDestination, result);

        }
    }
    
}
