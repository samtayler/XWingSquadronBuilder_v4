using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XWingSquadronBuilder_v4.BusinessLogic.Repositories;
using XWingSquadronBuilder_v4.Interfaces;

namespace XWingSquadronBuilder_v4.BusinessLogic.Models.NullModels
{
    public class NullPilot : IPilot
    {
        public string PilotAbility => "";

        public string ShipName => "";

        public IShipSize ShipSize => new ShipSize("Any");

        public int Cost => 0;

        public int UpgradesCost => 0;

        public IFaction Faction => new NullFaction();

        public int PilotSkill => 0;

        public int Attack => 0;

        public int Agility => 0;

        public int Hull => 0;

        public int Shield => 0;

        public IReadOnlyList<IUpgradeSlot> Upgrades => new List<IUpgradeSlot>();

        public IReadOnlyList<IAction> Actions => new List<IAction>() { Action.CreateAction("Focus","f") };

        public string Image => "";

        public string ShipIcon => "";

        public string Name => "";

        public bool Unique => false;

        public Guid Id => Guid.Empty;

        public event PropertyChangedEventHandler PropertyChanged;

        public IPilot DeepClone()
        {
            return new NullPilot();
        }

        public void Dispose()
        {
            return;
        }

        public bool Equals(IPilot other)
        {
            return Name == other.Name
                && ShipName == other.ShipName
                && other.Upgrades.All(Upgrades.Contains)
                && other.Upgrades.Count == Upgrades.Count
                && other.UpgradesCost == UpgradesCost;
        }
    }
}
