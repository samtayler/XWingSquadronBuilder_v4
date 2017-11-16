using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template10.Mvvm;
using XWingSquadronBuilder_v4.Interfaces;
using XWingSquadronBuilder_v4.Presentation.ViewModels.XWingModels.Interfaces;

namespace XWingSquadronBuilder_v4.Presentation.ViewModels.XWingModels
{
    public class PilotViewModel : BindableBase, IPilotViewModel
    {
        public PilotViewModel(IPilot pilot)
        {
            Pilot = pilot;
            Pilot.PropertyChanged += (sender, e) =>
            {
                RaisePropertyChanged(nameof(Upgrades));
            };
        }

        private IPilot pilot;

        public IPilot Pilot
        {
            get { return this.pilot; }
            private set { Set(ref pilot, value); }
        }

        public IReadOnlyList<IUpgradeSlotViewModel> Upgrades =>
            Pilot.Upgrades
            .Select(x => new UpgradeSlotViewModel(x))
            .OrderBy(x => x.UpgradeSlot.IsNullUpgrade)
            .ToList()
            .AsReadOnly();

        public bool Equals(IPilotViewModel other)
        {
            return this.Pilot.Equals(other.Pilot);
        }

        public override int GetHashCode()
        {
            return Pilot.GetHashCode();
        }
    }
}
