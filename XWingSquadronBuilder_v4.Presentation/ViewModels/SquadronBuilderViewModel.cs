using Prism.Commands;
using Prism.Mvvm;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XWingSquadronBuilder_v4.BusinessLogic.Repositories;
using XWingSquadronBuilder_v4.Interfaces;
using XWingSquadronBuilder_v4.Presentation.Commands;

namespace XWingSquadronBuilder_v4.Presentation.ViewModels
{
    public partial class SqudronBuilderViewModel : BindableBase
    {

        public SqudronBuilderViewModel(IFaction faction)
        {
            Faction = faction;
            PilotList = XWingRepository.Instance.PilotRepository.GetPilotsForFaction(faction);
            Squadron = new ObservableCollection<PilotViewModel>();
            AddPilotCommand = new AddPilotCommand(Squadron);
            RemovePilotCommand = new RemovePilotCommand(Squadron);
            ClearPilotsCommand = new DelegateCommand(ClearPilots);

            Squadron.CollectionChanged += Squadron_CollectionChanged;
        }

        private void Squadron_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            SquadronCost = Squadron.Sum(x => x.PilotCost);
        }

        private int squadronCost;

        public int SquadronCost
        {
            get { return this.squadronCost; }
            set { SetProperty(ref squadronCost, value); }
        }

        public IFaction Faction { get; }

        public List<IPilot> PilotList { get; }
        public ObservableCollection<PilotViewModel> Squadron { get; }

        public AddPilotCommand AddPilotCommand { get; }
        public RemovePilotCommand RemovePilotCommand { get; }
        public DelegateCommand ClearPilotsCommand { get; }
        

        private void ClearPilots()
        {
            Squadron.Clear();
        }

    }
}
