using Microsoft.Maps.MapControl.WPF;
using System;
using System.Collections.Generic;
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

        public static List<T> ToLocationList<T>(this XDocument gpxDocument) where T : OpenTourInterfaces.ILocation, new()
        {
            var gpxRoutePoints = new List<T>();

            foreach (XElement trackPoint in gpxDocument.Descendants().Where(n => n.Name.LocalName.Equals("trkpt")))
            {
                var atrributes = trackPoint.Attributes();
                try
                {
                    var location = new T();
                    location.Latitude = double.Parse(trackPoint.Attribute("lat").Value);
                    location.Longitude = double.Parse(trackPoint.Attribute("lon").Value);
                    gpxRoutePoints.Add(location);
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

        public static LocationCollection ToLocationCollection(this List<OpenTourInterfaces.ILocation> locationList)
        {
            LocationCollection collection = new LocationCollection();
            foreach (OpenTourInterfaces.ILocation location in locationList)
            {
                collection.Add(new Location(location.Latitude, location.Longitude));
            }
            return collection;
        }

        public static Location ToLocation(this OpenTourInterfaces.ILocation location)
        {
            return new Location(location.Latitude, location.Longitude);
        }

        public static OpenTourInterfaces.ILocation ToILocation<T>(this Location location) where T : OpenTourInterfaces.ILocation, new()
        {
            var iLocation = new T();
            location.Latitude = location.Latitude;
            location.Longitude = location.Longitude;
            return iLocation;
        }
    }
}
