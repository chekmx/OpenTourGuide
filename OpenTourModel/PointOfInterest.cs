using Newtonsoft.Json;
using OpenTourInterfaces;

namespace OpenTourModel
{
    public class PointOfInterest : IPointOfInterest
    {
        public PointOfInterest(ILocation location)
        {
            this.Location = location;
        }

        [JsonConstructor]
        public PointOfInterest(Location location)
        {
            this.Location = location;
        }

        public string Name { get; set; }
        public ILocation Location { get; set; }
    }
}