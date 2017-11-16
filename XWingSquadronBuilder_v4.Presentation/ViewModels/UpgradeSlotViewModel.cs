using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Template10.Mvvm;
using Windows.UI.Xaml.Controls;
using XWingSquadronBuilder_v4.Interfaces;
using XWingSquadronBuilder_v4.Presentation.Converters;

namespace XWingSquadronBuilder_v4.Presentation.ViewModels
{
    [DataContract]
    public class UpgradeSlotViewModel : BindableBase
    {        
        public UpgradeSlotViewModel(IUpgradeSlot upgradeSlot)
        {
            UpgradeSlot = upgradeSlot;
            IsEnabled = true;
        }
        [DataMember]
        private IUpgradeSlot upgradeSlot;

        public IUpgradeSlot UpgradeSlot
        {
            get { return this.upgradeSlot; }
            set { Set(ref upgradeSlot, value); }
        }
        [DataMember]
        private bool isEnabled;

        public bool IsEnabled
        {
            get { return this.isEnabled; }
            set { Set(ref isEnabled, value); }
        }


        public IEnumerable<TextBlock> AugmentText(string text, double fontsize)
        {
            return XWingTextAugmenter.AugementWithXWingIcons(text, fontsize);
        }
    }
}
