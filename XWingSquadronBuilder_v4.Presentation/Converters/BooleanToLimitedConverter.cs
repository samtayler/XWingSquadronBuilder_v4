using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace XWingSquadronBuilder_v4.Presentation.Converters
{
    public class BooleanToLimitedConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if ((bool)value)
                return "Limited";
            else
                return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            if ((string)value == "Limited")
                return true;
            else
                return false;
        }
    }
}
