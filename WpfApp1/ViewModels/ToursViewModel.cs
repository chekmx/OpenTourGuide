using System.Collections.ObjectModel;
using System.Linq;

namespace OpenTourClient.ViewModels
{
    public class ToursViewModel
    {
        private ITourRepository tourRepository;

        public ToursViewModel(ITourRepository tourRepository)
        {
            this.tourRepository = tourRepository;
            this.Tours = new ObservableCollection<TourViewModel>(tourRepository.LoadAll());
            this.SelectedTourViewModel = this.Tours.First();
        }

        public ObservableCollection<TourViewModel> Tours { get; set; }
        public TourViewModel SelectedTourViewModel { get; set; }
    }
}
