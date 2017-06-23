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
using XWingSquadronBuilder_v4.BusinessLogic.Repositories;
using XWingSquadronBuilder_v4.Interfaces;

namespace XWingSquadronBuilder_v4.Presentation.ViewModels
{
    public class FactionSelectViewModel : ViewModelBase
    {
        private List<IFaction> factions;

        public List<IFaction> Factions
        {
            get { return this.factions; }
            set { Set(ref factions, value); }
        }
        public override async Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> suspensionState)
        {
            Factions = XWingRepository.Instance.FactionRepository.GetAllFactions().Where(x => x.Name != "Any").ToList();
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

        public override async Task OnNavigatingFromAsync(NavigatingEventArgs args)
        {
            args.Cancel = false;
            await Task.CompletedTask;
        }

        public async void FactionSelected(IFaction faction)
        {
            await NavigationService.NavigateAsync(typeof(Views.SquadronBuilder), parameter: faction);
        }

    }
}
