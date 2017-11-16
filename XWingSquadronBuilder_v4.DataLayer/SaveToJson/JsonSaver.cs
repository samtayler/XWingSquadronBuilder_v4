using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;
using Windows.Storage;
using XWingSquadronBuilder_v4.DataLayer.RawData;
using XWingSquadronBuilder_v4.DataLayer.Respositories;

namespace XWingSquadronBuilder_v4.DataLayer.SaveToJson
{
    internal class JsonSaver : IPersistanceRepository
    {
        private const string squadronSaveName = "SquadronSaveData.json";

        private List<SquadronSaveData> savedSquadrons;

        public List<SquadronSaveData> SavedSquadrons
        {
            get
            {
                return savedSquadrons;
            }
        }

        private async Task LoadSavedSquadronsAsync()
        {
            StorageFolder localFolder = ApplicationData.Current.LocalFolder;

            try
            {
                using (var textStream = await localFolder.OpenStreamForReadAsync(squadronSaveName))
                {
                    DataContractJsonSerializer mySerializer = new DataContractJsonSerializer(typeof(RootSaveData));
                    var root = mySerializer.ReadObject(textStream) as RootSaveData;
                    savedSquadrons = root.Squadrons.ToList();
                }
            }
            catch(Exception e)
            {
                savedSquadrons = new List<SquadronSaveData>();
            }
        }

        public void SaveSquadron(SquadronSaveData squadron)
        {
            savedSquadrons.Add(squadron);
        }        

        public async Task SaveDataToDrive(IEnumerable<SquadronSaveData> saveData)
        {
            RootSaveData root = new RootSaveData();

            root.Squadrons = saveData.ToArray();

            StorageFolder localFolder = ApplicationData.Current.LocalFolder;

            var savingFile = await localFolder.CreateFileAsync(squadronSaveName, CreationCollisionOption.ReplaceExisting);

            using (var textStream = await savingFile.OpenStreamForWriteAsync())
            {
                DataContractJsonSerializer mySerializer = new DataContractJsonSerializer(typeof(RootSaveData));
                mySerializer.WriteObject(textStream, root);
            }
        }

        public static async Task<IPersistanceRepository> CreateJsonSaver()
        {
            var persistanceRepo = new JsonSaver();
            await persistanceRepo.LoadSavedSquadronsAsync();
            return persistanceRepo;
        }

    }
}
