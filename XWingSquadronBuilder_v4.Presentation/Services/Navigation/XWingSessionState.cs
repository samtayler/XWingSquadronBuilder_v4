using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XWingSquadronBuilder_v4.BusinessLogic.Models.NullModels;
using XWingSquadronBuilder_v4.BusinessLogic.Repositories;
using XWingSquadronBuilder_v4.Presentation.ViewModels.XWingModels;
using XWingSquadronBuilder_v4.Presentation.ViewModels.XWingModels.Interfaces;

namespace XWingSquadronBuilder_v4.Presentation.Services.Navigation
{
    public class XWingSessionState : IXWingSessionState
    {
        public XWingSessionState(IXWingRepository xWingRepository)
        {
            XWingRepository = xWingRepository;
            ActiveSquadron = new SquadronViewModel(new NullSquadron());
        }

        public IXWingRepository XWingRepository { get; }

        public ISquadronViewModel ActiveSquadron { get; private set; }

        public void ClearActiveSquadron()
        {
            ActiveSquadron = new SquadronViewModel(new NullSquadron());
        }

        public bool IsSquadronSaved()
        {
            return XWingRepository.IsSquadronSaved(ActiveSquadron.Squadron.Id);
        }

        public void SetActiveSquadron(ISquadronViewModel squadron)
        {
            ActiveSquadron = squadron;
        }
    }
}
