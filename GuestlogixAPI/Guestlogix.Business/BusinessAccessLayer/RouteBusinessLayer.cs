using Guestlogix.Business.IBusinessAccessLayer;
using Guestlogix.Data.IRepository;
using Guestlogix.Data.Repository;
using Guestlogix.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Guestlogix.Business.BusinessAccessLayer
{
    public class RouteBusinessLayer : IRouteBusinessLayer
    {
        private readonly IRouteRepository _routeRepository;

        /// <summary>
        /// Business Access Layer to fetch data from Data Access Layer
        /// </summary>
        public RouteBusinessLayer()
        {
            this._routeRepository = new RouteRepository();
        }

        /// <summary>
        /// Fetches list of Airports
        /// </summary>
        /// <returns>List of AirportModel objects</returns>
        public List<AirportModel> GetAirports()
        {
            return _routeRepository.GetAirports();
        }

        /// <summary>
        /// Calculates shortest path between Airports
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="destination"></param>
        /// <returns>Shortest Route as string</returns>
        public string GetShortestRoute(string origin, string destination)
        {
            return _routeRepository.GetShortestRoute(origin, destination);
        }
    }
}