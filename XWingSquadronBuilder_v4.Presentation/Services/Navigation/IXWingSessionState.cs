using XWingSquadronBuilder_v4.BusinessLogic.Repositories;
using XWingSquadronBuilder_v4.Presentation.ViewModels.XWingModels.Interfaces;

namespace XWingSquadronBuilder_v4.Presentation.Services.Navigation
{
    public interface IXWingSessionState
    {
        IXWingRepository XWingRepository { get; }
        ISquadronViewModel ActiveSquadron { get; }
        void SetActiveSquadron(ISquadronViewModel squadron);
        void ClearActiveSquadron();
        bool IsSquadronSaved();
    }
}