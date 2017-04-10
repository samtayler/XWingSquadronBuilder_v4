using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XWingSquadronBuilder_v4.Interfaces
{
    public interface IUpgradeModifier
    {
        IUpgradeType UpgradeType { get; }
        int CostRestriction { get; }
        int CostReduction { get; }
    }
}
