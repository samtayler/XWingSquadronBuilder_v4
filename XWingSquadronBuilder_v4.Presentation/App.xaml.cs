using System;
using System.Threading.Tasks;
using Template10.Common;
using Template10.Controls;
using Template10.Services.SettingsService;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.ApplicationModel.Core;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using XWingSquadronBuilder_v4.BusinessLogic.Repositories;
using XWingSquadronBuilder_v4.Presentation.Services.Navigation;

namespace XWingSquadronBuilder_v4.Presentation
{
    [Bindable]
    sealed partial class App : Template10.Common.BootStrapper
    {
        public App()
        {
            InitializeComponent();
            //SplashFactory = (e) => new Views.Splash(e);
            this.UnhandledException += App_UnhandledException;
            #region app settings

            //draw into the title bar

            //// some settings must be set in app.constructor
            //var settings = SettingsService.Instance;
            //RequestedTheme = settings.AppTheme;
            //CacheMaxDuration = settings.CacheMaxDuration;
            //ShowShellBackButton = settings.UseShellBackButton;
            ShowShellBackButton = true;
            #endregion
        }

        private void App_UnhandledException(object sender, Windows.UI.Xaml.UnhandledExceptionEventArgs e)
        {
            SaveStateAsync().Wait();
        }

        public async override Task OnSuspendingAsync(object s, SuspendingEventArgs e, bool prelaunchActivated)
        {
            await SaveStateAsync();
            await base.OnSuspendingAsync(s, e, prelaunchActivated);
        }

        private async Task SaveStateAsync()
        {
            var sessionState = SessionState["State"] as IXWingSessionState;
            await (sessionState.XWingRepository as IPersistanceRepository).SaveToStorageAsync();
        }

        public override UIElement CreateRootElement(IActivatedEventArgs e)
        {

            return new ModalDialog
            {
                DisableBackButtonWhenModal = true,
                Content = new Views.Shell(),
                ModalContent = new Views.Busy()
            };
        }

        public override async Task OnStartAsync(StartKind startKind, IActivatedEventArgs args)
        {
            CoreApplication.GetCurrentView().TitleBar.ExtendViewIntoTitleBar = true;

            //remove the solid-colored backgrounds behind the caption controls and system back button
            ApplicationViewTitleBar titleBar = ApplicationView.GetForCurrentView().TitleBar;
            titleBar.ButtonBackgroundColor = Colors.Transparent;
            titleBar.ButtonInactiveBackgroundColor = Colors.Transparent;
            // TODO: add your long-running task here
            if (!SessionState.ContainsKey("State"))
                SessionState.Add("State", new XWingSessionState(await XWingRepository.CreateXWingRepository()));
            //await NavigationService.NavigateAsync(typeof(Views.Shell));
        }
    }
}
