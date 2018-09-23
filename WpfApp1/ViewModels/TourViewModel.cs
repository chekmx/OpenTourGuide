using Microsoft.Maps.MapControl.WPF;
using OpenTourClient.Models;

namespace OpenTourClient.ViewModels
{
    public class TourViewModel
    {
        private ITourRepository tourRepository;

        public Tour Tour { get; private set; }

        public TourViewModel(ITourRepository tourRepository)
        {
            this.tourRepository = tourRepository;
            this.Tour = tourRepository.LoadAll()[0];
        }

        public TourViewModel(ITourRepository tourRepository, Tour tour)
        {
            this.tourRepository = tourRepository;
            this.Tour = tour;
        }

        public string TxtTourName
        {
            get { return Tour.Name; }
            set { Tour.Name = value; }
        }

        public string TxtDescription
        {
            get { return Tour.Description; }
            set { Tour.Description = value; }
        }

        public int IntZoomLevel
        {
            get { return Tour.ZoomLevel; }
            set { Tour.ZoomLevel = value; }
        }

        public Location Center
        {
            get { return Tour.Center; }
            set { Tour.Center = value; }
        }

        public string TxtCenter
        {
            get { return string.Format("{0},{1}",Tour.Center.Latitude, Tour.Center.Longitude) ; }
        }

        public string TxtTags
        {
            get
            {
                if (this.Tour.Tags == null) return string.Empty;
                return string.Join(", ", this.Tour.Tags);
            }
        }

        public Pushpin PushpinLocation
        {
            get
            {
                var pushpin = new Pushpin();
                MapLayer.SetPosition(pushpin, this.Center);
                return pushpin;
            }
        }
    }
}
