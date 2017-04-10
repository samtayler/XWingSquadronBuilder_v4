using System;
using System.Linq;
using System.Runtime.Serialization;
using XWingSquadronBuilder_v4.Interfaces;

namespace XWingSquadronBuilder_v4.DataLayer.Models
{
    [DataContract]
    public class ShipSize : IShipSize
    {
        public ShipSize(string size)
        {            
            var names = Enum.GetNames(typeof(_shipSizeRep));
            if (!names.Contains(size)) throw new ArgumentException("The given size was not a valid size. Valid sizes - (small, large, huge)");

            this.size = (_shipSizeRep)Enum.Parse(typeof(_shipSizeRep), size);
        }

        private enum _shipSizeRep
        {
            Small,
            Large,
            Huge
        }

        [DataMember]
        private _shipSizeRep size { get; }

        public override string ToString()
        {
            return Enum.GetName(typeof(_shipSizeRep), size);
        }

        public int CompareTo(IShipSize other)
        {
            return ToString().CompareTo(other.ToString());
        }

        public bool Equals(IShipSize other)
        {
            return ToString().Equals(other.ToString());
        }

        private ShipSize(_shipSizeRep size)
        {
            this.size = size;
        }

        public IShipSize DeepClone()
        {
            return new ShipSize(this.size);
        }
    }
}
