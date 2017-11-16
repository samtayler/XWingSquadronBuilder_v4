using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Text;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace XWingSquadronBuilder_v4.Presentation.Converters
{
    public class StringToTextBlocks : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return XWingTextAugmenter.AugementWithXWingIcons(value.ToString(), (int)parameter);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }


    public class XWingTextAugmenter
    {
        private static Dictionary<string, string> xwingKeyWords = new Dictionary<string, string>()
        {
            {"HIT","d" },{"CRIT","c" },{"CRITICAL","c" },{"EVADE","e" },{"FOCUS","f" },{"TARGETLOCK","l" },{"BOOST","b" },{"BARRELROLL","r" },
            { "CLOAK","k" },{"SLAM","s" },{"BOMB","B" },{"CREW","W" },{"CANNON","C" },{"MISSILE","M" },{"TORPEDO","P" },{"TURRET","U" },
            { "MODFIFICATION","m" },{"TITLE","t" }, { "ELITEPILOTTALET","E" },{"SYSTEMUPGRADE","S" },{"TECH","X" },{"K-TURN","2" },
            { "RIGHTBANK","9" },{"LEFTBANK","7" },{"RIGHTTURN","6" },{"LEFTTURN","4" },{"STRAIGHT","8" },{"RIGHTTALONROLL","\"" },
            { "LEFTTALONROLL","\'" },{ "RIGHTSEGNORSLOOP","2" },{"STOP","5" },{ "LEFTSEGNORSLOOP","1" }};


        public static IEnumerable<TextBlock> AugementWithXWingIcons(string text, double fontSize, FontStyle style = FontStyle.Normal)
        {
            List<TextBlock> textblocks = new List<TextBlock>();           

            if (!text.Any(x => x == '^'))
            {
                textblocks.Add(new TextBlock() { Text = text.Trim(), FontFamily = new FontFamily("Segoe UI"), FontStyle = style, Margin = new Windows.UI.Xaml.Thickness(2, 0, 2, 0), FontSize = fontSize, TextWrapping = Windows.UI.Xaml.TextWrapping.WrapWholeWords });
            }
            else
            {
                string[] splitText = text.Split(' ');

                foreach (var item in splitText)
                {
                    var wordSplit = item.Split('^');
                    if (wordSplit.Count() > 1 && xwingKeyWords.ContainsKey(wordSplit[1]))
                    {
                        textblocks.Add(new TextBlock() { Text = xwingKeyWords[wordSplit[1]].Trim(), FontStyle = style, FontFamily = new FontFamily("/Assets/Fonts/xwing-miniatures.ttf#x-wing-symbols"), Margin = new Windows.UI.Xaml.Thickness(2, 3, 2, 0), FontSize = fontSize, VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Center });
                    }
                    else
                    {
                        textblocks.Add(new TextBlock() { Text = wordSplit[0].Trim(), FontStyle = style, FontFamily = new FontFamily("Segoe UI"), Margin = new Windows.UI.Xaml.Thickness(2, 0, 2, 0), FontSize = fontSize });
                    }
                }
            }

            textblocks.First().Margin = new Windows.UI.Xaml.Thickness(0, 0, 2, 0);
            textblocks.Last().Margin = new Windows.UI.Xaml.Thickness(2, 0, 0, 0);

            return textblocks;
        }        
    }
}
