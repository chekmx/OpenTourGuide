using OpenTourClient.ViewModels;
using System.Windows;

namespace OpenTourClient.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel();
        }

        private void DemoItemsListBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            var mainWindow = this.DataContext as MainWindowViewModel;
            var toursView = mainWindow.ToursView.DataContext as ToursViewModel;
            mainWindow.TourView = new TourView(toursView.SelectedTourViewModel);
        }
    }
}
