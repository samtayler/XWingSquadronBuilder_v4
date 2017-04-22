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
using XWingSquadronBuilder_v4.BusinessLogic.Models;
using XWingSquadronBuilder_v4.Interfaces;

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace XWingSquadronBuilder_v4.Presentation.UserControls
{
    public sealed partial class UpgradePicker : ContentDialog
    {

        public List<IUpgrade> Upgrades
        {
            get { return (List<IUpgrade>)GetValue(UpgradesProperty); }
            set
            {
                SetValue(UpgradesProperty, value);
                SelectedUpgrade = new NullUpgrade(value.FirstOrDefault().UpgradeType);
            }
        }

        // Using a DependencyProperty as the backing store for Upgrades.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UpgradesProperty =
            DependencyProperty.Register("Upgrades", typeof(List<IUpgrade>), typeof(UpgradePicker), new PropertyMetadata(new List<IUpgrade>()));

        public IUpgrade SelectedUpgrade { get; private set; }

        public UpgradePicker()
        {
            this.InitializeComponent();
            
        }        

        private void ListViewItem_Tapped(object sender, TappedRoutedEventArgs e)
        {
            SelectedUpgrade = ((FrameworkElement)e.OriginalSource).DataContext as IUpgrade;
            Hide();
        }
    }
}
