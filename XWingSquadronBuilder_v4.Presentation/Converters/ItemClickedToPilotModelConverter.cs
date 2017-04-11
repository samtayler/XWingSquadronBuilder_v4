using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;


namespace XWingSquadronBuilder_v4.Presentation.Converters
{
    public class ItemClickedToPilotModelConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return ((ItemClickEventArgs)value).ClickedItem;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return new ItemClickEventArgs();            
        }
    }
}
