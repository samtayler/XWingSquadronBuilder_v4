using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Template10.Mvvm;

namespace XWingSquadronBuilder_v4.Presentation.ViewModels
{
    public class MenuItem : BindableBase
    {
        private string glyph;
        private string text;
        private DelegateCommand command;
        private Type navigationDestination;

        public string Glyph
        {
            get { return glyph; }
            set { Set(ref glyph, value); }
        }

        public string Text
        {
            get { return text; }
            set { Set(ref text, value); }
        }

        public ICommand Command
        {
            get { return command; }
            set { Set(ref command, (DelegateCommand)value); }
        }

        public Type NavigationDestination
        {
            get { return navigationDestination; }
            set { Set(ref navigationDestination, value); }
        }

        public bool IsNavigation
        {
            get { return navigationDestination != null; }
        }
    }

}
