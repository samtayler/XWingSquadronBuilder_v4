using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

using XWingSquadronBuilder_v4.Interfaces;

namespace XWingSquadronBuilder_v4.DataLayer.Models
{
    [DataContract]
    public class Upgrade : IUpgrade
    {
        [DataMember]
        public IEnumerable<IAction> AddActionModifiers { get; }

        [DataMember]
        public IEnumerable<IAction> RemoveActionModifiers { get; }

        [DataMember]
        public IEnumerable<IUpgradeModifier> AddUpgradeModifiers { get; }

        [DataMember]
        public IEnumerable<IUpgradeType> RemoveUpgradeModifiers { get; }

        [DataMember]
        public IDictionary<string, int> PilotAttributeModifiers { get; }

        [DataMember]
        public string Name { get; }

        [DataMember]
        public int Cost { get; }

        [DataMember]
        public int SlotsRequired { get; }

        [DataMember]
        public string CardText { get; }

        [DataMember]
        public bool Unique { get; }

        [DataMember]
        public bool Limited { get; }

        [DataMember]
        public string ShipLimited { get; }

        [DataMember]
        public string SizeRestriction { get; }

        [DataMember]
        public IFaction Faction { get; }

        [DataMember]
        public IUpgradeType UpgradeType { get; }

        public Upgrade(string name, int cost, int slotsRequired,
            string cardText, bool unique, bool limited, string shipLimited,
            string sizeRestriction, IFaction faction, IUpgradeType upgradeType,
            IEnumerable<IAction> addActionModifiers, IEnumerable<IAction> removeActionModifiers,
            IEnumerable<IUpgradeModifier> addUpgradeModifiers, IEnumerable<IUpgradeType> removeUpgradeModifiers, IDictionary<string, int> pilotAttributesModifiers)
        {
            Name = name;
            Cost = cost;
            SlotsRequired = slotsRequired;
            CardText = cardText;
            Unique = unique;
            Limited = limited;
            ShipLimited = shipLimited;
            SizeRestriction = sizeRestriction;
            Faction = faction;
            UpgradeType = upgradeType;
            AddActionModifiers = addActionModifiers;
            RemoveActionModifiers = removeActionModifiers;
            AddUpgradeModifiers = addUpgradeModifiers;
            RemoveUpgradeModifiers = RemoveUpgradeModifiers;
            PilotAttributeModifiers = pilotAttributesModifiers;
        }

        public override string ToString() => $"{Name} - {UpgradeType}";

        public bool Equals(IUpgrade other) => Name == other.Name && UpgradeType.Equals(other.UpgradeType);

        public IUpgrade DeepClone()
        {
            return null;
        }
    }

    public class UpgradeModifier : IUpgradeModifier
    {
        public UpgradeModifier(IUpgradeType upgradeType, int costRestriction, int costReduction)
        {
            UpgradeType = upgradeType;
            CostRestriction = costRestriction;
            CostReduction = costReduction;
        }

        public IUpgradeType UpgradeType { get; }
        public int CostRestriction { get; }
        public int CostReduction { get; }
    }
}
