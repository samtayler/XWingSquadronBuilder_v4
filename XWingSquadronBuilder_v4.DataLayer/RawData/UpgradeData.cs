using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace XWingSquadronBuilder_v4.DataLayer.RawData
{
    [DataContract]
    public class UpgradeRootobject
    {
        [DataMember]
        public UpgradesJson Upgrades { get; set; }
    }

    [DataContract]
    public class UpgradesJson
    {
        [DataMember]
        public UpgradeJson[] Upgrade { get; set; }
    }

    [DataContract]
    public class UpgradeJson
    {
        [DataMember]
        public string Faction { get; set; } = "";
        [DataMember]
        public string Name { get; set; } = "";
        [DataMember]
        public int Cost { get; set; } = 0;
        [DataMember]
        public int SlotsRequired { get; set; }
        [DataMember]
        public string Type { get; set; } = "";
        [DataMember]
        public string Description { get; set; } = "";
        [DataMember]
        public bool Unique { get; set; } = false;
        [DataMember]
        public string SizeRestriction { get; set; } = "";
        [DataMember]
        public bool Limited { get; set; } = false;
        [DataMember]
        public StatChange[] StatChanges { get; set; } = new StatChange[0];
        [DataMember]
        public string ShipLimited { get; set; } = "";
        [DataMember]
        public string ActionLimited { get; set; } = "";
        [DataMember]
        public string[] AddedActions { get; set; } = new string[0];
        [DataMember]
        public string[] RemovedActions { get; set; } = new string[0];
        [DataMember]
        public AddedUpgrade[] AddedUpgrades { get; set; } = new AddedUpgrade[0];
        [DataMember]
        public string[] RemovedUpgrades { get; set; } = new string[0];
        [DataMember]
        public ChooseUpgrade[] ChooseUpgrade { get; set; } = new ChooseUpgrade[0];
        [DataMember]
        public string[] DisabledUpgrades { get; set; } = new string[0];

        public override string ToString()
        {
            return $"{Name}";
        }
    }

    [DataContract]
    public class StatChange
    {
        [DataMember]
        public string Type { get; set; } = "";
        [DataMember]
        public int Value { get; set; } = 0;
    }
    [DataContract]
    public class AddedUpgrade
    {
        [DataMember]
        public string Type { get; set; } = "";
        [DataMember]
        public int CostReduction { get; set; } = 0;
        [DataMember]
        public int CostLimit { get; set; } = 100;
    }
    [DataContract]
    public class ChooseUpgrade
    {
        [DataMember]
        public string Type { get; set; } = "";
        [DataMember]
        public int CostReduction { get; set; } = 0;
        [DataMember]
        public int CostLimit { get; set; } = 0;
    }
}







