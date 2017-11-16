using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XWingSquadronBuilder_v4.Interfaces;

namespace XWingSquadronBuilder_v4.Presentation.ViewModels.Pages.Interfaces
{
    public interface IFactionSelectionViewModel
    {
        IReadOnlyList<ISquadron> SavedSquadronsEmpire { get; }
        IReadOnlyList<ISquadron> SavedSquadronsRebels { get; }
        IReadOnlyList<ISquadron> SavedSquadronsScum { get; }   
        IReadOnlyList<IFaction> Factions { get; }
        Task FactionSelectedAsync(string faction);
        Task OpenSquadronAsync(ISquadron squadron);
    }
}
