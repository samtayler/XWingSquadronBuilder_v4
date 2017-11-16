using System;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using XWingSquadronBuilder_v4.Interfaces;
using XWingSquadronBuilder_v4.Presentation.Converters;
using XWingSquadronBuilder_v4.Presentation.ViewModels;
using XWingSquadronBuilder_v4.Presentation.ViewModels.XWingModels;
using XWingSquadronBuilder_v4.Presentation.ViewModels.XWingModels.Interfaces;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace XWingSquadronBuilder_v4.Presentation.UserControls
{
    public sealed partial class PilotControl : UserControl
    {
        public delegate void PilotRemoveSelectedHandler(object sender, IPilotViewModel e);

        public event PilotRemoveSelectedHandler RemovePilot;

        public delegate void PilotCopySelectedHandler(object sender, IPilotViewModel e);

        public event PilotCopySelectedHandler CopyPilot;       

        public IPilotViewModel ViewModel
        {
            get { return (IPilotViewModel)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ViewModel.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ViewModelProperty =
            DependencyProperty.Register("ViewModel", typeof(IPilotViewModel), typeof(PilotControl), new PropertyMetadata(0));
        

        public PilotControl()
        {
            this.InitializeComponent();
        }    

        private void btnDelete_Tapped(object sender, TappedRoutedEventArgs e)
        {
            e.Handled = true;
            RemovePilot?.Invoke(sender, ViewModel);
        }

        private void btnCopy_Tapped(object sender, TappedRoutedEventArgs e)
        {
            e.Handled = true;
            CopyPilot?.Invoke(sender, ViewModel);
        }        
    }
}
