using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XWingSquadronBuilder_v4.Interfaces;

namespace XWingSquadronBuilder_v4.BusinessLogic.ViewModels
{
    public class UpgradeSlotViewModel : INotifyPropertyChanged
    {
        public UpgradeViewModel Upgrade { get; }
        public int CostReduction { get;  }
        public int CostRestriction { get; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
