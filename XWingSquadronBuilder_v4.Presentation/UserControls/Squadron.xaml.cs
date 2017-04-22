using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using XWingSquadronBuilder_v4.Interfaces;
using XWingSquadronBuilder_v4.Presentation.ViewModels;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace XWingSquadronBuilder_v4.Presentation.UserControls
{
    public sealed partial class Squadron : UserControl
    {
        public delegate void PilotRemoveSelectedHandler(object sender, IPilot e);

        public event PilotRemoveSelectedHandler RemovePilot;

        public delegate void PilotCopySelectedHandler(object sender, IPilot e);

        public event PilotCopySelectedHandler CopyPilot;

        public delegate void UpgradeSlotSelectedHandler(object sender, IUpgradeSlot e);

        public event UpgradeSlotSelectedHandler UpgradeSlotSelected;

        public ObservableCollection<IPilot> Pilots
        {
            get { return (ObservableCollection<IPilot>)GetValue(PilotsProperty); }
            set { SetValue(PilotsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Pilots.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PilotsProperty =
            DependencyProperty.Register(nameof(Pilots), typeof(ObservableCollection<IPilot>), typeof(Squadron), new PropertyMetadata(0));

        public Squadron()
        {
            this.InitializeComponent();
        }

        private void PilotControl_RemovePilot(object sender, Interfaces.IPilot e)
        {
            RemovePilot?.Invoke(sender, e);
        }

        private void PilotControl_CopyPilot(object sender, Interfaces.IPilot e)
        {
            CopyPilot?.Invoke(sender, e);
        }

        private void PilotControl_UpgradeSlotSelected(object sender, IUpgradeSlot e)
        {
            UpgradeSlotSelected?.Invoke(sender, e);
        }
    }
}
