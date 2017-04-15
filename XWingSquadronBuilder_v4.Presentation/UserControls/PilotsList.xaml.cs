﻿using System;
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
using XWingSquadronBuilder_v4.BusinessLogic.ViewModels;
using XWingSquadronBuilder_v4.Interfaces;


// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace XWingSquadronBuilder_v4.UserControls
{
    public sealed partial class PilotsList : UserControl
    {
        public delegate void PilotSelectedEventHandler(object sender, PilotViewModel e);

        public event PilotSelectedEventHandler PilotSelected;       

        public IEnumerable<IPilot> Pilots
        {
            get { return (IEnumerable<IPilot>)GetValue(PilotsProperty); }
            set { SetValue(PilotsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PilotsProperty =
            DependencyProperty.Register(nameof(Pilots), typeof(IEnumerable<IPilot>), typeof(PilotsList), new PropertyMetadata(0));


        public PilotsList()
        {
            this.InitializeComponent();
        }        
    }
}
