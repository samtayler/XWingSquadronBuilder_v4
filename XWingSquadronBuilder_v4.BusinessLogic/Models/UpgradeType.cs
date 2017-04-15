using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XWingSquadronBuilder_v4.Interfaces;

namespace XWingSquadronBuilder_v4.BusinessLogic.Models
{
    public class UpgradeType : IUpgradeType
    {
        public UpgradeType(string name, string image)
        {
            Name = name;
            Image = image;
        }

        public string Image { get; }
        public string Name { get; }

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

        public IUpgradeType DeepClone()
        {
            return new UpgradeType(this.Name, this.Image);
        }
    }
}
