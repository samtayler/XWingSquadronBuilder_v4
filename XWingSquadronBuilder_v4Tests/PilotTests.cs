using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XWingSquadronBuilder_v4.BusinessLogic.Repositories;

namespace XWingSquadronBuilder_v4.Tests
{
    [TestClass]
    public class PilotTests
    {
        XWingRepository Repo { get; } = XWingRepository.Instance;

        [TestMethod]
        public void PilotDeepCloneTest()
        {
            var pilot = Repo.PilotRepository.GetPilots().Single(x => x.Name == "Rexlar Brath");

            var addActionUpgrade = Repo.UpgradeRepository
                .GetAllUpgradesForType(Repo.UpgradeTypesRepository.GetUpgradeType("Modification"))
                .Single(x => x.Name == "Engine Upgrade");

            var addUpgradeUpgrade = Repo.UpgradeRepository
                .GetAllUpgradesForType(Repo.UpgradeTypesRepository.GetUpgradeType("Title"))
                .Single(x => x.Name == "TIE/x7");

            pilot.Upgrades.First(x => x.UpgradeType.Equals(Repo.UpgradeTypesRepository.GetUpgradeType("Title"))).Upgrade = addUpgradeUpgrade;
            var upgradeSlot = pilot.Upgrades.Where(x => x.UpgradeType.Name == "Modification").First();
            upgradeSlot.Upgrade = addActionUpgrade;

            var clonedPilot = pilot.DeepClone();

            Assert.IsTrue(pilot.Equals(clonedPilot));



        }
    }
}
