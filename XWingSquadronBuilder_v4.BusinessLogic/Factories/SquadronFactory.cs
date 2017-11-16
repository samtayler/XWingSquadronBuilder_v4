using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XWingSquadronBuilder_v4.BusinessLogic.Models;
using XWingSquadronBuilder_v4.BusinessLogic.Models.NullModels;
using XWingSquadronBuilder_v4.DataLayer.RawData;
using XWingSquadronBuilder_v4.Interfaces;

namespace XWingSquadronBuilder_v4.BusinessLogic.Factories
{
    public class SquadronFactory
    {
        public static ISquadron CreateSquadron(IFaction faction)
        {
            return new Squadron(faction);
        }

        public static ISquadron CreateSquadron()
        {
            return new NullSquadron();
        } 
    }
}
