using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XWingSquadronBuilder_v4.Interfaces;
using XWingSquadronBuilder_v4.Presentation.ViewModels.XWingModels.Interfaces;

namespace XWingSquadronBuilder_v4.Presentation.ViewModels.Pages.Interfaces
{
    public interface IPilotSelectionViewModel
    {
        IReadOnlyList<IPilot> Pilots { get; }
        ISquadronViewModel SquadronViewModel { get; }
        Task NavigateToPilotDetailsAsync(IPilot pilot);
    }
}
