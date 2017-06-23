using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Windows.UI.Popups;
using XWingSquadronBuilder_v4.Interfaces;
using XWingSquadronBuilder_v4.Presentation.ViewModels;

namespace XWingSquadronBuilder_v4.Presentation.Commands
{
//    public class AddPilotCommand : DelegateCommandBase
//    {
//        private ObservableCollection<PilotViewModel> squadron { get; }        

//        public AddPilotCommand(ObservableCollection<PilotViewModel> squadron)
//        {            
//            this.squadron = squadron;
//        }

//        protected override bool CanExecute(object parameter)
//        {
//            var pilot = new PilotViewModel(parameter as IPilot);            
//            if (pilot.Pilot.Unique)
//            {
               
//                if(!squadron.Any(x => pilot.Pilot.Equals(x.Pilot)))
//                {
//                    return true;
//                }
//                else
//                {
//                    var dialog = new MessageDialog($"{pilot.Pilot.Name} is a unique pilot and cannot be added more than once.", "Unique Pilot");
//#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
//                    dialog.ShowAsync();
//#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
//                    return false;
//                }
//            }
//            return true;
//        }

//        protected override void Execute(object parameter)
//        {
//            squadron.Add(new PilotViewModel(parameter as IPilot));
//        }
//    }


}
