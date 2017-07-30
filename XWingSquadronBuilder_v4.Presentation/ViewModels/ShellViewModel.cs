using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template10.Mvvm;
using XWingSquadronBuilder_v4.Presentation.Views;

namespace XWingSquadronBuilder_v4.Presentation.ViewModels
{
    public class ShellViewModel : ViewModelBase
    {
        public ObservableCollection<MenuItem> Menu { get; } = new ObservableCollection<MenuItem>();  

        public ShellViewModel()
        {
            // Build the menu
            Menu.Add(new MenuItem() { Glyph = "\uE10F", Text = "Home", NavigationDestination = typeof(FactionSelectionPage) });            
        }
    }

}
