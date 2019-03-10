using System;
using System.Collections.Generic;
using OpenTourInterfaces;
using OpenTourModel;

namespace OpenTourTourRepository
{
    public class FileTourRepository : ITourRepository<Tour>
    {

        public FileTourRepository()  {}

        private List<Tour> tours;

        public IEnumerable<Tour> LoadAll()
        {
            return tours ?? (tours = GenerateFakeTourData());
        }

        private static List<Tour> GenerateFakeTourData()
        {
            return new List<Tour>()
            {
                new Tour()
                {
                    Id = Guid.NewGuid(),
                    Name ="Pffaikersee Rundfahrt",
                    Description = "Easy Walk round the delightful Pffaikersee",
                    Tags = new List<string> { "Easy", "Wheel chair friendly", "Flat", "Round Trip"},
                    Center = new Location(47.339423, 8.776760),
                    ZoomLevel = 16
                },
                new Tour()
                {
                    Id = Guid.NewGuid(),
                    Name ="Greifensee Rundfahrt",
                    Description = "Easy Walk round the delightful Greifensee",
                    Tags = new List<string> { "Medium", "Wheel chair friendly", "Mostly  Flat", "Round Trip"},
                    Center = new Location(47.343601, 8.691503),
                    ZoomLevel = 8
                },
                new Tour()
                {
                    Id = Guid.NewGuid(),
                    Name ="Rosinli",
                    Description = "Walk up to Rosinli for the excellent view of the Zurich oberland",
                    Tags = new List<string> { "Difficult", "Steep", "Point to Point"},
                    Center = new Location(47.335700, 8.816237),
                    ZoomLevel = 14
                },
            };
        }

        public void Save(Tour tour)
        {
            this.tours.Add(tour);
        }
    }
}