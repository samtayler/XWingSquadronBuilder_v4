using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template10.Mvvm;
using Windows.UI.Xaml;
using XWingSquadronBuilder_v4.BusinessLogic.Repositories;
using XWingSquadronBuilder_v4.Interfaces;

namespace XWingSquadronBuilder_v4.Presentation.ViewModels
{
    public class UpgradeSelectorViewModel : BindableBase
    {
        private IUpgradeSlot upgradeSlot;
        private IReadOnlyList<string> uniqueNames;
        private IPilot pilot;
        private IUpgrade oldUpgrade;
        private IReadOnlyList<UpgradeViewModel> upgrades;

        public IReadOnlyList<UpgradeViewModel> Upgrades
        {
            get { return upgrades; }
            set { Set(ref upgrades, value); }
        }

        private Visibility visibility;

        public Visibility Visibility
        {
            get { return visibility; }
            private set { Set(ref visibility, value); }
        }

        public UpgradeSelectorViewModel()
        {
            Upgrades = new List<UpgradeViewModel>();
            Visibility = Visibility.Collapsed;
        }

        public void Show(IUpgradeSlot upgradeSlot, IReadOnlyList<string> uniqueNames, IPilot pilot)
        {
            this.upgradeSlot = upgradeSlot;
            this.uniqueNames = uniqueNames;
            this.pilot = pilot;
            this.oldUpgrade = upgradeSlot.Upgrade;
            upgradeSlot.ClearUpgrade();
            FilterUpgrades();
            Visibility = Visibility.Visible;            
        }

        private void FilterUpgrades()
        {
            List<UpgradeViewModel> upgrades = new List<UpgradeViewModel>();
            foreach (var upgrade in XWingRepository.Instance.UpgradeRepository.GetAllUpgradesForType(upgradeSlot.UpgradeType).Where(x => x.Faction.Equals(pilot.Faction) || 
            x.Faction.Equals(XWingRepository.Instance.FactionRepository.GetFactionAny())))
            {
                List<string> errors = new List<string>();                
                bool validationResult = true;
                foreach (var restriction in upgrade.UpgradeRestrictions)
                {
                    var result = restriction.IsSatisfiedBy(pilot, errors);
                    if (!result && restriction.SpecType == SpecificationType.Critical) validationResult &= result;                   
                }

                if (!validationResult) continue;

                if(uniqueNames.Contains(upgrade.Name))
                {
                    errors.Add($"{upgrade.Name} is a unique character and cannot be used more that once.");
                }

                upgradeSlot.RestrictionList.All(x => x.IsSatisfiedBy(upgrade, errors));
              
                upgrades.Add(new UpgradeViewModel(upgrade, errors));
            }

            Upgrades = upgrades.OrderBy(x => x.Upgrade.Cost).ThenBy(x => x.Upgrade.Name).ToList();
        }

        public void Hide()
        {
            Visibility = Visibility.Collapsed;
            Upgrades = new List<UpgradeViewModel>();
        }

        public void SelectUpgrade(UpgradeViewModel upgrade)
        {           
            upgradeSlot.SetUpgrade(upgrade.Upgrade);            
            Hide();
        }

        public void Cancel()
        {
            upgradeSlot.SetUpgrade(oldUpgrade);
            Hide();
        }
    }
}
