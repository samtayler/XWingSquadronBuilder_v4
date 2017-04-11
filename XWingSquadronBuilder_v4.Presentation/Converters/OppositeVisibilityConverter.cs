using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace XWingSquadronBuilder_v4.Presentation.Converters
{
   public class OppositeVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if ((Windows.UI.Xaml.Visibility)value == Windows.UI.Xaml.Visibility.Visible)
                return Windows.UI.Xaml.Visibility.Collapsed;
            else
                return Windows.UI.Xaml.Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            if ((Windows.UI.Xaml.Visibility)value == Windows.UI.Xaml.Visibility.Visible)
                return Windows.UI.Xaml.Visibility.Collapsed;
            else
                return Windows.UI.Xaml.Visibility.Visible;
        }
    }
}
