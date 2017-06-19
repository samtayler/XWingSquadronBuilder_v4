using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XWingSquadronBuilder_v4.Interfaces;

namespace XWingSquadronBuilder_v4.Presentation.ViewModels
{
    public class UpgradeViewModel : BindableBase
    {
        private IUpgrade upgrade;

        public IUpgrade Upgrade
        {
            get { return this.upgrade; }
            private set { SetProperty(ref upgrade, value); }
        }

        public UpgradeViewModel(IUpgrade upgrade)
        {
            this.upgrade = upgrade;
        }

        
    }
}
