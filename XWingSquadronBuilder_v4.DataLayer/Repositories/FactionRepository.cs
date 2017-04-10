using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XWingSquadronBuilder_v4.DataLayer.RawData;
using XWingSquadronBuilder_v4.DataLayer.RawDataImporter;
using XWingSquadronBuilder_v4.Interfaces;

namespace XWingSquadronBuilder_v4.DataLayer.Repositories
{
   
    public class FactionRepository : IFactionRepository
    {
        private FactionJson[] factions { get; }
        private Func<FactionJson, IFaction> CreateFaction { get; }

        public FactionRepository(Func<FactionJson, IFaction> createFaction)
        {
            CreateFaction = createFaction;
            factions = DataImporter.LoadFactions();
        }

        public List<IFaction> GetAllFactions()
        {
            return factions.Select(x => CreateFaction(x)).ToList();
        }

        public IFaction GetFaction(string name)
        {
            return CreateFaction(factions.Single(x => x.Name.ToLower() == name.ToLower()));
        }
    }
}
