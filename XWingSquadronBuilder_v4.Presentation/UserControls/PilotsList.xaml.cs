﻿using Microsoft.Toolkit.Uwp.UI.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
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


// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace XWingSquadronBuilder_v4.Presentation.UserControls
{
    public sealed partial class PilotsList : UserControl
    {
        private Expander previousExpander;

        public delegate void PilotSelectedEventHandler(IPilot e);

        public event PilotSelectedEventHandler PilotSelected;

        public IReadOnlyList<IPilot> Pilots
        {
            get { return (IReadOnlyList<IPilot>)GetValue(PilotsProperty); }
            set
            {
                SetValue(PilotsProperty, value);
                GroupedPilots = Pilots.OrderBy(pilot => pilot.ShipName).ThenBy(x => x.PilotSkill).ThenBy(x => x.Cost).ThenBy(x => x.Name).GroupBy(x => x.ShipName).ToList();                
            }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PilotsProperty =
            DependencyProperty.Register(nameof(Pilots), typeof(IReadOnlyList<IPilot>), typeof(PilotsList), new PropertyMetadata(new List<IPilot>()));



        private List<IGrouping<string, IPilot>> GroupedPilots
        {
            get { return (List<IGrouping<string, IPilot>>)GetValue(GroupedPilotsProperty); }
            set { SetValue(GroupedPilotsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty GroupedPilotsProperty =
            DependencyProperty.Register("GroupedPilots", typeof(List<IGrouping<string, IPilot>>), typeof(PilotsList), new PropertyMetadata(0));


        public PilotsList()
        {
            this.InitializeComponent();
        }

        private void ShipList_ItemClick(object sender, ItemClickEventArgs e)
        {
            PilotSelected?.Invoke((e.ClickedItem as IPilot).DeepClone());
        }        

        private void Expander1_Expanded(object sender, EventArgs e)
        {
            scrollViewer.ScrollToElement(sender as UIElement);
            if (previousExpander != null && previousExpander != sender as Expander)
                previousExpander.IsExpanded = false;
            previousExpander = sender as Expander;
        }
    }
}
