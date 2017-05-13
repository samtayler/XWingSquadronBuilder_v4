using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XWingSquadronBuilder_v4.Interfaces;
using XWingSquadronBuilder_v4.Presentation.ViewModels;

namespace XWingSquadronBuilder_v4.Presentation.Commands
{
    public class RemovePilotCommand : DelegateCommandBase
    {
        private ObservableCollection<PilotViewModel> squadron { get; }        

        public RemovePilotCommand(ObservableCollection<PilotViewModel> squadron)
        {           
            this.squadron = squadron;
        }

        protected override bool CanExecute(object parameter)
        {
            var pilot = parameter as PilotViewModel;
            return squadron.Contains(pilot);
        }

        protected override void Execute(object parameter)
        {
            squadron.Remove(parameter as PilotViewModel);
        }
    }
}
