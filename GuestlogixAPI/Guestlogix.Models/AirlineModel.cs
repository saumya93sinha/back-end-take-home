namespace Guestlogix.Models
{
    public class AirlineModel
    {
        private string name;
        private string twoDigitCode;
        private string threeDigitCode;
        private string country;

        public AirlineModel()
        {

        }

        public AirlineModel(string name, string twoDigitCode, string threeDigitCode, string country)
        {
            this.name = name;
            this.twoDigitCode = twoDigitCode;
            this.threeDigitCode = threeDigitCode;
            this.country = country;
        }

        public string Name { get { return this.name; } set { this.name = value; } }
        public string TwoDigitCode { get { return this.twoDigitCode; } set { this.twoDigitCode = value; } }
        public string ThreeDigitCode { get { return this.threeDigitCode; } set { this.threeDigitCode = value; } }
        public string Country { get { return this.country; } set { this.country = value; } }
    }
}