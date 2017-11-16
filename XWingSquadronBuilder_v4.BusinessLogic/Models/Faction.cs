using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using XWingSquadronBuilder_v4.Interfaces;

namespace XWingSquadronBuilder_v4.BusinessLogic.Models
{
    [DataContract]
    public class Faction : IFaction
    {
        [DataMember]
        public string Name { get; }
        [DataMember]
        public string ImageUri { get; }

        public Faction(string name, string imageUri)
        {
            Name = name;
            ImageUri = imageUri;
        }        

        public override string ToString() => Name;

        public IFaction DeepClone()
        {
            return new Faction(this.Name, this.ImageUri);
        }

        public bool Equals(IFaction other) => Name.Equals(other.Name);

        internal static IFaction CreateFaction(string name, string imageUri)
        {
            return new Faction(name, imageUri);
        }
    }
}
