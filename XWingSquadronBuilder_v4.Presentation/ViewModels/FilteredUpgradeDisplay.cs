using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Template10.Mvvm;
using Windows.UI.Xaml;
using XWingSquadronBuilder_v4.BusinessLogic.Models.NullModels;
using XWingSquadronBuilder_v4.BusinessLogic.Repositories;
using XWingSquadronBuilder_v4.Interfaces;
using XWingSquadronBuilder_v4.Presentation.ViewModels.XWingModels.Interfaces;

namespace XWingSquadronBuilder_v4.Presentation.ViewModels
{
    public class UpgradeSearchResults : FilteredUpgradeDisplay
    {
        public UpgradeSearchResults(IPilotViewModel pilot, IXWingRepository xWingRepository) : base(new NullUpgradeType(), pilot, xWingRepository) { }
      

        public override string Name => "Search Results";

        public override string Image => "";

        public override IReadOnlyList<IUpgrade> Upgrades
        {
            get
            {
                List<IUpgrade> upgrades = new List<IUpgrade>();
                var allUpgrades = xWingRepository.GetAllUpgrades()
                    .Where(upgrade => (upgrade.Faction.Equals(PilotViewModel.Pilot.Faction) 
                    || upgrade.Faction.Equals(xWingRepository.FactionAny))
                    && PilotViewModel.Upgrades.Any(upgradeSlotViewModel 
                    => upgradeSlotViewModel.UpgradeSlot.UpgradeType.Equals(upgrade.UpgradeType)));

                var queryMatches = allUpgrades.Where(x => x.Name.ToLower().Contains(SearchQuery.ToLower()));

                foreach (var upgrade in queryMatches)
                {
                    List<string> errors = new List<string>();
                    bool validationResult = true;
                    foreach (var restriction in upgrade.UpgradeRestrictions)
                    {
                        var result = restriction.IsSatisfiedBy(PilotViewModel.Pilot, errors);
                        if (!result && restriction.SpecType == SpecificationType.Critical) validationResult &= result;
                    }

                    if (!validationResult) continue;

                    //if (uniqueNames.Contains(upgrade.Name))
                    //{
                    //    errors.Add($"{upgrade.Name} is a unique character and cannot be used more that once.");
                    //}

                    if (PilotViewModel
                        .Upgrades
                        .Where(x => x.UpgradeSlot.UpgradeType.Equals(upgrade.UpgradeType))
                        .Any(x => x.UpgradeSlot.RestrictionList.All(y => y.IsSatisfiedBy(upgrade, errors))))
                        upgrades.Add(upgrade);
                }

                return upgrades.OrderBy(x => x.Cost).ThenBy(x => x.Name).ToList();

            }
        }

        private string searchQuery;

        public string SearchQuery
        {
            get { return this.searchQuery; }
            set
            {
                Set(ref searchQuery, value);
                RaisePropertyChanged(nameof(Upgrades));
            }
        }


        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }

        public override string ToString()
        {
            return Name;
        }
    }


    public class FilteredUpgradeDisplay : BindableBase, IEquatable<FilteredUpgradeDisplay>, IComparable<FilteredUpgradeDisplay>
    {
        public IUpgradeType UpgradeType { get; }
        protected IPilotViewModel PilotViewModel { get; }
        protected IXWingRepository xWingRepository { get; }

        public FilteredUpgradeDisplay(IUpgradeType upgradeType, IPilotViewModel pilot, IXWingRepository xWingRepository)
        {
            this.UpgradeType = upgradeType;
            this.PilotViewModel = pilot;
            this.xWingRepository = xWingRepository;
        }

        public virtual string Name => UpgradeType.Name;

        public virtual string Image => UpgradeType.ImageUri;

        public virtual IReadOnlyList<IUpgrade> Upgrades
        {
            get
            {
                List<IUpgrade> upgrades = new List<IUpgrade>();
                foreach (var upgrade in xWingRepository.GetAllUpgradesForType(UpgradeType).Where(x => x.Faction.Equals(PilotViewModel.Pilot.Faction) ||
                x.Faction.Equals(xWingRepository.FactionAny)))
                {
                    List<string> errors = new List<string>();
                    bool validationResult = true;
                    foreach (var restriction in upgrade.UpgradeRestrictions)
                    {
                        var result = restriction.IsSatisfiedBy(PilotViewModel.Pilot, errors);
                        if (!result && restriction.SpecType == SpecificationType.Critical) validationResult &= result;
                    }

                    if (!validationResult) continue;

                    //if (uniqueNames.Contains(upgrade.Name))
                    //{
                    //    errors.Add($"{upgrade.Name} is a unique character and cannot be used more that once.");
                    //}

                    if (PilotViewModel
                        .Upgrades
                        .Where(x => x.UpgradeSlot.UpgradeType.Equals(upgrade.UpgradeType))
                        .Any(x => x.UpgradeSlot.RestrictionList.All(y => y.IsSatisfiedBy(upgrade, errors))))
                        upgrades.Add(upgrade);
                }

                return upgrades.OrderBy(x => x.Cost).ThenBy(x => x.Name).ToList();
            }
        }

        public int CompareTo(FilteredUpgradeDisplay other)
        {
            return this.Name.CompareTo(other.Name);
        }

        public bool Equals(FilteredUpgradeDisplay other)
        {
            return this.Name.Equals(other.Name);
        }

        public override int GetHashCode()
        {
            return UpgradeType.GetHashCode();
        }
    }
}
