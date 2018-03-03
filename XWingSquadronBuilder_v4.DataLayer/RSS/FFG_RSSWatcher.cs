using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.Web.Syndication;

namespace XWingSquadronBuilder_v4.DataLayer.RSS
{
    [DataContract]
    public class FFG_RSSWatcher
    {
        [DataMember]
        public List<SyndicationItem> FeedItems { get; set; }

        private DateTime lastUpdated;
        private readonly Timer rssCheckTimer;
        private Timer retryTimer;
        private object _lock = new object();

        public FFG_RSSWatcher()
        {
            rssCheckTimer = new Timer(new TimerCallback(TimerGetFeed), null, TimeSpan.MinValue, TimeSpan.FromMinutes(5));
            TriggerRssUpdate();
        }

        private async void TimerGetFeed(object o)
        {
            await GetFeed();
        }

        public async Task TriggerRssUpdate()
        {
            await GetFeed();
        }

        private async Task GetFeed()
        {
            lock (_lock)
            {
                if (lastUpdated > DateTime.Now.Subtract(new TimeSpan(6, 0, 0))) return;
                lastUpdated = DateTime.Now;
            }
            SyndicationClient client = new SyndicationClient();
            SyndicationFeed feed;

            string uriString = @"https://www.fantasyflightgames.com/en/rss/?tags=x-wing&";
            Uri uri = new Uri(uriString);

            try
            {
                // Although most HTTP servers do not require User-Agent header, 
                // others will reject the request or return a different response if this header is missing.
                // Use the setRequestHeader() method to add custom headers.
                client.SetRequestHeader("User-Agent", "Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.2; WOW64; Trident/6.0)");
                feed = await client.RetrieveFeedAsync(uri);
                // Retrieve the title of the feed and store it in a string.
                string title = feed.Title.Text;
                // Iterate through each feed item.
                FeedItems = feed.Items.ToList();
                
            }
            catch (Exception ex)
            {
                lastUpdated = DateTime.MinValue;                
            }
        }
    }
}
