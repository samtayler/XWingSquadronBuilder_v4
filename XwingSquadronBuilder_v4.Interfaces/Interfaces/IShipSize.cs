using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XWingSquadronBuilder_v4.Interfaces
{
    public interface IShipSize : IComparable<IShipSize>, IEquatable<IShipSize>, IDeepCloneable<IShipSize>
    {
        string Size { get; }
    }
}
