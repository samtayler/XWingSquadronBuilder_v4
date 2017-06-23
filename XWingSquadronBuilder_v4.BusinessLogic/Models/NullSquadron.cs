using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XWingSquadronBuilder_v4.Interfaces;

namespace XWingSquadronBuilder_v4.BusinessLogic.Models
{
    public class NullSquadron : ISquadron
    {
        public string Name { get; set; } = "No Squadron";

        public ObservableCollection<IPilot> Pilots => new ObservableCollection<IPilot>();

        public int SquadronCostTotal => 0;

        public SortedSet<string> UniqueNameCards => new SortedSet<string>();

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

        public bool RemovePilot(IPilot pilot)
        {
            return true; //do nothing
        }
    }
}
