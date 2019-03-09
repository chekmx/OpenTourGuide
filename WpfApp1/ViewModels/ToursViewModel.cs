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

namespace OpenTourClient.ViewModels
{
    public class ToursViewModel : INotifyPropertyChanged
    {
        private ITourRepository<Tour> tourRepository;
        private bool canEdit;

        public GeoPosition<GeoCoordinate> CurrentPosition { get; private set; }

        public bool CanEdit
        {
            get {return this.canEdit; }
            set { this.canEdit = value; OnPropertyChanged("CanEdit"); }
        }

        public ObservableCollection<TourViewModel> Tours { get; set; }
        private TourViewModel selectedTourViewModel;

        public TourViewModel SelectedTourViewModel
        {
            get { return this.selectedTourViewModel; }
            set { this.selectedTourViewModel = value; OnPropertyChanged("SelectedTourViewModel"); }
        }

        public ToursViewModel(ITourRepository<Tour> tourRepository)
        {
            this.tourRepository = tourRepository;
            this.Tours = new ObservableCollection<TourViewModel>(tourRepository.LoadAll().Select(t => new TourViewModel(tourRepository, t)));
            this.SelectedTourViewModel = this.Tours.First();
            var watcher = new GeoCoordinateWatcher(GeoPositionAccuracy.High);
            watcher.PositionChanged += new EventHandler<GeoPositionChangedEventArgs<GeoCoordinate>>(watcher_PositionChanged);
            watcher.TryStart(false, TimeSpan.FromMilliseconds(15000));
        }

        private void watcher_PositionChanged(object sender, GeoPositionChangedEventArgs<GeoCoordinate> e)
        {
            this.CurrentPosition = e.Position;
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
                return save ?? (save = new CommandHandler(param => this.SaveSaveSelectedTour(param), true));
            }
        }


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

        private object SaveSaveSelectedTour(object param)
        {
            this.SelectedTourViewModel.Save();
            this.CanEdit = false;
            OnPropertyChanged("Tours");
            return this.SelectedTourViewModel;
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

        public void PopulateMap(Map map)
        {
            if (map != null)
            {
                map.Children.Clear();
                map.SetView(this.SelectedTourViewModel.Center.ToLocation(), this.SelectedTourViewModel.IntZoomLevel);
                map.Children.Add(this.SelectedTourViewModel.PushpinLocation);

                MapPolyline polyline = new MapPolyline();
                polyline.Stroke = new SolidColorBrush(Colors.Blue);
                polyline.StrokeThickness = 5;
                polyline.Opacity = 0.7;
                polyline.Locations = this.SelectedTourViewModel.Tour.Route?.ToLocationCollection();

                map.Children.Add(polyline);
                if (polyline.Locations != null)
                {
                    map.SetView(polyline.Locations, new Thickness(5), 0);
                }

                foreach (IPointOfInterest pointOfInterest in this.SelectedTourViewModel.Tour.PointsOfInterest)
                {
                    Pushpin pushpin = new Pushpin();
                    pushpin.MouseLeftButtonDown += PointOfInterestClicked;
                    pushpin.Location = pointOfInterest.Location.ToLocation();
                    map.Children.Add(pushpin);
                }
            }
        }

        private void PointOfInterestClicked(object sender, MouseButtonEventArgs e)
        {
            var pushPin = sender as Pushpin;
            if (pushPin != null)
            {
                var map = pushPin.Parent as MapLayer;
                TextBlock tb = new TextBlock();
                tb.Foreground = new SolidColorBrush(
                    Color.FromArgb(255, 128, 255, 128));
                tb.Margin = new Thickness(5);
                tb.Text = pushPin.Location.ToString();
                map.AddChild(tb, pushPin.Location);
            }
        }
    }
}
