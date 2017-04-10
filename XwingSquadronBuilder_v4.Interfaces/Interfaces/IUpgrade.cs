using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XWingSquadronBuilder_v4.Interfaces
{
    public interface IUpgrade : IEquatable<IUpgrade>, IDeepCloneable<IUpgrade>
    {
        IEnumerable<IAction> AddActionModifiers { get; }
        IEnumerable<IAction> RemoveActionModifiers { get; }
        IEnumerable<IUpgradeModifier> AddUpgradeModifiers { get; }
        IEnumerable<IUpgradeType> RemoveUpgradeModifiers { get; }
        IDictionary<string, int> PilotAttributeModifiers { get; }        
        
        string Name { get; }
        int Cost { get; }
        int SlotsRequired { get; }
        string CardText { get; }
        bool Unique { get; }
        bool Limited { get; }
        string ShipLimited { get; }
        string SizeRestriction { get; }  

        IFaction Faction { get; }        
        IUpgradeType UpgradeType { get; }        
    }
}
