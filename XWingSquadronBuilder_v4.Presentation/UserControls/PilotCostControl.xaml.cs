using System;
using System.Collections.Generic;
using System.ComponentModel;
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

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace XWingSquadronBuilder_v4.Presentation.UserControls
{
    public sealed partial class PilotCostControl : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private int upgradesCost;

        public int UpgradesCost
        {
            get { return upgradesCost; }
            set
            {
                upgradesCost = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(UpgradesCost)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TotalCost)));
            }
        }

        private int pilotCost;

        public int PilotCost
        {
            get { return pilotCost; }
            set
            {
                pilotCost = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(PilotCost)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TotalCost)));

            }
        }

        public int TotalCost
        {
            get
            {
                return PilotCost + UpgradesCost;
            }
        }

        public PilotCostControl()
        {
            this.InitializeComponent();
        }
    }
}
