using XWingSquadronBuilder_v4.DataLayer.RawData;
using XWingSquadronBuilder_v4.Interfaces;

namespace XWingSquadronBuilder_v4.BusinessLogic.Factories
{
    public interface IPilotFactory
    {
        IPilot CreatePilot(PilotJson pilot);
    }
}