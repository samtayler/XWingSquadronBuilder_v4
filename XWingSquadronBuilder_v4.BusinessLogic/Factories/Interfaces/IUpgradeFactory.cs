using XWingSquadronBuilder_v4.BusinessLogic.Models;
using XWingSquadronBuilder_v4.DataLayer.RawData;
using XWingSquadronBuilder_v4.Interfaces;

namespace XWingSquadronBuilder_v4.BusinessLogic.Factories
{
    public interface IUpgradeFactory
    {
        IUpgrade CreateUpgrade(UpgradeJson upgrade,UpgradeModifierParsersBase upgradeModifierParsers);
        IUpgrade CreateNullUpgrade(IUpgradeType type);

    }
}