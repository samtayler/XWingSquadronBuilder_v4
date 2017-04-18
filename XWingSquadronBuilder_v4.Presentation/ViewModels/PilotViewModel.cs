using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XWingSquadronBuilder_v4.Interfaces;

namespace XWingSquadronBuilder_v4.Presentation.ViewModels
{
    public class PilotViewModel : BindableBase
    {
        private IPilot pilot;

        public IPilot Pilot
        {
            get { return this.pilot; }
            set { SetProperty(ref pilot, value); }
        }


    }
}
