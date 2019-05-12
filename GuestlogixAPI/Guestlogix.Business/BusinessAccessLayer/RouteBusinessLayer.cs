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
        public RouteBusinessLayer()
        {
            this._routeRepository = new RouteRepository();
        }

        public List<AirportModel> GetAirports()
        {
            return _routeRepository.GetAirports();
        }

        public string GetShortestRoute(string origin, string destination)
        {
            return _routeRepository.GetShortestRoute(origin, destination);
        }
    }
}