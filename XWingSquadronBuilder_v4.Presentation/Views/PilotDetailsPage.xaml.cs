using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using XWingSquadronBuilder_v4.Interfaces;
using XWingSquadronBuilder_v4.Presentation.ViewModels.Pages;
using XWingSquadronBuilder_v4.Presentation.ViewModels.Pages.Interfaces;
using XWingSquadronBuilder_v4.Presentation.ViewModels.XWingModels.Interfaces;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace XWingSquadronBuilder_v4.Presentation.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PilotDetailsPage : Page
    {
        public IPilotDetailsViewModel ViewModel
        {
            get { return (IPilotDetailsViewModel)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ViewModel.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ViewModelProperty =
            DependencyProperty.Register(nameof(ViewModel), typeof(IPilotDetailsViewModel), typeof(PilotDetailsPage), new PropertyMetadata(0));



        public PilotDetailsPage()
        {
            ViewModel = new PilotDetailsViewModel();
            DataContext = ViewModel;
            this.InitializeComponent();

        }


        private void UpgradeSelector_UpgradeSelected(IUpgrade e)
        {
            var upgrade = e;
            var upgradeViewModels = ViewModel.PilotViewModel.Upgrades
                .Where(upgradeViewModel =>
                upgradeViewModel.UpgradeSlot.UpgradeType.Equals(upgrade.UpgradeType)
                && upgradeViewModel.UpgradeSlot.IsMutable
                && upgradeViewModel.UpgradeSlot.IsNullUpgrade);

            foreach (var upgradeViewModel in upgradeViewModels)
            {
                if (upgradeViewModel.UpgradeSlot.SetUpgrade(upgrade))
                {
                    break;
                }
            }
        }

        private void UpgradeSlot_Tapped(object sender, TappedRoutedEventArgs e)
        {
            e.Handled = true;
            var datacontext = ((FrameworkElement)sender).DataContext as IUpgradeSlotViewModel;
            UpgradeSelectorMain.SetSelectedUpgradeType(datacontext.UpgradeSlot.UpgradeType);
        }
    }
}
