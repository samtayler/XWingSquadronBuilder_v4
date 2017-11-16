using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Template10.Common;
using Template10.Mvvm;
using Template10.Services.NavigationService;
using Template10.Services.ViewService;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using XWingSquadronBuilder_v4.BusinessLogic.Factories;
using XWingSquadronBuilder_v4.BusinessLogic.Repositories;
using XWingSquadronBuilder_v4.Interfaces;
using XWingSquadronBuilder_v4.Presentation.Services.Navigation;
using XWingSquadronBuilder_v4.Presentation.ViewModels.Pages.Interfaces;
using XWingSquadronBuilder_v4.Presentation.ViewModels.XWingModels;

namespace XWingSquadronBuilder_v4.Presentation.ViewModels.Pages
{
    public class FactionSelectionViewModel : ViewModelBase, IFactionSelectionViewModel
    {
        private IXWingSessionState session { get; set; }

        private IReadOnlyList<ISquadron> savedSquadronsEmpire;

        public IReadOnlyList<ISquadron> SavedSquadronsEmpire
        {
            get { return this.savedSquadronsEmpire; }
            private set { Set(ref savedSquadronsEmpire, value); }
        }

        private IReadOnlyList<ISquadron> savedSquadronsRebels;

        public IReadOnlyList<ISquadron> SavedSquadronsRebels
        {
            get { return this.savedSquadronsRebels; }
            private set { Set(ref savedSquadronsRebels, value); }
        }

        private IReadOnlyList<ISquadron> savedSquadronsScum;

        public IReadOnlyList<ISquadron> SavedSquadronsScum
        {
            get { return this.savedSquadronsScum; }
            private set { Set(ref savedSquadronsScum, value); }
        }


        private IReadOnlyList<IFaction> factions;

        public IReadOnlyList<IFaction> Factions
        {
            get { return this.factions; }
            set { Set(ref factions, value); }
        }

        public FactionSelectionViewModel()
        {
            SavedSquadronsEmpire = new List<ISquadron>();
            SavedSquadronsRebels = new List<ISquadron>();
            SavedSquadronsScum = new List<ISquadron>();   
        }

        public override async Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> suspensionState)
        {
            session = SessionState["State"] as IXWingSessionState;
            Factions = session.XWingRepository.GetAllFactions().ToList();
            SavedSquadronsEmpire = session.XWingRepository.GetSavedSquadronsForFaction(session.XWingRepository.GetFaction("Empire"));
            SavedSquadronsRebels = session.XWingRepository.GetSavedSquadronsForFaction(session.XWingRepository.GetFaction("Rebels"));
            SavedSquadronsScum = session.XWingRepository.GetSavedSquadronsForFaction(session.XWingRepository.GetFaction("Scum"));
            if (suspensionState.Any())
            {
                //Value = suspensionState[nameof(Value)]?.ToString();
            }
            NavigationService.ClearHistory();
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

        public async Task FactionSelectedAsync(string faction)
        {
            session.ClearActiveSquadron();
            await NavigationService.NavigateAsync(typeof(Views.SquadronBuilder), session.XWingRepository.GetFaction(faction));
        }

        public async Task OpenSquadronAsync(ISquadron squadron)
        {
            session.SetActiveSquadron(new SquadronViewModel(squadron));
            await NavigationService.NavigateAsync(typeof(Views.SquadronBuilder)); 
        }
    }
}
