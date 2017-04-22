﻿using System;
using System.Collections.Generic;
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
using XWingSquadronBuilder_v4.BusinessLogic.Models;
using XWingSquadronBuilder_v4.Interfaces;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace XWingSquadronBuilder_v4.Presentation.UserControls
{
    public sealed partial class UpgradePickerPopup : FlyoutBase
    {
        public IEnumerable<IUpgrade> Upgrades { get; set; }
        public IUpgrade SelectedUpgrade { get; private set; }

        public UpgradePickerPopup()
        {
            this.InitializeComponent();
        }

        private void ListViewItem_Tapped(object sender, TappedRoutedEventArgs e)
        {
            SelectedUpgrade = ((FrameworkElement)e.OriginalSource).DataContext as IUpgrade;              
        }
    }
}