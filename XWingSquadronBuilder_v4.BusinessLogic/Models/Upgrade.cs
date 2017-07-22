using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using XWingSquadronBuilder_v4.BusinessLogic.Structures;
using XWingSquadronBuilder_v4.Interfaces;

namespace XWingSquadronBuilder_v4.BusinessLogic.Models
{
    public class Upgrade : IUpgrade
    {
        public IReadOnlyList<IAction> AddActionModifiers { get; }

        public IReadOnlyList<IAction> RemoveActionModifiers { get; }

        public IReadOnlyList<IUpgradeSlot> AddUpgradeModifiers { get; }

        public IReadOnlyList<IUpgradeType> RemoveUpgradeModifiers { get; }

        public IDictionary<string, int> PilotAttributeModifiers { get; }

        public IEnumerable<IUpgradeSlot> SelectableAddedUpgrades { get; }

        private IUpgradeSlot SelectedAddedUpgrade { get; set; }

        public string Name { get; }

        public int Cost { get; }        

        public string CardText { get; }

        public bool Unique { get; }

        public bool Limited { get; }       

        public IFaction Faction { get; }

        public IUpgradeType UpgradeType { get; }

        public IReadOnlyList<IXWingSpecification<IPilot>> UpgradeRestrictions { get; }       

        internal Upgrade(string name, int cost,
            string cardText, bool unique, bool limited,
            IFaction faction, IUpgradeType upgradeType, UpgradeModifierPackage modifiers, IReadOnlyList<IXWingSpecification<IPilot>> upgradeRestrictions)
        {
            Name = name;
            Cost = cost;            
            CardText = cardText;
            Unique = unique;
            Limited = limited;           
            Faction = faction;
            UpgradeType = upgradeType;

            AddActionModifiers = modifiers.AddActionModifiers;
            RemoveActionModifiers = modifiers.RemoveActionModifiers;
            AddUpgradeModifiers = modifiers.AddUpgradeModifiers.ToList();
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
