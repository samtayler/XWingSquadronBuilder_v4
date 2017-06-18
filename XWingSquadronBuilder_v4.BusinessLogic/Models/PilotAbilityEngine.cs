using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using XWingSquadronBuilder_v4.BusinessLogic.Structures;
using XWingSquadronBuilder_v4.Interfaces;

namespace XWingSquadronBuilder_v4.BusinessLogic.Models
{
    public class PilotAbilityEngine : INotifyPropertyChanged, IDisposable
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public int Agility => agility + GetPilotStatModification(nameof(Agility));
        public int Attack => attack + GetPilotStatModification(nameof(Attack));
        public int Hull => hull + GetPilotStatModification(nameof(Hull));
        public int PilotSkill => pilotSkill + GetPilotStatModification(nameof(PilotSkill));
        public int Shield => shield + GetPilotStatModification(nameof(Shield));
        public int Cost => GetCalculatedUpgradeSlots().Sum(x => x.Cost);
        private IReadOnlyList<IUpgradeSlot> _upgrades { get; }
        public List<IUpgradeSlot> Upgrades => GetCalculatedUpgradeSlots()
            .OrderBy(upgrade => upgrade.UpgradeType.Name).ThenBy(upgrade => upgrade.IsNullUpgrade).ToList();
        public List<IAction> Actions => CalculateActions().ToList();
        private HashSet<IAction> _actions { get; }
        private int attack { get; }
        private int agility { get; }
        private int hull { get; }
        private int shield { get; }
        private int pilotSkill { get; }


        public PilotAbilityEngine(PilotStatPackage stats, HashSet<IAction> actions, IEnumerable<IUpgradeSlot> upgrades)
        {
            this.attack = stats.Attack;
            this.agility = stats.Agility;
            this.hull = stats.Hull;
            this.shield = stats.Shield;
            this.pilotSkill = stats.PilotSkill;
            this._actions = actions;
            _upgrades = upgrades.ToList().AsReadOnly();
            foreach (var upgrade in _upgrades)
            {
                upgrade.PropertyChanged += UpgradeContainer_PropertyChanged;
            }           

        }

        private void UpgradeContainer_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Enabled") return;
            if (e.PropertyName == "Upgrade")
            {                
                NotifyPropertyChanged(nameof(Actions));
                NotifyPropertyChanged(nameof(Upgrades));
                NotifyPropertyChanged(nameof(Attack));
                NotifyPropertyChanged(nameof(Agility));
                NotifyPropertyChanged(nameof(Hull));
                NotifyPropertyChanged(nameof(Shield));
                NotifyPropertyChanged(nameof(PilotSkill));
                NotifyPropertyChanged(nameof(Cost));
            }
        }        

        public int GetPilotStatModification(string key)
        {
            return GetCalculatedUpgradeSlots().Select(x => GetPilotStatModFromContainer(x, key)).Sum();
        }

        private int GetPilotStatModFromContainer(IUpgradeSlot container, string key)
        {
            container.Upgrade.PilotAttributeModifiers.TryGetValue(key, out int pilotstat);
            return pilotstat;
        }

        public void Dispose()
        {
            foreach (var upgrade in _upgrades)
            {
                upgrade.PropertyChanged -= UpgradeContainer_PropertyChanged;
            }
        }

        private IEnumerable<IAction> CalculateActions()
        {
            return ApplyActionAddFromUpgrades(ApplyActionModelRemoval(_actions, Upgrades), Upgrades);
        }

        private IEnumerable<IUpgradeSlot> GetCalculatedUpgradeSlots()
        {
            return FlattenUpgradeTree(ApplyUpgradeSlotRemoval(_upgrades));
        }

        public static IEnumerable<IUpgradeSlot> FlattenUpgradeTree(IEnumerable<IUpgradeSlot> upgrades)
        {
            return upgrades.SelectMany(x => x.GetInnerUpgradeSlots().Concat(new[] { x }));
        }

        public static IEnumerable<IUpgradeSlot> ApplyUpgradeSlotRemoval(IEnumerable<IUpgradeSlot> upgrades)
        {
            var upgradeTypesToRemove = FlattenUpgradeTree(upgrades)
                .SelectMany(x => x.Upgrade.RemoveUpgradeModifiers);

            var finalUpgrades = new List<IUpgradeSlot>(upgrades);

            foreach (var item in upgradeTypesToRemove)
            {
                foreach (var upgrade in finalUpgrades.Where(x => x.UpgradeType.Equals(item)))
                {
                    if (upgrade.Upgrade.CardText == string.Empty)
                    {
                        finalUpgrades.Remove(upgrade);
                        break;
                    }
                }
            }

            return finalUpgrades;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="actions">A Collection of actions</param>
        /// <param name="upgrades">A collection of upgrages without upgradeRemoval applied</param>
        /// <returns></returns>
        public static IEnumerable<IAction> ApplyActionAddFromUpgrades(IEnumerable<IAction> actions, IEnumerable<IUpgradeSlot> upgrades)
        {
            var finalActions = new HashSet<IAction>(actions);
            var actionsToBeAdded = FlattenUpgradeTree(ApplyUpgradeSlotRemoval(upgrades))
               .SelectMany(x => x.Upgrade.AddActionModifiers);

            foreach (var action in actionsToBeAdded)
            {
                finalActions.Add(action);
            }

            return finalActions;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="actions">A Collection of actions</param>
        /// <param name="upgrades">A collection of upgrages without upgradeRemoval applied</param>
        /// <returns></returns>
        public static IEnumerable<IAction> ApplyActionModelRemoval(IEnumerable<IAction> actions, IEnumerable<IUpgradeSlot> upgrades)
        {
            var finalActions = new List<IAction>(actions);
            var actionsToBeRemoved = FlattenUpgradeTree(ApplyUpgradeSlotRemoval(upgrades))
                .SelectMany(x => x.Upgrade.RemoveActionModifiers);

            foreach (var item in actionsToBeRemoved)
            {
                finalActions.Remove(finalActions.FirstOrDefault(x => x.Equals(item)));
            }

            return finalActions;
        }

        /// <summary>
        /// Creates an exact clone
        /// </summary>
        /// <returns></returns>
        public PilotAbilityEngine DeepClone()
        {
            return new PilotAbilityEngine(
                new PilotStatPackage(this.attack, this.agility, this.hull,
                this.shield, this.pilotSkill),
                new HashSet<IAction>(this._actions.Select(x => x.DeepClone())),
                this._upgrades.Select(x => x.DeepClone()));
        }

        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
