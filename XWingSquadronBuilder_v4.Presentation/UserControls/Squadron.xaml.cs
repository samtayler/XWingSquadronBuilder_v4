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
using XWingSquadronBuilder_v4.Interfaces;
using XWingSquadronBuilder_v4.Presentation.ViewModels;
using XWingSquadronBuilder_v4.Presentation.ViewModels.XWingModels;
using XWingSquadronBuilder_v4.Presentation.ViewModels.XWingModels.Interfaces;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace XWingSquadronBuilder_v4.Presentation.UserControls
{
    public sealed partial class Squadron : UserControl
    {
        public delegate void PilotSelectedHandler(IPilotViewModel e);

        public event PilotSelectedHandler PilotSelected;

        public ISquadronViewModel ViewModel
        {
            get { return (ISquadronViewModel)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ViewModel.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ViewModelProperty =
            DependencyProperty.Register(nameof(ViewModel), typeof(ISquadronViewModel), typeof(Squadron), new PropertyMetadata(0));

        public Squadron()
        {
            this.InitializeComponent();
        }

        private void PilotControl_RemovePilot(object sender, IPilotViewModel e)
        {
            ViewModel.Squadron.RemovePilot(e.Pilot.Id);            
        }

        private void PilotControl_CopyPilot(object sender, IPilotViewModel e)
        {
            ViewModel.Squadron.AddPilot(e.Pilot.DeepClone());
        }

        private void PilotControl_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var pilot = ((IPilotViewModel)(((UserControl)sender).DataContext));
            PilotSelected?.Invoke(pilot);
        }
    }
}
