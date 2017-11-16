using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template10.Mvvm;
using Windows.UI.Popups;
using XWingSquadronBuilder_v4.Presentation.ViewModels.Pages.Interfaces;

namespace XWingSquadronBuilder_v4.Presentation.ViewModels.Pages
{
    public class ShellViewModel : ViewModelBase, IShellViewModel
    {
        public bool CheckCurrentSession()
        {
            if (!SessionState.ContainsKey("Squadron")) return false;

            return true;
        }  
    }
}
