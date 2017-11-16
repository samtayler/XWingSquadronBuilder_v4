using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Windows.UI.Xaml.Data;
using XWingSquadronBuilder_v4.Interfaces;
using XWingSquadronBuilder_v4.Presentation.ViewModels;
using XWingSquadronBuilder_v4.Presentation.ViewModels.XWingModels;

namespace XWingSquadronBuilder_v4.Presentation.Converters
{
    public class PilotToPilotViewModelConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return new PilotViewModel((IPilot)(value));
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return ((PilotViewModel)value).Pilot;
        }
    }
}
