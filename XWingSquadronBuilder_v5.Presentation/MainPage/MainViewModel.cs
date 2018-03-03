using System;
using GalaSoft.MvvmLight;
using System.Collections.Generic;
using Windows.UI.Xaml;
using System.Linq;
using XWingSquadronBuilder_v5.Presentation.XWingRss;
using Windows.Storage;

namespace XWingSquadronBuilder_v5.Presentation.MainPage
{
    public class MainViewModel : ViewModelBase
    {
        private IEnumerable<XWingRssItem> rssItems;
        public IEnumerable<XWingRssItem> RssItems
        {
            get
            {
                return rssItems;
            }
            set
            {
                Set(nameof(RssItems), ref rssItems, value);
            }
        }

        public RSSService<XWingRssItem> XWingRssFeed { get; }

        private readonly TimeSpan sixHours = new TimeSpan(6, 0, 0);

        public MainViewModel()
        {
            XWingRssFeed = new RSSService<XWingRssItem>(new Uri(@"https://www.fantasyflightgames.com/en/rss/?tags=x-wing&"), XWingRssItem.CreateXWingRssItem);
            XWingRssFeed.RSSItemsChanged += RssFeedItemsChanged;
            XWingRssFeed.Start();
        }

        private void RssFeedItemsChanged(object sender, EventArgs e)
        {
            RssItems = XWingRssFeed.FeedItems;
        }        
    }
}
