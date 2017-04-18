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
            Squadron = new ObservableCollection<IPilot>();
            AddPilotCommand = new AddPilotCommand(Squadron);
            RemovePilotCommand = new RemovePilotCommand(Squadron);
        }

        public IFaction Faction { get; }

        public List<IPilot> PilotList { get; }
        public ObservableCollection<IPilot> Squadron { get; }

        public AddPilotCommand AddPilotCommand { get; }
        public RemovePilotCommand RemovePilotCommand { get; }        

    }
}
