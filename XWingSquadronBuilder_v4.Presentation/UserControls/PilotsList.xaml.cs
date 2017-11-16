using System.Collections.Generic;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using XWingSquadronBuilder_v4.BusinessLogic.Repositories;
using XWingSquadronBuilder_v4.Interfaces;
using XWingSquadronBuilder_v4.Presentation.ViewModels;


// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace XWingSquadronBuilder_v4.Presentation.UserControls
{
    public sealed partial class PilotsList : UserControl
    {      
        public delegate void PilotSelectedEventHandler(IPilot e);

        public event PilotSelectedEventHandler PilotSelected;

        public IReadOnlyList<ShipDisplay> Ships
        {
            get { return (IReadOnlyList<ShipDisplay>)GetValue(ShipsProperty); }
            set { SetValue(ShipsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Ships.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ShipsProperty =
            DependencyProperty.Register(nameof(Ships), typeof(IReadOnlyList<ShipDisplay>), typeof(PilotsList), new PropertyMetadata(new List<ShipDisplay>()));


        public PilotsList()
        {
            this.InitializeComponent();            

        }

        private void GridViewItem_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            ShipDisplay ship = ((FrameworkElement)e.OriginalSource).DataContext as ShipDisplay;
            if (ship == null) return;

            //PilotSelector.ItemsSource = ship.Pilots;
            //PilotSelector.Visibility = Visibility.Visible;
            //ShipSelector.Visibility = Visibility.Collapsed;
        }

        private void GridViewItem_Tapped_1(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            IPilot pilot = ((FrameworkElement)e.OriginalSource).DataContext as IPilot;
            if (pilot == null) return;

            this.Visibility = Visibility.Collapsed;
            //PilotSelector.Visibility = Visibility.Collapsed;
            //ShipSelector.Visibility = Visibility.Visible;
            //PilotSelector.ItemsSource = null;
            PilotSelected?.Invoke(pilot);

        }
    }
}
