using Microsoft.Maps.MapControl.WPF;

namespace OpenTourInterfaces
{
    public interface IPointOfInterest
    {
        Location Location { get; set; }
        string Name { get; set; }
    }
}