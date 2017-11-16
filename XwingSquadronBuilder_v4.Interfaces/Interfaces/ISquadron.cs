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
        Guid Id { get; }
        string Name { get; set; }
        string Description { get; set; }
        Dictionary<Guid,IPilot> Pilots { get; }
        int CostCap { get; set; }
        int CostTotal { get; }        
        IFaction Faction { get; }

        bool AddPilot(IPilot pilot);
        bool RemovePilot(Guid id);
        void ClearAllPilots();
        void SetId(Guid id);
    }
}
