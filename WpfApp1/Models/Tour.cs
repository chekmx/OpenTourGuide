using Microsoft.Maps.MapControl.WPF;
using System.Collections.Generic;

namespace OpenTourClient.Models
{
    public class Tour
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<string> Tags { get; set; }
        public Location Center { get; set; }
        public int ZoomLevel { get; set; }
    }
}
