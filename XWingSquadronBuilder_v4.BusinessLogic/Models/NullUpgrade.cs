using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XWingSquadronBuilder_v4.BusinessLogic.Repositories;
using XWingSquadronBuilder_v4.Interfaces;

namespace XWingSquadronBuilder_v4.BusinessLogic.Models
{
    public class NullUpgrade : IUpgrade
    {
        internal NullUpgrade(IUpgradeType upgradeType)
        {
            UpgradeType = upgradeType;
        }

        public IReadOnlyList<IAction> AddActionModifiers => new List<IAction>();

        public IReadOnlyList<IAction> RemoveActionModifiers => new List<IAction>();

        public IReadOnlyList<IUpgradeSlot> AddUpgradeModifiers => new List<IUpgradeSlot>();

        public IReadOnlyList<IUpgradeType> RemoveUpgradeModifiers => new List<IUpgradeType>();

        public IDictionary<string, int> PilotAttributeModifiers => new Dictionary<string, int>();

        public string Name => string.Empty;

        public int Cost => 0;

        public int SlotsRequired => 0;

        public string CardText => "";

        public bool Unique => false;

        public bool Limited => false;       

        public IFaction Faction => XWingRepository.Instance.FactionRepository.GetFaction("Any");

        public IUpgradeType UpgradeType { get; }

        public IReadOnlyList<IXWingSpecification<IPilot>> UpgradeRestrictions => new List<IXWingSpecification<IPilot>>();

        public event PropertyChangedEventHandler PropertyChanged;

        public IUpgrade DeepClone()
        {
            return new NullUpgrade(this.UpgradeType);
        }

        public bool Equals(IUpgrade other)
        {
            return Name == other.Name && UpgradeType.Equals(other.UpgradeType) && CardText == other.CardText;
        }

        public IReadOnlyList<IUpgradeSlot> GetInnerUpgradeSlots()
        {
            return new List<IUpgradeSlot>();
        }        
    }
}
