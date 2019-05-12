namespace Guestlogix.Models
{
    public class RouteModel
    {
        private string airlineId;
        private string origin;
        private string destination;

        public RouteModel() { }

        public RouteModel(string airlineId, string origin, string destination)
        {
            this.airlineId = airlineId;
            this.origin = origin;
            this.destination = destination;
        }

        public string AirlineId { get { return this.airlineId; } set { this.airlineId = value; } }
        public string Origin { get { return this.origin; } set { this.origin = value; } }
        public string Destination { get { return this.destination; } set { this.destination = value; } }
    }
}