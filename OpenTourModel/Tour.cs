using System.Collections.Generic;
using System.Xml.Linq;
using OpenTourUtils;
using OpenTourInterfaces;
using Newtonsoft.Json;
using System.Linq;

namespace OpenTourModel
{
    public class Tour : ITour
    {
        private const string gpx = "gpx";
        private const string trk = "trk";
        private const string name = "name";

        public Tour() 
        {
            this.PointsOfInterest = new List<IPointOfInterest>();
        }

        [JsonConstructor]
        public Tour(Location center, List<Location> route, List<PointOfInterest> pointOfInterests)
        {
            this.Center = center;
            this.Route = route?.ToList<ILocation>();
            this.PointsOfInterest = pointOfInterests?.ToList<IPointOfInterest>();
        }

        public Tour(XDocument gpxDocument)
        {
            var gpxNameSpace = gpxDocument.Root.GetDefaultNamespace();
            this.Name = gpxDocument.Element(gpxNameSpace + gpx).Element(gpxNameSpace + trk).Element(gpxNameSpace + name).Value;
            this.Route = gpxDocument.ToLocationList<Location>().ToList<ILocation>();
            this.Center = this.Route.FirstOrDefault();
            this.PointsOfInterest = new List<IPointOfInterest>();
        }

        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("tags")]
        public List<string> Tags { get; set; }
        [JsonProperty("center")]
        public ILocation Center { get; set; }
        [JsonProperty("route")]
        public List<ILocation> Route { get; set; }
        [JsonProperty("pointsOfInterest")]
        public List<IPointOfInterest> PointsOfInterest { get; set; }
        [JsonProperty("zoomLevel")]
        public int ZoomLevel { get; set; }
    }
}
