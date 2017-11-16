using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XWingSquadronBuilder_v4.Interfaces;
using XWingSquadronBuilder_v4.Presentation.ViewModels.XWingModels.Interfaces;

namespace XWingSquadronBuilder_v4.Presentation.ViewModels.Pages.Interfaces
{
    public interface ISquadronBuilderViewModel
    {          
        Task NavigateToPilotSelectionAsync();
        Task NavigateToPilotDetails(IPilotViewModel e);
        ISquadronViewModel SquadronViewModel { get; }
        string IconPilotAdd { get; }
        bool IsSquadronSaved { get; }

    }
}
