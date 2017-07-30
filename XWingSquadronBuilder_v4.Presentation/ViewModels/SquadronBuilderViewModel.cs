using Template10.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using XWingSquadronBuilder_v4.BusinessLogic.Repositories;
using XWingSquadronBuilder_v4.Interfaces;
using Template10.Services.NavigationService;
using Windows.UI.Xaml.Navigation;
using XWingSquadronBuilder_v4.BusinessLogic.Factories;
using XWingSquadronBuilder_v4.Presentation.Views;
using Windows.UI;
using Windows.UI.ViewManagement;

namespace XWingSquadronBuilder_v4.Presentation.ViewModels
{

    public class SquadronBuilderViewModel : ViewModelBase, IDisposable
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


        private UpgradeSelectorViewModel upgradeSelector;

        public UpgradeSelectorViewModel UpgradeSelector
        {
            get { return upgradeSelector; }
            private set { Set(ref upgradeSelector, value); }
        }

        private IReadOnlyList<IPilot> pilotList;

        public IReadOnlyList<IPilot> PilotList
        {
            get { return this.pilotList; }
            private set { Set(ref pilotList, value); }
        }


        public int SquadronCost
        {
            get { return this.squadronCost; }
            set { Set(ref squadronCost, value); }
        }

        public SquadronBuilderViewModel()
        {
            Squadron = SquadronFactory.CreateSquadron();
            SquadronPilots = new ObservableCollection<PilotViewModel>();
            PilotList = new List<IPilot>();
            Faction = XWingRepository.Instance.FactionRepository.GetFactionAny();
            UpgradeSelector = new UpgradeSelectorViewModel();
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
            UpgradeSelector.Show(e.Item2, Squadron.UniqueNameCards, e.Item1);
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

        public async void AddPilot(IPilot e)
        {
            if (!Squadron.AddPilot(e))
            {
                if (e.Upgrades.Any(x => x.Upgrade.Unique) || e.Unique)
                {
                    MessageDialog uniqueDialog = new MessageDialog($"{e.Name} is a unique pilot or has unique upgrade cards.", "Unique constraint");
                    await uniqueDialog.ShowAsync();
                }
            }
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
