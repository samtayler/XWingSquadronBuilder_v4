using System;
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
using XWingSquadronBuilder_v4.Interfaces;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace XWingSquadronBuilder_v4.Presentation.UserControls
{
    public sealed partial class UpgradeSlotList : UserControl
    {
        public delegate void UpgradeSlotSelectHandler(object sender, IUpgradeSlot e);

        public event UpgradeSlotSelectHandler UpgradeSlotSelected;

        public IEnumerable<IUpgradeSlot> UpgradeSlots
        {
            get { return (IEnumerable<IUpgradeSlot>)GetValue(UpgradeSlotsProperty); }
            set { SetValue(UpgradeSlotsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for UpgradeSlots.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UpgradeSlotsProperty =
            DependencyProperty.Register(nameof(UpgradeSlots), typeof(IEnumerable<IUpgradeSlot>), typeof(UpgradeSlotList), new PropertyMetadata(0));
        

        public UpgradeSlotList()
        {
            this.InitializeComponent();
        }

        private void ListViewItem_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (e.Handled) return;
            e.Handled = true;
            UpgradeSlotSelected?.Invoke(sender, ((FrameworkElement)e.OriginalSource).DataContext as IUpgradeSlot);
        }        

        private void btnClearUpgrade_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (e.Handled) return;
            e.Handled = true;
            ((IUpgradeSlot)((FrameworkElement)sender).DataContext).ClearUpgrade();
        }
    }
}
