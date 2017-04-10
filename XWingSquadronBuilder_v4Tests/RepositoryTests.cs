using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using System.Linq;
using XWingSquadronBuilder_v4.DataLayer.RawDataImporter;
using XWingSquadronBuilder_v4.DataLayer.Repositories;

namespace XWingSquadronBuilderTests
{
    [TestClass]
    public class RepositoryTests
    {
        private XWingRepository repo = XWingRepository.Instance;

        [TestMethod]
        public void ActionParsingCountTest()
        {
            var actionsRaw = DataImporter.LoadActions();

            var actionsConverted = repo.ActionRepository.GetAllActions();

            Assert.AreEqual(actionsConverted.Count(), actionsRaw.Count());
        }

        [TestMethod]
        public void UpgradeTypesParsingCountTest()
        {
            var upgradeTypesRaw = DataImporter.LoadUpgradesTypes();

            var upgradeTypesConverted = repo.UpgradeTypesRepository.GetAllUpgradeTypes();

            Assert.AreEqual(upgradeTypesConverted.Count(), upgradeTypesRaw.Count());

        }

        [TestMethod]
        public void UpgradeParsingCountTest()
        {
            var upgradeRaw = DataImporter.LoadUpgrades();

            var upgradeConverted = repo.UpgradeRepository.GetAllUpgrades();

            Assert.AreEqual(upgradeConverted.Count(), upgradeRaw.Count());
        }

        [TestMethod]
        public void ShipParsingCountTest()
        {
            var pilotsRaw = DataImporter.GetPilots();

            var pilotsConverted = repo.PilotRepository.GetPilots();

            Assert.AreEqual(pilotsConverted.Count(), pilotsRaw.Count());

        }

        [TestMethod]
        public void ActionParsingContentTest()
        {
            var actionsRaw = DataImporter.LoadActions();

            var actionsConverted = repo.ActionRepository.GetAllActions();

            var items = from actionRaw in actionsRaw
                        join actionConverted in actionsConverted on
                        actionRaw.Name equals actionConverted.Name
                        where actionRaw.ImageSource == actionConverted.Image
                        select new { Name = actionConverted.Name, Image = actionConverted.Image };

            Assert.AreEqual(items.Count(), actionsRaw.Count());
        }

        [TestMethod]
        public void UpgradeTypeParsingIntegrityTest()
        {
            var typesRaw = DataImporter.LoadUpgradesTypes();

            var typesConverted = repo.UpgradeTypesRepository.GetAllUpgradeTypes();

            var items = from typeRaw in typesRaw
                        join typeConverted in typesConverted on
                        typeRaw.Name equals typeConverted.ToString()
                        select new { Name = typeRaw.Name };

            Assert.AreEqual(items.Count(), typesRaw.Count());
        }

        [TestMethod]
        public void UpgradeParsingIntegrityTest()
        {
            var upgradesRaw = DataImporter.LoadUpgrades();

            var upgradesConverted = repo.UpgradeRepository.GetAllUpgrades();

            var items = from upgradeRaw in upgradesRaw
                        join upgradeConverted in upgradesConverted on
                        upgradeRaw.Name equals upgradeConverted.Name
                        where upgradeRaw.Faction == upgradeConverted.Faction.ToString() &&
                        upgradeRaw.Cost == upgradeConverted.Cost &&
                        upgradeRaw.Description == upgradeConverted.CardText &&
                        upgradeRaw.Type == upgradeConverted.UpgradeType.ToString() &&
                        upgradeRaw.Unique == upgradeConverted.Unique &&
                        upgradeRaw.Limited == upgradeConverted.Limited &&
                        upgradeRaw.ShipLimited == upgradeConverted.ShipLimited &&
                        upgradeRaw.RemovedActions.Count() == upgradeConverted.RemoveActionModifiers.Count() &&
                        upgradeRaw.AddedActions.Count() == upgradeConverted.AddActionModifiers.Count() &&
                        upgradeRaw.AddedUpgrades.Count() == upgradeConverted.AddUpgradeModifiers.Count() &&
                        upgradeRaw.SizeRestriction == upgradeConverted.SizeRestriction &&
                        upgradeRaw.SlotsRequired == upgradeConverted.SlotsRequired &&
                        upgradeRaw.StatChanges.Count() == upgradeConverted.PilotAttributeModifiers.Count()
                        select new { Name = upgradeRaw.Name };

            var invalidItems = upgradesConverted.Where(x => !items.Any(y => y.Name == x.Name));


            foreach (var invalidItem in invalidItems)
            {
                Debug.WriteLine(invalidItem.Name);
            }
            Assert.AreEqual(upgradesRaw.Count(), items.Count());
            Assert.AreEqual(invalidItems.Count(), 0);
        }

        [TestMethod]
        public void ShipParsingIntegrityTest()
        {
            var pilotsRaw = DataImporter.GetPilots();

            var pilotsConverted = repo.PilotRepository.GetPilots();

            Assert.AreEqual(pilotsRaw.Count(), pilotsConverted.Count());

            var invalidPilots = from pilotR in pilotsRaw
                                join pilotC in pilotsConverted on
                                pilotR.Name equals pilotC.Name
                                where pilotC.Ship != pilotR.ShipName ||
                                pilotC.Attack != pilotR.Stats.Attack ||
                                pilotC.Agility != pilotR.Stats.Aglilty ||
                                pilotC.Hull != pilotR.Stats.Hull ||
                                pilotC.Shield != pilotR.Stats.Shield ||
                                pilotC.PilotAbility != pilotR.PilotAbility ||
                                pilotC.PilotSkill != pilotR.PilotSkill ||
                                pilotC.ShipSize.ToString() != pilotR.ShipSize ||
                                pilotC.Unique != pilotR.Unique ||
                                pilotC.Actions?.Count() != pilotR.Actions.Count() ||
                                pilotC.Upgrades?.Count() != pilotR.Upgrades.Count() ||
                                pilotC.Cost != pilotR.Cost
                                select new { PilotName = pilotC.Name, ShipName = pilotC.Ship };

            foreach (var item in invalidPilots)
            {
                Debug.WriteLine(item.PilotName + " " + item.ShipName);
            }

            Assert.IsFalse(invalidPilots.Any());
        }
    }
}
