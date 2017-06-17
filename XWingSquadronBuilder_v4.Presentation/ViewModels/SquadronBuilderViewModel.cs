using Prism.Commands;
using Prism.Mvvm;
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

namespace XWingSquadronBuilder_v4.Presentation.ViewModels
{
    public partial class SqudronBuilderViewModel : BindableBase
    {
        private int squadronCost;
        public ISquadron Squadron { get; private set; }
        public ObservableCollection<PilotViewModel> SquadronPilots { get; }


        private UpgradeSetter upgradeSetter;

        public UpgradeSetter UpgradeSetter
        {
            get { return this.upgradeSetter; }
            set { SetProperty(ref upgradeSetter, value); }
        }

        private Visibility showUpgradeSelector;

        public Visibility ShowUpgradeSelector
        {
            get { return this.showUpgradeSelector; }
            set { SetProperty(ref showUpgradeSelector, value); }
        }

        public int SquadronCost
        {
            get { return this.squadronCost; }
            set { SetProperty(ref squadronCost, value); }
        }

        public IFaction Faction { get; }

        public List<IPilot> PilotList { get; }

        public DelegateCommand<IPilot> AddPilotCommand { get; }
        public DelegateCommand<PilotViewModel> RemovePilotCommand { get; }
        public DelegateCommand ClearPilotsCommand { get; }
        public DelegateCommand<Tuple<IPilot, IUpgradeSlot>> SelectUpgradeCommand { get; }

        public SqudronBuilderViewModel(IFaction faction)
        {
            Faction = faction;
            PilotList = XWingRepository.Instance.PilotRepository.GetPilotsForFaction(faction);
            Squadron = new Squadron(faction);
            SquadronPilots = new ObservableCollection<PilotViewModel>();
            AddPilotCommand = new DelegateCommand<IPilot>(AddPilot);
            RemovePilotCommand = new DelegateCommand<PilotViewModel>(RemovePilot);
            ClearPilotsCommand = new DelegateCommand(ClearPilots);
            SelectUpgradeCommand = new DelegateCommand<Tuple<IPilot, IUpgradeSlot>>(SelectUpgrade);
            ShowUpgradeSelector = Visibility.Collapsed;
            Squadron.PropertyChanged += Squadron_PropertyChanged;
            Squadron.Pilots.CollectionChanged += Pilots_CollectionChanged;
        }

        private void SelectUpgrade(Tuple<IPilot, IUpgradeSlot> arg)
        {
            UpgradeSetter = new UpgradeSetter(this.Squadron.UniqueNameCards, arg.Item1, arg.Item2);
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

        private void AddPilot(IPilot pilot)
        {
            if(!Squadron.AddPilot(pilot))
            {
                MessageDialog pilotUniqueDialog = new MessageDialog($"{pilot.Name} is a unique pilot and cannot be added more than once.", "Unique pilot");
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
                pilotUniqueDialog.ShowAsync();
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
            }
        }

        private void RemovePilot(PilotViewModel pilot)
        {
            Squadron.RemovePilot(pilot.Pilot);
        }

        private void ClearPilots()
        {
            Squadron.ClearAllPilots();
        }

    }
}
