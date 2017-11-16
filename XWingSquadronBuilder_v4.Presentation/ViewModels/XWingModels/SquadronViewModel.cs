using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template10.Mvvm;
using XWingSquadronBuilder_v4.Interfaces;
using XWingSquadronBuilder_v4.Presentation.ViewModels.XWingModels.Interfaces;

namespace XWingSquadronBuilder_v4.Presentation.ViewModels.XWingModels
{
    public class SquadronViewModel : BindableBase, ISquadronViewModel
    {
        private ISquadron squadron;

        public SquadronViewModel(ISquadron squadron)
        {
            Squadron = squadron;
        }

        public ISquadron Squadron
        {
            get { return this.squadron; }
            private set
            {
                Set(ref squadron, value);
                //if this ever gets set more than once there will be a memory leak
                squadron.PropertyChanged += (sender, args) =>
                {
                    RaisePropertyChanged(nameof(Pilots));
                };
            }
        }       

        //This is not very efficient
        public IReadOnlyList<IPilotViewModel> Pilots => 
            Squadron.Pilots.Values.Select(pilot => new PilotViewModel(pilot)).ToList();       

    }
}
