using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace XWingSquadronBuilder_v4.Interfaces
{
    public interface IPilot : IEquatable<IPilot>, IDeepCloneable<IPilot>
    {
        string Name { get; }
        string PilotAbility { get; }
        string Ship { get; }
        IShipSize ShipSize { get; }
        bool Unique { get; }
        int Cost { get; }
        IFaction Faction { get; }

        int PilotSkill { get; }
        int Attack { get; }
        int Agility { get; }
        int Hull { get; }
        int Shield { get; }

        IEnumerable<IUpgradeSlot> Upgrades { get; }
        IEnumerable<IAction> Actions { get; }

        string Image { get; }
        string ShipIcon { get; }       

    }
}
