using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using XWingSquadronBuilder_v4.Interfaces;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Input;
using Template10.Mvvm;
using System.Collections.ObjectModel;
using Windows.UI.Xaml.Controls;
using XWingSquadronBuilder_v4.Presentation.Converters;

namespace XWingSquadronBuilder_v4.Presentation.ViewModels
{
    public class PilotViewModel : BindableBase, IEquatable<PilotViewModel>, IDisposable
    {
        private IPilot pilot;

        public PilotViewModel(IPilot pilot)
        {
            this.pilot = pilot;
            pilot.PropertyChanged += Pilot_PropertyChanged;
            PilotCost = pilot.Cost + pilot.UpgradesCost;
            Upgrades = new ObservableCollection<UpgradeSlotViewModel>(pilot.Upgrades.Select(upgrade => new UpgradeSlotViewModel(upgrade)).OrderBy(upgrade => upgrade.UpgradeSlot.UpgradeType.Name)
                .ThenBy(upgrade => upgrade.UpgradeSlot.IsNotNullUpgrade));

        }

        private void Pilot_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            UpgradesChanged();
            PilotCost = pilot.Cost + pilot.UpgradesCost;
        }

        public IPilot Pilot
        {
            get { return this.pilot; }
            set { Set(ref pilot, value); }
        }


        private ObservableCollection<UpgradeSlotViewModel> upgrades;

        public ObservableCollection<UpgradeSlotViewModel> Upgrades
        {
            get { return this.upgrades; }
            set { Set(ref upgrades, value); }
        }

        private int pilotCost;

        public int PilotCost
        {
            get { return this.pilotCost; }
            set { Set(ref pilotCost, value); }
        }

        private Visibility collapsed = Visibility.Collapsed;

        public Visibility Collapsed
        {
            get { return this.collapsed; }
            set { Set(ref collapsed, value); }
        }

        public void ToggleCollapsed()
        {
            if (Collapsed == Visibility.Collapsed) Collapsed = Visibility.Visible;
            else if (Collapsed == Visibility.Visible) Collapsed = Visibility.Collapsed;
        }

        private void UpgradesChanged()
        {
            List<IUpgradeSlot> upgradesToAdd = new List<IUpgradeSlot>();
            List<UpgradeSlotViewModel> upgradetoRemove = new List<UpgradeSlotViewModel>();
            foreach (var upgrade in Pilot.Upgrades)
            {
                if (!Upgrades.Any(x => x.UpgradeSlot == upgrade))
                {
                    upgradesToAdd.Add(upgrade);
                }
            }
            foreach (var upgrade in Upgrades)
            {
                if (!Pilot.Upgrades.Any(x => x == upgrade.UpgradeSlot))
                {
                    upgradetoRemove.Add(upgrade);
                }
            }

            foreach (var upgrade in upgradetoRemove)
            {
                Upgrades.Remove(upgrade);
            }

            foreach (var upgrade in upgradesToAdd)
            {
                Upgrades.Add(new UpgradeSlotViewModel(upgrade));
            }

            Upgrades = new ObservableCollection<UpgradeSlotViewModel>(Upgrades.OrderBy(upgrade => upgrade.UpgradeSlot.UpgradeType.Name)
                .ThenBy(upgrade => upgrade.UpgradeSlot.IsNotNullUpgrade));
        }
        
        public IEnumerable<TextBlock> AugementText(string text, double fontsize)
        {
            return XWingTextAugmenter.AugementWithXWingIcons(text, fontsize, Pilot.Unique ? Windows.UI.Text.FontStyle.Normal : Windows.UI.Text.FontStyle.Italic);
        }

        public bool Equals(PilotViewModel other)
        {
            return Pilot.Equals(other.Pilot);
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    pilot.PropertyChanged -= Pilot_PropertyChanged;
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~PilotViewModel() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
