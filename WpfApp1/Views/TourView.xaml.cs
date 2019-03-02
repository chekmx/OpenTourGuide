using OpenTourClient.ViewModels;
using System.Windows;
using System.Windows.Controls;
using Unity.Attributes;

namespace OpenTourClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class TourView : UserControl
    {
        [Dependency]
        public TourViewModel ViewModel
        {
            private get
            {
                return this.DataContext as TourViewModel;
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

        public TourView(TourViewModel tourViewModel)
        {
            InitializeComponent();
            this.DataContext = tourViewModel;
        }

        private void PopulateMap()
        {
            if (Map != null)
            {
                Map.Children.Clear();
                Map.SetView(this.ViewModel.Center, this.ViewModel.IntZoomLevel);
                Map.Children.Add(this.ViewModel.PushpinLocation);
            }
        }
    }
}
