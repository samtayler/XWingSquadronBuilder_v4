using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XWingSquadronBuilder_v4.Interfaces
{
    public interface ISquadron : INotifyPropertyChanged, IDeepCloneable<ISquadron>
    {
        ObservableCollection<IPilot> Pilots { get; }
        int SquadronCostTotal { get; }
        IEnumerable<IUpgrade> UniqueUpgradesList { get; }
        IEnumerable<IPilot> UniquePilotsList { get; }

        bool AddPilot(IPilot pilot);
        bool RemovePilot(IPilot pilot);
    }
}
