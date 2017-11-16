using System;
using System.Collections.Generic;

namespace XWingSquadronBuilder_v4.Interfaces
{
    public interface IUpgradeType : IEquatable<IUpgradeType>, IComparable<IUpgradeType>, IDeepCloneable<IUpgradeType>
    {     
        string Name { get; }
        string ImageUri { get; }           
    }
}