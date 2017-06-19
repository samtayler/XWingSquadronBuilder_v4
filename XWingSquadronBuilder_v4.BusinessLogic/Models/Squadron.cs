using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using XWingSquadronBuilder_v4.BusinessLogic.Logic;
using XWingSquadronBuilder_v4.Interfaces;

namespace XWingSquadronBuilder_v4.BusinessLogic.Models
{
    public class Squadron : ISquadron, IDisposable
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<IPilot> Pilots { get; private set; }
        public SortedSet<string> UniqueNameCards { get; }
        public IFaction Faction { get; }        


        public string Name { get; set; } = "Unnamed Squadron";
        public int SquadronCostTotal => Pilots.Sum(x => x.Cost + x.Upgrades.Sum(y => y.Upgrade.Cost));

        public Func<IXWingCard, bool> CheckSquadronRequirements { get; }

        public Squadron(IFaction faction)
        {
            Pilots = new ObservableCollection<IPilot>();
            UniqueNameCards = new SortedSet<string>();
            Faction = faction;
            CheckSquadronRequirements = CheckUnique;
        }

        public bool AddPilot(IPilot pilot)
        {
            if (!CheckSquadronRequirements(pilot)) return false;

            Pilots.Add(pilot);
            NotifyPropertyChanged(nameof(SquadronCostTotal));
            return true;
        }

        public void ClearAllPilots()
        {
            foreach (var pilot in Pilots)
            {
                pilot.Dispose();
            }
            this.Pilots.Clear();
            NotifyPropertyChanged(nameof(SquadronCostTotal));
            UniqueNameCards.Clear();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="card"></param>
        /// <returns>True if check is OK</returns>
        private bool CheckUnique(IXWingCard card)
        {
            if (card.Unique)
            {
                return UniqueNameCards.Add(card.Name);
            }

            return true;
        }

        public bool RemovePilot(IPilot pilot)
        {
            if (!Pilots.Remove(pilot)) return false;

            pilot.Dispose();
            NotifyPropertyChanged(nameof(SquadronCostTotal));
            UniqueNameCards.Remove(pilot.Name);
            return true;
        }

        public void SortPilotsByName(bool ascending)
        {
            if (ascending)
            {
                Pilots = new ObservableCollection<IPilot>(Pilots.OrderBy(pilot => pilot.Name).ThenByDescending(pilot => pilot.Cost + pilot.UpgradesCost));
            }
            else
            {
                Pilots = new ObservableCollection<IPilot>(Pilots.OrderByDescending(pilot => pilot.Name).ThenByDescending(pilot => pilot.Cost + pilot.UpgradesCost));
            }
        }

        public void SortPilotsByPilotSkill(bool ascending)
        {
            if (ascending)
            {
                Pilots = new ObservableCollection<IPilot>(Pilots.OrderBy(pilot => pilot.PilotSkill).ThenByDescending(pilot => pilot.Name));
            }
            else
            {
                Pilots = new ObservableCollection<IPilot>(Pilots.OrderByDescending(pilot => pilot.PilotSkill).ThenByDescending(pilot => pilot.Name));
            }
        }

        public void SortPilotsByCost(bool ascending)
        {
            if (ascending)
            {
                Pilots = new ObservableCollection<IPilot>(Pilots.OrderBy(pilot => pilot.Cost + pilot.UpgradesCost).ThenByDescending(pilot => pilot.Name));
            }
            else
            {
                Pilots = new ObservableCollection<IPilot>(Pilots.OrderByDescending(pilot => pilot.Cost + pilot.UpgradesCost).ThenByDescending(pilot => pilot.Name));
            }
        }

        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void Dispose()
        {

        }

        public ISquadron DeepClone()
        {
            throw new NotImplementedException();
        }
    }
}
