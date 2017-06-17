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
using XWingSquadronBuilder_v4.BusinessLogic.Repositories;
using XWingSquadronBuilder_v4.Interfaces;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace XWingSquadronBuilder_v4.Presentation.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class FactionSelectionPage : Page
    {
        public List<IFaction> Factions
        {
            get { return (List<IFaction>)GetValue(FactionsProperty); }
            set { SetValue(FactionsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Factions.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FactionsProperty =
            DependencyProperty.Register("Factions", typeof(List<IFaction>), typeof(FactionSelectionPage), new PropertyMetadata(0));



        public FactionSelectionPage()
        {
            Factions = XWingRepository.Instance.FactionRepository.GetAllFactions().Where(faction => faction.Name != "Any").ToList();
            this.InitializeComponent();          
        }

        private void gwFactionSelect_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.Frame.Navigate(typeof(SquadronBuilder), e.ClickedItem);
        }
    }
}
