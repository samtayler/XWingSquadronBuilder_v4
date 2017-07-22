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
using XWingSquadronBuilder_v4.Presentation.ViewModels;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace XWingSquadronBuilder_v4.Presentation.UserControls
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SquadronPrintView : Page
    {
        public SquadronBuilderViewModel ViewModel
        {
            get { return (SquadronBuilderViewModel)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ViewModel.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ViewModelProperty =
            DependencyProperty.Register("ViewModel", typeof(SquadronBuilderViewModel), typeof(SquadronPrintView), new PropertyMetadata(0));


        public SquadronPrintView(SquadronBuilderViewModel viewModel)
        {
            ViewModel = viewModel;
            this.InitializeComponent();
        }
    }
}
