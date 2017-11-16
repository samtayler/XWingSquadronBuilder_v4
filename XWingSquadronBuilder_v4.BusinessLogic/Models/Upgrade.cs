using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using XWingSquadronBuilder_v4.BusinessLogic.Structures;
using XWingSquadronBuilder_v4.Interfaces;

namespace XWingSquadronBuilder_v4.BusinessLogic.Models
{
    [DataContract]
    public class Upgrade : IUpgrade
    {
        [DataMember]
        public IReadOnlyList<IAction> AddActionModifiers { get; }

        [DataMember]
        public IReadOnlyList<IAction> RemoveActionModifiers { get; }

        [DataMember]
        public IReadOnlyList<IUpgradeSlot> AddUpgradeModifiers { get; }

        [DataMember]
        public IReadOnlyList<IUpgradeType> RemoveUpgradeModifiers { get; }

        [DataMember]
        public IDictionary<string, int> PilotAttributeModifiers { get; }

        [DataMember]
        public IEnumerable<IUpgradeSlot> SelectableAddedUpgrades { get; }

        [DataMember]
        private IUpgradeSlot SelectedAddedUpgrade { get; set; }

        [DataMember]
        public string Name { get; }

        [DataMember]
        public int Cost { get; }

        [DataMember]
        public string CardText { get; }

        [DataMember]
        public bool Unique { get; }

        [DataMember]
        public bool Limited { get; }

        [DataMember]
        public IFaction Faction { get; }

        [DataMember]
        public IUpgradeType UpgradeType { get; }

        [DataMember]
        public IReadOnlyList<IXWingSpecification<IPilot>> UpgradeRestrictions { get; }        

        public Guid Id { get; }

        internal Upgrade(
            string name, 
            int cost,
            string cardText, 
            bool unique, 
            bool limited,
            IFaction faction, 
            IUpgradeType upgradeType, 
            UpgradeModifierPackage modifiers, 
            IReadOnlyList<IXWingSpecification<IPilot>> upgradeRestrictions)
        {
            Id = Guid.NewGuid();
            Name = name;
            Cost = cost;            
            CardText = cardText;
            Unique = unique;
            Limited = limited;           
            Faction = faction;
            UpgradeType = upgradeType;
            

            AddActionModifiers = modifiers.AddActionModifiers;
            RemoveActionModifiers = modifiers.RemoveActionModifiers;
            AddUpgradeModifiers = modifiers.AddUpgradeModifiers;
            RemoveUpgradeModifiers = modifiers.RemoveUpgradeModifiers;
            PilotAttributeModifiers = modifiers.PilotAttributeModifiers;
            SelectableAddedUpgrades = modifiers.ChooseableUpgradeModifiers;
            UpgradeRestrictions = upgradeRestrictions;
        }       

        public override string ToString()
        {
            return $"{Name} - {UpgradeType}";
        }

        private void SetSelectableUpgrade(IUpgradeSlot selected)
        {
            SelectedAddedUpgrade = selected;
        }

        public IReadOnlyList<IUpgradeSlot> GetInnerUpgradeSlots()
        {
            return AddUpgradeModifiers.Select(x => x.GetInnerUpgradeSlots().Concat(new[] { x }))
                .Aggregate(new List<IUpgradeSlot>(), (x, y) => x.Concat(y).ToList());
        }

        public bool Equals(IUpgrade other)
        {
            return Name == other.Name && UpgradeType.Equals(other.UpgradeType) && CardText == other.CardText;
        }

        public IUpgrade DeepClone()
        {
            return new Upgrade(this.Name, this.Cost,
                this.CardText, this.Unique, this.Limited, this.Faction.DeepClone(), this.UpgradeType.DeepClone(), 
                new UpgradeModifierPackage(this.AddActionModifiers.Select(x => x.DeepClone()).ToList().AsReadOnly(), 
                this.RemoveActionModifiers.Select(x => x.DeepClone()).ToList().AsReadOnly(), 
                this.AddUpgradeModifiers.Select(x => x.DeepClone()).ToList().AsReadOnly(), 
                this.RemoveUpgradeModifiers.Select(x => x.DeepClone()).ToList().AsReadOnly(), 
                this.PilotAttributeModifiers.Select(x => new KeyValuePair<string, int>(x.Key, x.Value))
                .ToDictionary(x => x.Key, y => y.Value), SelectableAddedUpgrades.Select(x => x.DeepClone()).ToList().AsReadOnly()),UpgradeRestrictions.ToList().AsReadOnly());
        }
    }
}
