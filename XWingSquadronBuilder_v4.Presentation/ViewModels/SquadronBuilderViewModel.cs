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
using XWingSquadronBuilder.BusinessLogic.Models;
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

        public SqudronBuilderViewModel(IFaction faction)
        {
            Faction = faction;
            PilotList = XWingRepository.Instance.PilotRepository.GetPilotsForFaction(faction);
            Squadron = new Squadron(faction);
            SquadronPilots = new ObservableCollection<PilotViewModel>();
            AddPilotCommand = new DelegateCommand<IPilot>(new Action<IPilot>(AddPilot));
            RemovePilotCommand = new DelegateCommand<PilotViewModel>(new Action<PilotViewModel>(RemovePilot));
            ClearPilotsCommand = new DelegateCommand(ClearPilots);

            Squadron.PropertyChanged += Squadron_PropertyChanged;
            Squadron.Pilots.CollectionChanged += Pilots_CollectionChanged;
        }

        private void Pilots_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            List<IPilot> pilotsToAdd = new List<IPilot>();
            List<PilotViewModel> pilotsToRemove = new List<PilotViewModel>();
            foreach (var pilot in Squadron.Pilots)
            {
                if(!SquadronPilots.Any(x => x.Pilot == pilot))
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
            Squadron.AddPilot(pilot);
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
