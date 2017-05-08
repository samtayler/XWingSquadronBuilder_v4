using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Prism.Commands;
using XWingSquadronBuilder_v4.Interfaces;
using Windows.UI.Xaml;

namespace XWingSquadronBuilder_v4.Presentation.ViewModels
{
    public class PilotViewModel : BindableBase
    {
        private IPilot pilot;

        public PilotViewModel(IPilot pilot)
        {
            this.pilot = pilot;
            ToggleCollapsed = new DelegateCommand(() =>
            {
                if (Collapsed == Visibility.Collapsed) Collapsed = Visibility.Visible;
                else if (Collapsed == Visibility.Visible) Collapsed = Visibility.Collapsed;
            });
        }

        public IPilot Pilot
        {
            get { return this.pilot; }
            set { SetProperty(ref pilot, value); }
        }


        private Visibility collapsed;

        public Visibility Collapsed
        {
            get { return this.collapsed; }
            set { SetProperty(ref collapsed, value); }
        }

        public ICommand ToggleCollapsed { get; private set; }

    }
}
