using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using XWingSquadronBuilder_v4.Interfaces;

namespace XWingSquadronBuilder_v4.BusinessLogic.ViewModels
{
    public class UpgradeViewModel : INotifyPropertyChanged, IEquatable<UpgradeViewModel>
    {        

        public List<UpgradeSlotViewModel> AddUpgradeModifiers { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        public IUpgrade Upgrade { get; }

        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public IEnumerable<UpgradeSlotViewModel> GetInnerUpgradeSlots()
        {
            return AddUpgradeModifiers.SelectMany((x) => x.GetInnerUpgradeSlots().Concat(new[] { x }));           
        }

        public bool Equals(UpgradeViewModel other)
        {
            return Upgrade.Name == other.Upgrade.Name && Upgrade.UpgradeType.Equals(other.Upgrade.UpgradeType);
        }
    }
}
