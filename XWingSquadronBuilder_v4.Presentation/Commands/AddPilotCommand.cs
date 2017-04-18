using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using XWingSquadronBuilder_v4.Interfaces;

namespace XWingSquadronBuilder_v4.Presentation.Commands
{
    public class AddPilotCommand : DelegateCommandBase
    {
        private ObservableCollection<IPilot> squadron { get; }        

        public AddPilotCommand(ObservableCollection<IPilot> squadron)
        {            
            this.squadron = squadron;
        }

        protected override bool CanExecute(object parameter)
        {
            var pilot = parameter as IPilot;
            if (pilot.Unique)
                return !squadron.Any(x => pilot.Equals(x));
            return true;
        }

        protected override void Execute(object parameter)
        {
            squadron.Add(parameter as IPilot);
        }
    }


}
