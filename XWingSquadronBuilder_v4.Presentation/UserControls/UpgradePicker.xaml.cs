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
using XWingSquadronBuilder_v4.BusinessLogic.Extensions;
using XWingSquadronBuilder_v4.BusinessLogic.Models;
using XWingSquadronBuilder_v4.BusinessLogic.Repositories;
using XWingSquadronBuilder_v4.Interfaces;

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace XWingSquadronBuilder_v4.Presentation.UserControls
{
    public sealed partial class UpgradePicker : ContentDialog
    {
        public IUpgradeSlot upgradeSlot { get; }
        private IPilot pilot { get; }


        public IEnumerable<IUpgrade> Upgrades
        {
            get { return (IEnumerable<IUpgrade>)GetValue(UpgradesProperty); }
            set { SetValue(UpgradesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Upgrades.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UpgradesProperty =
            DependencyProperty.Register("Upgrades", typeof(IEnumerable<IUpgrade>), typeof(UpgradePicker), new PropertyMetadata(0));




        public UpgradePicker(IPilot pilot, IUpgradeSlot upgradeSlot)
        {           
            this.InitializeComponent();
            this.pilot = pilot;
            this.upgradeSlot = upgradeSlot;
            var posibleUpgrades = XWingRepository.Instance.UpgradeRepository.GetAllUpgradesForType(upgradeSlot.UpgradeType);
            Upgrades = FilterUpgrades(posibleUpgrades);
        }

        private IEnumerable<IUpgrade> FilterUpgrades(IEnumerable<IUpgrade> posUpgrades)
        {
            return (from upgrade in posUpgrades
                           where (upgrade.Faction.Equals(pilot.Faction) || upgrade.Faction.Name == "Any") &&
                           (upgrade.Cost <= upgradeSlot.CostRestriction) &&
                           (pilot.Ship.Contains(upgrade.ShipLimited)) &&
                           (pilot.Actions.Any(x => x.Name.Contains(upgrade.ActionLimited))) &&
                           (upgrade.Limited ? !pilot.Upgrades.Any(x => x.Upgrade.Equals(upgrade)) : true) &&
                           (pilot.Upgrades.Where(x => x.UpgradeType.Equals(upgradeSlot.UpgradeType) &&  ((x.Upgrade is NullUpgrade) || x == upgradeSlot)).Count() >= upgrade.SlotsRequired) &&
                           (pilot.Upgrades.AreRemovalConditionsMet(upgrade)) &&
                           (upgrade.SizeRestriction == string.Empty || upgrade.SizeRestriction.ToUpper() == pilot.ShipSize.ToString().ToUpper())
                           orderby upgrade.Cost, upgrade.Name
                           select upgrade).ToList();

        }

        private void ListViewItem_Tapped(object sender, TappedRoutedEventArgs e)
        {
            upgradeSlot.Upgrade = ((FrameworkElement)e.OriginalSource).DataContext as IUpgrade;
            Hide();
        }

        private void btnClose_Tapped(object sender, TappedRoutedEventArgs e)
        {
            e.Handled = true;
            Hide();
        }
    }
}
