using OpenTourClient.Views;
using OpenTourTourRepository;
using OpenTourInterfaces;
using OpenTourModel;
using Unity;

namespace OpenTourClient.ViewModels
{
    public class MainWindowViewModel
    {
        public ToursView ToursView;
        public TourView TourView;

        public MainWindowViewModel()
        {
            IUnityContainer container = new UnityContainer();
            ITourRepository<Tour> repo = new WebTourRepository<Tour>() as ITourRepository<Tour>;
            container.RegisterInstance(typeof(ITourRepository<Tour>), repo);
            container.RegisterInstance(typeof(ToursViewModel), new ToursViewModel(container.Resolve<ITourRepository<Tour>>()));

            this.ToursView = container.Resolve<ToursView>();
            this.TourView = container.Resolve<TourView>();

            this.NavigationItems = new[]
            {
                new NavigationItem("Magnify", this.ToursView),
                new NavigationItem("Walk", this.TourView)
            };
        }

        public NavigationItem[] NavigationItems { get; private set; }
    }
}
