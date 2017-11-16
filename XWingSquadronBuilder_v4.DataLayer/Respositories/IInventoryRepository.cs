using System.Threading.Tasks;
using XWingSquadronBuilder_v4.DataLayer.RawData;

namespace XWingSquadronBuilder_v4.DataLayer.Respositories
{
    public interface IInventoryRepository
    {
        ActionJson[] ActionJson { get; }
        FactionJson[] FactionJson { get; }
        PilotJson[] PilotJson { get; }
        UpgradeJson[] UpgradeJson { get; }
        UpgradeTypeJson[] UpgradeTypeJson { get; }         
    }
}