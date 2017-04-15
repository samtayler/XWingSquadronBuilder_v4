using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using XWingSquadronBuilderClasses.Interfaces;

namespace XWingSquadronBuilder.BusinessLogic.Models
{
    public class Squadron : ISquadron, IDisposable
    {
        public Squadron(IFaction faction)
        {
            Pilots = new ObservableCollection<IPilot>();
            Faction = faction;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<IPilot> Pilots { get; }
        public int SquadronCostTotal => Pilots.Sum(x => x.Cost + x.Upgrades.Sum(y => y.Upgrade.Cost));
        public IFaction Faction { get; }
        

        public IEnumerable<IUpgrade> UniqueUpgradesList => Pilots.Select(x => x.Upgrades.Select(y => y.Upgrade).Where(y => y.Unique))
            .Aggregate(new List<IUpgrade>(), (x, y) => x.Concat(y).ToList());

        public IEnumerable<IPilot> UniquePilotsList => Pilots.Select(x => x).Where(x => x.Unique);

        public bool AddPilot(IPilot pilot)
        {
            if (RunChecks(pilot))
            {
                Pilots.Add(pilot);                
                NotifyChanges();
                return true;
            }
            return false;
        }       

        private bool RunChecks(IPilot pilot)
        {
            return CheckUnique(pilot);
        }

        private void NotifyChanges()
        {            
            NotifyPropertyChanged(nameof(SquadronCostTotal));
        }

        public bool RemovePilot(IPilot pilot)
        {
            if (!Pilots.Remove(pilot)) return false;            
            pilot.Dispose();
            NotifyChanges();
            return true;
        }       

        private bool CheckUnique(IPilot pilot)
        {
            return !UniquePilotsList.Any(x => x.Equals(pilot));
        }

        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Pilot_PilotCopied(object sender, IPilot e)
        {
            AddPilot(e);
        }        

        private void Pilot_PilotRemoved(object sender, IPilot e)
        {
            RemovePilot(e);
        }

        public void Dispose()
        {
            foreach (var pilot in Pilots)
            {                
                pilot.Dispose();
            }
        }
    }
}
