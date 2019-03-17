using MaterialDesignThemes.Wpf;
using Microsoft.Maps.MapControl.WPF;
using OpenTourClient.Views;
using OpenTourInterfaces;
using OpenTourModel;
using OpenTourUtils;
using System;
using System.Linq;
using System.Windows.Media;

namespace OpenTourClient.ViewModels
{
    public class TourViewModel
    {
        private ITourRepository<Tour> tourRepository;

        public Tour Tour { get; private set; }

        public TourViewModel(ITourRepository<Tour> tourRepository)
        {
            this.tourRepository = tourRepository;
            this.Tour = tourRepository.LoadAll().ToList()[0];
        }

        public TourViewModel(ITourRepository<Tour> tourRepository, Tour tour)
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

        public OpenTourInterfaces.ILocation Center
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

            set
            {
                this.Tour.Tags = value.Replace(", ", ",").Split(',').ToList();
            }
        }

        internal void Save()
        {
            this.tourRepository.Save(this.Tour);
        }

        public Pushpin PushpinLocation
        {
            get
            {
                var pushpin = new Pushpin();
                pushpin.Background = Brushes.Green;
                MapLayer.SetPosition(pushpin, this.Center.ToLocation());
                return pushpin;
            }
        }

        public Card GetPointOfInterest(IPointOfInterest pointOfInterest)
        {
            Card card = new Card
            {
                Content = new PointOfInterestView(pointOfInterest),
                Width = 200
            };
            return card;
        }

        public Card GetTourCard(TourViewModel tourView)
        {
            Card card = new Card
            {
                Content = new TourCardView(tourView),
                Width = 200
            };
            return card;
        }
    }
}
