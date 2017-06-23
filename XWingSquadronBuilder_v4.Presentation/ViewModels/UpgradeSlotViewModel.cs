using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template10.Mvvm;
using Windows.UI.Xaml.Controls;
using XWingSquadronBuilder_v4.Interfaces;
using XWingSquadronBuilder_v4.Presentation.Converters;

namespace XWingSquadronBuilder_v4.Presentation.ViewModels
{
    public class UpgradeSlotViewModel : BindableBase
    {
        private IUpgradeSlot upgradeSlot;

        public UpgradeSlotViewModel(IUpgradeSlot upgradeSlot)
        {
            UpgradeSlot = upgradeSlot;
        }

        public IUpgradeSlot UpgradeSlot
        {
            get { return this.upgradeSlot; }
            set { Set(ref upgradeSlot, value); }
        }

        public IEnumerable<TextBlock> AugmentText(string text, double fontsize)
        {
            return XWingTextAugmenter.AugementWithXWingIcons(text, fontsize);
        }
    }
}
