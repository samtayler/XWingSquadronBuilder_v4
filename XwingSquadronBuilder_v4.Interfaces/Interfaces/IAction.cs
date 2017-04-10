﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XWingSquadronBuilder_v4.Interfaces
{
    public interface IAction : IEquatable<IAction>, IDeepCloneable<IAction>
    {
        string Name { get; }
        string Image { get; }
    }
}
