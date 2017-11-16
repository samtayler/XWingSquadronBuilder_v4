using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace XWingSquadronBuilder_v4.Presentation.ViewModels
{
    public interface IPopupFrameViewModel
    {
        Visibility IsVisible { get; }
        void Cancel();
        void Finish();
    }
}
