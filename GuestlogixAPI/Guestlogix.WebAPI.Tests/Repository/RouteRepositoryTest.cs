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
            //Set some initial data as we are not reading CSVs here
            List<AirportModel> airports = new List<AirportModel>();
            airports.Add(new AirportModel("Port Bouet Airport", "Abidjan", "Cote d'Ivoire", "ABJ", 5.261390209, -3.926290035));
            airports.Add(new AirportModel("Brussels Airport","Brussels","Belgium","BRU",50.90140152,4.48443985));
            airports.Add(new AirportModel("Lester B. Pearson International Airport","Toronto","Canada","YYZ",43.67720032,-79.63059998));
            airports.Add(new AirportModel("General Edward Lawrence Logan International Airport","Boston","United States","BOS",42.36429977,-71.00520325));

            List<AirlineModel> airlines = new List<AirlineModel>();
            airlines.Add(new AirlineModel("Air China", "AC", "CCA", "China"));
            airlines.Add(new AirlineModel("China Southern Airlines", "CZ", "CSN", "China"));
            airlines.Add(new AirlineModel("Southwest Airlines", "WN", "SWA", "United States"));
            airlines.Add(new AirlineModel("Turkish Airlines", "TK", "THY", "Turkey"));
            airlines.Add(new AirlineModel("United Airlines", "UA", "UAL", "United States"));
            airlines.Add(new AirlineModel("WestJet", "WS", "WJA", "Canada"));

            List<RouteModel> routes = new List<RouteModel>();
            routes.Add(new RouteModel("AC", "ABJ", "BRU"));
            routes.Add(new RouteModel("CZ", "BRU", "YYZ"));
            routes.Add(new RouteModel("UA", "ABJ", "BOS"));
            routes.Add(new RouteModel("TK", "BRU", "BOS"));

            _routeRepository = new RouteRepository(airports, airlines, routes);
        }
        
        /// <summary>
        /// Checks for same origin and destination
        /// </summary>
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

        /// <summary>
        /// Checks for origin and destination that are not present in Airports data
        /// </summary>
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

        /// <summary>
        /// Checks if null values are set for origin or destination
        /// </summary>
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

        /// <summary>
        /// ABJ and BRU has direct connection. This test validates that.
        /// </summary>
        [TestMethod]
        public void GetShortestRoute_CaseDirectRoute()
        {
            // Arrange
            string origin = "ABJ";
            string destination = "BRU";

            // Act
            var result = _routeRepository.GetShortestRoute(origin, destination);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("ABJ(Port Bouet Airport) to BRU(Brussels Airport) by AC(Air China)\r\n", result);

        }

        /// <summary>
        /// Checks if No Route found between origin and destination
        /// </summary>
        [TestMethod]
        public void GetShortestRoute_CaseNoRouteFound()
        {
            // Arrange
            string origin = "YYZ";
            string destination = "BOS";

            // Act
            var result = _routeRepository.GetShortestRoute(origin, destination);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(ApplicationConstants.NoRouteFound, result);

        }

        /// <summary>
        /// ABJ and BOS has direct route as well as Route via ABJ==>BRU==>BOS.
        /// This test checks if the method is returning the shortest route.
        /// </summary>
        [TestMethod]
        public void GetShortestRoute_CaseShortestDirectRoute()
        {
            // Arrange
            string origin = "ABJ";
            string destination = "BOS";

            // Act
            var result = _routeRepository.GetShortestRoute(origin, destination);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("ABJ(Port Bouet Airport) to BOS(General Edward Lawrence Logan International Airport) by UA(United Airlines)\r\n", result);

        }

        /// <summary>
        /// Checks for one connecting flight i.e. ABJ==>BRU==>YYZ
        /// </summary>
        [TestMethod]
        public void GetShortestRoute_CaseOneConnectingFlight()
        {
            // Arrange
            string origin = "ABJ";
            string destination = "YYZ";

            // Act
            var result = _routeRepository.GetShortestRoute(origin, destination);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("ABJ(Port Bouet Airport) to BRU(Brussels Airport) by AC(Air China)\r\n ==>> BRU(Brussels Airport) to YYZ(Lester B. Pearson International Airport) by CZ(China Southern Airlines)\r\n", result);

        }

        [TestCleanup]
        public void TestCleanup()
        {
            _routeRepository = null;
        }
    }
    
}
