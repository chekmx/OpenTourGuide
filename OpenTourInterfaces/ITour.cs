﻿using System;
using System.Collections.Generic;

namespace OpenTourInterfaces
{
    public interface ITour
    {
        Guid Id { get; set; }
        ILocation Center { get; set; }
        string Description { get; set; }
        string Name { get; set; }
        List<ILocation> Route { get; set; }
        List<string> Tags { get; set; }
        List<IPointOfInterest> PointsOfInterest { get; set; }
        int ZoomLevel { get; set; }
    }
}