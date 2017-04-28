using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XWingSquadronBuilder_v4.Interfaces;

namespace XWingSquadronBuilder_v4.BusinessLogic.Models
{
    public class Faction : IFaction
    {
        public string Name { get; }
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

        public bool Equals(IFaction other) => Name.Equals(other.Name);
    }
}
