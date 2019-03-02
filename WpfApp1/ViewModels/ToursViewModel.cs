using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using System.Device.Location;
using System;
using OpenTourUtils;

namespace OpenTourClient.ViewModels
{
    public class ToursViewModel : INotifyPropertyChanged
    {
        private ITourRepository tourRepository;
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

        public ToursViewModel(ITourRepository tourRepository)
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
            var tour = new Models.Tour();
            this.SelectedTourViewModel = new TourViewModel(tourRepository, tour);
            tour.Center = this.CurrentPosition.Location.ToLocation();
            tour.ZoomLevel = 16;
            this.Tours.Add(this.SelectedTourViewModel);
            return tour;
        }

        private object SaveSaveSelectedTour(object param)
        {
            this.SelectedTourViewModel.Save();
            this.CanEdit = false;
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
    }
}
