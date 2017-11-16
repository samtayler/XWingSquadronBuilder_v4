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
using XWingSquadronBuilder_v4.Presentation.Services.Navigation;

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace XWingSquadronBuilder_v4.Presentation.UserControls
{
    public sealed partial class SaveSquadronDialog : ContentDialog
    {
        public SaveSquadronDialog()
        {
            this.InitializeComponent();
            var sessionState = Template10.Common.BootStrapper.Current.SessionState["State"] as IXWingSessionState;
            this.txbxName.Text = sessionState.ActiveSquadron.Squadron.Name;
            this.txbxDescription.Text = sessionState.ActiveSquadron.Squadron.Description;
        }

        private void SaveBtnClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            var sessionState = Template10.Common.BootStrapper.Current.SessionState["State"] as IXWingSessionState;
            sessionState.ActiveSquadron.Squadron.Name = this.txbxName.Text;
            sessionState.ActiveSquadron.Squadron.Description = this.txbxDescription.Text;  
            sessionState.XWingRepository.SaveSquadron(sessionState.ActiveSquadron.Squadron);
            Hide();
        }

        private void CancelBtnClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            Hide();
        }
    }
}
