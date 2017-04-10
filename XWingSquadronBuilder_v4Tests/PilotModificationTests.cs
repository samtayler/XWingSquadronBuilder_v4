using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using XWingSquadronBuilder_v4.DataLayer.Repositories;

namespace XWingSquadronBuilderTests
{
    [TestClass]
    public class PilotModificationTests
    {
        //XWingRepository Repo { get; } = XWingRepository.Instance;

        //[TestMethod]
        //public void AddActionTest()
        //{
        //    var pilot = Repo.PilotRepository.GetPilots().Single(x => x.Name == "Rexlar Brath");              

        //    var addActionUpgrade = Repo.UpgradeRepository
        //        .GetAllUpgradesForType(Repo.UpgradeTypesRepository.GetUpgradeType("Modification"))
        //        .Single(x => x.Name == "Engine Upgrade");

        //    Assert.IsFalse(pilot.Actions.Contains(Repo.ActionRepository.GetAction("Boost")));
        //    var upgradeSlot = pilot.Upgrades.Where(x => x.Name == "Modification").ToList()[0];
        //    upgradeSlot.Upgrade = addActionUpgrade;

        //    Assert.IsTrue(pilot.Actions.Contains(Repo.ActionRepository.GetAction("Boost")));
        //}

        //[TestMethod]
        //public void AddUpgradeTest()
        //{
        //    //TODO : Set this to darth vader
        //    var pilot = Repo.PilotRepository.GetPilots().Single(x => x.Name == "Rexlar Brath");

        //    var addUpgradeUpgrade = Repo.UpgradeRepository
        //        .GetAllUpgradesForType(Repo.UpgradeTypesRepository.GetUpgradeType("Title"))
        //        .Single(x => x.Name == "TIE/x1");

        //    Assert.IsFalse(pilot.Upgrades.Count(x => x.UpgradeType.Equals(Repo.UpgradeTypesRepository.GetUpgradeType("System Upgrade"))) > 0);

        //    pilot.Upgrades.First(x => x.UpgradeType.Equals(Repo.UpgradeTypesRepository.GetUpgradeType("Title"))).Upgrade = addUpgradeUpgrade;

        //    Assert.IsTrue(pilot.Upgrades.Count(x => x.UpgradeType.Equals(Repo.UpgradeTypesRepository.GetUpgradeType("System Upgrade"))) > 0);
        //}

        //[TestMethod]
        //public void RemoveUpgradeTest()
        //{
        //    var pilot = Repo.PilotRepository.GetPilots().Single(x => x.Name == "Rexlar Brath");

        //    var removeUpgradeUpgrade = Repo.UpgradeRepository
        //        .GetAllUpgradesForType(Repo.UpgradeTypesRepository.GetUpgradeType("Title"))
        //        .Single(x => x.Name == "TIE/x7");

        //    Assert.IsTrue(pilot.Upgrades.Count(x => x.UpgradeType.Equals(Repo.UpgradeTypesRepository.GetUpgradeType("Cannon"))) > 0);
        //    Assert.IsTrue(pilot.Upgrades.Count(x => x.UpgradeType.Equals(Repo.UpgradeTypesRepository.GetUpgradeType("Missile"))) > 0);

        //    pilot.Upgrades.First(x => x.UpgradeType.Equals(Repo.UpgradeTypesRepository.GetUpgradeType("Title"))).Upgrade = removeUpgradeUpgrade;

        //    Assert.IsFalse(pilot.Upgrades.Count(x => x.UpgradeType.Equals(Repo.UpgradeTypesRepository.GetUpgradeType("Cannon"))) > 0);
        //    Assert.IsFalse(pilot.Upgrades.Count(x => x.UpgradeType.Equals(Repo.UpgradeTypesRepository.GetUpgradeType("Missile"))) > 0);
        //}

        //[TestMethod]
        //public void ChangePilotStatsTest()
        //{
        //    var pilot = Repo.PilotRepository.GetPilots().Single(x => x.Name == "Rexlar Brath");

        //    var statChangeUpgrade = Repo.UpgradeRepository
        //        .GetAllUpgradesForType(Repo.UpgradeTypesRepository.GetUpgradeType("Modification"))
        //        .Single(x => x.Name == "Hull Upgrade");

        //    Assert.AreEqual(3, pilot.Hull);

        //    pilot.Upgrades.First(x => x.UpgradeType.Equals(Repo.UpgradeTypesRepository.GetUpgradeType("Modification"))).Upgrade = statChangeUpgrade;

        //    Assert.AreEqual(4, pilot.Hull);
        //}
    }
}
