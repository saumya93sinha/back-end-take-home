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
    //[Authorize]
    public class RouteController : ApiController
    {
        private readonly IRouteBusinessLayer _routeBAL;

        public RouteController()
        {
            this._routeBAL = new RouteBusinessLayer();
        }

        [HttpGet]
        [Route("route/getairports")]
        public List<AirportModel> GetAirports()
        {
            return _routeBAL.GetAirports();
        }

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
