using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using XWingSquadronBuilder_v4.BusinessLogic.Repositories;
using XWingSquadronBuilder_v4.Interfaces;

namespace XWingSquadronBuilder_v4.BusinessLogic.Models
{
    [DataContract]
    public class UpgradeType : IUpgradeType
    {
        private UpgradeType(string name, string imageUri)
        {
            Name = name;
            ImageUri = imageUri;
        }

        [DataMember]
        public string ImageUri { get; }
        [DataMember]
        public string Name { get; }          

        public static IUpgradeType CreateUpgradeType(string name, string imageUri)
        {
            return new UpgradeType(name, imageUri);
        }

        public override string ToString()
        {
            return Name;
        }

        public int CompareTo(IUpgradeType other)
        {
            return Name.CompareTo(other.Name);
        }

        public bool Equals(IUpgradeType other)
        {
            return Name.Equals(other.Name);
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }

        public IUpgradeType DeepClone()
        {
            return CreateUpgradeType(this.Name, this.ImageUri);
        }        
    }
}
