using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.Storage.Streams;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media.Imaging;


namespace XWingSquadronBuilder_v5.Presentation.Helpers
{
    [DataContract]
    public class AsyncImageCache : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private BitmapImage image;
        public BitmapImage Image
        {
            get
            {
                return image;
            }
            private set
            {
                if(value != image)
                {
                    image = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Image)));
                }
            }
        }

        [DataMember]
        private Uri imageUri { get; }

        public AsyncImageCache(string imageUri, Thread mainThread)
        { 
            
            this.imageUri = new Uri(imageUri);
            DownloadImage();
        }

        private async void DownloadImage()
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(imageUri);
                BitmapImage bitmap = new BitmapImage();
                if (response != null && response.StatusCode == HttpStatusCode.OK)
                {
                    using (var stream = await response.Content.ReadAsStreamAsync())
                    {
                        using (var memStream = new MemoryStream())
                        {
                            await stream.CopyToAsync(memStream);
                            memStream.Position = 0;
                            bitmap.SetSource(memStream.AsRandomAccessStream());
                        }
                    }
                    Image = bitmap;
                }
            }
        }
    }
}
