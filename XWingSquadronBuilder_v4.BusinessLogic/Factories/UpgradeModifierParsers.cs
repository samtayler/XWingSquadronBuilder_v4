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
        private IUpgradeSlotFactory upgradeSlotFactory;
        private IUpgradeTypesRepository typeRepo;
        private IUpgradeRepository upgradeRepo;        

        public UpgradeModifierParser(IUpgradeSlotFactory upgradeSlotFactory, IUpgradeTypesRepository typeRepo, IUpgradeRepository upgradeRepo)
        {
            this.upgradeSlotFactory = upgradeSlotFactory;
            this.typeRepo = typeRepo;
            this.upgradeRepo = upgradeRepo;
        }

        public override IReadOnlyList<IAction> ParseAddedActions(string[] data)
        {
            return data.Select(action => XWingRepository.Instance.ActionRepository.GetAction(action)).ToList();
        }

        public override IReadOnlyList<IUpgradeSlot> ParseAddedUpgrades(AddedUpgradeJson[] data)
        {
            List<IUpgradeSlot> newUpgradeSlots = new List<IUpgradeSlot>();
            foreach (var upgrade in data)
            {
                if (upgrade.Upgrade == "")
                {
                    newUpgradeSlots.Add(upgradeSlotFactory.CreateEmpty(typeRepo.GetUpgradeType(upgrade.Type), UpgradeRestrictionParser.ParseRestrictionsForUpgradeSlot(upgrade.Restrictions), upgrade.CostReduction));
                }
                else
                {
                    newUpgradeSlots.Add(upgradeSlotFactory.CreatePrefilled(upgradeRepo.GetAllUpgradesForType(typeRepo.GetUpgradeType(upgrade.Type)).Single(x => x.Name == upgrade.Upgrade), upgrade.CostReduction));
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
            return data.Select(action => XWingRepository.Instance.ActionRepository.GetAction(action)).ToList();
        }

        public override IReadOnlyList<IUpgradeType> ParseRemovedUpgrades(string[] data)
        {
            return data.Select(x => XWingRepository.Instance.UpgradeTypesRepository.GetUpgradeType(x)).ToList();
        }

        public override IReadOnlyList<IUpgradeSlot> ParseSelectableUpgrades(ChooseUpgradeJson[] data)
        {
            return data.Select(upgrade => upgradeSlotFactory.CreateEmpty(typeRepo.GetUpgradeType(upgrade.Type))).ToList();
        }
    }

}