using OpenTourClient.Views;

namespace OpenTourClient.ViewModels
{
    public class NavigationItem
    {
        public NavigationItem(string packIconKind, object content)
        {
            this.PackIconKind = packIconKind;
            this.Content = content;
        }

        public string PackIconKind { get; private set; }
        public object Content { get; private set; }
    }
}