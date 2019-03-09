using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenTourInterfaces;
using OpenTourModel;
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
            ITour testTour = new Tour(gpxTestData);
            testTour.Name.Should().Be("Wetzikon Cycling");
            testTour.Route.Count().Should().Be(2261);
        }
    }
}