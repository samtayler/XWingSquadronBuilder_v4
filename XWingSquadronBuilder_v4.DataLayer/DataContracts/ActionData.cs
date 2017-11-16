using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace XWingSquadronBuilder_v4.DataLayer.RawData
{
    [DataContract]
    public class ActionsRootobject
    {
        [DataMember]
        public ActionJson[] Actions { get; set; }
    }

    [DataContract]
    public class ActionJson
    {
        [DataMember]
        public string Name { get; set; } = "";
        [DataMember]
        public string ImageSource { get; set; } = "";
    }
}
