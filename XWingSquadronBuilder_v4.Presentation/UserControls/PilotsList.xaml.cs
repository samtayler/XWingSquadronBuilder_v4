using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
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


// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace XWingSquadronBuilder_v4.UserControls
{
    public sealed partial class PilotsList : UserControl//, INotifyPropertyChanged
    {
        //public delegate void PilotSelectedEventHandler(object sender, IPilot e);

        //public event PilotSelectedEventHandler PilotSelected;
        //public event PropertyChangedEventHandler PropertyChanged;

        //public PilotListViewModel ViewModel
        //{
        //    get { return _ViewModel; }
        //    set
        //    {
        //        if (_ViewModel != value)
        //        {
        //            _ViewModel = value;
        //            NotifyPropertyChanged();
        //        }
        //    }
        //}
        //private PilotListViewModel _ViewModel;

        //private void NotifyPropertyChanged([CallerMemberName] string propertyChanged = "")
        //{
        //    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyChanged));
        //}

        public PilotsList()
        {
            this.InitializeComponent();
        }

        //private void ShipList_ItemClick(object sender, ItemClickEventArgs e)
        //{
        //    PilotSelected?.Invoke(this, e.ClickedItem as IPilot);
        //}
    }
}
