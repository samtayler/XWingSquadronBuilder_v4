using System;
using System.Linq;
using XWingSquadronBuilder_v4.BusinessLogic.Repositories;
using XWingSquadronBuilder_v4.DataLayer.RawData;
using XWingSquadronBuilder_v4.Interfaces;
using XWingSquadronBuilder_v4.BusinessLogic.Models;
using System.Collections.Generic;

namespace XWingSquadronBuilder_v4.BusinessLogic.Factories
{
    public class DataParsers
    {
        public static IAction CreateAction(ActionJson action)
        {
            return new Models.Action(action.Name, action.ImageSource);
        }

        public static IFaction CreateFaction(FactionJson faction)
        {
            return new Faction(faction.Name, faction.Image);
        }          

        public static IUpgradeType CreateUpgradeType(UpgradeTypeJson upgradeType)
        {
            return new UpgradeType(upgradeType.Name, upgradeType.ImageSource);
        }
    }
}
