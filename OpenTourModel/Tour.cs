﻿using System.Collections.Generic;
using System.Xml.Linq;
using OpenTourUtils;
using OpenTourInterfaces;
using Newtonsoft.Json;
using System.Linq;
using System;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

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
        public Tour(Location center, List<Location> route, List<PointOfInterest> pointsOfInterest)
        {
            this.Center = center;
            this.Route = route?.ToList<ILocation>();
            this.PointsOfInterest = pointsOfInterest?.ToList<IPointOfInterest>();
        }

        public Tour(XDocument gpxDocument)
        {
            this.Id = Guid.NewGuid();
            var gpxNameSpace = gpxDocument.Root.GetDefaultNamespace();
            this.Name = gpxDocument.Element(gpxNameSpace + gpx).Element(gpxNameSpace + trk).Element(gpxNameSpace + name).Value;
            this.Route = gpxDocument.ToLocationList<Location>().ToList<ILocation>();
            this.Center = this.Route.FirstOrDefault();
            this.PointsOfInterest = new List<IPointOfInterest>();
        }
        [BsonId]
        [JsonProperty("_id")]
        public Guid Id { get; set; }
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
