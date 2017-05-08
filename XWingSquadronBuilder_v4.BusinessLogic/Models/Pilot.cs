using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XWingSquadronBuilder_v4.BusinessLogic.Structures;
using XWingSquadronBuilder_v4.Interfaces;

namespace XWingSquadronBuilder_v4.BusinessLogic.Models
{
    public class Pilot : IPilot
    {
        public string Name { get; }

        public IFaction Faction { get; }

        public string Ship { get; }

        public bool Unique { get; }

        public string PilotAbility { get; }

        public string Image { get; }

        public int Cost => cost + AbilityEngine.Cost;
        private int cost;

        public event PropertyChangedEventHandler PropertyChanged;

        public IShipSize ShipSize { get; }

        public int Attack => AbilityEngine.Attack;

        public int Agility => AbilityEngine.Agility;

        public int Hull => AbilityEngine.Hull;

        public int Shield => AbilityEngine.Shield;

        public int PilotSkill => AbilityEngine.PilotSkill;

        public ObservableCollection<IAction> Actions => AbilityEngine.Actions;

        public ObservableCollection<IUpgradeSlot> Upgrades => AbilityEngine.Upgrades;

        private PilotAbilityEngine AbilityEngine { get; }         

        public string ShipIcon { get; }       

        public Pilot(string shipName, string name, bool unique, IFaction faction, int cost, PilotStatPackage stats, string pilotAbility,
                string imageUri, IShipSize shipSize, HashSet<IAction> actions, IEnumerable<IUpgradeSlot> upgrades, string shipIcon)
        {
            Name = name;
            Faction = faction;
            Ship = shipName;
            Unique = unique;
            PilotAbility = pilotAbility;
            Image = imageUri;
            ShipSize = shipSize;
            AbilityEngine = new PilotAbilityEngine(stats, actions, upgrades);
            AbilityEngine.PropertyChanged += AbilityEngine_PropertyChanged;
            this.cost = cost;
            ShipIcon = shipIcon;           
        }

        private void AbilityEngine_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }

        public override string ToString()
        {
            return $"{Name} {Ship}";
        }

        public void Dispose()
        {
            AbilityEngine.PropertyChanged -= AbilityEngine_PropertyChanged;
            AbilityEngine.Dispose();
        }

        public bool Equals(IPilot other)
        {
            return Name == other.Name 
                && Ship == other.Ship;
        }

        private Pilot(string shipName, string name, bool unique, IFaction faction, int cost,  string pilotAbility,
                string imageUri, IShipSize shipSize, PilotAbilityEngine engine, string shipIcon)
        {
            Name = name;
            Faction = faction;
            Ship = shipName;
            Unique = unique;
            PilotAbility = pilotAbility;
            Image = imageUri;
            ShipSize = shipSize;
            AbilityEngine = engine;
            AbilityEngine.PropertyChanged += AbilityEngine_PropertyChanged;
            this.cost = cost;
            ShipIcon = shipIcon;
        }

        public IPilot DeepClone()
        {
            return new Pilot(this.Ship, this.Name, this.Unique, this.Faction.DeepClone(), 
                this.cost, this.PilotAbility, this.Image, this.ShipSize.DeepClone(), 
                this.AbilityEngine.DeepClone(), this.ShipIcon);
        }
    }
}
