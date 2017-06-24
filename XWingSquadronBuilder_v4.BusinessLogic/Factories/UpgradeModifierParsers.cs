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
        public abstract List<IUpgradeSlot> ParseAddedUpgrades(AddedUpgradeJson[] data);
        public abstract List<IUpgradeType> ParseRemovedUpgrades(string[] data);
        public abstract List<IAction> ParseAddedActions(string[] data);
        public abstract List<IAction> ParseRemovedActions(string[] data);
        public abstract Dictionary<string, int> ParseChangedStats(StatChangeJson[] data);
    }

    public class UpgradeModifierParser : UpgradeModifierParsersBase
    {
        public override List<IAction> ParseAddedActions(string[] data)
        {
            return data.Select(action => XWingRepository.Instance.ActionRepository.GetAction(action)).ToList();
        }

        public override List<IUpgradeSlot> ParseAddedUpgrades(AddedUpgradeJson[] data)
        {
            return data.Select(upgrade =>
            (IUpgradeSlot)new UpgradeSlot(XWingRepository.Instance.UpgradeTypesRepository.GetUpgradeType(upgrade.Type), 
            new NullUpgrade(XWingRepository.Instance.UpgradeTypesRepository.GetUpgradeType(upgrade.Type)),
            upgrade.CostReduction, upgrade.CostLimit)).ToList();
        }

        public override Dictionary<string, int> ParseChangedStats(StatChangeJson[] data)
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