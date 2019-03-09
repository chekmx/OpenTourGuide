using OpenTourInterfaces;

namespace OpenTourModel
{
    public class Location : ILocation
    {
        public Location() { }

        public Location(double latitude, double longitude)
        {
            this.Longitude = longitude;
            this.Latitude = latitude;
        }

        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public double Altitude { get; set; }
    }
}