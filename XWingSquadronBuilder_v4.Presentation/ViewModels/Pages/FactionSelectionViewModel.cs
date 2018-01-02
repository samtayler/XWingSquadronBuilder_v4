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



        public IReadOnlyList<ISquadron> SavedSquadronsEmpire =>
            session?.XWingRepository.SavedSquadrons.Values
            .Where(squadron => squadron.Faction
            .Equals(session?.XWingRepository.GetFaction("Empire")))
            .ToList().AsReadOnly();

        public IReadOnlyList<ISquadron> SavedSquadronsRebels =>
            session?.XWingRepository.SavedSquadrons.Values
            .Where(squadron => squadron.Faction
            .Equals(session.XWingRepository.GetFaction("Rebels")))
            .ToList().AsReadOnly();

        public IReadOnlyList<ISquadron> SavedSquadronsScum =>
            session?.XWingRepository.SavedSquadrons.Values
            .Where(squadron => squadron.Faction
            .Equals(session.XWingRepository.GetFaction("Scum")))
            .ToList().AsReadOnly();


        private IReadOnlyList<IFaction> factions;

        public IReadOnlyList<IFaction> Factions
        {
            get { return this.factions; }
            set { Set(ref factions, value); }
        }

        public override async Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> suspensionState)
        {
            NavigationService.ClearHistory();
            session = SessionState["State"] as IXWingSessionState;
            Factions = session.XWingRepository.GetAllFactions().ToList();
            session.ClearActiveSquadron();
            if (suspensionState.Any())
            {
                //Value = suspensionState[nameof(Value)]?.ToString();
            }

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

        public void DeleteSavedSquadron(ISquadron squadron)
        {
            session.XWingRepository.DeleteSavedSquadron(squadron.Id);
            switch (squadron.Faction.Name)
            {
                case ("Empire"):
                    {
                        RaisePropertyChanged(nameof(SavedSquadronsEmpire));
                        break;
                    }
                case ("Rebels"):
                    {
                        RaisePropertyChanged(nameof(SavedSquadronsRebels));
                        break;
                    }
                case ("Scum"):
                    {
                        RaisePropertyChanged(nameof(SavedSquadronsScum));
                        break;
                    }
                default:
                    break;
            }
        }

        public override async Task OnNavigatingFromAsync(NavigatingEventArgs args)
        {
            args.Cancel = false;
            await Task.CompletedTask;
        }

        public async Task FactionSelectedAsync(string faction)
        {
            await NavigationService.NavigateAsync(typeof(Views.SquadronBuilder), session.XWingRepository.GetFaction(faction));
        }

        public async Task OpenSquadronAsync(ISquadron squadron)
        {
            session.SetActiveSquadron(new SquadronViewModel(squadron));
            await NavigationService.NavigateAsync(typeof(Views.SquadronBuilder));
        }
    }
}
