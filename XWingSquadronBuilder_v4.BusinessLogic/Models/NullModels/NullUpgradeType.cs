using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XWingSquadronBuilder_v4.Interfaces;

namespace XWingSquadronBuilder_v4.BusinessLogic.Models.NullModels
{
    public class NullUpgradeType : IUpgradeType
    {
        public string Name => "";

        public string ImageUri => "";

        public int CompareTo(IUpgradeType other)
        {
            throw new NotImplementedException();
        }

        public IUpgradeType DeepClone()
        {
            return this;
        }

        public bool Equals(IUpgradeType other)
        {
            return Name == other.Name && ImageUri == other.ImageUri;
        }
    }
}
