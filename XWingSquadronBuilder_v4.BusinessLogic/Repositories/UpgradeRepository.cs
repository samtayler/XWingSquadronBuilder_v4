using System;
using System.Collections.Generic;
using System.Linq;
using XWingSquadronBuilder_v4.DataLayer.RawData;
using XWingSquadronBuilder_v4.DataLayer.RawDataImporter;
using XWingSquadronBuilder_v4.Interfaces;

namespace XWingSquadronBuilder_v4.BusinessLogic.Repositories
{
    public class UpgradeRepository : IUpgradeRepository
    {
        private Func<UpgradeJson, IUpgrade> CreateUpgrade { get; }
        public UpgradeRepository(Func<UpgradeJson, IUpgrade> createUpgrade)
        {
            CreateUpgrade = createUpgrade;
            upgrades = DataImporter.LoadUpgrades();
        }

        private IReadOnlyList<UpgradeJson> upgrades { get; }

        public IReadOnlyList<IUpgrade> GetAllUpgrades()
            => (from upgrade in upgrades select CreateUpgrade(upgrade)).ToList().AsReadOnly();

        public IReadOnlyList<IUpgrade> GetAllUpgradesForType(IUpgradeType type)
             => (from upgrade in upgrades where upgrade.Type == type.Name select CreateUpgrade(upgrade)).ToList().AsReadOnly();
    }
}

