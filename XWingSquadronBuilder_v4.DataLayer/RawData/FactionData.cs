using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace XWingSquadronBuilder_v4.DataLayer.RawData
{
    [DataContract]
    public class FactionJsonRoot
    {
        [DataMember]
        public FactionJson[] Factions { get; set; }
    }
    [DataContract]
    public class FactionJson
    {
        [DataMember]
        public string Name { get; set; } = "";
        [DataMember]
        public string Image { get; set; } = "";
    } 


}
