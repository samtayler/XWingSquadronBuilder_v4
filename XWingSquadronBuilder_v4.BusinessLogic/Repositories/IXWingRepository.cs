using System;
using System.Collections.Generic;
using Template10.Common;
using XWingSquadronBuilder_v4.Interfaces;

namespace XWingSquadronBuilder_v4.BusinessLogic.Repositories
{
    public interface IXWingRepository
    {
        IObservableDictionary<Guid, ISquadron> SavedSquadrons { get; }
        IReadOnlyList<IAction> GetAllActions();
        IAction GetAction(string name);
        IUpgradeType GetUpgradeType(string name);
        IReadOnlyList<IUpgradeType> GetAllUpgradeTypes();
        IReadOnlyList<IUpgrade> GetAllUpgrades();
        IReadOnlyList<IUpgrade> GetAllUpgradesForType(IUpgradeType type);
        IUpgrade GetUpgrade(string name, string upgradeType);
        IReadOnlyList<IPilot> GetPilotsForFaction(IFaction faction);
        IReadOnlyList<IPilot> GetPilots();
        IPilot GetPilot(string Name, string ShipName);
        IFaction GetFaction(string name);
        IFaction FactionAny { get; }
        IReadOnlyList<IFaction> GetAllFactions();
        IReadOnlyList<ISquadron> GetSavedSquadronsForFaction(IFaction faction);
        IReadOnlyList<ISquadron> GetSavedSquadrons();
        void SaveSquadron(ISquadron squadron);
        bool IsSquadronSaved(Guid id);
        void DeleteSavedSquadron(Guid id);
    }
}