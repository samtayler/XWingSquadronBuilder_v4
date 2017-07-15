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
            private set
            {
                if (value.Equals(upgrade)) return;
                upgrade = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Upgrade)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Cost)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsNotNullUpgrade)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsNullUpgrade)));
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

        public bool IsNotNullUpgrade => !IsNullUpgrade;

        public bool IsNullUpgrade => (Upgrade is NullUpgrade);

        public IReadOnlyList<IXWingSpecification<IUpgrade>> RestrictionList { get; }

        public bool IsMutable { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        internal UpgradeSlot(IUpgradeType upgradeType, IUpgrade upgrade, IReadOnlyList<IXWingSpecification<IUpgrade>> restrictions, int costReduction = 0, bool isMutable = true)
        {
            UpgradeType = upgradeType ?? throw new ArgumentNullException(nameof(upgradeType));
            this.upgrade = upgrade ?? throw new ArgumentNullException(nameof(upgrade));
            CostReduction = costReduction;
            RestrictionList = restrictions;
            IsMutable = isMutable;
        }

        public IReadOnlyList<IUpgradeSlot> GetInnerUpgradeSlots()
        {
            return Upgrade.GetInnerUpgradeSlots();
        }

        public bool SetUpgrade(IUpgrade upgrade)
        {
            if (!upgrade.UpgradeType.Equals(UpgradeType)) return false;           
            this.Upgrade = upgrade;
            return true;
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
            return new UpgradeSlot(this.UpgradeType.DeepClone(), this.Upgrade.DeepClone(), RestrictionList.ToList().AsReadOnly(), this.CostReduction, this.IsMutable);
        }

        public int CompareTo(IUpgradeSlot other)
        {
            return this.UpgradeType.CompareTo(other.UpgradeType);
        }
    }
}
