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

        public UpgradeSelectorViewModel ViewModel
        {
            get { return (UpgradeSelectorViewModel)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ViewModel.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ViewModelProperty =
            DependencyProperty.Register("ViewModel", typeof(UpgradeSelectorViewModel), typeof(UpgradeSelector), new PropertyMetadata(0));
        

        public UpgradeSelector()
        {
            this.InitializeComponent();
            Visibility = Visibility.Collapsed;

        }

        private void Grid_Tapped(object sender, TappedRoutedEventArgs e)
        {
            e.Handled = true;
            ViewModel.Cancel();           
        }

        private void ListViewItem_Tapped(object sender, TappedRoutedEventArgs e)
        {
            e.Handled = true;           
            ViewModel.SelectUpgrade(((e.OriginalSource as FrameworkElement).DataContext as UpgradeViewModel));            
        }

        public IEnumerable<TextBlock> AugmentText(string text, double fontsize)
        {
            return XWingTextAugmenter.AugementWithXWingIcons(text, fontsize);
        }

        private void Grid_Tapped_1(object sender, TappedRoutedEventArgs e)
        {
            e.Handled = true;
        }

        private void btnShowErrors_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var button = sender as Button;
            var flyout = button.Flyout;
            flyout.ShowAt(button);
            
        }
    }
}
