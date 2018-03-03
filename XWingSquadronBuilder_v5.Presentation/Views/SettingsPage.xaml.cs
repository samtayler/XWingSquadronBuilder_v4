using System;

using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

using XWingSquadronBuilder_v5.Presentation.ViewModels;

namespace XWingSquadronBuilder_v5.Presentation.Views
{
    public sealed partial class SettingsPage : Page
    {
        private SettingsViewModel ViewModel
        {
            get { return DataContext as SettingsViewModel; }
        }

        //// TODO WTS: Change the URL for your privacy policy in the Resource File, currently set to https://YourPrivacyUrlGoesHere

        public SettingsPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            ViewModel.Initialize();
        }
    }
}
