using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XWingSquadronBuilder_v4.Interfaces
{
    public interface IXWingCard
    {
        string Name { get; }
        bool Unique { get; }
        Guid Id { get; }
    }
}
