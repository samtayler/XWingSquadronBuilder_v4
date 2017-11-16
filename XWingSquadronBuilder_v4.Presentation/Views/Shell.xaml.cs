using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Template10.Common;
using Template10.Controls;
using Template10.Services.NavigationService;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
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
    public sealed partial class Shell : Page
    {
        public IShellViewModel ViewModel
        {
            get { return (IShellViewModel)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ViewModel.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ViewModelProperty =
            DependencyProperty.Register("ViewModel", typeof(IShellViewModel), typeof(Shell), new PropertyMetadata(0));



        private INavigationService navService { get; }

        public static Shell Instance { get; set; }

        //Services.SettingsServices.SettingsService _settings;

        public Shell()
        {
            ViewModel = new ShellViewModel();
            Instance = this;
            InitializeComponent();
            this.navService = App.Current.NavigationServiceFactory(BackButton.Attach, ExistingContent.Exclude, frame: this.ContentFrame);
            //_settings = Services.SettingsServices.SettingsService.Instance;
        }

        private async void HamburgerButtonInfo_Tapped(object sender, RoutedEventArgs e)
        {

            if (ViewModel.CheckCurrentSession()) return;

            MessageDialog ms = new MessageDialog($"There is an unsaved squadron open. If you continue any unsaved changes will be lost. {Environment.NewLine} Do you want to continue?", "Unsaved squadron");
            ms.Commands.Add(new UICommand("Yes", (para) => { }));
            ms.Commands.Add(new UICommand("No", (para) => { }));

            var resultantCommand = await ms.ShowAsync();

            if (resultantCommand.Label == "Yes")
            {
                return;
            }

        }

        private void NavView_Loaded(object sender, RoutedEventArgs e)
        {

            // set the initial SelectedItem 
            foreach (NavigationViewItemBase item in NavView.MenuItems)
            {
                if (item is NavigationViewItem && item.Tag.ToString() == "Home")
                {
                    NavView.SelectedItem = item;
                    break;
                }
            }
        }

        private async void NavView_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            if (args.IsSettingsInvoked)
            {
                //ContentFrame.Navigate(typeof(SettingsPage));
            }
            else
            {
                switch (args.InvokedItem)
                {
                    case "Home":
                        {
                            await navService.NavigateAsync(typeof(FactionSelectionPage));                            
                            break;
                        }

                }
            }
        }

        private async void NavView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            if (args.IsSettingsSelected)
            {
                //ContentFrame.Navigate(typeof(SettingsPage));
            }
            else
            {
                NavigationViewItem item = args.SelectedItem as NavigationViewItem;

                switch (item.Tag)
                {
                    case "Home":
                        await navService.NavigateAsync(typeof(FactionSelectionPage));
                        break;
                }
            }
        }
    }

}