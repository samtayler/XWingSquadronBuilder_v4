using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Template10.Mvvm;
using Template10.Services.NavigationService;
using Windows.UI.Xaml.Navigation;
using XWingSquadronBuilder_v4.BusinessLogic.Factories;
using XWingSquadronBuilder_v4.BusinessLogic.Models.NullModels;
using XWingSquadronBuilder_v4.BusinessLogic.Repositories;
using XWingSquadronBuilder_v4.Interfaces;
using XWingSquadronBuilder_v4.Presentation.Services.Navigation;
using XWingSquadronBuilder_v4.Presentation.ViewModels.Pages.Interfaces;
using XWingSquadronBuilder_v4.Presentation.ViewModels.XWingModels;
using XWingSquadronBuilder_v4.Presentation.ViewModels.XWingModels.Interfaces;
using XWingSquadronBuilder_v4.Presentation.Views;

namespace XWingSquadronBuilder_v4.Presentation.ViewModels.Pages
{

    public class SquadronBuilderViewModel : ViewModelBase, ISquadronBuilderViewModel
    {
        private IXWingSessionState session { get; set; }

        private ISquadronViewModel squadronViewModel;

        public ISquadronViewModel SquadronViewModel
        {
            get { return this.squadronViewModel; }
            private set { Set(ref squadronViewModel, value); }
        }

        public SquadronBuilderViewModel()
        {
            SquadronViewModel = new SquadronViewModel(SquadronFactory.CreateSquadron());
        }

        public override async Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> suspensionState)
        {
            session = SessionState["State"] as IXWingSessionState;

            if (session.ActiveSquadron.Squadron is NullSquadron)
            {
                SquadronViewModel = new SquadronViewModel(SquadronFactory.CreateSquadron(parameter as IFaction));
                session.SetActiveSquadron(SquadronViewModel);
            }
            else
            {
                SquadronViewModel = session.ActiveSquadron;
            }

            //NavigationService.ClearHistory();
            await Task.CompletedTask;
        }

        public override async Task OnNavigatedFromAsync(IDictionary<string, object> suspensionState, bool suspending)
        {
            await Task.CompletedTask;
        }

        public override async Task OnNavigatingFromAsync(NavigatingEventArgs args)
        {
            args.Cancel = false;
            await Task.CompletedTask;
        }

        public async void AddPilot(IPilot e)
        {
            //if (!Squadron.AddPilot(e))
            //{
            //    if (e.Upgrades.Any(x => x.Upgrade.Unique) || e.Unique)
            //    {
            //        MessageDialog uniqueDialog = new MessageDialog($"{e.Name} is a unique pilot or has unique upgrade cards.", "Unique constraint");
            //        await uniqueDialog.ShowAsync();
            //    }
            //}
            await Task.CompletedTask;
        }

        public void RemovePilot(PilotViewModel e)
        {
            //Squadron.RemovePilot(e.Pilot);
        }

        public void ClearPilots()
        {
            //Squadron.ClearAllPilots();
        }

        public async Task NavigateToPilotSelectionAsync()
        {
            await NavigationService.NavigateAsync(typeof(ShipSelectionPage));
        }

        public async Task NavigateToPilotDetails(IPilotViewModel e)
        {
            await NavigationService.NavigateAsync(typeof(PilotDetailsPage), parameter: e.Pilot.Id);
        }

        public bool IsSquadronSaved
        {
            get
            {
                return session?.IsSquadronSaved() ?? false;
            }
        }

        public string IconPilotAdd
        {
            get
            {
                if (session == null) return "";
                var factions = session.XWingRepository.GetAllFactions();
                var empire = factions.Single(x => x.Name == "Empire");
                var rebels = factions.Single(x => x.Name == "Rebels");
                var scum = factions.Single(x => x.Name == "Scum");

                if (SquadronViewModel.Squadron.Faction.Equals(empire)) return "F";
                else if (SquadronViewModel.Squadron.Faction.Equals(rebels)) return "w";
                else if (SquadronViewModel.Squadron.Faction.Equals(scum)) return "v";

                return "v";
            }
        }
    }
}
