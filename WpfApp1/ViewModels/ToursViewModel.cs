using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using System.Device.Location;
using System;
using System.Xml.Linq;
using OpenTourModel;
using Microsoft.Maps.MapControl.WPF;
using OpenTourInterfaces;
using OpenTourUtils;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;
using OpenTourClient.Views;
using MaterialDesignThemes.Wpf;

namespace OpenTourClient.ViewModels
{
    public class ToursViewModel : INotifyPropertyChanged
    {
        private ITourRepository<Tour> tourRepository;
        private bool canEdit;

        public OpenTourModel.Location CurrentPosition { get; private set; }
        
        public bool CanEdit
        {
            get {return this.canEdit; }
            set { this.canEdit = value; OnPropertyChanged("CanEdit"); }
        }

        public ObservableCollection<TourViewModel> Tours { get; set; }
        private TourViewModel selectedTourViewModel;

        public void ShowTours(Map map)
        {
            if (map != null && this.SelectedTourViewModel != null)
            {
                foreach (TourViewModel tourView in this.Tours)
                {
                    Pushpin pushpin = new Pushpin();
                    pushpin.ToolTip = GetPointOfInterest();
                    pushpin.Location = tourView.Center.ToLocation();
                    MapLayer.SetPosition(pushpin, pushpin.Location);
                    map.Children.Add(pushpin);
                }
            }
        }

        public void ShowRouteMap(Map map)
        {
            if (map != null && this.SelectedTourViewModel != null)
            {
                MapPolyline polyline = new MapPolyline();
                polyline.Stroke = new SolidColorBrush(Colors.Blue);
                polyline.StrokeThickness = 5;
                polyline.Opacity = 0.7;
                polyline.Locations = this.SelectedTourViewModel.Tour.Route?.ToLocationCollection();

                map.Children.Add(polyline);
                if (polyline.Locations != null)
                {
                    try
                    {
                        map.SetView(polyline.Locations, new Thickness(5), 0);
                    }
                    catch (Exception ex)
                    {
                        //TODO Add nlogging
                    }
                }
            }
        }

        public void ShowPointsOfInterest(Map map)
        {
            if (map != null && this.SelectedTourViewModel != null)
            {
                foreach (IPointOfInterest pointOfInterest in this.SelectedTourViewModel.Tour.PointsOfInterest)
                {
                    Pushpin pushpin = new Pushpin();
                    pushpin.ToolTip = GetPointOfInterest();
                    pushpin.Location = pointOfInterest.Location.ToLocation();
                    MapLayer.SetPosition(pushpin, pushpin.Location);
                    map.Children.Add(pushpin);
                }
            }
        }

        public TourViewModel SelectedTourViewModel
        {
            get { return this.selectedTourViewModel; }
            set { this.selectedTourViewModel = value; OnPropertyChanged("SelectedTourViewModel"); }
        }

        public ToursViewModel(ITourRepository<Tour> tourRepository)
        {
            this.tourRepository = tourRepository;
            this.Tours = new ObservableCollection<TourViewModel>(tourRepository.LoadAll().Select(t => new TourViewModel(tourRepository, t)));
            this.SelectedTourViewModel = this.Tours.FirstOrDefault();
            var watcher = new GeoCoordinateWatcher(GeoPositionAccuracy.High);
            watcher.PositionChanged += new EventHandler<GeoPositionChangedEventArgs<GeoCoordinate>>(watcher_PositionChanged);
            watcher.TryStart(false, TimeSpan.FromMilliseconds(15000));
            this.CurrentPosition =  new OpenTourModel.Location(this.SelectedTourViewModel.Center);
        }

        private void watcher_PositionChanged(object sender, GeoPositionChangedEventArgs<GeoCoordinate> e)
        {
            this.CurrentPosition = new OpenTourModel.Location(e.Position.Location.Latitude, e.Position.Location.Longitude);
            OnPropertyChanged("CurrentPosition");
            this.ShowCurrentLocation();
        }

        private void ShowCurrentLocation()
        {
            var pushPin = new Pushpin();
            pushPin.Location = this.CurrentPosition.ToLocation();
            this.Map.Children.Add(pushPin);
            this.Map.SetView(this.CurrentPosition.ToLocation(), this.DefaultZoom);
        }

        private ICommand createNew;

        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand CreateNew
        {
            get
            {
                return createNew ?? (createNew = new CommandHandler(param => this.ExecuteNewCommand(param), true));
            }
        }

        private ICommand edit;

        public ICommand Edit
        {
            get
            {
                return edit ?? (edit = new CommandHandler(param => this.Editable(param), true));
            }
        }

        private ICommand save;

        public ICommand Save
        {
            get
            {
                return save ?? (save = new CommandHandler(param => this.SaveSelectedTour(param), true));
            }
        }

        public double DefaultZoom { get; set; }
        public Map Map { get; internal set; }
        public Card CurrentCard { get; private set; }

        private object ExecuteNewCommand(object param)
        {
            Microsoft.Win32.OpenFileDialog openFileDlg = new Microsoft.Win32.OpenFileDialog();
            var result = openFileDlg.ShowDialog();

            if (result.Equals(true))
            {
                XDocument document = XDocument.Load(openFileDlg.FileName);

                var tour = new Tour(document);
                this.SelectedTourViewModel = new TourViewModel(tourRepository, tour);
                //tour.Center = this.CurrentPosition.Location.ToLocation();
                tour.ZoomLevel = 16;
                this.Tours.Add(this.SelectedTourViewModel);
                this.CanEdit = true;
                OnPropertyChanged("Tours");
                return tour;
            }

            return null;
        }

        private object SaveSelectedTour(object param)
        {
            if (this.SelectedTourViewModel != null)
            {
                this.SelectedTourViewModel.Save();
                this.CanEdit = false;
                OnPropertyChanged("Tours");
                return this.SelectedTourViewModel;
            }
            return null;
        }

        private object Editable(object param)
        {
            this.CanEdit = true;
            return true;
        }

        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public void ShowSelectedTourLocation(Map map)
        {
            if (map != null && this.SelectedTourViewModel != null)
            {
                map.Children.Clear();
                map.SetView(this.SelectedTourViewModel.Center.ToLocation(), this.SelectedTourViewModel.IntZoomLevel);
                map.Children.Add(this.SelectedTourViewModel.PushpinLocation);
            }
        }

        private Card GetPointOfInterest()
        {
            Card card = new Card
            {
                Content = new PointOfInterestView(),
                Width = 200
            };
            return card;
        }
    }
}
