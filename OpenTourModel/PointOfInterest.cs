using Microsoft.Maps.MapControl.WPF;
using OpenTourInterfaces;

namespace OpenTourModel
{
    public class PointOfInterest : IPointOfInterest
    {
        public PointOfInterest(Location location)
        {
            this.Location = location;
        }

        public string Name { get; set; }
        public Location Location { get; set; }
    }
}