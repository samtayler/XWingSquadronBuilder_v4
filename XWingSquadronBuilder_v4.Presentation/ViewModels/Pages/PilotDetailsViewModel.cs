using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Template10.Mvvm;
using XWingSquadronBuilder_v4.Interfaces;
using Windows.UI.Xaml.Controls;
using XWingSquadronBuilder_v4.Presentation.Converters;
using Windows.UI.Xaml.Navigation;
using Template10.Services.NavigationService;
using XWingSquadronBuilder_v4.BusinessLogic.Factories;
using XWingSquadronBuilder_v4.Presentation.ViewModels.XWingModels.Interfaces;
using XWingSquadronBuilder_v4.Presentation.ViewModels.Pages.Interfaces;
using XWingSquadronBuilder_v4.Presentation.ViewModels.XWingModels;
using XWingSquadronBuilder_v4.Presentation.Views;
using System.Collections.ObjectModel;
using XWingSquadronBuilder_v4.Presentation.Services.Navigation;

namespace XWingSquadronBuilder_v4.Presentation.ViewModels.Pages
{
    public class PilotDetailsViewModel : ViewModelBase, IPilotDetailsViewModel
    {
        public PilotDetailsViewModel()
        {
            PilotViewModel = new PilotViewModel(PilotFactory.CreateNullPilot());
        }

        private IXWingSessionState session { get; set; }

        private IPilotViewModel pilotViewModel;

        public IPilotViewModel PilotViewModel
        {
            get { return pilotViewModel; }
            private set
            {
                Set(ref pilotViewModel, value);
                pilotViewModel.PropertyChanged += (sender, e) =>
                {
                    RaisePropertyChanged(nameof(UsedUpgradeSlots));
                };
            }
        }

        private ISquadronViewModel squadronViewModel;

        public ISquadronViewModel SquadronViewModel
        {
            get { return squadronViewModel; }
            private set
            {
                Set(ref squadronViewModel, value);
                squadronViewModel.PropertyChanged += (sender, args) =>
                {
                    RaisePropertyChanged(nameof(IsPilotSavedToSquadron));
                };
            }
        }

        public IReadOnlyList<IUpgradeSlotViewModel> UsedUpgradeSlots => PilotViewModel?.Upgrades
            .Where(x => x.UpgradeSlot.IsNotNullUpgrade).OrderBy(x => x.UpgradeSlot.IsNullUpgrade).ToList();


        public override async Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> suspensionState)
        {
            session = SessionState["State"] as IXWingSessionState;
            SquadronViewModel = session.ActiveSquadron;
            PilotViewModel = SquadronViewModel.Pilots.Single(x => x.Pilot.Id == (Guid)parameter);
            //if (suspensionState.Any())
            //{
            //    //Value = suspensionState[nameof(Value)]?.ToString();
            //}

            await Task.CompletedTask;
        }

        public override async Task OnNavigatedFromAsync(IDictionary<string, object> suspensionState, bool suspending)
        {
            NavigationService.ClearHistory();
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

        public async Task NavigateToSquadronMain()
        {
            await NavigationService.NavigateAsync(typeof(SquadronBuilder));
        }

        public void SavePilotToSquadron()
        {
            SquadronViewModel.Squadron.AddPilot(PilotViewModel.Pilot);
        }

        public async Task RemovePilotFromSquadron()
        {
            SquadronViewModel.Squadron.RemovePilot(PilotViewModel.Pilot.Id);
            await NavigateToSquadronMain();
        }

        public bool IsPilotSavedToSquadron => SquadronViewModel?.Squadron.Pilots.ContainsKey(PilotViewModel.Pilot.Id) ?? true;


    }
}
