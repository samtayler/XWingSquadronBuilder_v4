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

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace XWingSquadronBuilder_v4.Presentation.UserControls
{
    public sealed partial class UpgradeSelector : UserControl
    {
        public UpgradeSetter UpgradeSetter
        {
            get { return (UpgradeSetter)GetValue(UpgradeSetterProperty); }
            set { SetValue(UpgradeSetterProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Filter.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UpgradeSetterProperty =
            DependencyProperty.Register(nameof(UpgradeSetter), typeof(UpgradeSetter), typeof(UpgradeSelector), new PropertyMetadata(0));

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
            UpgradeSetter.SetUpgrade(((FrameworkElement)e.OriginalSource).DataContext as IUpgrade);
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
