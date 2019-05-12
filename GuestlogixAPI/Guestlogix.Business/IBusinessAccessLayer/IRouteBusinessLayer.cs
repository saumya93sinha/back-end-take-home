using Guestlogix.Data.Repository;
using Guestlogix.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Guestlogix.Business.IBusinessAccessLayer
{
    public interface IRouteBusinessLayer
    {
        List<AirportModel> GetAirports();

        string GetShortestRoute(string origin, string destination);
    }
}