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
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            PopulateMap();
        }
    }
}
