using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XWingSquadronBuilder_v4.Interfaces;

namespace XWingSquadronBuilder_v4.BusinessLogic.Structures
{
    public struct UpgradeModifierPackage
    {
        public UpgradeModifierPackage(IReadOnlyList<IAction> addActionModifiers, IReadOnlyList<IAction> removeActionModifiers,
            IReadOnlyList<IUpgradeSlot> addUpgradeModifiers, IReadOnlyList<IUpgradeType> removeUpgradeModifiers, 
            IDictionary<string, int> pilotAttributeModifiers,
            IReadOnlyList<IUpgradeSlot> chooseableUpgradeModifiers) : this()
        {
            AddActionModifiers = addActionModifiers ?? throw new ArgumentNullException(nameof(addActionModifiers));
            RemoveActionModifiers = removeActionModifiers ?? throw new ArgumentNullException(nameof(removeActionModifiers));
            AddUpgradeModifiers = addUpgradeModifiers ?? throw new ArgumentNullException(nameof(addUpgradeModifiers));
            RemoveUpgradeModifiers = removeUpgradeModifiers ?? throw new ArgumentNullException(nameof(removeUpgradeModifiers));
            PilotAttributeModifiers = pilotAttributeModifiers ?? throw new ArgumentNullException(nameof(pilotAttributeModifiers));
            ChooseableUpgradeModifiers = chooseableUpgradeModifiers ?? throw new ArgumentNullException(nameof(chooseableUpgradeModifiers));
        }

        public IReadOnlyList<IAction> AddActionModifiers { get; }

        public IReadOnlyList<IAction> RemoveActionModifiers { get; }

        public IReadOnlyList<IUpgradeSlot> AddUpgradeModifiers { get; }

        public IReadOnlyList<IUpgradeType> RemoveUpgradeModifiers { get; }

        public IDictionary<string, int> PilotAttributeModifiers { get; }

        public IReadOnlyList<IUpgradeSlot> ChooseableUpgradeModifiers { get; set; }
    }
}
