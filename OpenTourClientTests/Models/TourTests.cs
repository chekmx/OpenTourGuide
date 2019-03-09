using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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
    }
}