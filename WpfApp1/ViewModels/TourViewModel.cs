using Microsoft.Maps.MapControl.WPF;
using OpenTourClient.Models;

namespace OpenTourClient.ViewModels
{
    public class TourViewModel
    {
        private Tour tour;

        public TourViewModel()
        {     
        }

        public TourViewModel(Tour tour)
        {
            this.tour = tour;
        }

        public string TxtTourName
        {
            get { return tour.Name; }
            set { tour.Name = value; }
        }

        public string TxtDescription
        {
            get { return tour.Description; }
            set { tour.Description = value; }
        }

        public int IntZoomLevel
        {
            get { return tour.ZoomLevel; }
            set { tour.ZoomLevel = value; }
        }

        public Location Center
        {
            get { return tour.Center; }
            set { tour.Center = value; }
        }

        public string TxtCenter
        {
            get { return string.Format("{0},{1}",tour.Center.Latitude, tour.Center.Longitude) ; }
        }

        public string TxtTags
        {
            get { return string.Join(", ", this.tour.Tags);  }
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
