using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XWingSquadronBuilder_v4.Interfaces;

namespace XWingSquadronBuilder_v4.Presentation.ViewModels.XWingModels.Interfaces
{
    public interface IUpgradeSlotViewModel : INotifyPropertyChanged, IEquatable<IUpgradeSlotViewModel>
    {
        IUpgradeSlot UpgradeSlot { get; }
    }
}
