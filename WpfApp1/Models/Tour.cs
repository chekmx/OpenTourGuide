using Microsoft.Maps.MapControl.WPF;
using System.Collections.Generic;
using System.Xml.Linq;
using OpenTourUtils;

namespace OpenTourClient.Models
{
    public class Tour
    {
        private const string gpx = "gpx";
        private const string trk = "trk";
        private const string name = "name";

        public Tour() { }

        public Tour(XDocument gpxDocument)
        {
            var gpxNameSpace = gpxDocument.Root.GetDefaultNamespace();
            this.Name = gpxDocument.Element(gpxNameSpace + gpx).Element(gpxNameSpace + trk).Element(gpxNameSpace + name).Value;
            this.Route = gpxDocument.ToLocationCollection();
            this.Center = this.Route[0];
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public List<string> Tags { get; set; }
        public Location Center { get; set; }
        public LocationCollection Route { get; set; }
        public int ZoomLevel { get; set; }
    }
}
