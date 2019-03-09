using Microsoft.Maps.MapControl.WPF;
using System.Collections.Generic;
using OpenTourInterfaces;
using OpenTourModel;

namespace OpenTourClient.ViewModels
{
    public class TourRepository : ITourRepository
    {

        public TourRepository()  {}

        private List<ITour> tours;

        public IEnumerable<ITour> LoadAll()
        {
            return tours ?? (tours = GenerateFakeTourData());
        }

        private static List<ITour> GenerateFakeTourData()
        {
            return new List<ITour>()
            {
                new Tour()
                {
                    Name ="Pffaikersee Rundfahrt",
                    Description = "Easy Walk round the delightful Pffaikersee",
                    Tags = new List<string> { "Easy", "Wheel chair friendly", "Flat", "Round Trip"},
                    Center = new Location(47.339423, 8.776760),
                    ZoomLevel = 16
                },
                new Tour()
                {
                    Name ="Greifensee Rundfahrt",
                    Description = "Easy Walk round the delightful Greifensee",
                    Tags = new List<string> { "Medium", "Wheel chair friendly", "Mostly  Flat", "Round Trip"},
                    Center = new Location(47.343601, 8.691503),
                    ZoomLevel = 8
                },
                new Tour()
                {
                    Name ="Rosinli",
                    Description = "Walk up to Rosinli for the excellent view of the Zurich oberland",
                    Tags = new List<string> { "Difficult", "Steep", "Point to Point"},
                    Center = new Location(47.335700, 8.816237),
                    ZoomLevel = 14
                },
            };
        }

        public void Save(ITour tour)
        {
            this.tours.Add(tour);
        }
    }
}