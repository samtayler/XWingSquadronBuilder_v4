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
using XWingSquadronBuilder_v4.BusinessLogic.Models;
using XWingSquadronBuilder_v4.BusinessLogic.Models.NullModels;
using XWingSquadronBuilder_v4.Interfaces;
using XWingSquadronBuilder_v4.Presentation.ViewModels;
using XWingSquadronBuilder_v4.Presentation.ViewModels.XWingModels.Interfaces;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace XWingSquadronBuilder_v4.Presentation.UserControls
{
    public sealed partial class UpgradeSlotList : UserControl
    {


        public bool ShowEmptyUpgrades
        {
            get { return (bool)GetValue(ShowEmptyUpgradesProperty); }
            set { SetValue(ShowEmptyUpgradesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ShowEmptyUpgrades.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ShowEmptyUpgradesProperty =
            DependencyProperty.Register("ShowEmptyUpgrades", typeof(bool), typeof(UpgradeSlotList), new PropertyMetadata(true));


        public static IUpgradeSlotViewModel designTimeModel = new UpgradeSlotViewModel(new UpgradeSlot(new NullUpgradeType(), new NullUpgrade(new NullUpgradeType()), new List<IXWingSpecification<IUpgrade>>()));

        public delegate void UpgradeSlotSelectHandler(object sender, IUpgradeSlotViewModel e);

        public event UpgradeSlotSelectHandler UpgradeSlotSelected;

        public IReadOnlyList<IUpgradeSlotViewModel> UpgradeSlots
        {
            get
            {
                if (ShowEmptyUpgrades)
                {
                    return (IReadOnlyList<IUpgradeSlotViewModel>)GetValue(UpgradeSlotsProperty);
                }
                else
                {
                    return ((IReadOnlyList<IUpgradeSlotViewModel>)GetValue(UpgradeSlotsProperty)).Where(x => !x.UpgradeSlot.IsNullUpgrade).ToList().AsReadOnly();
                }

            }
            set { SetValue(UpgradeSlotsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for UpgradeSlots.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UpgradeSlotsProperty =
            DependencyProperty.Register(nameof(UpgradeSlots), typeof(IReadOnlyList<IUpgradeSlotViewModel>), typeof(UpgradeSlotList), new PropertyMetadata(new List<IUpgradeSlotViewModel>() { designTimeModel }));


        public UpgradeSlotList()
        {
            this.InitializeComponent();
        }

        private void ListViewItem_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (e.Handled) return;
            e.Handled = true;
            UpgradeSlotSelected?.Invoke(sender, (((FrameworkElement)e.OriginalSource).DataContext as IUpgradeSlotViewModel));
        }

        private void btnClearUpgrade_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (e.Handled) return;
            e.Handled = true;
            ((IUpgradeSlotViewModel)((FrameworkElement)sender).DataContext).UpgradeSlot.ClearUpgrade();
        }
    }
}
