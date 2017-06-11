using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XWingSquadronBuilder_v4.BusinessLogic.Extensions;
using XWingSquadronBuilder_v4.BusinessLogic.Models;
using XWingSquadronBuilder_v4.Interfaces;

namespace XWingSquadronBuilder_v4.BusinessLogic.Logic
{
    public class UpgradeFilter
    {
        private SortedSet<string> uniqueCardsInUse;
        private IPilot pilot;
        private IUpgradeSlot upgradeSlot;
        private List<Predicate<IUpgrade>> checkList = new List<Predicate<IUpgrade>>();


        public UpgradeFilter(SortedSet<string> uniqueCardsInUse, IPilot pilot, IUpgradeSlot upgradeSlot)
        {
            this.uniqueCardsInUse = uniqueCardsInUse;
            this.pilot = pilot;
            this.upgradeSlot = upgradeSlot;
            checkList = new List<Predicate<IUpgrade>>() {
                new Predicate<IUpgrade>((upgrade) => upgrade.Faction.Equals(pilot.Faction) || upgrade.Faction.Name == "Any"), // correct faction
                new Predicate<IUpgrade>((upgrade) => upgrade.Cost <= upgradeSlot.CostRestriction), // within cost restriction. slot with no cost restriction have a restriction set to 100 pts
                new Predicate<IUpgrade>((upgrade) => pilot.Actions.Any(action => action.Name.Contains(upgrade.ActionLimited))), // does pilot have the required action
                new Predicate<IUpgrade>((upgrade) => pilot.Ship.Contains(upgrade.ShipLimited)), // can the upgrade be applyed to this ship
                new Predicate<IUpgrade>((upgrade) => upgrade.Limited ? !pilot.Upgrades.Any(y => y.Upgrade.Equals(upgrade)) : true), // if upgrade is limited, make sure the upgrade is not already applied
                new Predicate<IUpgrade>((upgrade) => pilot.Upgrades.Where(uSlot => uSlot.UpgradeType.Equals(upgradeSlot.UpgradeType) &&  ((uSlot.Upgrade is NullUpgrade) || upgrade == upgradeSlot)).Count() >= upgrade.SlotsRequired), // make sure there are enough slots avaliable for the upgrade
                new Predicate<IUpgrade>((upgrade) => pilot.Upgrades.AreRemovalConditionsMet(upgrade)), // make sure if there are slots to be removed that they can be
                new Predicate<IUpgrade>((upgrade) => upgrade.SizeRestriction == string.Empty || upgrade.SizeRestriction.ToUpper() == pilot.ShipSize.ToString().ToUpper())}; // check size restrictions
        }

        public bool IsUpgradeApplicable(IUpgrade upgrade)
        {
            return checkList.All(upgradeCheck => upgradeCheck(upgrade)); // Run all checks       
        }
    }
}
