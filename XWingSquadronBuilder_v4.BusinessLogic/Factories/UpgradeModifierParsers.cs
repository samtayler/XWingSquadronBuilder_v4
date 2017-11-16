using System;
using System.Collections.Generic;
using System.Linq;
using XWingSquadronBuilder_v4.BusinessLogic.Models;
using XWingSquadronBuilder_v4.BusinessLogic.Repositories;
using XWingSquadronBuilder_v4.DataLayer.RawData;
using XWingSquadronBuilder_v4.Interfaces;

namespace XWingSquadronBuilder_v4.BusinessLogic.Factories
{
    public abstract class UpgradeModifierParsersBase
    {
        public abstract IReadOnlyList<IUpgradeSlot> ParseAddedUpgrades(AddedUpgradeJson[] data);
        public abstract IReadOnlyList<IUpgradeType> ParseRemovedUpgrades(string[] data);
        public abstract IReadOnlyList<IAction> ParseAddedActions(string[] data);
        public abstract IReadOnlyList<IAction> ParseRemovedActions(string[] data);
        public abstract Dictionary<string, int> ParseChangedStats(StatChangeJson[] data);
        public abstract IReadOnlyList<IUpgradeSlot> ParseSelectableUpgrades(ChooseUpgradeJson[] data);
    }

    public class UpgradeModifierParser : UpgradeModifierParsersBase
    {
        private UpgradeSlotFactory upgradeSlotFactory { get; }
        private Func<string, IUpgradeType> getUpgradeType { get; }
        private Func<string, string, IUpgrade> getUpgrade { get; }
        private Func<string, IAction> getAction { get; }

        public UpgradeModifierParser(UpgradeSlotFactory upgradeSlotFactory,
            Func<string, IUpgradeType> getUpgradeType,
            Func<string, string, IUpgrade> getUpgrade,
            Func<string, IAction> getAction)
        {
            this.upgradeSlotFactory = upgradeSlotFactory;
            this.getUpgradeType = getUpgradeType;
            this.getUpgrade = getUpgrade;
            this.getAction = getAction;
        }

        public override IReadOnlyList<IAction> ParseAddedActions(string[] data)
        {
            return data.Select(action => getAction(action)).ToList();
        }

        public override IReadOnlyList<IUpgradeSlot> ParseAddedUpgrades(AddedUpgradeJson[] data)
        {
            List<IUpgradeSlot> newUpgradeSlots = new List<IUpgradeSlot>();
            foreach (var upgrade in data)
            {
                if (string.IsNullOrEmpty(upgrade.Upgrade))
                {
                    newUpgradeSlots.Add(upgradeSlotFactory.CreateEmpty(getUpgradeType(upgrade.Type), UpgradeRestrictionParser.ParseRestrictionsForUpgradeSlot(upgrade.Restrictions ?? new List<RestrictionJson>()), upgrade.CostReduction));
                }
                else
                {
                    newUpgradeSlots.Add(upgradeSlotFactory.CreatePrefilled(getUpgrade(upgrade.Upgrade, upgrade.Type), upgrade.CostReduction));
                }
            }

            return newUpgradeSlots;
        }

        public override Dictionary<string, int> ParseChangedStats(StatChangeJson[] data)
        {
            Dictionary<string, int> output = data.Select(stat => Tuple.Create(stat.Type, stat.Value)
            ).ToDictionary((x => x.Item1), y => y.Item2);

            return output;
        }

        public override IReadOnlyList<IAction> ParseRemovedActions(string[] data)
        {
            return data.Select(action => getAction(action)).ToList();
        }

        public override IReadOnlyList<IUpgradeType> ParseRemovedUpgrades(string[] data)
        {
            return data.Select(x => getUpgradeType(x)).ToList();
        }

        public override IReadOnlyList<IUpgradeSlot> ParseSelectableUpgrades(ChooseUpgradeJson[] data)
        {
            return data.Select(upgrade => upgradeSlotFactory.CreateEmpty(getUpgradeType(upgrade.Type))).ToList();
        }
    }

}