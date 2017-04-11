using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using XWingSquadronBuilder_v4.BusinessLogic.ViewModels;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace XWingSquadronBuilder_v4.UserControls
{
    public sealed partial class PilotControl : UserControl
    {
        public delegate void PilotRemoveSelectedHandler(object sender, PilotViewModel e);

        public event PilotRemoveSelectedHandler RemovePilot;

        public delegate void PilotCopySelectedHandler(object sender, PilotViewModel e);

        public event PilotCopySelectedHandler CopyPilot;

        public delegate void UpgradeSlotSelectedHandler(object sender, UpgradeSlotViewModel e);

        public event UpgradeSlotSelectedHandler UpgradeSlotSelected;

        public PilotViewModel ViewModel
        {
            get { return (PilotViewModel)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ViewModel.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ViewModelProperty =
            DependencyProperty.Register(nameof(ViewModel), typeof(PilotViewModel), typeof(PilotControl), new PropertyMetadata(0));

        public PilotControl()
        {
            this.InitializeComponent();
        }
    }
}
