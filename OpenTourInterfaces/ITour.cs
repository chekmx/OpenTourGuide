using System.Collections.Generic;
using Microsoft.Maps.MapControl.WPF;

namespace OpenTourInterfaces
{
    public interface ITour
    {
        Location Center { get; set; }
        string Description { get; set; }
        string Name { get; set; }
        LocationCollection Route { get; set; }
        List<string> Tags { get; set; }
        int ZoomLevel { get; set; }
    }
}