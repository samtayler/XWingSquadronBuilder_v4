using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Template10.Common;
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
using XWingSquadronBuilder_v4.Presentation.ViewModels;
using static Template10.Common.BootStrapper;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace XWingSquadronBuilder_v4.Presentation.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Shell : Page
    {
        private INavigationService navService { get; }

        public Shell()
        {
            this.InitializeComponent();
            // Navigate to the first page (optionally)                
            navService = Current.NavigationServiceFactory(BackButton.Attach, ExistingContent.Exclude,SplitViewFrame);            
            var type = (DataContext as ShellViewModel).Menu.First().NavigationDestination;           
            
            navService.NavigateAsync(typeof(Views.FactionSelectionPage));
        }

        private void btnMenu_Click(object sender, RoutedEventArgs e)
        {
            svShell.IsPaneOpen = !svShell.IsPaneOpen;
        }        

        private void SplitViewOpener_ManipulationCompleted(object sender, ManipulationCompletedRoutedEventArgs e)
        {
            if (e.Cumulative.Translation.X > 50)
            {
                svShell.IsPaneOpen = true;
            }
        }

        private void SplitViewPane_ManipulationCompleted(object sender, ManipulationCompletedRoutedEventArgs e)
        {
            if (e.Cumulative.Translation.X < -50)
            {
                svShell.IsPaneOpen = false;
            }
            if (e.Cumulative.Translation.X > 50)
            {
                svShell.IsPaneOpen = true;
            }
        }        

        private async void ListViewItem_Tapped(object sender, TappedRoutedEventArgs e)
        {
            e.Handled = true;

            var menuItem = ((FrameworkElement)sender).DataContext as MenuItem;
            if (menuItem.IsNavigation)
            {
                await navService.NavigateAsync(menuItem.NavigationDestination);
            }
            else
            {
                menuItem.Command.Execute(null);
            }

            svShell.IsPaneOpen = false;
        }
    }
}
