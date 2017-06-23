using Template10.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using XWingSquadronBuilder_v4.BusinessLogic.Logic;
using XWingSquadronBuilder_v4.BusinessLogic.Models;
using XWingSquadronBuilder_v4.BusinessLogic.Repositories;
using XWingSquadronBuilder_v4.Interfaces;
using XWingSquadronBuilder_v4.Presentation.Commands;
using Template10.Services.NavigationService;
using Windows.UI.Xaml.Navigation;
using XWingSquadronBuilder_v4.BusinessLogic.Factories;
using XWingSquadronBuilder_v4.Presentation.Views;
using Windows.UI;
using Windows.UI.ViewManagement;

namespace XWingSquadronBuilder_v4.Presentation.ViewModels
{

    public class SqudronBuilderViewModel : ViewModelBase, IDisposable
    {
        private int squadronCost;
        public ISquadron Squadron { get; private set; }


        private ObservableCollection<PilotViewModel> squadronPilots;

        public ObservableCollection<PilotViewModel> SquadronPilots
        {
            get { return this.squadronPilots; }
            private set { Set(ref squadronPilots, value); }
        }


        private IFaction faction;

        public IFaction Faction
        {
            get { return this.faction; }
            private set { Set(ref faction, value); }
        }


        private List<IPilot> pilotList;

        public List<IPilot> PilotList
        {
            get { return this.pilotList; }
            private set { Set(ref pilotList, value); }
        }

        private UpgradeSetter upgradeSetter;

        public UpgradeSetter UpgradeSetter
        {
            get { return this.upgradeSetter; }
            set { Set(ref upgradeSetter, value); }
        }

        private Visibility showUpgradeSelector;

        public Visibility ShowUpgradeSelector
        {
            get { return this.showUpgradeSelector; }
            set { Set(ref showUpgradeSelector, value); }
        }

        public int SquadronCost
        {
            get { return this.squadronCost; }
            set { Set(ref squadronCost, value); }
        }

        public SqudronBuilderViewModel()
        {
            Squadron = SquadronFactory.CreateSquadron();
            SquadronPilots = new ObservableCollection<PilotViewModel>();
            PilotList = new List<IPilot>();
            ShowUpgradeSelector = Visibility.Collapsed;
            Faction = XWingRepository.Instance.FactionRepository.GetFactionAny();
        }

        public override async Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> suspensionState)
        {
            Dispose();
            if (suspensionState.Any())
            {
                //Value = suspensionState[nameof(Value)]?.ToString();
            }

            Faction = parameter as IFaction;
            PilotList = XWingRepository.Instance.PilotRepository.GetPilotsForFaction(Faction);
            Squadron = SquadronFactory.CreateSquadron(Faction);
            Squadron.PropertyChanged += Squadron_PropertyChanged;
            Squadron.Pilots.CollectionChanged += Pilots_CollectionChanged;
            if (Faction.Name == "Empire") ApplyAccentColor(Colors.White);
            if (Faction.Name == "Rebels") ApplyAccentColor(Colors.Red);
            if (Faction.Name == "Scum") ApplyAccentColor(Colors.Gold);



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

        public void SelectUpgrade(Tuple<IPilot, IUpgradeSlot> e)
        {
            UpgradeSetter = new UpgradeSetter(this.Squadron.UniqueNameCards, e.Item1, e.Item2);
            ShowUpgradeSelector = Visibility.Visible;
        }

        private void Pilots_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            List<IPilot> pilotsToAdd = new List<IPilot>();
            List<PilotViewModel> pilotsToRemove = new List<PilotViewModel>();
            foreach (var pilot in Squadron.Pilots)
            {
                if (!SquadronPilots.Any(x => x.Pilot == pilot))
                {
                    pilotsToAdd.Add(pilot);
                }
            }
            foreach (var pilot in SquadronPilots)
            {
                if (!Squadron.Pilots.Any(x => x == pilot.Pilot))
                {
                    pilotsToRemove.Add(pilot);
                }
            }

            foreach (var pilot in pilotsToRemove)
            {
                SquadronPilots.Remove(pilot);
            }

            foreach (var pilot in pilotsToAdd)
            {
                SquadronPilots.Add(new PilotViewModel(pilot));
            }
        }

        private void Squadron_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            SquadronCost = Squadron.SquadronCostTotal;
        }

        public void AddPilot(IPilot e)
        {
            if (!Squadron.AddPilot(e))
            {
                MessageDialog pilotUniqueDialog = new MessageDialog($"{e.Name} is a unique pilot and cannot be added more than once.", "Unique pilot");
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
                pilotUniqueDialog.ShowAsync();
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
            }
        }

        public async void NavigateHome()
        {
            await NavigationService.NavigateAsync(typeof(FactionSelectionPage));
        }

        public void RemovePilot(PilotViewModel e)
        {
            Squadron.RemovePilot(e.Pilot);
        }

        public void ClearPilots()
        {
            Squadron.ClearAllPilots();
        }

        public Color SetThemeColour()
        {
            return Colors.Red;
        }

        public static void ApplyAccentColor(Color accentColor)
        {
            if (!new AccessibilitySettings().HighContrast)
            {
                Application.Current.Resources["SystemAccentColor"] = accentColor;
            }
        }

        public void Dispose()
        {
            Squadron.PropertyChanged -= Squadron_PropertyChanged;
            Squadron.Pilots.CollectionChanged -= Pilots_CollectionChanged;
        }
    }
}
