using Microsoft.Maps.MapControl.WPF;
using OpenTourClient.ViewModels;
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
                PopulateMap();
            }
        }

        public TourView()
        {
            InitializeComponent();

        }

        private void PopulateMap()
        {
            if (Map != null)
            {
                Map.Children.Clear();
                Map.SetView(this.ViewModel.SelectedTourViewModel.Center, this.ViewModel.SelectedTourViewModel.IntZoomLevel);
                Map.Children.Add(this.ViewModel.SelectedTourViewModel.PushpinLocation);
            }

            MapPolyline polyline = new MapPolyline();
            polyline.Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Blue);
            polyline.StrokeThickness = 5;
            polyline.Opacity = 0.7;
            polyline.Locations = this.ViewModel.SelectedTourViewModel.Tour.Route;

            Map.Children.Add(polyline);
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            PopulateMap();
        }
    }
}
