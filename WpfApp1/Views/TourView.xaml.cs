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
            }
        }

        public TourView()
        {
            InitializeComponent();
        }
    }
}
