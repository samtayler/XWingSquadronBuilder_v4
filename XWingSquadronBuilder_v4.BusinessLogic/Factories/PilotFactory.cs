using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XWingSquadronBuilder_v4.BusinessLogic.Models;
using XWingSquadronBuilder_v4.BusinessLogic.Models.NullModels;
using XWingSquadronBuilder_v4.BusinessLogic.Repositories;
using XWingSquadronBuilder_v4.DataLayer.RawData;
using XWingSquadronBuilder_v4.Interfaces;

namespace XWingSquadronBuilder_v4.BusinessLogic.Factories
{
    public class PilotFactory
    {
        public PilotFactory(Func<string, IAction> getAction, UpgradeSlotFactory upgradeSlotFactory, Func<string, IFaction> getFaction, Func<string, IUpgradeType> getUpgradeType)
        {
            GetAction = getAction;
            this.upgradeSlotFactory = upgradeSlotFactory;
            GetFaction = getFaction;
            GetUpgradeType = getUpgradeType;
        }

        private Func<string, IAction> GetAction { get; }
        private UpgradeSlotFactory upgradeSlotFactory { get; }
        private Func<string, IFaction> GetFaction { get; }
        private Func<string, IUpgradeType> GetUpgradeType { get; }

        public IPilot CreatePilot(PilotJson pilot)
        {
            System.Diagnostics.Debug.WriteLine(pilot.Name);
            return new Pilot(pilot.ShipName, pilot.Name, pilot.Unique,
                GetFaction(pilot.Faction), pilot.Cost,
                new Structures.PilotStatPackage(pilot.Stats.Attack,
                pilot.Stats.Aglilty, pilot.Stats.Hull, pilot.Stats.Shield, pilot.PilotSkill),
                pilot.PilotAbility, pilot.Image, new ShipSize(pilot.ShipSize),
                new HashSet<IAction>(pilot.Actions.Select(x => GetAction(x))),
                pilot.Upgrades.Select(x => upgradeSlotFactory.CreateEmpty(GetUpgradeType(x))).ToList(), pilot.ShipIcon);
        }

        public static IPilot CreateNullPilot() => new NullPilot();
    }
}
