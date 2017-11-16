using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XWingSquadronBuilder_v4.Interfaces;
using XWingSquadronBuilder_v4.Presentation.ViewModels.XWingModels.Interfaces;

namespace XWingSquadronBuilder_v4.Presentation.ViewModels.Pages.Interfaces
{
    public interface IPilotDetailsViewModel : INotifyPropertyChanged
    {
        IPilotViewModel PilotViewModel { get; }
        ISquadronViewModel SquadronViewModel { get; }
        bool IsPilotSavedToSquadron { get; }
        Task NavigateToSquadronMain();
        void SavePilotToSquadron();
        Task RemovePilotFromSquadron();


    }
}
