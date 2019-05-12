using Guestlogix.Data.IRepository;
using System;
using System.Collections.Generic;
using System.Web;
using Guestlogix.Models;
using Microsoft.VisualBasic.FileIO;
using System.Linq;

namespace Guestlogix.Data.Repository
{
    public class RouteRepository : IRouteRepository
    {
        #region PrivateVariables
        private List<AirportModel> airportList;
        private List<AirlineModel> airlineList;
        private List<RouteModel> routeList;
        private const string AirlinesFile = "airlines.csv";
        private const string AirportsFile = "airports.csv";
        private const string RoutesFile = "routes.csv";
        #endregion

        #region Properties
        public List<AirportModel> AirportList { get { return airportList; } }
        public List<AirlineModel> AirlineList { get { return airlineList; } }
        public List<RouteModel> RouteList { get { return routeList; } }
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor used by when api call is made
        /// </summary>
        public RouteRepository()
        {
            airportList = GetAirports();
            airlineList = GetAirlines();
            routeList = GetRoutes();
        }

        /// <summary>
        /// Constructor used while running Test cases
        /// </summary>
        /// <param name="airportList"></param>
        /// <param name="airlineList"></param>
        /// <param name="routeList"></param>
        public RouteRepository(List<AirportModel> airportList, List<AirlineModel> airlineList, List<RouteModel> routeList)
        {
            this.airportList = airportList;
            this.airlineList = airlineList;
            this.routeList = routeList;
        }
        #endregion

        #region PrivateMethods
        /// <summary>
        /// Used to populate AirlineList from airlines.csv
        /// </summary>
        /// <returns>List of objects of AirlineModel</returns>
        private List<AirlineModel> GetAirlines()
        {
            string path = HttpContext.Current.Server.MapPath("~/data/") + AirlinesFile;
            TextFieldParser txtParser = new TextFieldParser(path);
            List<AirlineModel> airlines = new List<AirlineModel>();
            txtParser.HasFieldsEnclosedInQuotes = true;
            txtParser.SetDelimiters(",");
            txtParser.ReadLine();
            while (!txtParser.EndOfData)
            {
                string[] fields = txtParser.ReadFields();
                airlines.Add(new AirlineModel
                {
                    Name = fields[0].Trim(),
                    TwoDigitCode = fields[1].Trim().ToUpper(),
                    ThreeDigitCode = fields[2].Trim().ToUpper(),
                    Country = fields[3].Trim()
                });
            }
            txtParser.Close();
            return airlines;
        }

        /// <summary>
        /// Used to populate RouteList from routes.csv
        /// </summary>
        /// <returns>List of objects of RouteModel</returns>
        private List<RouteModel> GetRoutes()
        {
            string path = HttpContext.Current.Server.MapPath("~/data/") + RoutesFile;
            TextFieldParser txtParser = new TextFieldParser(path);
            List<RouteModel> routes = new List<RouteModel>();
            txtParser.HasFieldsEnclosedInQuotes = true;
            txtParser.SetDelimiters(",");
            txtParser.ReadLine();
            while (!txtParser.EndOfData)
            {
                string[] fields = txtParser.ReadFields();
                routes.Add(new RouteModel
                {
                    AirlineId = fields[0].Trim().ToUpper(),
                    Origin = fields[1].Trim().ToUpper(),
                    Destination = fields[2].Trim().ToUpper()
                });
            }
            txtParser.Close();
            return routes;
        }

        /// <summary>
        /// Using Breadth First Search Algorithm to get all connections till 
        /// destination airport between AirportNodes of the path tree with shortest distance between them
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="destination"></param>
        /// <param name="originFlightTree"></param>
        /// <returns>Dictionary<string, RouteModel></returns>
        private Dictionary<string, RouteModel> GetDirectPathNodes(string origin, string destination, Dictionary<string, List<RouteModel>> originFlightTree)
        {
            Queue<string> breadthLevelAirportNodes = new Queue<string>();
            Dictionary<string, bool> traversedAirportNodes = new Dictionary<string, bool>();
            Dictionary<string, RouteModel> directPathNodes = new Dictionary<string, RouteModel>();
            breadthLevelAirportNodes.Enqueue(origin);
            traversedAirportNodes[origin] = true;
            while (breadthLevelAirportNodes.Count > 0)
            {
                string selectedAirport = breadthLevelAirportNodes.Dequeue();
                if (originFlightTree.ContainsKey(selectedAirport))
                {
                    foreach (var route in originFlightTree[selectedAirport])
                    {
                        if (!traversedAirportNodes.ContainsKey(route.Destination) || !traversedAirportNodes[route.Destination])
                        {
                            breadthLevelAirportNodes.Enqueue(route.Destination);
                            traversedAirportNodes[route.Destination] = true;
                            directPathNodes[route.Destination] = route;
                            if (route.Destination == destination)
                            {
                                break;
                            }
                        }
                    }
                }
            }

            return directPathNodes;
        }

        /// <summary>
        /// Returns the string value containing exact route by Flight name to be shown to users
        /// </summary>
        /// <param name="finalRoutes"></param>
        /// <returns>string</returns>
        private string GetShortestPath(List<RouteModel> finalRoutes)
        {
            string resultPath = String.Empty;
            foreach (var route in finalRoutes)
            {
                string airlineName = route.AirlineId + (airlineList.Exists(x => x.TwoDigitCode == route.AirlineId) ? "(" + airlineList.Where(x => x.TwoDigitCode == route.AirlineId).FirstOrDefault().Name + ")" : String.Empty);
                string originAirport = route.Origin + (airportList.Exists(x => x.IATA3 == route.Origin) ? "(" + airportList.Where(x => x.IATA3 == route.Origin).FirstOrDefault().Name + ")" : String.Empty);
                string destinationAirport = route.Destination + (airportList.Exists(x => x.IATA3 == route.Destination) ? "(" + airportList.Where(x => x.IATA3 == route.Destination).FirstOrDefault().Name + ")" : String.Empty);

                resultPath += (!String.IsNullOrEmpty(resultPath) ? " ==>> " : "") + originAirport + " to " + destinationAirport + " by " + airlineName + Environment.NewLine;
            }

            return resultPath;
        }
        #endregion

        #region PublicMethods
        /// <summary>
        /// Used to populate AirportList from airports.csv
        /// </summary>
        /// <returns>List of objects of AirportModel</returns>
        public List<AirportModel> GetAirports()
        {
            string path = HttpContext.Current.Server.MapPath("~/data/") + AirportsFile;
            TextFieldParser txtParser = new TextFieldParser(path);
            List<AirportModel> airports = new List<AirportModel>();
            txtParser.HasFieldsEnclosedInQuotes = true;
            txtParser.SetDelimiters(",");
            txtParser.ReadLine();
            while (!txtParser.EndOfData)
            {
                string[] fields = txtParser.ReadFields();
                //if (fields[3].Trim().ToUpper() != "\\N")
                //{
                    airports.Add(new AirportModel
                    {
                        Name = fields[0].Trim(),
                        City = fields[1].Trim(),
                        Country = fields[2].Trim(),
                        IATA3 = fields[3].Trim().ToUpper(),
                        Latitude = double.Parse(fields[4].Trim()),
                        Longitude = double.Parse(fields[5].Trim())
                    });
                //}
            }
            txtParser.Close();

            return airports.OrderBy(x => x.IATA3).ToList(); ;
        }

        /// <summary>
        /// Calculates shortest route by tree node and BFS method
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="destination"></param>
        /// <returns>string</returns>
        public string GetShortestRoute(string origin, string destination)
        {
            string shortestPath = ApplicationConstants.NoRouteFound;

            if (origin != null && destination != null && origin == destination)
            {
                shortestPath = ApplicationConstants.SameOriginDestination;
                return shortestPath;
            }

            if (origin != null && destination != null && airportList.Any(x => x.IATA3 == origin) && airportList.Any(x => x.IATA3 == destination))
            {
                origin = origin.ToUpper();
                destination = destination.ToUpper();

                //This step will convert tree node and child structures for all origins and flights originating from them
                Dictionary<string, List<RouteModel>> originFlightTree = new Dictionary<string, List<RouteModel>>();
                foreach (var route in routeList)
                {
                    if (!originFlightTree.ContainsKey(route.Origin))
                    {
                        originFlightTree[route.Origin] = new List<RouteModel>();
                    }
                    originFlightTree[route.Origin].Add(route);
                }

                //Using Breadth First Search Algorithm to get all connections till destination airport between AirportNodes of the path tree with shortest distance between them
                Dictionary<string, RouteModel> directPathNodes = GetDirectPathNodes(origin, destination, originFlightTree);

                //Get the shortest path between origin and destination airports
                List<RouteModel> finalRoutes = new List<RouteModel>();
                for (var i = destination; directPathNodes.ContainsKey(i); i = directPathNodes[i].Origin)
                {
                    finalRoutes.Insert(0, directPathNodes[i]);
                }

                if (finalRoutes.Count != 0)
                {
                    shortestPath = GetShortestPath(finalRoutes);
                }
            }
            else
            {
                shortestPath = ApplicationConstants.InvalidOriginDestination;
            }

            return shortestPath;
        }
        #endregion
    }
}