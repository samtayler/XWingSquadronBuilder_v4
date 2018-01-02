using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Text;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Documents;
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
            { "MODIFICATION","m" },{"TITLE","t" }, { "ELITEPILOTTALET","E" },{"SYSTEMUPGRADE","S" },{"TECH","X" },{"K-TURN","2" },
            { "RIGHTBANK","9" },{"LEFTBANK","7" },{"RIGHTTURN","6" },{"LEFTTURN","4" },{"STRAIGHT","8" },{"RIGHTTALONROLL","\"" },
            { "LEFTTALONROLL","\'" },{ "RIGHTSEGNORSLOOP","2" },{"STOP","5" },{ "LEFTSEGNORSLOOP","1" }};


        public static IEnumerable<Run> AugementWithXWingIcons(string text, double fontSize, FontStyle style = FontStyle.Normal)
        {
            List<Run> runblock = new List<Run>();

            foreach (var txt in text.Split(' '))
            {
                Run r = new Run();
                if (txt.FirstOrDefault() == '^')
                {
                    string insertText = xwingKeyWords.ContainsKey(txt.Remove(0, 1).ToUpper()) ? xwingKeyWords[txt.Remove(0, 1).ToUpper()].Trim() : "Unknown Icon";
                    r.FontFamily = new FontFamily("/Assets/Fonts/xwing-miniatures.ttf#x-wing-symbols");
                    r.Text = insertText + " ";
                }
                else
                {
                    r.Text = txt + " ";
                }
                runblock.Add(r);
            }

            return runblock;               
        }
    }
}
