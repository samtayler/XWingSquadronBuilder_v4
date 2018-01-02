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
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using XWingSquadronBuilder_v4.Presentation.Converters;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace XWingSquadronBuilder_v4.Presentation.UserControls
{
    public sealed partial class XWingFormattedTextBlock : UserControl
    {
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set
            {
                SetValue(TextProperty, value);
                ConvertText(value);
            }
        }

        private void ConvertText(string value)
        {
            txblkUpgradeText.Inlines.Clear();
            foreach (var item in XWingTextAugmenter.AugementWithXWingIcons(value, FontSize, FontStyle))
            {
                txblkUpgradeText.Inlines.Add(item);
            }            
        }

        // Using a DependencyProperty as the backing store for Text.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register(nameof(Text), typeof(string), typeof(XWingFormattedTextBlock), new PropertyMetadata(""));


        public IEnumerable<Run> ConvertedText
        {
            get { return (IEnumerable<Run>)GetValue(ConvertedTextProperty); }
            set { SetValue(ConvertedTextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ConvertedText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ConvertedTextProperty =
            DependencyProperty.Register(nameof(ConvertedText), typeof(IEnumerable<Run>), typeof(XWingFormattedTextBlock), new PropertyMetadata(new List<Run>()));

        public TextAlignment TextAlignment
        {
            get { return (TextAlignment)GetValue(TextAlignmentProperty); }
            set { SetValue(TextAlignmentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TextAlignment.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextAlignmentProperty =
            DependencyProperty.Register(nameof(TextAlignment), typeof(TextAlignment), typeof(XWingFormattedTextBlock), new PropertyMetadata(TextAlignment.Left));

        public XWingFormattedTextBlock()
        {
            this.InitializeComponent();
        }
    }
}
