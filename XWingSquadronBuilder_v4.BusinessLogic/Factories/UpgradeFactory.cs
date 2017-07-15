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
    public class UpgradeFactory : IUpgradeFactory
    {        
        private IFactionRepository factionRepo;
        private IUpgradeTypesRepository upgradeTypeRepo;

        public UpgradeFactory(IFactionRepository factionRepo, IUpgradeTypesRepository upgradeTypeRepo)
        {            
            this.factionRepo = factionRepo;
            this.upgradeTypeRepo = upgradeTypeRepo;
        }

        public IUpgrade CreateNullUpgrade(IUpgradeType type)
        {
            return new NullUpgrade(type);
        }

        public IUpgrade CreateUpgrade(UpgradeJson upgrade, UpgradeModifierParsersBase upgradeModifierParsers)
        {
            return new Upgrade(upgrade.Name, upgrade.Cost,
                upgrade.Description, upgrade.Unique, upgrade.Limited,
                factionRepo.GetFaction(upgrade.Faction),
                upgradeTypeRepo.GetUpgradeType(upgrade.Type),
                new Structures.UpgradeModifierPackage(
                upgradeModifierParsers.ParseAddedActions(upgrade.AddedActions.ToArray()),
                upgradeModifierParsers.ParseRemovedActions(upgrade.RemovedActions.ToArray()),
                upgradeModifierParsers.ParseAddedUpgrades(upgrade.AddedUpgrades.ToArray()),
                upgradeModifierParsers.ParseRemovedUpgrades(upgrade.RemovedUpgrades.ToArray()),
                upgradeModifierParsers.ParseChangedStats(upgrade.StatChanges.ToArray()),
                upgradeModifierParsers.ParseSelectableUpgrades(upgrade.ChooseUpgrade.ToArray())), UpgradeRestrictionParser.ParseRestrictionsForUpgrade(upgrade));
        }
    }
}
