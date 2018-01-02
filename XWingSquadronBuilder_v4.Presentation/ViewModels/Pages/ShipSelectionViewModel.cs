using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Template10.Mvvm;
using Template10.Services.NavigationService;
using Windows.UI.Xaml.Navigation;
using XWingSquadronBuilder_v4.Interfaces;
using XWingSquadronBuilder_v4.Presentation.Services.Navigation;
using XWingSquadronBuilder_v4.Presentation.ViewModels.Pages.Interfaces;
using XWingSquadronBuilder_v4.Presentation.ViewModels.XWingModels.Interfaces;
using XWingSquadronBuilder_v4.Presentation.Views;

namespace XWingSquadronBuilder_v4.Presentation.ViewModels.Pages
{
    public class ShipSelectionViewModel : ViewModelBase, IShipSelectionViewModel
    {
        private class ShipComparer : IEqualityComparer<IPilot>
        {
            public bool Equals(IPilot x, IPilot y)
            {
                return x.ShipName == y.ShipName;
            }

            public int GetHashCode(IPilot obj)
            {
                return obj.ShipName.GetHashCode();
            }
        }

        public ShipSelectionViewModel()
        {
            Ships = new List<ShipDisplay>();
        }

        private IReadOnlyList<ShipDisplay> ships;

        public IReadOnlyList<ShipDisplay> Ships
        {
            get { return this.ships; }
            private set { Set(ref ships, value); }
        }

        private IXWingSessionState session { get; set; }

        private ISquadronViewModel squadronViewModel;

        public ISquadronViewModel SquadronViewModel
        {
            get { return this.squadronViewModel; }
            private set { Set(ref squadronViewModel, value); }
        }

        private void LoadPilots(IFaction faction)
        {
            var pilots = session.XWingRepository.GetPilotsForFaction(faction);
            Ships = pilots.Distinct(new ShipComparer()).Select(x => 
            new ShipDisplay(x.ShipIcon, x.ShipName, new List<IPilot>())).ToList();           
        }

        public override async Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> suspensionState)
        {
            session = SessionState["State"] as IXWingSessionState;
            SquadronViewModel = session.ActiveSquadron;
            LoadPilots(SquadronViewModel.Squadron.Faction);

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

        public async Task NavigateToPilotSelectionAsync(ShipDisplay ship)
        {
            await NavigationService.NavigateAsync(typeof(PilotSelectionPage), parameter: ship.ShipName);
        }

    }


}
