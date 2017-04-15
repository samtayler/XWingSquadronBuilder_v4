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
        public UpgradeModifierPackage(List<IAction> addActionModifiers, List<IAction> removeActionModifiers, 
            List<IUpgradeSlot> addUpgradeModifiers, List<IUpgradeType> removeUpgradeModifiers, 
            Dictionary<string, int> pilotAttributeModifiers) : this()
        {
            AddActionModifiers = addActionModifiers ?? throw new ArgumentNullException(nameof(addActionModifiers));
            RemoveActionModifiers = removeActionModifiers ?? throw new ArgumentNullException(nameof(removeActionModifiers));
            AddUpgradeModifiers = addUpgradeModifiers ?? throw new ArgumentNullException(nameof(addUpgradeModifiers));
            RemoveUpgradeModifiers = removeUpgradeModifiers ?? throw new ArgumentNullException(nameof(removeUpgradeModifiers));
            PilotAttributeModifiers = pilotAttributeModifiers ?? throw new ArgumentNullException(nameof(pilotAttributeModifiers));
        }

        public List<IAction> AddActionModifiers { get; }

        public List<IAction> RemoveActionModifiers { get; }

        public List<IUpgradeSlot> AddUpgradeModifiers { get; }

        public List<IUpgradeType> RemoveUpgradeModifiers { get; }

        public Dictionary<string, int> PilotAttributeModifiers { get; }
    }
}
