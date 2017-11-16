using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using XWingSquadronBuilder_v4.BusinessLogic.Structures;
using XWingSquadronBuilder_v4.Interfaces;

namespace XWingSquadronBuilder_v4.BusinessLogic.Models
{
    [DataContract]
    public class Pilot : IPilot
    {
        public event PropertyChangedEventHandler PropertyChanged;

        [DataMember]
        public string Name { get; }
        [DataMember]
        public IFaction Faction { get; }
        [DataMember]
        public string ShipName { get; }
        [DataMember]
        public bool Unique { get; }
        [DataMember]
        public string PilotAbility { get; }
        [DataMember]
        public string Image { get; }
        [DataMember]
        public int Cost { get; }
        [DataMember]
        public IShipSize ShipSize { get; }
        [DataMember]
        private PilotAbilityEngine AbilityEngine { get; }
        [DataMember]
        public string ShipIcon { get; }

        public int UpgradesCost => AbilityEngine.Cost;             

        public int Attack => AbilityEngine.Attack;

        public int Agility => AbilityEngine.Agility;

        public int Hull => AbilityEngine.Hull;

        public int Shield => AbilityEngine.Shield;

        public int PilotSkill => AbilityEngine.PilotSkill;

        public IReadOnlyList<IAction> Actions => AbilityEngine.Actions;

        public IReadOnlyList<IUpgradeSlot> Upgrades => AbilityEngine.Upgrades;
        
        public Guid Id { get; }

        internal Pilot(string shipName, string name, bool unique, IFaction faction, int cost, PilotStatPackage stats, string pilotAbility,
                string imageUri, IShipSize shipSize, HashSet<IAction> actions, IReadOnlyList<IUpgradeSlot> upgrades, string shipIcon)
        {
            Name = name;
            Faction = faction;
            ShipName = shipName;
            Unique = unique;
            PilotAbility = pilotAbility;
            Image = imageUri;
            ShipSize = shipSize;
            AbilityEngine = new PilotAbilityEngine(stats, actions, upgrades);
            AbilityEngine.PropertyChanged += AbilityEngine_PropertyChanged;
            this.Cost = cost;
            ShipIcon = shipIcon;
            Id = Guid.NewGuid();
        }

        private void AbilityEngine_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }

        public override string ToString()
        {
            return $"{Name} {ShipName}";
        }               

        public void Dispose()
        {
            AbilityEngine.PropertyChanged -= AbilityEngine_PropertyChanged;
            AbilityEngine.Dispose();
        }

        public bool Equals(IPilot other)
        {
            return Name == other.Name 
                && ShipName == other.ShipName
                && other.Upgrades.All(Upgrades.Contains) 
                && other.Upgrades.Count == Upgrades.Count
                && other.UpgradesCost == UpgradesCost;
        }

       
        private Pilot(string shipName, string name, bool unique, IFaction faction, int cost,  string pilotAbility,
                string imageUri, IShipSize shipSize, PilotAbilityEngine engine, string shipIcon)
        {
            Name = name;
            Faction = faction;
            ShipName = shipName;
            Unique = unique;
            PilotAbility = pilotAbility;
            Image = imageUri;
            ShipSize = shipSize;
            AbilityEngine = engine;
            AbilityEngine.PropertyChanged += AbilityEngine_PropertyChanged;
            this.Cost = cost;
            ShipIcon = shipIcon;
            Id = Guid.NewGuid();
        }

        public IPilot DeepClone()
        {
            return new Pilot(this.ShipName, this.Name, this.Unique, this.Faction.DeepClone(), 
                this.Cost, this.PilotAbility, this.Image, this.ShipSize.DeepClone(), 
                this.AbilityEngine.DeepClone(), this.ShipIcon);
        }
    }
}
