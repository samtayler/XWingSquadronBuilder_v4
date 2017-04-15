using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace XWingSquadronBuilder_v4.BusinessLogic.ViewModels
{
    public class PilotListViewModel : INotifyPropertyChanged
    {
        public PilotListViewModel(IEnumerable<PilotViewModel> pilots)
        {
            Pilots = pilots;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public IEnumerable<PilotViewModel> Pilots { get; }

    }
}
