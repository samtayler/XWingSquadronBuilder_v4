﻿using System;
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
using XWingSquadronBuilder_v4.Presentation.UserControls;
using XWingSquadronBuilder_v4.Presentation.ViewModels;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace XWingSquadronBuilder_v4.Presentation.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SquadronBuilder : Page
    {
        public SqudronBuilderViewModel ViewModel { get; set; }

        public SquadronBuilder()
        {
            this.InitializeComponent();
            ViewModel = new SqudronBuilderViewModel(XWingRepository.Instance.FactionRepository.GetFaction("Empire"));
        }

        private async void Squadron_UpgradeSlotSelected(object sender, Interfaces.IUpgradeSlot e)
        {
            var pilot = sender as IPilot;
            var upgradeSlot = e;

            ((UIElement)Squadron)

            var upgradeSelector = new UpgradePicker()
            {
                Upgrades = XWingRepository.Instance.UpgradeRepository.GetAllUpgradesForType(upgradeSlot.UpgradeType)
                .OrderBy(x => x.Cost).ThenBy(x => x.Name).ToList()
            };

            await upgradeSelector.ShowAsync();

            upgradeSlot.Upgrade = upgradeSelector.SelectedUpgrade;
        }        
    }
}