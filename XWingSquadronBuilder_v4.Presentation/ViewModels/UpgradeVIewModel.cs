using Template10.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XWingSquadronBuilder_v4.Interfaces;
using XWingSquadronBuilder_v4.Presentation.Converters;
using Windows.UI.Xaml.Controls;

namespace XWingSquadronBuilder_v4.Presentation.ViewModels
{
    public class UpgradeViewModel : BindableBase
    {
        private IUpgrade upgrade;

        private IReadOnlyList<string> errorsList;

        public IReadOnlyList<string> ErrorsList
        {
            get { return this.errorsList; }
            private set { Set(ref errorsList, value); }
        }

        public bool HasErrors => ErrorsList.Count > 0;        

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

        public IEnumerable<TextBlock> AugmentText(string text, double fontsize)
        {
            return XWingTextAugmenter.AugementWithXWingIcons(text, fontsize);
        }

    }
}
