using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Template10.Common;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using XWingSquadronBuilder_v4.Presentation.Services.Navigation;
using XWingSquadronBuilder_v4.Presentation.ViewModels;

// The Templated Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234235

namespace XWingSquadronBuilder_v4.Presentation.UserControls
{
    public sealed class PilotListBase : Control
    {
        public List<ShipDisplay> Ships { get; }

        public PilotListBase(IEnumerable<ShipDisplay> ships)
        {
            this.DefaultStyleKey = typeof(PilotListBase);
            Ships = ships.ToList();
        }

        public PilotListBase()
        {
            this.DefaultStyleKey = typeof(PilotListBase);
            Ships = new List<ShipDisplay>();
        }        
    }
}
