using Guestlogix.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Guestlogix.Data.IRepository
{
    public interface IRouteRepository
    {
        List<AirportModel> GetAirports();

        string GetShortestRoute(string origin, string destination);
    }
}
