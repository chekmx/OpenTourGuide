using Microsoft.Maps.MapControl.WPF;
using System;
using System.Device.Location;

namespace OpenTourUtils
{
    public static class LocationExtensionMethods
    {
        public static Location ToLocation(this GeoCoordinate location)
        {
            return new Location(location.Latitude, location.Longitude, location.Altitude);
        }
    }
}
