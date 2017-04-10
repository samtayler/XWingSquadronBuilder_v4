using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using XWingSquadronBuilder_v4.Interfaces;

namespace XWingSquadronBuilder_v4.DataLayer.Models
{
    [DataContract]
    public class Pilot : IPilot
    {       
        [DataMember]
        public string Name { get; }

        [DataMember]
        public IFaction Faction { get; }

        [DataMember]
        public string Ship { get; }

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
        public int Attack { get; }

        [DataMember]
        public int Agility { get; }

        [DataMember]
        public int Hull { get; }

        [DataMember]
        public int Shield { get; }

        [DataMember]
        public int PilotSkill { get; }

        [DataMember]
        public IEnumerable<IAction> Actions { get; }

        [DataMember]
        public IEnumerable<IUpgradeType> Upgrades { get; }

        [DataMember]
        public string ShipIcon { get; }

        public Pilot(string shipName, string name, bool unique, IFaction faction, int cost, int attack, int agility, int hull, int shield, int pilotSkill, string pilotAbility,
                string imageUri, IShipSize shipSize, IEnumerable<IAction> actions, IEnumerable<IUpgradeType> upgrades, string shipIcon)
        {
            Name = name;
            Attack = attack;
            Agility = agility;
            Hull = hull;
            Shield = shield;
            PilotSkill = pilotSkill;
            Faction = faction;
            Ship = shipName;
            Unique = unique;
            PilotAbility = pilotAbility;
            Image = imageUri;
            ShipSize = shipSize;
            Cost = cost;
            ShipIcon = shipIcon;

        }        

        public override string ToString()
        {
            return $"{Name} {Ship}";
        }

        public bool Equals(IPilot other)
        {
            return Name == other.Name && Ship == other.Ship;
        }

        public IPilot DeepClone()
        {
            return new Pilot(this.Ship, this.Name, this.Unique, this.Faction.DeepClone(),
                this.Cost, this.Attack, this.Agility, this.Hull, this.Shield,
                this.PilotSkill, this.PilotAbility, this.Image, this.ShipSize.DeepClone(),
                this.Actions.Select(x => x.DeepClone()), this.Upgrades.Select(x => x.DeepClone()), this.ShipIcon);
        }
    }
}
