using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using XWingSquadronBuilder_v4.Interfaces;

namespace XWingSquadronBuilder_v4.DataLayer.Models.Models
{
    [DataContract]
    public class Faction : IFaction
    {
        [DataMember]
        public string Name { get; }
        [DataMember]
        public string Image { get; }

        public Faction(string name, string image)
        {
            Name = name;
            Image = image;
        }

        public override string ToString() => Name;

        public IFaction DeepClone()
        {
            return new Faction(this.Name, this.Image);
        }
    }
}
