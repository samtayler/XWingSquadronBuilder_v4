using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XWingSquadronBuilder_v4.Interfaces;

namespace XWingSquadronBuilder_v4.Presentation.ViewModels
{
    public class ShipDisplay
    {
        public ShipDisplay(string icon, string shipName, IReadOnlyList<IPilot> pilots)
        {
            Icon = icon;
            ShipName = shipName;
            Pilots = pilots;
        }

        public string Icon { get; }
        public string ShipName { get; }
        public IReadOnlyList<IPilot> Pilots { get; set; }
    }
}
