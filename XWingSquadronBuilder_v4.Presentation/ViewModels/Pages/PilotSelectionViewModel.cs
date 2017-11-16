using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Template10.Mvvm;
using Template10.Services.NavigationService;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using XWingSquadronBuilder_v4.BusinessLogic.Repositories;
using XWingSquadronBuilder_v4.Interfaces;
using XWingSquadronBuilder_v4.Presentation.Services.Navigation;
using XWingSquadronBuilder_v4.Presentation.ViewModels.Pages.Interfaces;
using XWingSquadronBuilder_v4.Presentation.ViewModels.XWingModels.Interfaces;
using XWingSquadronBuilder_v4.Presentation.Views;

namespace XWingSquadronBuilder_v4.Presentation.ViewModels.Pages
{
    public class PilotSelectionViewModel : ViewModelBase, IPilotSelectionViewModel
    {
        private IXWingSessionState session { get; set; }
        private IReadOnlyList<IPilot> pilots;

        public IReadOnlyList<IPilot> Pilots
        {
            get { return this.pilots; }
            private set { Set(ref pilots, value); }
        }

        private ISquadronViewModel squadronViewModel;

        public ISquadronViewModel SquadronViewModel
        {
            get { return this.squadronViewModel; }
            set { Set(ref squadronViewModel, value); }
        }

        public PilotSelectionViewModel() { }


        public override async Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> suspensionState)
        {
            session = SessionState["State"] as IXWingSessionState;
            SquadronViewModel = session.ActiveSquadron;

            string shipname = parameter as string;
            Pilots = session.XWingRepository.GetPilotsForFaction(SquadronViewModel.Squadron.Faction)
                .Where(x => x.ShipName == shipname)
                .OrderByDescending(x => x.PilotSkill)
                .ThenByDescending(x => x.Cost)
                .ThenBy(x => x.Name)
                .ToList();
            //if (suspensionState.Any())
            //{
            //    //Value = suspensionState[nameof(Value)]?.ToString();
            //}    

            await Task.CompletedTask;
        }

        public override async Task OnNavigatedFromAsync(IDictionary<string, object> suspensionState, bool suspending)
        {
            if (suspending)
            {
                //suspensionState[nameof(Value)] = Value;
            }
            await Task.CompletedTask;
        }

        public override async Task OnNavigatingFromAsync(NavigatingEventArgs args)
        {
            args.Cancel = false;
            await Task.CompletedTask;
        }

        public async Task NavigateToPilotDetailsAsync(IPilot pilot)
        {
            if (!SquadronViewModel.Squadron.AddPilot(pilot)) return;

            await NavigationService.NavigateAsync(typeof(SquadronBuilder));
        }

    }
}
