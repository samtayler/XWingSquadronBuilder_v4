using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using XWingSquadronBuilder_v4.DataLayer.Models;
using XWingSquadronBuilder_v4.Interfaces;

namespace XWingSquadronBuilder_v4.BusinessLogic.ViewModels
{
    public class UpgradeSlotViewModel : INotifyPropertyChanged
    {
        public UpgradeSlotViewModel(IUpgradeType upgradeType, UpgradeViewModel upgrade, int costReduction = 0, int costRestriction = 0)
        {
            UpgradeType = upgradeType;
            Upgrade = upgrade;
            CostReduction = costReduction;
            CostRestriction = costRestriction;
        }

        public IUpgradeType UpgradeType { get; }
        private UpgradeViewModel _Upgrade;

        public UpgradeViewModel Upgrade
        {
            get { return _Upgrade; }
            set
            {
                _Upgrade = value;
                NotifyPropertyChanged();
            }
        }

        public int CostReduction { get; }
        public int CostRestriction { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        public int Cost => Upgrade.Upgrade.Cost + Cost;

        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public IEnumerable<UpgradeSlotViewModel> GetInnerUpgradeSlots()
        {
            return Upgrade.GetInnerUpgradeSlots();
        }

        public void ClearUpgrade()
        {
            Upgrade = new UpgradeViewModel(new NullUpgrade(this.UpgradeType));
        }
    }
}
