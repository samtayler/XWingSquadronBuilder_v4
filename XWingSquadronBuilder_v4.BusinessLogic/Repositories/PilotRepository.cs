using System;
using System.Collections.Generic;
using System.Linq;
using XWingSquadronBuilder_v4.BusinessLogic.Factories;
using XWingSquadronBuilder_v4.DataLayer.RawData;
using XWingSquadronBuilder_v4.DataLayer.RawDataImporter;
using XWingSquadronBuilder_v4.Interfaces;

namespace XWingSquadronBuilder_v4.BusinessLogic.Repositories
{
    internal class PilotRepository : IPilotRepository
    {
        private IReadOnlyList<PilotJson> pilots;
        private IPilotFactory pilotFactory;

        public PilotRepository(IPilotFactory pilotFactory)
        { 
            this.pilotFactory = pilotFactory;
            pilots = DataImporter.GetPilots().ToList().AsReadOnly();
        }

        public IReadOnlyList<IPilot> GetPilotsForFaction(IFaction faction) => pilots.Where(pilot => pilot.Faction == faction.Name).Select(pilotFactory.CreatePilot).ToList();

        public IReadOnlyList<IPilot> GetPilots() => pilots.Select(pilotFactory.CreatePilot).ToList();

    }
}
