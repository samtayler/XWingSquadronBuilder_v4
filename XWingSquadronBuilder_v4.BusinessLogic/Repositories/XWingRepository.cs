using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XWingSquadronBuilder_v4.BusinessLogic.Factories;
using XWingSquadronBuilder_v4.BusinessLogic.Models;
using XWingSquadronBuilder_v4.DataLayer.RawData;
using XWingSquadronBuilder_v4.DataLayer.Respositories;
using XWingSquadronBuilder_v4.Interfaces;

namespace XWingSquadronBuilder_v4.BusinessLogic.Repositories
{
    public class XWingRepository : IXWingRepository, IPersistanceRepository
    {
        private IDataPortal dataPortal { get; }
        private UpgradeFactory upgradeFactory { get; }
        private PilotFactory pilotFactory { get; }

        private Dictionary<Guid, ISquadron> savedSquadrons { get; }

        private XWingRepository(IDataPortal dataPortal)
        {
            this.dataPortal = dataPortal;
            this.upgradeFactory = new UpgradeFactory(this.GetFaction, this.GetUpgradeType, this.GetAction, this.GetUpgrade);
            this.pilotFactory = new PilotFactory(GetAction, new UpgradeSlotFactory(upgradeFactory), GetFaction, GetUpgradeType);
            savedSquadrons = new Dictionary<Guid, ISquadron>();
            LoadSquadrons();
        }

        public static async Task<IXWingRepository> CreateXWingRepository()
        {
            var dataPortal = await LocalDataPortal.CreateLocalDataPortal();
            return new XWingRepository(dataPortal);
        }

        public IReadOnlyList<IFaction> GetAllFactions()
        {
            var factions = dataPortal.InventoryRepository.FactionJson;
            return factions.Where(x => x.Name.ToLower() != "any").Select(x => Faction.CreateFaction(x.Name, x.Image)).ToList();
        }

        public IFaction GetFaction(string name)
        {
            var factions = dataPortal.InventoryRepository.FactionJson;
            var factionAnyData = factions.Single(x => x.Name.ToLower() == name.ToLower());
            return Faction.CreateFaction(factionAnyData.Name, factionAnyData.Image);
        }

        public IFaction FactionAny
        {
            get
            {
                var factions = dataPortal.InventoryRepository.FactionJson;
                var factionAnyData = factions.Single(x => x.Name.ToLower() == "any");
                return Faction.CreateFaction(factionAnyData.Name, factionAnyData.Image);
            }
        }

        public IReadOnlyList<IAction> GetAllActions()
        {
            var actions = dataPortal.InventoryRepository.ActionJson;
            return (from action in actions
                    select Models.Action.CreateAction(action.Name, action.ImageSource)).ToList().AsReadOnly();
        }

        public IAction GetAction(string name)
        {
            var actions = dataPortal.InventoryRepository.ActionJson;
            return (from action in actions
                    where action.Name == name
                    select Models.Action.CreateAction(action.Name, action.ImageSource)).Single();
        }

        public IReadOnlyList<IUpgrade> GetAllUpgrades()
        {
            return (dataPortal.InventoryRepository.UpgradeJson.Select(x =>
            upgradeFactory.CreateUpgrade(x))).ToList().AsReadOnly();
        }

        public IReadOnlyList<IUpgrade> GetAllUpgradesForType(IUpgradeType type)
        {
            return (dataPortal.InventoryRepository.UpgradeJson.Where(x => x.Type.Equals(type.Name)).Select(x =>
            upgradeFactory.CreateUpgrade(x))).ToList().AsReadOnly();
        }

        public IUpgrade GetUpgrade(string name, string upgradeType)
        {
            return upgradeFactory.CreateUpgrade(dataPortal.InventoryRepository.UpgradeJson.Single(x => x.Name == name && x.Type == upgradeType));
        }

        public IUpgrade GetNullUpgrade(IUpgradeType type)
        {
            return upgradeFactory.CreateNullUpgrade(type);
        }

        public IReadOnlyList<IUpgradeType> GetAllUpgradeTypes()
        {
            return (from upgradeType in dataPortal.InventoryRepository.UpgradeTypeJson
                    select UpgradeType.CreateUpgradeType(upgradeType.Name, upgradeType.ImageSource)).ToList();
        }

        public IUpgradeType GetUpgradeType(string name)
        {
            var upgradeType = dataPortal.InventoryRepository.UpgradeTypeJson.Single(x => x.Name == name);
            return UpgradeType.CreateUpgradeType(upgradeType.Name, upgradeType.ImageSource);

        }

        public IReadOnlyList<IPilot> GetPilotsForFaction(IFaction faction)
        {
            var pilots = dataPortal.InventoryRepository.PilotJson;
            return pilots.Where(pilot => pilot.Faction == faction.Name).Select(pilotFactory.CreatePilot).ToList();
        }

        public IPilot GetNullPilot()
        {
            return PilotFactory.CreateNullPilot();
        }

        public IReadOnlyList<IPilot> GetPilots()
        {
            var pilots = dataPortal.InventoryRepository.PilotJson;
            return pilots.Select(pilotFactory.CreatePilot).ToList();
        }

        public IPilot GetPilot(string Name, string ShipName)
        {
            var pilots = dataPortal.InventoryRepository.PilotJson;
            return pilotFactory.CreatePilot(pilots.Single(x => x.Name == Name && x.ShipName == ShipName));
        }

        public bool IsSquadronSaved(Guid id)
        {
            return savedSquadrons.ContainsKey(id);
        }

        public IReadOnlyList<ISquadron> GetSavedSquadronsForFaction(IFaction faction)
        {
            return savedSquadrons.Values.Where(x => x.Faction.Equals(faction)).ToList().AsReadOnly();
        }

        public IReadOnlyList<ISquadron> GetSavedSquadrons()
        {
            return savedSquadrons.Values.ToList().AsReadOnly();
        }

        public void SaveSquadron(ISquadron squadron)
        {
            if (!IsSquadronSaved(squadron.Id))
                savedSquadrons.Add(squadron.Id, squadron);
        }

        private ISquadron generateSquadronFromSaveData(SquadronSaveData saveData)
        {
            var squadron = SquadronFactory.CreateSquadron(GetFaction(saveData.Faction));

            foreach (var pilotData in saveData.Pilots)
            {
                var pilot = GetPilot(pilotData.Name, pilotData.ShipName);

                var upgradesToAdd = new List<IUpgrade>();

                foreach (var upgradeData in pilotData.Upgrades)
                {
                    upgradesToAdd.Add(GetUpgrade(upgradeData.Name, upgradeData.UpgradeType));
                }

                foreach (var upgrade in upgradesToAdd.OrderByDescending(x => x.AddUpgradeModifiers.Count()))
                {
                    pilot.Upgrades.Single(x => x.IsNullUpgrade && x.UpgradeType.Equals(upgrade.UpgradeType)).SetUpgrade(upgrade);
                }

                squadron.AddPilot(pilot);
            }

            squadron.SetId(saveData.Id);
            squadron.CostCap = saveData.CostCap;
            squadron.Name = saveData.Name;
            squadron.Description = saveData.Description;


            return squadron;
        }

        private void LoadSquadrons()
        {
            foreach (var squadronData in dataPortal.PersistanceRepository.SavedSquadrons)
            {
                var squadron = generateSquadronFromSaveData(squadronData);
                savedSquadrons.Add(squadron.Id, squadron);
            }
        }

        private SquadronSaveData CompileSaveData(ISquadron squadron)
        {
            SquadronSaveData squadronsave = new SquadronSaveData(squadron.Id, squadron.Name, squadron.Description, squadron.CostCap, squadron.Faction.Name);
            foreach (var pilot in squadron.Pilots.Values)
            {
                var pilotSave = new PilotSaveData(pilot.Name, pilot.ShipName);

                foreach (var upgradeSlot in pilot.Upgrades.Where(x => x.IsNotNullUpgrade))
                {
                    pilotSave.Upgrades.Add(new UpgradeSaveData(upgradeSlot.Upgrade.Name, upgradeSlot.Upgrade.UpgradeType.Name));
                }

                squadronsave.Pilots.Add(pilotSave);
            }

            return squadronsave;
        }

        public async Task SaveToStorageAsync()
        {
            await dataPortal.PersistanceRepository.SaveDataToDrive(savedSquadrons.Select(x => CompileSaveData(x.Value)));
        }
    }
}
