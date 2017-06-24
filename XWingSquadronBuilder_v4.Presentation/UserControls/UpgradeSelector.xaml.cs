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
using XWingSquadronBuilder_v4.BusinessLogic.Logic;
using XWingSquadronBuilder_v4.Interfaces;
using XWingSquadronBuilder_v4.Presentation.Converters;
using XWingSquadronBuilder_v4.Presentation.ViewModels;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace XWingSquadronBuilder_v4.Presentation.UserControls
{
    public sealed partial class UpgradeSelector : UserControl
    {
        public delegate void UpgradeSelectedHandler(IUpgrade e);

        public event UpgradeSelectedHandler UpgradeSelected;

        public IEnumerable<UpgradeViewModel> Upgrades
        {
            get { return (IEnumerable<UpgradeViewModel>)GetValue(UpgradesProperty); }
            set { SetValue(UpgradesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Upgrades.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UpgradesProperty =
            DependencyProperty.Register("Upgrades", typeof(IEnumerable<UpgradeViewModel>), typeof(UpgradeSelector), new PropertyMetadata(0));


        public UpgradeSelector()
        {
            this.InitializeComponent();
        }

        private void Grid_Tapped(object sender, TappedRoutedEventArgs e)
        {
            e.Handled = true;
            Visibility = Visibility.Collapsed;
        }

        private void ListViewItem_Tapped(object sender, TappedRoutedEventArgs e)
        {
            e.Handled = true;
            var upgrade = (((e.OriginalSource as FrameworkElement).DataContext as UpgradeViewModel).Upgrade);
            UpgradeSelected?.Invoke(upgrade);            
            Visibility = Visibility.Collapsed;
        }

        public IEnumerable<TextBlock> AugmentText(string text, double fontsize)
        {
            return XWingTextAugmenter.AugementWithXWingIcons(text, fontsize);
        }

        private void Grid_Tapped_1(object sender, TappedRoutedEventArgs e)
        {
            e.Handled = true;
        }
    }
}
