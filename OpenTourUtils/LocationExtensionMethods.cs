using Microsoft.Maps.MapControl.WPF;
using System;
using System.Device.Location;
using System.Linq;
using System.Xml.Linq;

namespace OpenTourUtils
{
    public static class LocationExtensionMethods
    {
        public static Location ToLocation(this GeoCoordinate location)
        {
            return new Location(location.Latitude, location.Longitude, location.Altitude);
        }

        public static LocationCollection ToLocationCollection(this XDocument gpxDocument)
        {
            LocationCollection gpxRoutePoints = new LocationCollection();

            foreach (XElement trackPoint in gpxDocument.Descendants().Where(n => n.Name.LocalName.Equals("trkpt")))
            {
                var atrributes = trackPoint.Attributes();
                try
                {
                    gpxRoutePoints.Add(new Location(double.Parse(trackPoint.Attribute("lat").Value), double.Parse(trackPoint.Attribute("lon").Value)));
                }
                catch
                {
                    // Most likely if these values don't exist in the file it is 
                    // formatted incorrectly or corrupt.  In a real app we would 
                    // display some kind of error message to the user. 
                }
            }

            return gpxRoutePoints;
        }
    }
}
