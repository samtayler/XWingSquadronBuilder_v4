using Test_app_1.ViewModels;

using Windows.UI.Xaml.Controls;

namespace Test_app_1.Views
{
    public sealed partial class MainPage : Page
    {
        public MainViewModel ViewModel { get; } = new MainViewModel();
        public MainPage()
        {
            InitializeComponent();
        }
    }
}
