using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace XWingSquadronBuilder_v4.DataLayer.RawData
{
    [DataContract]
    public class RootSaveData
    {
        [DataMember]
        public SquadronSaveData[] Squadrons { get; set; } = new SquadronSaveData[0];
    }

    [DataContract]
    public class SquadronSaveData
    {
        public SquadronSaveData(Guid id, string name, string description, int costCap, string faction)
        {
            Id = id;
            Name = name;
            Description = description;
            CostCap = costCap;
            Faction = faction;
        }

        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public List<PilotSaveData> Pilots { get; set; } = new List<PilotSaveData>();
        [DataMember]
        public int CostCap { get; set; }
        [DataMember]
        public string Faction { get; set; }
        [DataMember]
        public Guid Id { get; set; }
    }

    [DataContract]
    public class PilotSaveData
    {
        public PilotSaveData(string name, string shipName)
        {
            Name = name;
            ShipName = shipName;
        }

        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string ShipName { get; set; }
        [DataMember]
        public List<UpgradeSaveData> Upgrades { get; set; } = new List<UpgradeSaveData>();
    }

    [DataContract]
    public class UpgradeSaveData
    {
        public UpgradeSaveData(string name, string upgradeType)
        {
            Name = name;
            UpgradeType = upgradeType;
        }

        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string UpgradeType { get; set; }
    }
}
