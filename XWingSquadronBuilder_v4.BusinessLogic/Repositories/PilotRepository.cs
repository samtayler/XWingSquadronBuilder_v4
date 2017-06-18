using System;
using System.Collections.Generic;
using System.Linq;
using XWingSquadronBuilder_v4.DataLayer.RawData;
using XWingSquadronBuilder_v4.DataLayer.RawDataImporter;
using XWingSquadronBuilder_v4.Interfaces;

namespace XWingSquadronBuilder_v4.BusinessLogic.Repositories
{
    internal class PilotRepository : IPilotRepository
    {
        private IReadOnlyList<PilotJson> pilots { get; }           
        private Func<PilotJson, IPilot> CreatePilot { get; }

        public PilotRepository(Func<PilotJson, IPilot> createPilot)
        { 
            CreatePilot = createPilot;
            pilots = DataImporter.GetPilots().ToList().AsReadOnly();
        }

        public List<IPilot> GetPilotsForFaction(IFaction faction) => pilots.Where(pilot => pilot.Faction == faction.Name).Select(CreatePilot).ToList();

        public List<IPilot> GetPilots() => pilots.Select(CreatePilot).ToList();

    }
}
