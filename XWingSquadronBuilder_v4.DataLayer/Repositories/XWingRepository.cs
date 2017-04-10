using System;
using XWingSquadronBuilder_v4.DataLayer.RawDataImporter;
using XWingSquadronBuilder_v4.Interfaces;

namespace XWingSquadronBuilder_v4.DataLayer.Repositories
{
    public class XWingRepository
    {
        public IPilotRepository PilotRepository { get; }
        public IActionRepository ActionRepository { get; }
        public IUpgradeRepository UpgradeRepository { get; }
        public IUpgradeTypesRepository UpgradeTypesRepository { get; }
        public IFactionRepository FactionRepository { get; }

        private static readonly XWingRepository instance = new XWingRepository();

        // Explicit static constructor to tell C# compiler
        // not to mark type as beforefieldinit
        static XWingRepository()
        {
        }

        private XWingRepository()
        {
            FactionRepository = new FactionRepository(DataParsers.CreateFaction);
            UpgradeTypesRepository = new UpgradeTypesRepository(DataParsers.CreateUpgradeType);
            ActionRepository = new ActionRepository(DataParsers.CreateAction);
            PilotRepository = new PilotRepository(DataParsers.CreatePilot);
            UpgradeRepository = new UpgradeRepository(DataParsers.CreateUpgrade);
        }

        public static XWingRepository Instance
        {
            get
            {
                return instance;
            }
        }
    }
}
