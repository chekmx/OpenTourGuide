using System.Windows.Controls;
using Unity.Attributes;
using OpenTourClient.ViewModels;

namespace OpenTourClient.Views
{
    /// <summary>
    /// Interaction logic for Window1.xaml
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
                Map.SetView(this.ViewModel.SelectedTourViewModel.Center, this.ViewModel.SelectedTourViewModel.IntZoomLevel);
                Map.Children.Add(this.ViewModel.SelectedTourViewModel.PushpinLocation);
            }
        }
    }
}
