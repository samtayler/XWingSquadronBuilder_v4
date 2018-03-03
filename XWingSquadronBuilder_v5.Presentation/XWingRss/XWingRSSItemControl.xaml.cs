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

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace XWingSquadronBuilder_v5.Presentation.XWingRss
{
    public sealed partial class XWingRSSItemControl : UserControl
    {
        public XWingRssItem RssItem
        {
            get { return (XWingRssItem)GetValue(RssItemProperty); }
            set { SetValue(RssItemProperty, value); }
        }

        // Using a DependencyProperty as the backing store for RssItem.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RssItemProperty =
            DependencyProperty.Register("RssItem", typeof(XWingRssItem), typeof(XWingRSSItemControl), new PropertyMetadata(0));


        public XWingRSSItemControl()
        {
            this.InitializeComponent();
        }

        private async void UserControl_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var success = await Windows.System.Launcher.LaunchUriAsync(RssItem.Link);

            if (success)
            {
                // URI launched
            }
            else
            {
                // URI launch failed
            }

        }
    }
}
