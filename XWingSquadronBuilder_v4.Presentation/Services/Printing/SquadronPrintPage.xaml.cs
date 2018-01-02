using System.Collections.Generic;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Documents;
using XWingSquadronBuilder_v4.BusinessLogic.Models.NullModels;
using XWingSquadronBuilder_v4.Presentation.UserControls.PrintControls;
using XWingSquadronBuilder_v4.Presentation.ViewModels.XWingModels;
using XWingSquadronBuilder_v4.Presentation.ViewModels.XWingModels.Interfaces;

namespace XWingSquadronBuilder_v4.Presentation.Services.Printing
{
    /// <summary>
    /// Page content to send to the printer
    /// </summary>
    public sealed partial class SquadronPrintPage : Page
    {
        public SquadronPrintPage()
        {
            InitializeComponent();
        }
        public void AddContent(UIElement uIElement)
        {
            ContentPanel.Children.Add(uIElement);
        }

        public void RemoveLastContentItem()
        {
            ContentPanel.Children.Remove(ContentPanel.Children.Last());
        }

    }
}
