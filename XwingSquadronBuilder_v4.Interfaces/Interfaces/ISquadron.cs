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
        string Name { get; }
        ObservableCollection<IPilot> Pilots { get; }
        int SquadronCostTotal { get; }
        IReadOnlyList<string> UniqueNameCards { get; }        

        bool AddPilot(IPilot pilot);
        bool RemovePilot(IPilot pilot);
        void ClearAllPilots();
    }
}
