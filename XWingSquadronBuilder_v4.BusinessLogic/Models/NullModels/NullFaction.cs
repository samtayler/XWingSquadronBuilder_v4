using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XWingSquadronBuilder_v4.Interfaces;

namespace XWingSquadronBuilder_v4.BusinessLogic.Models.NullModels
{
    public class NullFaction : IFaction
    {
        public string Name => "";

        public string ImageUri => "";

        public IFaction DeepClone()
        {
            return this;
        }

        public bool Equals(IFaction other)
        {
            return Name == other.Name && ImageUri == other.ImageUri;
        }
    }
}
