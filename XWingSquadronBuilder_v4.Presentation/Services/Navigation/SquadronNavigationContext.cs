using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XWingSquadronBuilder_v4.Interfaces;

namespace XWingSquadronBuilder_v4.Presentation.Services.Navigation
{
    public class SquadronNavigationContext
    {
        public SquadronNavigationContext(ISquadron squadron)
        {
            Squadron = squadron;
        }

        public ISquadron Squadron { get; }
    }

    public class SquadronNavigationContext<T> : SquadronNavigationContext
    {
        public SquadronNavigationContext(ISquadron squadron, T parameter) : base(squadron)
        {
            Parameter = parameter;
        }

        public T Parameter { get; }
    }
}
