using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using XWingSquadronBuilder_v4.BusinessLogic.Repositories;
using XWingSquadronBuilder_v4.Interfaces;

namespace XWingSquadronBuilder_v4.BusinessLogic.Models.NullModels
{
    public class NullSquadron : ISquadron
    {
        public Guid Id => Guid.NewGuid();

        public string Name { get; set; } = "No Squadron";

        public IFaction Faction => new NullFaction();

        public int CostTotal => 0;

        public IReadOnlyList<string> UniqueNameCards => new List<string>();

        public Dictionary<Guid, IPilot> Pilots => new Dictionary<Guid, IPilot>();

        public string Description { get; set; } = "";

        public int CostCap { get; set; } = 0;

        public event PropertyChangedEventHandler PropertyChanged;

        public bool AddPilot(IPilot pilot)
        {
            return true; //do nothing
        }

        public void ClearAllPilots()
        {
            return; //do nothing
        }

        public ISquadron DeepClone()
        {
            return this; //don't copy
        }

        public bool RemovePilot(Guid id)
        {
            return true; //do nothing
        }

        public void SetId(Guid id)
        {
            return;
        }
    }
}
