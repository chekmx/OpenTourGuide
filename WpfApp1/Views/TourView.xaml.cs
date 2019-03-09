using OpenTourClient.ViewModels;
using OpenTourModel;
using OpenTourUtils;
using System.Windows.Controls;
using Unity;

namespace OpenTourClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class TourView : UserControl
    {

        [Dependency]
        public ToursViewModel ViewModel
        {
            private get
            {
                return this.DataContext as ToursViewModel;
            }
            set
            {
                this.DataContext = value;
                this.ViewModel.PopulateMap(Map);
            }
        }

        public TourView()
        {
            InitializeComponent();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.ViewModel.PopulateMap(Map);
        }

        private void Map_MouseRightButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (this.ViewModel.CanEdit)
            {
                var location = Map.ViewportPointToLocation(e.GetPosition(Map));
                this.ViewModel.SelectedTourViewModel.Tour.PointsOfInterest.Add(new PointOfInterest(location.ToILocation<Location>()));
                this.ViewModel.PopulateMap(Map);
            }
        }
    }
}
