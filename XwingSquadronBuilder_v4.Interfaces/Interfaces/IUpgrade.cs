using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XWingSquadronBuilder_v4.Interfaces
{
    public interface IUpgrade : IXWingCard, IEquatable<IUpgrade>, IDeepCloneable<IUpgrade>
    {
        string Name { get; }
        bool Unique { get; }
        IReadOnlyList<IAction> AddActionModifiers { get; }
        IReadOnlyList<IAction> RemoveActionModifiers { get; }
        IReadOnlyList<IUpgradeSlot> AddUpgradeModifiers { get; }
        IReadOnlyList<IUpgradeType> RemoveUpgradeModifiers { get; }
        IDictionary<string, int> PilotAttributeModifiers { get; }
        IReadOnlyList<IXWingSpecification<IPilot>> UpgradeRestrictions { get; }
        
        int Cost { get; }        
        string CardText { get; }       
        bool Limited { get; }       

        IFaction Faction { get; }        
        IUpgradeType UpgradeType { get; }
        IReadOnlyList<IUpgradeSlot> GetInnerUpgradeSlots();
    }
}
