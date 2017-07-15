using System.Collections.Generic;
using XWingSquadronBuilder_v4.Interfaces;

namespace XWingSquadronBuilder_v4.BusinessLogic.Factories
{
    public interface IUpgradeSlotFactory
    {
        IUpgradeSlot CreateEmpty(IUpgradeType type, IReadOnlyList<IXWingSpecification<IUpgrade>> restrictions = null, int costReduction = 0);
        IUpgradeSlot CreatePrefilled(IUpgrade upgrade, int costReduction = 0);
    }
}