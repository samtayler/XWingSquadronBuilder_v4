using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.Storage;
using XWingSquadronBuilder_v4.DataLayer.RawData;
using XWingSquadronBuilder_v4.DataLayer.Respositories;

namespace XWingSquadronBuilder_v4.DataLayer.RawDataImporter
{
    internal class JsonDataImporter : IInventoryRepository
    {
        private JsonDataImporter() { }

        public ActionJson[] ActionJson { get; private set; }
        public PilotJson[] PilotJson { get; private set; }
        public UpgradeJson[] UpgradeJson { get; private set; }
        public UpgradeTypeJson[] UpgradeTypeJson { get; private set; }
        public FactionJson[] FactionJson { get; private set; }

        public static async Task<IInventoryRepository> CreateJsonDataImporter()
        {
            var inventoryRepo = new JsonDataImporter();
            await inventoryRepo.loadAllDataAsync();
            return inventoryRepo;
        }

        private Task loadAllDataAsync()
        {
            return Task.WhenAll(loadActionDataAsync(),
                loadFactionDataAsync(),
                loadPilotDataAsync(),
                loadUpgradeDataAsync(),
                loadUpgradeTypeDataAsync());
        }

        private async Task loadActionDataAsync()
        {
            var root = await GetDataFromJson<ActionsRootobject>("Actions");
            ActionJson = root.Actions;
        }

        private async Task loadPilotDataAsync()
        {
            var root = await GetDataFromJson<PilotsJsonRoot>("Pilots");
            PilotJson = root.Pilots;
        }

        private async Task loadUpgradeDataAsync()
        {
            var root = await GetDataFromJson<UpgradesRootJson>("Upgrades");
            UpgradeJson = root.Upgrades;
        }

        private async Task loadFactionDataAsync()
        {
            var root = await GetDataFromJson<FactionJsonRoot>("Factions");
            FactionJson = root.Factions;
        }

        private async Task loadUpgradeTypeDataAsync()
        {
            var root = await GetDataFromJson<UpgradeTypesRootobject>("UpgradeTypes");
            UpgradeTypeJson = root.UpgradeTypes;
        }

        private static async Task<T> GetDataFromJson<T>(string dataname)
        {
            var storageFolder = Package.Current.InstalledLocation;

            StorageFile file = await storageFolder.GetFileAsync($"XWingSquadronBuilder_v4.DataLayer\\Json\\{dataname}.json");

            using (var inputStream = await file.OpenReadAsync())
            using (var stream = inputStream.AsStreamForRead())
            {
                DataContractJsonSerializer mySerializer = new DataContractJsonSerializer(typeof(T));
                var root = (T)mySerializer.ReadObject(stream);

                return root;
            }
        }

    }
}

