using Template10.Services.NavigationService;
using Windows.UI.Xaml;
using System;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using XWingSquadronBuilder_v4.Interfaces;
using XWingSquadronBuilder_v4.Presentation.UserControls;
using XWingSquadronBuilder_v4.Presentation.ViewModels;
using XWingSquadronBuilder_v4.Presentation.ViewModels.Pages;
using XWingSquadronBuilder_v4.Presentation.ViewModels.Pages.Interfaces;
using static Template10.Common.BootStrapper;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace XWingSquadronBuilder_v4.Presentation.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SquadronBuilder : Page
    {
        public ISquadronBuilderViewModel ViewModel
        {
            get { return (ISquadronBuilderViewModel)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ViewModel.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ViewModelProperty =
            DependencyProperty.Register(nameof(ViewModel), typeof(ISquadronBuilderViewModel), typeof(SquadronBuilder), new PropertyMetadata(0));

        public SquadronBuilder()
        {
            ViewModel = new SquadronBuilderViewModel();
            DataContext = ViewModel;
            this.InitializeComponent();             
        }

        private async void btnSave_Click(object sender, RoutedEventArgs e)
        {
            SaveSquadronDialog dialog = new SaveSquadronDialog();
            await dialog.ShowAsync();
        }
    }
}
