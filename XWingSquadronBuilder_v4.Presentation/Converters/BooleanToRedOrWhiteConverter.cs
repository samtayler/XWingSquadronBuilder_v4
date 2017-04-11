using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;
using Windows.UI;

namespace XWingSquadronBuilder_v4.Presentation.Converters
{
    public class BooleanToRedOrWhiteConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if ((bool)value)
                return new SolidColorBrush(Colors.White);
            else
                return new SolidColorBrush(Colors.Red);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            if (((SolidColorBrush)value).Color == Colors.White)
                return true;
            else if (((SolidColorBrush)value).Color == Colors.Red)
                return false;
            else
                return null;
        }
    }
}
