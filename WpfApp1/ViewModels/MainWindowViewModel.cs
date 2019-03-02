using OpenTourClient.Views;
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
            container.RegisterType<ITourRepository, TourRepository>();

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
