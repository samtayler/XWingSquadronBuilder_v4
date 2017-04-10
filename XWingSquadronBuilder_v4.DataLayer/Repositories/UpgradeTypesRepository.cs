using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using XWingSquadronBuilder_v4.DataLayer.RawData;
using XWingSquadronBuilder_v4.DataLayer.RawDataImporter;
using XWingSquadronBuilder_v4.Interfaces;

namespace XWingSquadronBuilder_v4.DataLayer.Repositories
{
  

    public class UpgradeTypesRepository : IUpgradeTypesRepository
    {
        private IEnumerable<UpgradeTypeJson> upgradeTypes { get; }
        private Func<UpgradeTypeJson, IUpgradeType> CreateUpgradeType { get; }


        public UpgradeTypesRepository(Func<UpgradeTypeJson, IUpgradeType> createUpgradeType)
        {
            CreateUpgradeType = createUpgradeType;
            upgradeTypes = DataImporter.LoadUpgradesTypes();
        }

        public List<IUpgradeType> GetAllUpgradeTypes()
        {
            return (from upgradeType in upgradeTypes
                    select CreateUpgradeType(upgradeType)).ToList();
        }

        public IUpgradeType GetUpgradeType(string name)
        {
            return CreateUpgradeType(upgradeTypes.Single(x => x.Name == name));
                   
        }
    }
}
