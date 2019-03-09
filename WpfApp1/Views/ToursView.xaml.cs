using System.Windows.Controls;
using OpenTourClient.ViewModels;
using OpenTourUtils;
using Unity;

namespace OpenTourClient.Views
{
    /// <summary>
    /// Interaction logic for ToursView.xaml
    /// </summary>
    public partial class ToursView : UserControl
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

        public ToursView()
        {
            InitializeComponent();
            
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            PopulateMap();  
        }

        private void PopulateMap()
        {
            if (Map != null)
            {
                Map.Children.Clear();
                Map.SetView
                    (this.ViewModel.SelectedTourViewModel.Center.ToLocation(), 
                     this.ViewModel.SelectedTourViewModel.IntZoomLevel);
                Map.Children.Add(this.ViewModel.SelectedTourViewModel.PushpinLocation);
            }
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var test = this.ViewModel.Tours.Count;
        }
    }
}
