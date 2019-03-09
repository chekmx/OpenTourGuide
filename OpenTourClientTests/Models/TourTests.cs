using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using OpenTourInterfaces;
using OpenTourModel;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace OpenTourClient.Models.Tests
{
    [TestClass()]
    public class TourTests
    {
        [TestMethod()]

        public void TourTest()
        {
            var filePath = "./TestData/testgpx.gpx";
            var gpxTestData = XDocument.Load(filePath);
            Tour testTour = new Tour(gpxTestData);
            testTour.Name.Should().Be("Wetzikon Cycling");
            testTour.Route.Count().Should().Be(2261);
        }

        [TestMethod()]
        public void TourTestDeSerialize()
        {
            var jsonSingle = "{\"name\":\"Pffaikersee Rundfahrt\",\"description\":\"Easy Walk round the delightful Pffaikersee\",\"tags\":[\"Easy\",\"Wheel chair friendly\",\"Flat\",\"Round Trip\"],\"center\":{\"latitude\":47.339423,\"longitude\":8.77676,\"altitude\":0,\"altitudeReference\":0},\"route\":null,\"pointsOfInterest\":[],\"zoomLevel\":16}";
            var jsonArray = "[{\"name\":\"Pffaikersee Rundfahrt\",\"description\":\"Easy Walk round the delightful Pffaikersee\",\"tags\":[\"Easy\",\"Wheel chair friendly\",\"Flat\",\"Round Trip\"],\"center\":{\"latitude\":47.339423,\"longitude\":8.77676,\"altitude\":0,\"altitudeReference\":0},\"route\":null,\"pointsOfInterest\":[],\"zoomLevel\":16},{\"name\":\"Greifensee Rundfahrt\",\"description\":\"Easy Walk round the delightful Greifensee\",\"tags\":[\"Medium\",\"Wheel chair friendly\",\"Mostly  Flat\",\"Round Trip\"],\"center\":{\"latitude\":47.343601,\"longitude\":8.691503,\"altitude\":0,\"altitudeReference\":0},\"route\":null,\"pointsOfInterest\":[],\"zoomLevel\":8},{\"name\":\"Rosinli\",\"description\":\"Walk up to Rosinli for the excellent view of the Zurich oberland\",\"tags\":[\"Difficult\",\"Steep\",\"Point to Point\"],\"center\":{\"latitude\":47.3357,\"longitude\":8.816237,\"altitude\":0,\"altitudeReference\":0},\"route\":null,\"pointsOfInterest\":[],\"zoomLevel\":14}]";

            var testTour  = JsonConvert.DeserializeObject<Tour>(jsonSingle);
            testTour.Name.Should().Be("Pffaikersee Rundfahrt");

            var testTourCollection = JsonConvert.DeserializeObject<List<Tour>>(jsonArray);
            testTourCollection.Count().Should().Be(3);
        }
    }
}