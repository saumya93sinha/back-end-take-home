using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Guestlogix.Data
{
    public static class ApplicationConstants
    {
        public static string SameOriginDestination = "Origin Airport IATA and Destination Airport IATA are same!";
        public static string NoRouteFound = "No route found between origin and destination";
        public static string InvalidOriginDestination = "Invalid origin or destination";
    }
}