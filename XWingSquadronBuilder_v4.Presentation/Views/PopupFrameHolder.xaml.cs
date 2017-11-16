using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Template10.Common;
using Template10.Services.NavigationService;
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
    public sealed partial class PopupFrameHolder : UserControl
    { 
        INavigationService navService { get; }

        public PopupFrameHolder()
        {
            this.InitializeComponent();
            navService = BootStrapper.Current.NavigationServiceFactory(Template10.Common.BootStrapper.BackButton.Ignore, Template10.Common.BootStrapper.ExistingContent.Exclude, ContentFrame);
            Visibility = Visibility.Collapsed;
        }

        public async void ShowAsync(Type page)
        {
            await navService.NavigateAsync(page);
            Visibility = Visibility.Visible;
        }

        public void Hide()
        {
            Visibility = Visibility.Collapsed;
        }        
    }
}
