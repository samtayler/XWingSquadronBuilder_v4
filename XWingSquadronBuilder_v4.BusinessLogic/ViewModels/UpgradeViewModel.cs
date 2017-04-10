using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using XWingSquadronBuilder_v4.DataLayer.Models;
using XWingSquadronBuilder_v4.Interfaces;

namespace XWingSquadronBuilder_v4.BusinessLogic.ViewModels
{
    public class UpgradeViewModel : INotifyPropertyChanged, IEquatable<UpgradeViewModel>
    {
        public UpgradeViewModel(IUpgrade upgrade)
        {
            Upgrade = upgrade;
            AddUpgradeModifiers = new List<UpgradeSlotViewModel>(Upgrade.AddUpgradeModifiers
                .Select(x => new UpgradeSlotViewModel(x.UpgradeType, CreateNullUpgradeViewModel(x.UpgradeType), x.CostReduction, x.CostRestriction)));
        }       

        public static UpgradeViewModel CreateNullUpgradeViewModel(IUpgradeType type)
        {
            return new UpgradeViewModel(new NullUpgrade(type));
        }

        public List<UpgradeSlotViewModel> AddUpgradeModifiers { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        public IUpgrade Upgrade { get; }

        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public IEnumerable<UpgradeSlotViewModel> GetInnerUpgradeSlots()
        {
            return AddUpgradeModifiers.Select(x => x.GetInnerUpgradeSlots().Concat(new[] { x }))
                .Aggregate(new List<UpgradeSlotViewModel>(), (x, y) => x.Concat(y).ToList());
        }

        public bool Equals(UpgradeViewModel other)
        {
            return Upgrade.Name == other.Upgrade.Name && Upgrade.UpgradeType.Equals(other.Upgrade.UpgradeType);
        }
    }
}
