using System.Collections.Generic;
using System.Runtime.Serialization;

namespace XWingSquadronBuilder_v4.DataLayer.RawData
{
    [DataContract]
    public class UpgradeTypesRootobject
    {
        [DataMember]
        public UpgradeTypesJson UpgradeTypes { get; set; }
    }
    [DataContract]
    public class UpgradeTypesJson
    {
        [DataMember]
        public UpgradeTypeJson[] UpgradeType { get; set; }
    }
    [DataContract]
    public class UpgradeTypeJson
    {
        [DataMember]
        public string Name { get; set; } = "";
        [DataMember]
        public string ImageSource { get; set; } = "";
    }
}
