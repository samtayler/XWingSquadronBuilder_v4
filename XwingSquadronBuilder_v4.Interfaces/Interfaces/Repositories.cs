using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XWingSquadronBuilder_v4.Interfaces
{
    public interface IUpgradeTypesRepository
    {
        IUpgradeType GetUpgradeType(string name);
        List<IUpgradeType> GetAllUpgradeTypes();
    }

    public interface IUpgradeRepository
    {
        IReadOnlyList<IUpgrade> GetAllUpgrades();
        IReadOnlyList<IUpgrade> GetAllUpgradesForType(IUpgradeType type);
    }

    public interface IPilotRepository
    {
        List<IPilot> GetPilotsForFaction(IFaction faction);
        List<IPilot> GetPilots();
    }

    public interface IFactionRepository
    {
        IFaction GetFaction(string name);
        List<IFaction> GetAllFactions();
    }
}
