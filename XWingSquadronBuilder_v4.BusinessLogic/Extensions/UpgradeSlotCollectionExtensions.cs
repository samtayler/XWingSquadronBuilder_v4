using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XWingSquadronBuilder_v4.Interfaces;

namespace XWingSquadronBuilder_v4.BusinessLogic.Extensions
{
    public static class UpgradeSlotCollectionExtensions
    {
        public static bool AreRemovalConditionsMet(this IEnumerable<IUpgradeSlot> upgrades, IUpgrade upgrade)
        {
            var upgradeTypesToRemove = upgrade.RemoveUpgradeModifiers;
            List<IUpgradeSlot> slotsToBeRemoved = new List<IUpgradeSlot>();

            foreach (var uType in upgradeTypesToRemove)
            {
                foreach (var upgradeSlot in upgrades)
                {
                    if (upgradeSlot.IsNullUpgrade && upgradeSlot.UpgradeType.Equals(uType))
                    {
                        slotsToBeRemoved.Add(upgradeSlot);
                        break;
                    }
                }
            }

            return upgradeTypesToRemove.Count() == slotsToBeRemoved.Count();
        }
    }
}
