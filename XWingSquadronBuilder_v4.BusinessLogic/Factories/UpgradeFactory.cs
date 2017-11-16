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
    public class UpgradeFactory
    {
        public UpgradeFactory(
            Func<string, IFaction> getFaction,
            Func<string, IUpgradeType> getUpgradeType,
            Func<string, IAction> getAction,
            Func<string, string, IUpgrade> getUpgrade)
        {
            GetFaction = getFaction;
            GetUpgradeType = getUpgradeType;
            GetAction = getAction;
            GetUpgrade = getUpgrade;
        }

        private Func<string, IAction> GetAction { get; }
        private Func<string, IFaction> GetFaction { get; }
        private Func<string, IUpgradeType> GetUpgradeType { get; }
        private Func<string, string, IUpgrade> GetUpgrade { get; }

        public IUpgrade CreateNullUpgrade(IUpgradeType type)
        {
            return new NullUpgrade(type);
        }

        public IUpgrade CreateUpgrade(UpgradeJson upgrade)
        {
            var upgradeSlotFactory = new UpgradeSlotFactory(this);
            UpgradeModifierParser parser = new UpgradeModifierParser(upgradeSlotFactory, GetUpgradeType, GetUpgrade, GetAction);
            return new Upgrade(upgrade.Name, upgrade.Cost,
                upgrade.Description, upgrade.Unique, upgrade.Limited,
                GetFaction(upgrade.Faction),
                GetUpgradeType(upgrade.Type),
                new Structures.UpgradeModifierPackage(
                parser.ParseAddedActions(upgrade.AddedActions?.ToArray() ?? new string[0]),
                parser.ParseRemovedActions(upgrade.RemovedActions?.ToArray() ?? new string[0]),
                parser.ParseAddedUpgrades(upgrade.AddedUpgrades?.ToArray() ?? new AddedUpgradeJson[0]),
                parser.ParseRemovedUpgrades(upgrade.RemovedUpgrades?.ToArray() ?? new string[0]),
                parser.ParseChangedStats(upgrade.StatChanges?.ToArray() ?? new StatChangeJson[0]),
                parser.ParseSelectableUpgrades(upgrade.ChooseUpgrade?.ToArray() ?? new ChooseUpgradeJson[0])),
                UpgradeRestrictionParser.ParseRestrictionsForUpgrade(GetUpgradeType, GetAction, upgrade));
        }
    }
}
