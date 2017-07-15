using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XWingSquadronBuilder_v4.BusinessLogic.Models;
using XWingSquadronBuilder_v4.Interfaces;

namespace XWingSquadronBuilder_v4.BusinessLogic.Factories
{
    public class UpgradeSlotFactory : IUpgradeSlotFactory
    {
        private IUpgradeFactory upgradeFactory;

        public UpgradeSlotFactory(IUpgradeFactory upgradeFactory)
        {
            this.upgradeFactory = upgradeFactory;
        }

        public IUpgradeSlot CreateEmpty(IUpgradeType type, IReadOnlyList<IXWingSpecification<IUpgrade>> restrictions = null, int costReduction = 0)
        {
            if (restrictions == null) restrictions = new List<IXWingSpecification<IUpgrade>>();

            return new UpgradeSlot(type, upgradeFactory.CreateNullUpgrade(type), restrictions, costReduction);
        }

        public IUpgradeSlot CreatePrefilled(IUpgrade upgrade, int costReduction = 0)
        {
            return new UpgradeSlot(upgrade.UpgradeType, upgrade, new List<IXWingSpecification<IUpgrade>>(), costReduction);
        }
    }
}
