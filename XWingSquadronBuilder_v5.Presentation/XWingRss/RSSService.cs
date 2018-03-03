using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Xml;
using Windows.Storage;
using Windows.Web.Syndication;

namespace XWingSquadronBuilder_v5.Presentation.XWingRss
{
    public class RSSService<T> : IDisposable
    {
        private List<T> feedItems;
        public IReadOnlyList<T> FeedItems => feedItems.AsReadOnly();

        private string settingsKey => $"feedUpdated{nameof(T)}";

        private Uri uri { get; }
        private Timer rssCheckTimer { get; }
        private DateTime lastUpdated;

        public event EventHandler RSSItemsChanged;
        private Func<SyndicationItem, T> ObjectFactory { get; }
        /// <summary>
        /// Create RSS Service with default timer of 6 hours
        /// </summary>
        /// <param name="uri">the absolute uri of the RSS feed.</param>
        public RSSService(Uri uri, Func<SyndicationItem, T> objectFactory) : this(uri, objectFactory, new TimeSpan(6, 0, 0)) { }

        public RSSService(Uri uri, Func<SyndicationItem, T> objectFactory, TimeSpan timerInterval)
        {
            this.ObjectFactory = objectFactory;
            LoadLastUpdated();
            App.Current.Suspending += Current_Suspending;
            feedItems = new List<T>();
            this.uri = uri;
            rssCheckTimer = new Timer(timerInterval.TotalMilliseconds);
            rssCheckTimer.Elapsed += RssCheckTimer_ElapsedAsync;
            rssCheckTimer.AutoReset = true;
        }

        private void LoadLastUpdated()
        {
            var appSettings = ApplicationData.Current.LocalSettings;
            if (appSettings.Values.ContainsKey(settingsKey))
            {
                lastUpdated = new DateTime((long)appSettings.Values[settingsKey]);
            }
            else
            {
                lastUpdated = DateTime.MinValue;
            }
        }

        private void Current_Suspending(object sender, Windows.ApplicationModel.SuspendingEventArgs e)
        {
            SaveFeedItems();
            SaveLastUpdated();
        }

        private async void RssCheckTimer_ElapsedAsync(object sender, ElapsedEventArgs e)
        {
            await GetFeed();
        }

        public void Start()
        {
            rssCheckTimer.Start();
            LoadOrDownload();
        }

        public async Task GetFeed()
        {
            try
            {
                SyndicationClient client = new SyndicationClient();
                SyndicationFeed feed;

                // Although most HTTP servers do not require User-Agent header, 
                // others will reject the request or return a different response if this header is missing.
                // Use the setRequestHeader() method to add custom headers.
                client.SetRequestHeader("User-Agent", "Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.2; WOW64; Trident/6.0)");
                feed = await client.RetrieveFeedAsync(this.uri);
                // Retrieve the title of the feed and store it in a string.
                string title = feed.Title.Text;
                // Iterate through each feed item.
                feedItems = new List<T>();
                foreach (SyndicationItem item in feed.Items)
                {
                    try
                    {
                        feedItems.Add(ObjectFactory(item));
                    }
                    catch { }
                }
                RSSItemsChanged?.Invoke(this, new EventArgs());
                lastUpdated = DateTime.Now;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Debug.WriteLine(ex.StackTrace);
            }
        }

        private void SaveLastUpdated()
        {
            var appSettings = ApplicationData.Current.LocalSettings;
            if (appSettings.Values.ContainsKey(settingsKey))
            {
                appSettings.Values[settingsKey] = lastUpdated.Ticks;
            }
            else
            {
                appSettings.Values.Add(settingsKey, lastUpdated.Ticks);
            }
        }

        private void LoadOrDownload()
        {
            var appStorage = IsolatedStorageFile.GetUserStoreForApplication();
            try
            {
                using (var feedFile = appStorage.OpenFile($@"feed.json", System.IO.FileMode.Open))
                {
                    DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(List<T>));
                    feedItems = serializer.ReadObject(feedFile) as List<T>;
                    feedFile.Close();
                    RSSItemsChanged?.Invoke(this, new EventArgs());
                    GetFeed();
                }
            }
            catch (Exception ex)
            {
                GetFeed();
            }
        }

        private void SaveFeedItems()
        {
            var appStorage = IsolatedStorageFile.GetUserStoreForApplication();

            using (var fileStream = appStorage.OpenFile($@"feed.json", System.IO.FileMode.Create))
            {
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(List<T>));
                serializer.WriteObject(fileStream, feedItems);
                fileStream.Close();
            }
        }

        public void Dispose()
        {
            SaveFeedItems();
            SaveLastUpdated();
            rssCheckTimer.Stop();
            rssCheckTimer.Dispose();
        }
    }
}
