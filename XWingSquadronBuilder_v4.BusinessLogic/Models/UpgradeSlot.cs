using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XWingSquadronBuilder_v4.Interfaces;

namespace XWingSquadronBuilder_v4.BusinessLogic.Models
{
    public class UpgradeSlot : IUpgradeSlot
    {
        public IUpgradeType UpgradeType { get; }

        public IUpgrade Upgrade
        {
            get
            {
                return this.upgrade;
            }
            set
            {
                if (this.upgrade != null) this.upgrade.PropertyChanged -= Upgrade_PropertyChanged;
                this.upgrade = value ?? throw new ArgumentNullException(nameof(Upgrade));
                this.upgrade.PropertyChanged += Upgrade_PropertyChanged;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Upgrade"));
            }
        }

        private IUpgrade upgrade;

        public int CostReduction { get; }

        public int CostRestriction { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        public UpgradeSlot(IUpgradeType upgradeType, IUpgrade upgrade, int costReduction = 0, int costRestriction = 0)
        {
            UpgradeType = upgradeType ?? throw new ArgumentNullException(nameof(upgradeType));
            this.upgrade = upgrade ?? throw new ArgumentNullException(nameof(upgrade));
            CostReduction = costReduction;
            CostRestriction = costRestriction;
        }

        public IEnumerable<IUpgradeSlot> GetInnerUpgradeSlots()
        {
            return Upgrade.GetInnerUpgradeSlots();
        }

        private void Upgrade_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(sender, e);
        }

        public void Dispose()
        {
            if (this.Upgrade != null) this.Upgrade.PropertyChanged -= Upgrade_PropertyChanged;
        }        

        public bool Equals(IUpgradeSlot other)
        {
            return this.UpgradeType.Equals(other.UpgradeType) &&
                CostReduction == other.CostReduction &&
                CostRestriction == other.CostRestriction;
        }

        public override string ToString()
        {
            return $"{UpgradeType}";
        }

        public void ClearUpgrade()
        {
            Upgrade = new NullUpgrade(UpgradeType);
        }

        public IUpgradeSlot DeepClone()
        {
            throw new NotImplementedException();
        }
    }
}
