using Template10.Mvvm;
using System.Collections.Generic;
using XWingSquadronBuilder_v4.Interfaces;
using XWingSquadronBuilder_v4.Presentation.ViewModels.XWingModels.Interfaces;

namespace XWingSquadronBuilder_v4.Presentation.ViewModels.XWingModels
{
    public class UpgradeViewModel : BindableBase, IUpgradeViewModel
    {
        private IUpgrade upgrade;

        private IReadOnlyList<string> errorsList;

        public IReadOnlyList<string> ErrorsList
        {
            get { return this.errorsList; }
            private set { Set(ref errorsList, value); }
        }

        public bool NoErrors => ErrorsList.Count == 0;        

        public IUpgrade Upgrade
        {
            get { return this.upgrade; }
            private set { Set(ref upgrade, value); }
        }

        public UpgradeViewModel(IUpgrade upgrade, IReadOnlyList<string> errorList)
        {
            this.upgrade = upgrade;
            this.ErrorsList = errorList;
        }

        public bool Equals(IUpgradeViewModel other)
        {
            return this.Upgrade.Equals(other.Upgrade);
        }
    }
}
