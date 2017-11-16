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
    public class UpgradeSlotViewModel : BindableBase, IUpgradeSlotViewModel
    {
        private IUpgradeSlot upgradeSlot;

        public UpgradeSlotViewModel(IUpgradeSlot upgradeSlot)
        {
            UpgradeSlot = upgradeSlot;
        }

        public IUpgradeSlot UpgradeSlot
        {
            get { return this.upgradeSlot; }
            private set { Set(ref upgradeSlot, value); }
        }

        public bool Equals(IUpgradeSlotViewModel other)
        {
            return this.UpgradeSlot.Equals(other.UpgradeSlot);
        }
    }
}
