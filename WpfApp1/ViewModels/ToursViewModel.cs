using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;

namespace OpenTourClient.ViewModels
{
    public class ToursViewModel : INotifyPropertyChanged
    {
        private ITourRepository tourRepository;

        public ToursViewModel(ITourRepository tourRepository)
        {
            this.tourRepository = tourRepository;
            this.Tours = new ObservableCollection<TourViewModel>(tourRepository.LoadAll().Select(t => new TourViewModel(tourRepository, t)));
            this.SelectedTourViewModel = this.Tours.First();
        }

        public ObservableCollection<TourViewModel> Tours { get; set; }
        public TourViewModel SelectedTourViewModel { get; set; }

        private ICommand createNew;

        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand CreateNew
        {
            get
            {
                return createNew ?? (createNew = new CommandHandler(param => this.ExecuteNewCommand(param), true));
            }
        }

        public ICommand Save
        {
            get
            {
                return createNew ?? (createNew = new CommandHandler(param => this.SaveSaveSelectedTour(param), true));
            }
        }

        private object ExecuteNewCommand(object param)
        {
            var tour = new Models.Tour();
            this.SelectedTourViewModel = new TourViewModel(tourRepository, tour);
            this.Tours.Add(this.SelectedTourViewModel);
            OnPropertyChanged("SelectedTourViewModel");
            return tour;
        }

        private object SaveSaveSelectedTour(object param)
        {
            this.SelectedTourViewModel.Save();
            OnPropertyChanged("SelectedTourViewModel");
            return this.SelectedTourViewModel;
        }

        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
