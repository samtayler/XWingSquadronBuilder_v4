using System;

using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

using XWingSquadronBuilder_v5.Presentation.Services;
using XWingSquadronBuilder_v5.Presentation.ViewModels;

namespace XWingSquadronBuilder_v5.Presentation.Views
{
    public sealed partial class ShellPage : Page
    {
        private ShellViewModel ViewModel
        {
            get { return DataContext as ShellViewModel; }
        }

        public ShellPage()
        {
            InitializeComponent();
            DataContext = ViewModel;
            ViewModel.Initialize(shellFrame);
        }
    }
}
