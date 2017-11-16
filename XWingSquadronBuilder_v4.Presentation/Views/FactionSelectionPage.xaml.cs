using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Template10.Services.NavigationService;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using XWingSquadronBuilder_v4.BusinessLogic.Repositories;
using XWingSquadronBuilder_v4.Interfaces;
using XWingSquadronBuilder_v4.Presentation.ViewModels.Pages;
using XWingSquadronBuilder_v4.Presentation.ViewModels.Pages.Interfaces;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace XWingSquadronBuilder_v4.Presentation.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class FactionSelectionPage : Page
    {
        public IFactionSelectionViewModel ViewModel
        {
            get { return (IFactionSelectionViewModel)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ViewModelProperty =
            DependencyProperty.Register(nameof(ViewModel), typeof(IFactionSelectionViewModel), typeof(FactionSelectionPage), new PropertyMetadata(0));

        public FactionSelectionPage()
        {
            ViewModel = new FactionSelectionViewModel();
            DataContext = ViewModel;
            this.InitializeComponent();
        }

        //private async void gwFactionSelect_ItemClick(object sender, ItemClickEventArgs e)
        //{
        //    await ViewModel.FactionSelectedAsync(e.ClickedItem as IFaction);
        //} 
        

        private async void btnNewSquadron_Tapped(object sender, TappedRoutedEventArgs e)
        {
            await ViewModel.FactionSelectedAsync(((FrameworkElement)sender).Tag as string);
        }

        private async void lvSquadrons_ItemClick(object sender, ItemClickEventArgs e)
        {
            await ViewModel.OpenSquadronAsync(e.ClickedItem as ISquadron);
        }
    }
}
