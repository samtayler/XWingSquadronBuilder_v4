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

namespace XWingSquadronBuilder_v4.Presentation.UserControls
{    
    public sealed partial class PopupControl : UserControl
    {
        public object InternalContent
        {
            get { return (object)GetValue(InternalContentProperty); }
            set { SetValue(InternalContentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for InternalContent.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty InternalContentProperty =
            DependencyProperty.Register("InternalContent", typeof(object), typeof(PopupControl), new PropertyMetadata(0));

        public PopupControl()
        {
            this.InitializeComponent();
        }

        private void Grid_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Visibility = Visibility.Collapsed;
        }
    }
}
