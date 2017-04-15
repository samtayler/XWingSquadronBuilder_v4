using System;
using System.Collections.Generic;
using System.Linq;
using XWingSquadronBuilder_v4.DataLayer.RawData;
using XWingSquadronBuilder_v4.DataLayer.RawDataImporter;
using XWingSquadronBuilder_v4.Interfaces;

namespace XWingSquadronBuilder_v4.BusinessLogic.Repositories
{
   

    internal class ActionRepository : IActionRepository
    {
        private IReadOnlyList<ActionJson> actions { get; }

        private Func<ActionJson ,IAction> CreateAction { get; }

        public ActionRepository(Func<ActionJson, IAction> createAction)
        {
            CreateAction = createAction;
            actions = DataImporter.LoadActions();
        }

        public IReadOnlyList<IAction> GetAllActions()
        {
            return (from action in actions
                    select CreateAction(action)).ToList().AsReadOnly();
        }

        public IAction GetAction(string name)
        {
            return (from action in actions
                    where action.Name == name
                    select CreateAction(action)).Single();
        }
    }


}
