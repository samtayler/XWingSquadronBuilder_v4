using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace XWingSquadronBuilder_v4.Presentation.Services.Printing
{
    /// <summary>
    /// Page content to send to the printer
    /// </summary>
    public sealed partial class PageToPrint : Page
    {
        public RichTextBlock TextContentBlock { get; set; }

        public PageToPrint()
        {
            this.InitializeComponent();
            TextContentBlock = TextContent;
        }
    }
}
