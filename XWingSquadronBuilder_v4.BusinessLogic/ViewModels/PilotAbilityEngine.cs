using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using XWingSquadronBuilder_v4.Interfaces;

namespace XWingSquadronBuilder_v4.BusinessLogic.ViewModels
{
    public class PilotAbilityEngine : INotifyPropertyChanged, IDisposable
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public int Agility => agility + GetPilotStatModification(nameof(Agility));
        public IEnumerable<IAction> Actions => CalculateActions();
        public int Attack => attack + GetPilotStatModification(nameof(Attack));
        public int Hull => hull + GetPilotStatModification(nameof(Hull));
        public int PilotSkill => pilotSkill + GetPilotStatModification(nameof(PilotSkill));
        public int Shield => shield + GetPilotStatModification(nameof(Shield));
        public int Cost => GetCalculatedUpgradeSlots().Sum(x => x.Upgrade.Cost);
        public IEnumerable<UpgradeSlotViewModel> Upgrades => GetCalculatedUpgradeSlots();
        private IReadOnlyList<IUpgradeSlot> _upgrades { get; }
        private List<IAction> _actions { get; }
        private int attack { get; }
        private int agility { get; }
        private int hull { get; }
        private int shield { get; }
        private int pilotSkill { get; }


        public PilotAbilityEngine(PilotStatPackage stats, IReadOnlyList<IAction> actions, List<IUpgradeSlot> upgrades)
        {
            this.attack = stats.Attack;
            this.agility = stats.Agility;
            this.hull = stats.Hull;
            this.shield = stats.Shield;
            this.pilotSkill = stats.PilotSkill;
            this._actions = actions.ToList();
            _upgrades = upgrades.ToList().AsReadOnly();
            foreach (var upgrade in _upgrades)
            {
                upgrade.PropertyChanged += UpgradeContainer_PropertyChanged;
            }
        }

        private void UpgradeContainer_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            NotifyPropertyChanged(nameof(Attack));
            NotifyPropertyChanged(nameof(Agility));
            NotifyPropertyChanged(nameof(Hull));
            NotifyPropertyChanged(nameof(Shield));
            NotifyPropertyChanged(nameof(Upgrades));
            NotifyPropertyChanged(nameof(Actions));
            NotifyPropertyChanged(nameof(PilotSkill));
            NotifyPropertyChanged(nameof(Cost));
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
                upgrade.Dispose();
            }
        }

        private IEnumerable<IAction> CalculateActions()
        {
            return ApplyActionAddFromUpgrades(ApplyActionModelRemoval(_actions, Upgrades), Upgrades);
        }

        private IEnumerable<UpgradeViewModel> GetCalculatedUpgradeSlots()
        {
            return FlattenUpgradeTree(ApplyUpgradeSlotRemoval(_upgrades));
        }

        public static IEnumerable<UpgradeViewModel> FlattenUpgradeTree(IEnumerable<UpgradeViewModel> upgrades)
        {
            return upgrades.Select(x => x.GetInnerUpgradeSlots().Concat(new[] { x })).Aggregate(new List<UpgradeViewModel>(), (x, y) => x.Concat(y).ToList());
        }

        public static IEnumerable<UpgradeViewModel> ApplyUpgradeSlotRemoval(IEnumerable<UpgradeViewModel> upgrades)
        {
            var upgradeTypesToRemove = FlattenUpgradeTree(upgrades)
                .Select(x => x.Upgrade.RemoveUpgradeModifiers)
                .Aggregate(new List<IUpgradeType>(), (x, y) => x.Concat(y).ToList());

            var finalUpgrades = new List<IUpgradeSlot>(upgrades);

            foreach (var item in upgradeTypesToRemove)
            {
                finalUpgrades.Remove(finalUpgrades.FirstOrDefault(x => x.UpgradeType.Equals(item)));
            }

            return finalUpgrades;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="actions">A Collection of actions</param>
        /// <param name="upgrades">A collection of upgrages without upgradeRemoval applied</param>
        /// <returns></returns>
        public static IEnumerable<IAction> ApplyActionAddFromUpgrades(IEnumerable<IAction> actions, IEnumerable<UpgradeViewModel> upgrades)
        {
            var finalActions = new List<IAction>(actions);
            var actionsToBeAdded = FlattenUpgradeTree(ApplyUpgradeSlotRemoval(upgrades))
               .Select(x => x.Upgrade.AddActionModifiers)
               .Aggregate(new List<IAction>(), (finalList, currentList) => finalList.Concat(currentList).ToList());

            finalActions.AddRange(actionsToBeAdded);

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
                .Select(x => x.Upgrade.RemoveActionModifiers)
                .Aggregate(new List<IAction>(), (finalList, currentList) => finalList.Concat(currentList).ToList());

            foreach (var item in actionsToBeRemoved)
            {
                finalActions.Remove(finalActions.FirstOrDefault(x => x.Equals(item)));
            }

            return finalActions;
        }

        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public struct PilotStatPackage
    {
        public PilotStatPackage(int attack, int agility, int hull, int shield, int pilotSkill)
        {
            Attack = attack;
            Agility = agility;
            Hull = hull;
            Shield = shield;
            PilotSkill = pilotSkill;
        }

        public int Attack { get; }
        public int Agility { get; }
        public int Hull { get; }
        public int Shield { get; }
        public int PilotSkill { get; }
    }
}
