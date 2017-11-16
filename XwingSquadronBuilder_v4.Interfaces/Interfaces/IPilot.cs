using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace XWingSquadronBuilder_v4.Interfaces
{
    public interface IPilot : IXWingCard, IEquatable<IPilot>, IDeepCloneable<IPilot>, INotifyPropertyChanged, IDisposable
    {
        //string Name { get; }
        //bool Unique { get; }        
        string PilotAbility { get; }
        string ShipName { get; }
        IShipSize ShipSize { get; }
        int Cost { get; }
        int UpgradesCost { get; }
        IFaction Faction { get; }

        int PilotSkill { get; }
        int Attack { get; }
        int Agility { get; }
        int Hull { get; }
        int Shield { get; }

        IReadOnlyList<IUpgradeSlot> Upgrades { get; }
        IReadOnlyList<IAction> Actions { get; }

        string Image { get; }
        string ShipIcon { get; }

    }
}
