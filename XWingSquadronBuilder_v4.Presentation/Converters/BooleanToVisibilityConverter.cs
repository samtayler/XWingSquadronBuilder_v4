using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace XWingSquadronBuilder_v4.Presentation.Converters
{
    /// <summary>
    /// True = visible
    /// False = Collapsed
    /// </summary>
    public class BooleanToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if ((bool)value)
                return Windows.UI.Xaml.Visibility.Visible;
            else
                return Windows.UI.Xaml.Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return (Windows.UI.Xaml.Visibility)value == Windows.UI.Xaml.Visibility.Visible;               
        }       
    }
   
}
