using System;
using XWingSquadronBuilder_v4.BusinessLogic.Factories;
using XWingSquadronBuilder_v4.DataLayer.RawDataImporter;
using XWingSquadronBuilder_v4.Interfaces;

namespace XWingSquadronBuilder_v4.BusinessLogic.Repositories
{
    public class XWingRepository
    {
        public IPilotRepository PilotRepository { get; }
        public IActionRepository ActionRepository { get; }
        public IUpgradeRepository UpgradeRepository { get; }
        public IUpgradeTypesRepository UpgradeTypesRepository { get; }
        public IFactionRepository FactionRepository { get; }
        public IPilotFactory PilotFactory { get; }
        public IUpgradeFactory UpgradeFactory { get; }
        public IUpgradeSlotFactory UpgradeSlotFactory { get; }
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
            UpgradeFactory = new UpgradeFactory(FactionRepository, UpgradeTypesRepository);
            UpgradeSlotFactory = new UpgradeSlotFactory(UpgradeFactory);
            PilotFactory = new PilotFactory(UpgradeSlotFactory, FactionRepository, UpgradeTypesRepository);

            PilotRepository = new PilotRepository(PilotFactory);
            UpgradeRepository = new UpgradeRepository(UpgradeFactory, UpgradeSlotFactory,UpgradeTypesRepository);
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
