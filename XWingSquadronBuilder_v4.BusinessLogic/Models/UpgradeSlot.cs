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
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Upgrade)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Cost)));
            }
        }

        private IUpgrade upgrade;


        public int CostReduction { get; }

        public int CostRestriction { get; }

        public int Cost
        {
            get
            {
                if (CostReduction > 0)
                {
                    return (Upgrade.Cost - CostReduction) < 0 ? 0 : (Upgrade.Cost - CostReduction);
                }
                else
                {
                    return Upgrade.Cost;
                }
            }
        }

        private bool enabled = true;

        public bool Enabled
        {
            get { return enabled; }
            private set
            {
                if (value != enabled)
                {
                    enabled = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Enabled)));
                }
            }
        }

        public void Enable() => Enabled = true;

        public void Disable() => Enabled = false;

        public event PropertyChangedEventHandler PropertyChanged;

        public UpgradeSlot(IUpgradeType upgradeType, IUpgrade upgrade, int costReduction = 0, int costRestriction = 100)
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
                CostRestriction == other.CostRestriction
                && Upgrade.Equals(other.Upgrade);
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
            return new UpgradeSlot(this.UpgradeType.DeepClone(), this.Upgrade.DeepClone(), this.CostReduction, this.CostRestriction);
        }

        public int CompareTo(IUpgradeSlot other)
        {
            return this.UpgradeType.CompareTo(other.UpgradeType);
        }
    }
}
