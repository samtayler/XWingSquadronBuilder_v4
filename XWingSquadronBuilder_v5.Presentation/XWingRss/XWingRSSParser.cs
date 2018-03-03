using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using Windows.UI.Xaml.Media.Imaging;
using Windows.Web.Syndication;

namespace XWingSquadronBuilder_v5.Presentation.XWingRss
{

    //<span class="meta-date float-left">          
    //        Published 22 February 2018          
    //    </span>
    public class XWingRSSParser
    {
        public static string GetPublishDate(SyndicationItem item)
        {
            var summaryText = item.Summary.Text.Replace("\n", "");
            string spanText = "<span class=\"meta-date float-left\">";
            var startIndex = summaryText.IndexOf(spanText) + spanText.Length;
            var endIndex = summaryText.IndexOf(@"</span>");

            var publishText = summaryText.Substring(startIndex, endIndex - startIndex);
            return publishText.Trim();
        }

        public static string GetDescription(SyndicationItem item)
        {
            var summaryText = item.Summary.Text.Replace("\n", "");
            var startIndex = summaryText.IndexOf("<p>") + 3;
            var endIndex = summaryText.IndexOf(@"</p>");

            var description = summaryText.Substring(startIndex, endIndex - startIndex);
            return description;
        }

        public static string GetTitle(SyndicationItem item)
        {
            var summaryText = item.Summary.Text.Replace("\n", "");
            var startIndex = summaryText.IndexOf("<h1>") + 4;
            var endIndex = summaryText.IndexOf(@"</h1>");

            var titleString = summaryText.Substring(startIndex, endIndex - startIndex);
            return titleString;
        }

        public static string GetImageUrl(SyndicationItem item)
        {
            var summaryText = item.Summary.Text;
            var lines = summaryText.Split('\n').Select(x => x.Trim());
            int startIndexImg = 0;
            foreach (var line in lines)
            {
                if (line.StartsWith("<img") && line.Contains("blog-visual device-break"))
                {
                    startIndexImg = summaryText.IndexOf(line);
                    break;
                }
            }

            //var startIndexImg = summaryText.IndexOf("<img class=\"blog - visual device -break\"");
            int endIndexImg = summaryText.Length;
            for (int i = startIndexImg; i < summaryText.Length; i++)
            {
                if (summaryText[i] == '>')
                {
                    endIndexImg = i;
                    break;
                }
            }
            var itemImgSubstring = summaryText.Substring(startIndexImg, endIndexImg);

            var linkParser = new Regex(@"\b(?:https?://|www\.)\S+\b", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            var result = linkParser.Match(itemImgSubstring).Value;
            return result;
        }
    }




}
