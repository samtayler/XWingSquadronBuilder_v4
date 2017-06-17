using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Text;
using Windows.UI.Xaml.Data;

namespace XWingSquadronBuilder_v4.Presentation.Converters
{
    public class BooleanToTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return ((bool)value).ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return bool.Parse(value.ToString());
        }
    }
}
