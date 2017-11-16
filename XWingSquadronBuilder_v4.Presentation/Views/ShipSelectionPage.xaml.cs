using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using XWingSquadronBuilder_v4.Presentation.ViewModels;
using XWingSquadronBuilder_v4.Presentation.ViewModels.Pages;
using XWingSquadronBuilder_v4.Presentation.ViewModels.Pages.Interfaces;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace XWingSquadronBuilder_v4.Presentation.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ShipSelectionPage : Page
    {
        public IShipSelectionViewModel ViewModel
        {
            get { return (IShipSelectionViewModel)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ViewModel.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ViewModelProperty =
            DependencyProperty.Register(nameof(ViewModel), typeof(IShipSelectionViewModel), typeof(ShipSelectionPage), new PropertyMetadata(0));

        public ShipSelectionPage()
        {
            ViewModel = new ShipSelectionViewModel();
            DataContext = ViewModel;
            this.InitializeComponent();             
        }

        private async void gvShipList_ItemClick(object sender, ItemClickEventArgs e)
        {
            await ViewModel.NavigateToPilotSelectionAsync(e.ClickedItem as ShipDisplay);
        }
    }
}
