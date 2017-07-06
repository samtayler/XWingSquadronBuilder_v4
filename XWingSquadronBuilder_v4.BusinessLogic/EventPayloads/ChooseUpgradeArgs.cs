using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XWingSquadronBuilder_v4.Interfaces;

namespace XWingSquadronBuilder_v4.BusinessLogic.EventPayloads
{
    public class ChooseUpgradeArgs : EventArgs
    {
        public ChooseUpgradeArgs(IEnumerable<IUpgradeType> upgradeTypes, Action<IUpgradeType> setUpgradeType)
        {
            UpgradeTypes = upgradeTypes;
            SetUpgradeType = setUpgradeType;
        }

        public IEnumerable<IUpgradeType> UpgradeTypes { get; }
        public Action<IUpgradeType> SetUpgradeType { get; }
    }
}
