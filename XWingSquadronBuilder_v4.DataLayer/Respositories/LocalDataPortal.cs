using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XWingSquadronBuilder_v4.DataLayer.RawDataImporter;
using XWingSquadronBuilder_v4.DataLayer.SaveToJson;

namespace XWingSquadronBuilder_v4.DataLayer.Respositories
{
    public class LocalDataPortal : IDataPortal
    {
        private LocalDataPortal(IInventoryRepository inventoryRepository, IPersistanceRepository persistanceRepository)
        {
            InventoryRepository = inventoryRepository;
            PersistanceRepository = persistanceRepository;
        }

        public IInventoryRepository InventoryRepository { get; }
        public IPersistanceRepository PersistanceRepository { get; }

        public static async Task<IDataPortal> CreateLocalDataPortal()
        {
            var jsonImporter = JsonDataImporter.CreateJsonDataImporter();
            var jsonSaver = JsonSaver.CreateJsonSaver();

            await Task.WhenAll(jsonImporter, jsonSaver);

            return new LocalDataPortal(jsonImporter.Result, jsonSaver.Result);
        }

    }
}
