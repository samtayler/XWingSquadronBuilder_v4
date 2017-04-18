﻿using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using XWingSquadronBuilder_v4.Interfaces;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace XWingSquadronBuilder_v4.Presentation.UserControls
{
    public sealed partial class PilotControl : UserControl
    {
        public delegate void PilotRemoveSelectedHandler(object sender, IPilot e);

        public event PilotRemoveSelectedHandler RemovePilot;

        public delegate void PilotCopySelectedHandler(object sender, IPilot e);

        public event PilotCopySelectedHandler CopyPilot;

        public delegate void UpgradeSlotSelectedHandler(object sender, IUpgradeSlot e);

        public event UpgradeSlotSelectedHandler UpgradeSlotSelected;

        public IPilot Pilot
        {
            get { return (IPilot)GetValue(PilotProperty); }
            set { SetValue(PilotProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ViewModel.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PilotProperty =
            DependencyProperty.Register(nameof(Pilot), typeof(IPilot), typeof(PilotControl), new PropertyMetadata(0));

        public PilotControl()
        {
            this.InitializeComponent();
        }

        private void UpgradeSlotList_UpgradeSlotSelected(object sender, IUpgradeSlot e)
        {
            UpgradeSlotSelected?.Invoke(this, e);
        }

        private void btnDelete_Tapped(object sender,TappedRoutedEventArgs e)
        {
            RemovePilot?.Invoke(this, Pilot);
        }

        private void btnCopy_Tapped(object sender, TappedRoutedEventArgs e)
        {
            CopyPilot?.Invoke(this, Pilot.DeepClone());
        }
    }
}
