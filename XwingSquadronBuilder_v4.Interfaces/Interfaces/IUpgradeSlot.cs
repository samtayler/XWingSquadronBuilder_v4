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
        IUpgrade Upgrade { get; set; }
        int CostReduction { get; }
        int CostRestriction { get; }
        int Cost { get; }


        bool Enabled { get;}
        void Enable();
        void Disable();
        IEnumerable<IUpgradeSlot> GetInnerUpgradeSlots();
        void ClearUpgrade();
    }
}
