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
        public UpgradeModifierPackage(IEnumerable<IAction> addActionModifiers, IEnumerable<IAction> removeActionModifiers,
            IEnumerable<IUpgradeSlot> addUpgradeModifiers, IEnumerable<IUpgradeType> removeUpgradeModifiers, 
            IDictionary<string, int> pilotAttributeModifiers) : this()
        {
            AddActionModifiers = addActionModifiers ?? throw new ArgumentNullException(nameof(addActionModifiers));
            RemoveActionModifiers = removeActionModifiers ?? throw new ArgumentNullException(nameof(removeActionModifiers));
            AddUpgradeModifiers = addUpgradeModifiers ?? throw new ArgumentNullException(nameof(addUpgradeModifiers));
            RemoveUpgradeModifiers = removeUpgradeModifiers ?? throw new ArgumentNullException(nameof(removeUpgradeModifiers));
            PilotAttributeModifiers = pilotAttributeModifiers ?? throw new ArgumentNullException(nameof(pilotAttributeModifiers));
        }

        public IEnumerable<IAction> AddActionModifiers { get; }

        public IEnumerable<IAction> RemoveActionModifiers { get; }

        public IEnumerable<IUpgradeSlot> AddUpgradeModifiers { get; }

        public IEnumerable<IUpgradeType> RemoveUpgradeModifiers { get; }

        public IDictionary<string, int> PilotAttributeModifiers { get; }
    }
}
