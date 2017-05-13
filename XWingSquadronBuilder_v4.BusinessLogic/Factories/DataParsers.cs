using System;
using System.Linq;
using XWingSquadronBuilder_v4.BusinessLogic.Repositories;
using XWingSquadronBuilder_v4.DataLayer.RawData;
using XWingSquadronBuilder_v4.Interfaces;
using XWingSquadronBuilder_v4.BusinessLogic.Models;
using System.Collections.Generic;

namespace XWingSquadronBuilder_v4.BusinessLogic.Factories
{
    public class DataParsers
    {
        public static IAction CreateAction(ActionJson action)
        {
            return new Models.Action(action.Name, action.ImageSource);
        }

        public static IFaction CreateFaction(FactionJson faction)
        {
            return new Faction(faction.Name, faction.Image);
        }

        public static IPilot CreatePilot(PilotJson pilot)
        {
            return new Pilot(pilot.ShipName, pilot.Name, pilot.Unique,
                XWingRepository.Instance.FactionRepository.GetFaction(pilot.Faction), pilot.Cost, 
                new Structures.PilotStatPackage(pilot.Stats.Attack,
                pilot.Stats.Aglilty, pilot.Stats.Hull, pilot.Stats.Shield, pilot.PilotSkill),
                pilot.PilotAbility, pilot.Image, new ShipSize(pilot.ShipSize),
                new HashSet<IAction>(pilot.Actions.Select(x => XWingRepository.Instance.ActionRepository.GetAction(x))),
                pilot.Upgrades.Select(x => 
                new UpgradeSlot(XWingRepository.Instance.UpgradeTypesRepository.GetUpgradeType(x),
                new NullUpgrade(XWingRepository.Instance.UpgradeTypesRepository.GetUpgradeType(x)))), 
                pilot.ShipIcon);
        }

        public static IUpgrade CreateUpgrade(UpgradeJson upgrade)
        {
            var upgradeParsers = new UpgradeModifierParser();

            return new Upgrade(upgrade.Name, upgrade.Cost, upgrade.SlotsRequired,
                upgrade.Description, upgrade.Unique, upgrade.Limited, upgrade.ShipLimited, 
                upgrade.ActionLimited, upgrade.SizeRestriction,
                XWingRepository.Instance.FactionRepository.GetFaction(upgrade.Faction),
                XWingRepository.Instance.UpgradeTypesRepository.GetUpgradeType(upgrade.Type), 
                new Structures.UpgradeModifierPackage(
                upgradeParsers.ParseAddedActions(upgrade.AddedActions),
                upgradeParsers.ParseRemovedActions(upgrade.RemovedActions),
                upgradeParsers.ParseAddedUpgrades(upgrade.AddedUpgrades),
                upgradeParsers.ParseRemovedUpgrades(upgrade.RemovedUpgrades),
                upgradeParsers.ParseChangedStats(upgrade.StatChanges)));
        }

        public static IUpgradeType CreateUpgradeType(UpgradeTypeJson upgradeType)
        {
            return new UpgradeType(upgradeType.Name, upgradeType.ImageSource);
        }
    }
}
