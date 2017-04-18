using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XWingSquadronBuilder_v4.Interfaces;

namespace XWingSquadronBuilder_v4.Presentation.Commands
{
    public class RemovePilotCommand : DelegateCommandBase
    {
        private ObservableCollection<IPilot> squadron { get; }        

        public RemovePilotCommand(ObservableCollection<IPilot> squadron)
        {           
            this.squadron = squadron;
        }

        protected override bool CanExecute(object parameter)
        {
            var pilot = parameter as IPilot;
            return squadron.Contains(pilot);
        }

        protected override void Execute(object parameter)
        {
            squadron.Remove(parameter as IPilot);
        }
    }
}
