using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Windows.Networking.Connectivity;
using Windows.Storage.Streams;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace XWingSquadronBuilder_v5.Presentation.XWingRss
{
    public class StoredImage : INotifyPropertyChanged
    {
        private readonly CoreDispatcher uiThread;
        private Uri ImageUri { get; set; }
        private string Filename { get; set; }

        public StoredImage(Uri uri)
        {
            ImageUri = uri;
            //uiThread = Application.Current.;
        }

        public DateTime LastUsed { get; set; }       

        private IRandomAccessStream imageStream;

        public IRandomAccessStream ImageStream
        {
            get { return imageStream; }
            set
            {
                if(!imageStream.Equals(value))
                {
                    imageStream = value;
                    NotifyPropertyChanged(nameof(ImageStream));
                }                
            }
        }



        private byte[] imageBytes;

        private void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;


        /// <summary>
        /// 
        /// </summary>
        private void DownloadImage()
        {

            //var image = ImageStorage.ExtractFromLocalStorage(ImageUri);
            //if (image.PixelHeight != 0)
            //{
            //   // ImageBytes = image;
            //    return;
            //}

            //image = new BitmapImage(ImageUri);
            //image.ImageOpened += (sender, e) =>
            //{
            //    //ImageBytes = image;
            //    ImageStorage.StoreImage(ImageUri, image);
            //};

            //image.ImageFailed += (sender, e) =>
            //{
            //    if (NetworkInformation.GetInternetConnectionProfile().GetNetworkConnectivityLevel() != NetworkConnectivityLevel.InternetAccess)
            //    {
            //        NetworkInformation.NetworkStatusChanged += NetworkInformation_NetworkStatusChanged;
            //    }
            //    //ImageBytes = new BitmapImage(new Uri("ms-appx:///Assets/MainPageBackgroud.jpg"));
            //};
        }

        private void NetworkInformation_NetworkStatusChanged(object sender)
        {
            if (NetworkInformation.GetInternetConnectionProfile().GetNetworkConnectivityLevel() != NetworkConnectivityLevel.InternetAccess) return;

            NetworkInformation.NetworkStatusChanged -= NetworkInformation_NetworkStatusChanged;
            DownloadImage();
        }
    }
}
