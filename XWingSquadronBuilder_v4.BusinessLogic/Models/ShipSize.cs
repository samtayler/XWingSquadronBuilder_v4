using System;
using System.Linq;
using System.Runtime.Serialization;
using XWingSquadronBuilder_v4.Interfaces;

namespace XWingSquadronBuilder_v4.BusinessLogic.Models
{
    [DataContract]
    public class ShipSize : IShipSize
    {
        public ShipSize(string size)
        {
            this.Size = size;
        }       

        [DataMember]
        public string Size { get; }        

        public override string ToString()
        {
            return Size;
        }

        public int CompareTo(IShipSize other)
        {
            return ToString().CompareTo(other.ToString());
        }

        public bool Equals(IShipSize other)
        {
            return ToString().Equals(other.ToString());
        }        

        public IShipSize DeepClone()
        {
            return new ShipSize(this.Size);
        }
    }
}
