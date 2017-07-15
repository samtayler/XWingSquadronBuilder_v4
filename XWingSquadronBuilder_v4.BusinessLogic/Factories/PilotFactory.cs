using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XWingSquadronBuilder_v4.BusinessLogic.Models;
using XWingSquadronBuilder_v4.BusinessLogic.Repositories;
using XWingSquadronBuilder_v4.DataLayer.RawData;
using XWingSquadronBuilder_v4.Interfaces;

namespace XWingSquadronBuilder_v4.BusinessLogic.Factories
{
    public class PilotFactory : IPilotFactory
    {
        private IUpgradeSlotFactory upgradeSlotFactory;
        private IFactionRepository factionRepo;
        private IUpgradeTypesRepository upgradeTypeRepo;

        public PilotFactory(IUpgradeSlotFactory upgradeSlotFactory, IFactionRepository factionRepo, IUpgradeTypesRepository upgradeTypeRepo)
        {
            this.upgradeSlotFactory = upgradeSlotFactory;
            this.factionRepo = factionRepo;
            this.upgradeTypeRepo = upgradeTypeRepo;
        }

        public IPilot CreatePilot(PilotJson pilot)
        {
            return new Pilot(pilot.ShipName, pilot.Name, pilot.Unique,
                factionRepo.GetFaction(pilot.Faction), pilot.Cost,
                new Structures.PilotStatPackage(pilot.Stats.Attack,
                pilot.Stats.Aglilty, pilot.Stats.Hull, pilot.Stats.Shield, pilot.PilotSkill),
                pilot.PilotAbility, pilot.Image, new ShipSize(pilot.ShipSize),
                new HashSet<IAction>(pilot.Actions.Select(x => XWingRepository.Instance.ActionRepository.GetAction(x))),
                pilot.Upgrades.Select(x => upgradeSlotFactory.CreateEmpty(upgradeTypeRepo.GetUpgradeType(x))).ToList(),pilot.ShipIcon);
        }
    }
}
