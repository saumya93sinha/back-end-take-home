using Guestlogix.Business.BusinessAccessLayer;
using Guestlogix.Business.IBusinessAccessLayer;
using Guestlogix.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Guestlogix.WebAPI.Controllers
{
    public class RouteController : ApiController
    {
        private readonly IRouteBusinessLayer _routeBAL;

        public RouteController()
        {
            this._routeBAL = new RouteBusinessLayer();
        }

        /// <summary>
        /// This api method fetches the list of all Airports from Airport.csv that will be populated in the origin and destination dropdowns on UI
        /// </summary>
        /// <returns>List of AirportModel objects</returns>
        [HttpGet]
        [Route("route/getairports")]
        public List<AirportModel> GetAirports()
        {
            return _routeBAL.GetAirports();
        }

        /// <summary>
        /// This api method calculates the shortest route between two Airports
        /// </summary>
        /// <param name="routeSearchParam"></param>
        /// <returns>Shortest route as a string</returns>
        [HttpPost]
        [Route("route/getshortestroute")]
        public IHttpActionResult GetShortestRoute(RouteSearchParam routeSearchParam)
        {
            try
            {
                return Ok(_routeBAL.GetShortestRoute(routeSearchParam.Origin, routeSearchParam.Destination));
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }
    }
}