using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using XWingSquadronBuilder_v4.Interfaces;

namespace XWingSquadronBuilder_v4.DataLayer.Models
{
    [DataContract]
    public class UpgradeType : IUpgradeType
    {
        public UpgradeType(string name, string image)
        {
            Name = name;
            Image = image;
        }

        [DataMember]
        public string Image { get; }
        [DataMember]
        public string Name { get; }

        public override string ToString() => Name;        

        public int CompareTo(IUpgradeType other) => Name.CompareTo(other.Name);

        public bool Equals(IUpgradeType other) => Name.Equals(other.Name);

        public IUpgradeType DeepClone() => new UpgradeType(this.Name, this.Image);

    }
}
