using System;
using System.Collections.Generic;
using System.Linq;
using XWingSquadronBuilder_v4.DataLayer.Models;
using XWingSquadronBuilder_v4.DataLayer.RawData;
using XWingSquadronBuilder_v4.DataLayer.Repositories;
using XWingSquadronBuilder_v4.Interfaces;

namespace XWingSquadronBuilder_v4.DataLayer.RawDataImporter
{
    public abstract class UpgradeModifierParsersBase
    {
        public abstract List<IUpgradeModifier> ParseAddedUpgrades(AddedUpgrade[] data);
        public abstract List<IUpgradeType> ParseRemovedUpgrades(string[] data);
        public abstract List<IAction> ParseAddedActions(string[] data);
        public abstract List<IAction> ParseRemovedActions(string[] data);
        public abstract Dictionary<string, int> ParseChangedStats(StatChange[] data);
    }

    public class UpgradeModifierParser : UpgradeModifierParsersBase
    {
        public override List<IAction> ParseAddedActions(string[] data)
        {
            return data.Select(action => XWingRepository.Instance.ActionRepository.GetAction(action)).ToList();
        }

        public override List<IUpgradeModifier> ParseAddedUpgrades(AddedUpgrade[] data)
        {
            return data.Select(upgrade =>
            (IUpgradeModifier)new UpgradeModifier(XWingRepository.Instance.UpgradeTypesRepository.GetUpgradeType(upgrade.Type),
            upgrade.CostLimit, upgrade.CostReduction)).ToList();
        }

        public override Dictionary<string, int> ParseChangedStats(StatChange[] data)
        {
            Dictionary<string, int> output = data.Select(stat => Tuple.Create(stat.Type, stat.Value)
            ).ToDictionary((x => x.Item1), y => y.Item2);

            return output;
        }

        public override List<IAction> ParseRemovedActions(string[] data)
        {
            return data.Select(action => XWingRepository.Instance.ActionRepository.GetAction(action)).ToList();
        }

        public override List<IUpgradeType> ParseRemovedUpgrades(string[] data)
        {
            return data.Select(x => XWingRepository.Instance.UpgradeTypesRepository.GetUpgradeType(x)).ToList();
        }
    }

}