using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using XWingSquadronBuilder_v4.BusinessLogic.Structures;
using XWingSquadronBuilder_v4.Interfaces;

namespace XWingSquadronBuilder_v4.BusinessLogic.Models
{
    public class Upgrade : IUpgrade
    {
        public IEnumerable<IAction> AddActionModifiers { get; }

        public IEnumerable<IAction> RemoveActionModifiers { get; }

        public IEnumerable<IUpgradeSlot> AddUpgradeModifiers { get; }

        public IEnumerable<IUpgradeType> RemoveUpgradeModifiers { get; }

        public IDictionary<string, int> PilotAttributeModifiers { get; }

        public IEnumerable<IUpgradeSlot> SelectableAddedUpgrades { get; }

        private IUpgradeSlot SelectedAddedUpgrade { get; set; }

        public string Name { get; }

        public int Cost { get; }

        public int SlotsRequired { get; }

        public string CardText { get; }

        public bool Unique { get; }

        public bool Limited { get; }

        public string ShipLimited { get; }

        public string ActionLimited { get; }

        public string SizeRestriction { get; }

        public IFaction Faction { get; }

        public IUpgradeType UpgradeType { get; }

        public event PropertyChangedEventHandler PropertyChanged;        

        public Upgrade(string name, int cost, int slotsRequired,
            string cardText, bool unique, bool limited, string shipLimited, string actionLimited,
            string sizeRestriction, IFaction faction, IUpgradeType upgradeType, UpgradeModifierPackage modifiers)
        {
            Name = name;
            Cost = cost;
            SlotsRequired = slotsRequired;
            CardText = cardText;
            Unique = unique;
            Limited = limited;
            ShipLimited = shipLimited;
            ActionLimited = actionLimited;
            SizeRestriction = sizeRestriction;
            Faction = faction;
            UpgradeType = upgradeType;

            AddActionModifiers = modifiers.AddActionModifiers;
            RemoveActionModifiers = modifiers.RemoveActionModifiers;
            AddUpgradeModifiers = modifiers.AddUpgradeModifiers.Select(x =>
            {
                x.PropertyChanged += AddedUpgrade_PropertyChanged;
                return x;
            }).ToList();
            RemoveUpgradeModifiers = modifiers.RemoveUpgradeModifiers;
            PilotAttributeModifiers = modifiers.PilotAttributeModifiers;
        }

        private void AddedUpgrade_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(sender, e);
        }

        public override string ToString()
        {
            return $"{Name} - {UpgradeType}";
        }

        private void SetSelectableUpgrade(IUpgradeSlot selected)
        {
            SelectedAddedUpgrade = selected;
        }

        public IEnumerable<IUpgradeSlot> GetInnerUpgradeSlots()
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
            return new Upgrade(this.Name, this.Cost, this.SlotsRequired, 
                this.CardText, this.Unique, this.Limited, this.ShipLimited,this.ActionLimited, 
                this.SizeRestriction, this.Faction.DeepClone(), this.UpgradeType.DeepClone(), 
                new UpgradeModifierPackage(this.AddActionModifiers.Select(x => x.DeepClone()), 
                this.RemoveActionModifiers.Select(x => x.DeepClone()), 
                this.AddUpgradeModifiers.Select(x => x.DeepClone()), 
                this.RemoveUpgradeModifiers.Select(x => x.DeepClone()), 
                this.PilotAttributeModifiers.Select(x => new KeyValuePair<string, int>(x.Key, x.Value))
                .ToDictionary(x => x.Key, y => y.Value), SelectableAddedUpgrades.Select(x => x.DeepClone())));
        }
    }
}
