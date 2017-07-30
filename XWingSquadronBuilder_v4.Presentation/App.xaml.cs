using System;
using System.Threading.Tasks;
using Template10.Services.SettingsService;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml.Data;

namespace XWingSquadronBuilder_v4.Presentation
{
    [Bindable]
    sealed partial class App : Template10.Common.BootStrapper
    {
        public App()
        {
            InitializeComponent();
            //SplashFactory = (e) => new Views.Splash(e);

            #region app settings

            //// some settings must be set in app.constructor
            //var settings = SettingsService.Instance;
            //RequestedTheme = settings.AppTheme;
            //CacheMaxDuration = settings.CacheMaxDuration;
            //ShowShellBackButton = settings.UseShellBackButton;

            #endregion
        }

        public override async Task OnStartAsync(StartKind startKind, IActivatedEventArgs args)
        {
            // TODO: add your long-running task here
            await NavigationService.NavigateAsync(typeof(Views.Shell));
        }
    }
}
