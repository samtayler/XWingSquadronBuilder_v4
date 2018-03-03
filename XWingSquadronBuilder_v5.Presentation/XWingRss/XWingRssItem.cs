using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Media.Imaging;
using Windows.Web.Syndication;
using System.Runtime.CompilerServices;

namespace XWingSquadronBuilder_v5.Presentation.XWingRss
{
    [DataContract]
    public class XWingRssItem : INotifyPropertyChanged
    {
        [DataMember] private string title;
        [DataMember] private string description;
        [DataMember] private Uri link;
        [DataMember] private Uri externalImageSource;
        [DataMember] private string publishDate;

        private BitmapImage imageSource;

        public event PropertyChangedEventHandler PropertyChanged;

        public string Title
        {
            get => title; set
            {
                title = value;
                NotifyPropertyChanged();
            }
        }
        public string Description
        {
            get => description; set
            {
                description = value;
                NotifyPropertyChanged();
            }
        }
        public Uri Link
        {
            get => link; set
            {
                link = value;
                NotifyPropertyChanged();
            }
        }
        public string PublishDate
        {
            get => publishDate; set
            {
                publishDate = value;
            }
        }
        public BitmapImage ImageSource
        {
            get => imageSource; set
            {
                imageSource = value;
                NotifyPropertyChanged();
            }
        }

        public static XWingRssItem CreateXWingRssItem(SyndicationItem feedItem)
        {
            return new XWingRssItem(feedItem.Title.Text, XWingRSSParser.GetDescription(feedItem),
                new Uri(feedItem.Id), new Uri(XWingRSSParser.GetImageUrl(feedItem)), XWingRSSParser.GetPublishDate(feedItem));
        }

        public XWingRssItem(string title, string description, Uri link, Uri externalImageSource, string publishDate)
        {
            this.Title = title;
            this.Description = description;
            this.Link = link;
            this.externalImageSource = externalImageSource;
            this.PublishDate = publishDate;
            CompleteInitialisation(new StreamingContext());
        }

        [OnDeserialized]
        private void CompleteInitialisation(StreamingContext streamingContext)
        {            
            this.imageSource = new BitmapImage();
            ImageStorage.GetImage(externalImageSource).ContinueWith(CompleteImageLoad);
        }

        private async void CompleteImageLoad(Task<IRandomAccessStream> completedTask)
        {
            await CoreApplication.MainView.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, async () =>
             {
                 IRandomAccessStream accessStream = await completedTask;
                 accessStream.Seek(0);
                 await ImageSource.SetSourceAsync(accessStream);
             });
        }

        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
