using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using XWingSquadronBuilder_v4.Interfaces;

namespace XWingSquadronBuilder_v4.BusinessLogic.Models
{
    [DataContract]
    public class Squadron : ISquadron
    {
        private const int defaultCostCap = 100;
        public event PropertyChangedEventHandler PropertyChanged;

        [DataMember]
        public Guid Id { get; private set; }

        [DataMember]
        public Dictionary<Guid, IPilot> Pilots { get; private set; }

        [DataMember]
        public IFaction Faction { get; }

        private string name = "Unnamed Squadron";

        [DataMember]
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                NotifyPropertyChanged();
            }
        }

        private string description = "";

        [DataMember]
        public string Description
        {
            get { return description; }
            set
            {
                description = value;
                NotifyPropertyChanged();
            }
        }

        public int CostTotal => Pilots.Values.Sum(x => x.Cost + x.Upgrades.Sum(y => y.Upgrade.Cost));

        [DataMember]
        public int CostCap { get; set; }

        public Squadron(IFaction faction)
        {
            Pilots = new Dictionary<Guid, IPilot>();
            Faction = faction ?? throw new ArgumentNullException(nameof(faction));
            Id = Guid.NewGuid();
            CostCap = defaultCostCap;
        }

        public bool AddPilot(IPilot pilot)
        {
            Pilots.Add(pilot.Id, pilot);
            NotifySquadronChanges();
            return true;
        }

        private void NotifySquadronChanges()
        {
            NotifyPropertyChanged(nameof(CostTotal));
            NotifyPropertyChanged(nameof(Pilots));
        }

        public void ClearAllPilots()
        {
            foreach (var pilot in Pilots.Values)
            {
                pilot.Dispose();
            }
            this.Pilots.Clear();
            NotifySquadronChanges();
        }

        public bool RemovePilot(Guid id)
        {
            if (!Pilots.ContainsKey(id)) return false;

            Pilots[id].Dispose();
            Pilots.Remove(id);
            NotifySquadronChanges();
            return true;
        }

        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ISquadron DeepClone()
        {
            throw new NotImplementedException();
        }

        public void SetId(Guid id)
        {
            Id = id;
        }
    }
}
