using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace XWingSquadronBuilder_v4.Presentation.Converters
{
    public class TextAlignmentToHorizontalAlignment : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            TextAlignment align = (TextAlignment)value;

            switch (align)
            {
                case TextAlignment.Center:
                    return HorizontalAlignment.Center;
                case TextAlignment.Left:
                    return HorizontalAlignment.Left;
                case TextAlignment.Right:
                    return HorizontalAlignment.Right;
                case TextAlignment.Justify:
                    return HorizontalAlignment.Stretch;
                case TextAlignment.DetectFromContent:
                    return HorizontalAlignment.Left;
                default:
                    return HorizontalAlignment.Left;
            } 
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            HorizontalAlignment align = (HorizontalAlignment)value;

            switch (align)
            {
                case HorizontalAlignment.Center:
                    return TextAlignment.Center;
                case HorizontalAlignment.Left:
                    return TextAlignment.Left;
                case HorizontalAlignment.Right:
                    return TextAlignment.Right;
                case HorizontalAlignment.Stretch:
                    return TextAlignment.Justify;
                default:
                    return TextAlignment.DetectFromContent;
            }
        }
    }
}
