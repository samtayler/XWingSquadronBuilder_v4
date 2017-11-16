using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using XWingSquadronBuilder_v4.BusinessLogic.Factories;
using XWingSquadronBuilder_v4.BusinessLogic.Logic;
using XWingSquadronBuilder_v4.BusinessLogic.Repositories;
using XWingSquadronBuilder_v4.Interfaces;
using XWingSquadronBuilder_v4.Presentation.Converters;
using XWingSquadronBuilder_v4.Presentation.Services.Navigation;
using XWingSquadronBuilder_v4.Presentation.ViewModels;
using XWingSquadronBuilder_v4.Presentation.ViewModels.XWingModels;
using XWingSquadronBuilder_v4.Presentation.ViewModels.XWingModels.Interfaces;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace XWingSquadronBuilder_v4.Presentation.UserControls
{
    public sealed partial class UpgradeSelector : UserControl
    {
        public delegate void UpgradeSelectedHandler(IUpgrade e);

        public event UpgradeSelectedHandler UpgradeSelected;

        public ObservableCollection<FilteredUpgradeDisplay> UpgradeTypes
        {
            get { return (ObservableCollection<FilteredUpgradeDisplay>)GetValue(UpgradeTypesProperty); }
            set { SetValue(UpgradeTypesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for UpgradeTypes.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UpgradeTypesProperty =
            DependencyProperty.Register(nameof(UpgradeTypes), typeof(ObservableCollection<FilteredUpgradeDisplay>), typeof(UpgradeSelector), new PropertyMetadata(new List<FilteredUpgradeDisplay>()));

        public IPilotViewModel Pilot
        {
            get { return (IPilotViewModel)GetValue(PilotProperty); }
            set
            {
                SetValue(PilotProperty, value);
                UpgradeTypes =
                    new ObservableCollection<FilteredUpgradeDisplay>(Pilot.Upgrades.Select(x => x.UpgradeSlot.UpgradeType).Distinct()
                    .Select(x => new FilteredUpgradeDisplay(x, Pilot, (App.Current.SessionState["State"] as IXWingSessionState).XWingRepository)));
                Pilot.PropertyChanged += (sender, args) =>
                {

                    if (Pilot.Upgrades.Select(x => x.UpgradeSlot.UpgradeType).Distinct().Count() == UpgradeTypes.Count()) return;

                    var item = PivotMain.SelectedItem as FilteredUpgradeDisplay;
                    foreach (var upgradeType in Pilot.Upgrades.Select(x => x.UpgradeSlot.UpgradeType).Distinct())
                    {
                        if (!UpgradeTypes.Any(x => x.UpgradeType.Equals(upgradeType)))
                        {
                            UpgradeTypes.Add(new FilteredUpgradeDisplay(upgradeType, Pilot, (App.Current.SessionState["State"] as IXWingSessionState).XWingRepository));
                        }
                    }

                    foreach (var filteredUpgrade in UpgradeTypes.ToList())
                    {
                        // don't remove search tab
                        if (filteredUpgrade.Name == "Search Results") continue;

                        if (!Pilot.Upgrades.Any(x => x.UpgradeSlot.UpgradeType.Equals(filteredUpgrade.UpgradeType)))
                        {
                            UpgradeTypes.Remove(filteredUpgrade);
                        }
                        PivotMain.SelectedIndex = 0;
                    }

                    if (!UpgradeTypes.Any(x => x.UpgradeType.Equals(item.UpgradeType)))
                    {
                        PivotMain.SelectedIndex = 0;
                    }
                    else
                    {
                        if (PivotMain.SelectedIndex != PivotMain.Items.IndexOf(item))
                            PivotMain.SelectedIndex = PivotMain.Items.IndexOf(item);
                    }

                };
            }
        }

        // Using a DependencyProperty as the backing store for Pilot.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PilotProperty =
            DependencyProperty.Register(nameof(Pilot), typeof(IPilotViewModel), typeof(UpgradeSelector), new PropertyMetadata(0));



        public UpgradeSelector()
        {
            UpgradeTypes = new ObservableCollection<FilteredUpgradeDisplay>();
            Pilot = new PilotViewModel(PilotFactory.CreateNullPilot());
            this.InitializeComponent();
        }

        private void ListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            UpgradeSelected?.Invoke(e.ClickedItem as IUpgrade);
        }

        public void SetSelectedUpgradeType(IUpgradeType upgradeType)
        {
            var index = PivotMain.Items.IndexOf(PivotMain.Items.Cast<FilteredUpgradeDisplay>().Single(x => x.UpgradeType.Equals(upgradeType)));
            PivotMain.SelectedIndex = index;
        }

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var upgradeTypesItem = PivotMain.Items.Cast<FilteredUpgradeDisplay>();
            if (!upgradeTypesItem.Any(x => x.Name == "Search Results"))
            {
                UpgradeTypes.Add(new UpgradeSearchResults(this.Pilot, (Template10.Common.BootStrapper.Current.SessionState["State"] as IXWingSessionState).XWingRepository));
            }
            var searchtab = UpgradeTypes.Single(x => x as UpgradeSearchResults != null) as UpgradeSearchResults;

            if (SearchBox.Text == string.Empty)
            {
                UpgradeTypes.Remove(searchtab);
            }
            else
            {
                searchtab.SearchQuery = SearchBox.Text;
                PivotMain.SelectedItem = searchtab;
            }

        }
    }
}
