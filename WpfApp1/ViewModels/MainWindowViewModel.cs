using OpenTourClient.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace OpenTourClient.ViewModels
{
    public class MainWindowViewModel
    {
        public MainWindowViewModel()
        {
            IUnityContainer container = new UnityContainer();
            container.RegisterType<ITourRepository, TourRepository>();

            ToursView toursView = container.Resolve<ToursView>();
            TourView tourView = container.Resolve<TourView>();

            this.NavigationItems = new[]
            {
                new NavigationItem("Magnify", toursView),
                new NavigationItem("Walk", tourView)
            };
        }

        public NavigationItem[] NavigationItems { get; private set; }
    }
}
