using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XWingSquadronBuilderClasses.Interfaces;

namespace XWingSquadronBuilder.BusinessLogic.Models
{
    public class UpgradeSelector
    {
        public IEnumerable<IUpgrade> Upgrades => GetUpgrades();
        public IPilot Pilot { get; }
        private int SlotsAvaiable { get; }
        private IUpgradeSlot Slot { get; }
        private IEnumerable<IUpgrade> UniquesTaken { get; }

        public UpgradeSelector(IPilot pilot, IUpgradeSlot slot, IEnumerable<IUpgrade> uniquesTaken)
        {
            Pilot = pilot;
            Slot = slot;
            UniquesTaken = uniquesTaken;
            SlotsAvaiable = pilot.Upgrades.Where(x => x.Upgrade.Name == "" && x.UpgradeType.Equals(slot.UpgradeType)).Count();
        }

        public void SetUpgrade(IUpgrade upgrade)
        {
            Slot.Upgrade = upgrade;
        }

        private IEnumerable<IUpgrade> GetUpgrades()
        {
            var upgrades = XWingRepository.Instance.UpgradeRepository.GetAllUpgradesForType(Slot.UpgradeType)                
                .Where(x => x.Cost <= Slot.CostRestriction &&
                Pilot.Ship.Contains(x.ShipLimited) &&
                (x.SizeRestriction == Pilot.ShipSize.ToString() || string.IsNullOrEmpty(x.SizeRestriction)) &&
                x.SlotsRequired <= SlotsAvaiable &&
                (x.Faction.Equals(Pilot.Faction) || x.Faction.Name == "Any")).Except(UniquesTaken);

            return upgrades;
        }

    }
}
