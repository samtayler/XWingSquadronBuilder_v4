using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using XWingSquadronBuilder_v4.Interfaces;

namespace XWingSquadronBuilder_v4.Presentation.ViewModels.XWingModels.Interfaces
{    
    public interface ISquadronViewModel : INotifyPropertyChanged
    {
        ISquadron Squadron { get; }  
        IReadOnlyList<IPilotViewModel> Pilots { get; }
    }
}
