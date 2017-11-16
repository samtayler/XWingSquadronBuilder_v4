using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Text;
using Windows.UI.Xaml.Controls;
using XWingSquadronBuilder_v4.Interfaces;
using XWingSquadronBuilder_v4.Presentation.Converters;

namespace XWingSquadronBuilder_v4.Presentation.Extensions
{
    public static class IUpgradeFormattingExtension
    {
        public static IEnumerable<TextBlock> AugmentText(this string cardText, double fontSize, FontStyle style = FontStyle.Normal)
        {
            return XWingTextAugmenter.AugementWithXWingIcons(cardText, fontSize, style);
        }
    }
}
