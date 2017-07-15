using System;
using System.Collections.Generic;
using System.Linq;
using XWingSquadronBuilder_v4.BusinessLogic.Factories;
using XWingSquadronBuilder_v4.DataLayer.RawData;
using XWingSquadronBuilder_v4.DataLayer.RawDataImporter;
using XWingSquadronBuilder_v4.Interfaces;

namespace XWingSquadronBuilder_v4.BusinessLogic.Repositories
{
    internal class UpgradeRepository : IUpgradeRepository
    {        
        private readonly IUpgradeFactory upgradeFactory;
        private readonly IUpgradeSlotFactory upgradeSlotFactory;
        private readonly IUpgradeTypesRepository typeRepo;

        public UpgradeRepository(IUpgradeFactory upgradeFactory, IUpgradeSlotFactory upgradeSlotFactory, IUpgradeTypesRepository typeRepo)
        {
            this.upgradeFactory = upgradeFactory;
            this.upgradeSlotFactory = upgradeSlotFactory;
            this.typeRepo = typeRepo;
            upgrades = DataImporter.LoadUpgrades();
        }

        private IReadOnlyList<UpgradeJson> upgrades { get; }

        public IReadOnlyList<IUpgrade> GetAllUpgrades()
            => (upgrades.Select(x => upgradeFactory.CreateUpgrade(x, new UpgradeModifierParser(upgradeSlotFactory, typeRepo,this)))).ToList().AsReadOnly();

        public IReadOnlyList<IUpgrade> GetAllUpgradesForType(IUpgradeType type)
             => (upgrades.Where(x => x.Type.Equals(type.Name)).Select(x => upgradeFactory.CreateUpgrade(x, new UpgradeModifierParser(upgradeSlotFactory, typeRepo, this)))).ToList().AsReadOnly();
    }
}

