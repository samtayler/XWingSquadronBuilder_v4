using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace XWingSquadronBuilder_v4.DataLayer.RawData
{

    [DataContract]
    public class PilotsJsonRoot
    {
        [DataMember] public PilotJson[] Pilots { get; set; }
    }

    [DataContract]
    public class PilotJson
    {
        [DataMember] public string Image { get; set; } = "";
        [DataMember] public string Name { get; set; } = "";
        [DataMember] public string[] Upgrades { get; set; } = new string[0];
        [DataMember] public string Faction { get; set; } = "";
        [DataMember] public string ShipSize { get; set; } = "";
        [DataMember] public string ShipIcon { get; set; } = "";
        [DataMember] public string ShipName { get; set; } = "";
        [DataMember] public string[] Actions { get; set; } = new string[0];
        [DataMember] public StatsJson Stats { get; set; } = new StatsJson();
        [DataMember] public int Cost { get; set; } = 0;
        [DataMember] public bool Unique { get; set; } = false;
        [DataMember] public string PilotAbility { get; set; } = "";
        [DataMember] public int PilotSkill { get; set; } = 0;
    }

    [DataContract]
    public class StatsJson
    {
        [DataMember] public int Shield { get; set; } = 0;
        [DataMember] public int Hull { get; set; } = 0;
        [DataMember] public int Aglilty { get; set; } = 0;
        [DataMember] public int Attack { get; set; } = 0;
    }
}
