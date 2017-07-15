using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XWingSquadronBuilder_v4.Interfaces
{
    public interface IActionRepository
    {
        IAction GetAction(string name);
        IReadOnlyList<IAction> GetAllActions();
    }

    public interface IUpgradeTypesRepository
    {
        IUpgradeType GetUpgradeType(string name);
        IReadOnlyList<IUpgradeType> GetAllUpgradeTypes();
    }

    public interface IUpgradeRepository
    {
        IReadOnlyList<IUpgrade> GetAllUpgrades();
        IReadOnlyList<IUpgrade> GetAllUpgradesForType(IUpgradeType type);
    }

    public interface IPilotRepository
    {
        IReadOnlyList<IPilot> GetPilotsForFaction(IFaction faction);
        IReadOnlyList<IPilot> GetPilots();
    }

    public interface IFactionRepository
    {
        IFaction GetFaction(string name);
        IFaction GetFactionAny();
        IReadOnlyList<IFaction> GetAllFactions();
    }
}
