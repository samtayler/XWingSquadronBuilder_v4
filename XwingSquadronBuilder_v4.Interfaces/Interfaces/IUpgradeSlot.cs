using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XWingSquadronBuilder_v4.Interfaces
{
    public interface IUpgradeSlot : INotifyPropertyChanged, IEquatable<IUpgradeSlot>, IDeepCloneable<IUpgradeSlot>, IComparable<IUpgradeSlot>
    {
        IUpgradeType UpgradeType { get; }
        IUpgrade Upgrade { get; }
        int CostReduction { get; }
        int CostRestriction { get; }
        int Cost { get; }
        bool IsNotNullUpgrade { get; }
        bool IsNullUpgrade { get; }
        IReadOnlyList<IXWingSpecification<IUpgrade>> RestrictionList { get; }
        bool IsMutable { get; }
        
        IReadOnlyList<IUpgradeSlot> GetInnerUpgradeSlots();
        void ClearUpgrade();
        bool SetUpgrade(IUpgrade upgrade);


    }
}
