using OpenTourInterfaces;

namespace OpenTourModel
{
    public class Location : ILocation
    {
        public Location() { }

        public Location(ILocation location)
        {
            this.Longitude = location.Longitude;
            this.Latitude = location.Latitude;
        }

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