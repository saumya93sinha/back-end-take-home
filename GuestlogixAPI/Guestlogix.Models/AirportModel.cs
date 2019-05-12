namespace Guestlogix.Models
{
    public class AirportModel
    {

        private string name;
        private string city;
        private string country;
        private string iATA3;
        private double latitude;
        private double longitude;

        public AirportModel() { }

        public AirportModel(string name, string city, string country, string iATA3, double latitude, double longitude)
        {
            this.name = name;
            this.city = city;
            this.country = country;
            this.iATA3 = iATA3;
            this.latitude = latitude;
            this.longitude = longitude;
        }

        public string Name { get { return this.name; } set { this.name = value; } }
        public string City { get { return this.city; } set { this.city = value; } }
        public string Country { get { return this.country; } set { this.country = value; } }
        public string IATA3 { get { return this.iATA3; } set { this.iATA3 = value; } }
        public double Latitude { get { return this.latitude; } set { this.latitude = value; } }
        public double Longitude { get { return this.longitude; } set { this.longitude = value; } }
    }
}