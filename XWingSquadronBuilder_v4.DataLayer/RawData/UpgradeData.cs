using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace XWingSquadronBuilder_v4.DataLayer.RawData
{
    [DataContract]
    public class RestrictionJson
    {
        [DataMember] public string Attribute { get; set; } = "";
        [DataMember] public string Operand { get; set; } = "";
        [DataMember] public string Value { get; set; } = "";
    }

    [DataContract]
    public class AddedUpgradeJson
    {
        [DataMember] public string Type { get; set; } = "";
        [DataMember] public int CostReduction { get; set; } = 0;        
        [DataMember] public List<RestrictionJson> Restrictions { get; set; } = new List<RestrictionJson>();
        [DataMember] public string Upgrade { get; set; } = "";
        [DataMember] public bool Mutable { get; set; } = true;

        public override string ToString()
        {
            return $"{Type} - {Upgrade}";
        }

    }

    [DataContract]
    public class StatChangeJson
    {
        [DataMember] public string Type { get; set; } = "";
        [DataMember] public int Value { get; set; } = 0;
    }

    [DataContract]
    public class ChooseUpgradeJson
    {
        [DataMember] public string Type { get; set; } = "";
    }    

    [DataContract]
    public class UpgradeJson
    {
        [DataMember] public string Faction { get; set; } = "";
        [DataMember] public string Name { get; set; } = "";
        [DataMember] public int Cost { get; set; } = 0;        
        [DataMember] public string Type { get; set; } = "";
        [DataMember] public string Description { get; set; } = "";
        [DataMember] public bool Limited { get; set; } = false;
        [DataMember] public bool Unique { get; set; } = false;
        [DataMember] public List<AddedUpgradeJson> AddedUpgrades { get; set; } = new List<AddedUpgradeJson>();
        [DataMember] public List<RestrictionJson> Restrictions { get; set; } = new List<RestrictionJson>();
        [DataMember] public List<string> RemovedUpgrades { get; set; } = new List<string>();
        [DataMember] public List<string> RemovedActions { get; set; } = new List<string>();
        [DataMember] public List<StatChangeJson> StatChanges { get; set; } = new List<StatChangeJson>();
        [DataMember] public List<string> AddedActions { get; set; } = new List<string>();
        [DataMember] public List<ChooseUpgradeJson> ChooseUpgrade { get; set; } = new List<ChooseUpgradeJson>();

        public override string ToString()
        {
            return $"{Name} - {Type}";
        }
    }

    [DataContract]
    public class UpgradesJson
    {
        [DataMember] public UpgradeJson[] Upgrade { get; set; } = new UpgradeJson[] { };
    }

    [DataContract]
    public class UpgradesRootJson
    {
        [DataMember] public UpgradesJson Upgrades { get; set; } = new UpgradesJson();
    }
}







