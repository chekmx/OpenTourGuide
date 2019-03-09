using Microsoft.Maps.MapControl.WPF;
using OpenTourInterfaces;

namespace OpenTourModel
{
    public class PointOfInterest : IPointOfInterest
    {
        public PointOfInterest(ILocation location)
        {
            this.Location = location;
        }

        public string Name { get; set; }
        public ILocation Location { get; set; }
    }
}