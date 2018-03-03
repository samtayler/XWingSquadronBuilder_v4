using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Windows.Networking.Connectivity;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Media.Imaging;

namespace XWingSquadronBuilder_v5.Presentation.XWingRss
{
    public class ImageStorage
    {
        private static readonly string imageStorageFolder = "TempImages";
        private static ConcurrentQueue<Tuple<Uri, TaskCompletionSource<IRandomAccessStream>>> downloadQueue;

        static ImageStorage()
        {
            NetworkInformation.NetworkStatusChanged += NetworkInformation_NetworkStatusChanged;
        }

        public static void StoreImage(Uri imageUri, byte[] imageBytes)
        {
            var isolatedAppStorageFile = IsolatedStorageFile.GetUserStoreForApplication();
            IsolatedStorageFileStream outputStream = null;
            string isolatedStoragePath = GetFileNameInIsolatedStorage(imageUri);
            try
            {
                if (!isolatedAppStorageFile.DirectoryExists(imageStorageFolder))
                {
                    isolatedAppStorageFile.CreateDirectory(imageStorageFolder);
                }
                if (isolatedAppStorageFile.FileExists(isolatedStoragePath))
                {
                    isolatedAppStorageFile.DeleteFile(isolatedStoragePath);
                }
                outputStream = isolatedAppStorageFile.CreateFile(isolatedStoragePath);
                outputStream.Write(imageBytes, 0, imageBytes.Length);
                outputStream.Close();
                isolatedAppStorageFile.Close();
            }
            catch
            {
                //We cannot do anything here.
                if (outputStream != null) outputStream.Close();
            }
        }

        public static async Task<IRandomAccessStream> GetImage(Uri uri)
        {
            return await Task.Run(async () =>
            {
                TaskCompletionSource<IRandomAccessStream> completedImageStream = new TaskCompletionSource<IRandomAccessStream>();
                var currentNetworkConnectivityLevel = NetworkInformation.GetInternetConnectionProfile().GetNetworkConnectivityLevel();
                if (InternalStorageContainsImage(uri))
                {
                    completedImageStream.SetResult(ExtractFromLocalStorage(uri));
                }
                else if (currentNetworkConnectivityLevel == NetworkConnectivityLevel.InternetAccess)
                {
                    (bool, byte[]) result = DownloadImageFromUri(uri);
                    if (result.Item1)
                    {
                        StoreImage(uri, result.Item2);
                        completedImageStream.SetResult(ExtractFromLocalStorage(uri));
                    }
                    else
                    {
                        downloadQueue.Enqueue(new Tuple<Uri, TaskCompletionSource<IRandomAccessStream>>(uri, completedImageStream));
                    }
                }
                else
                {
                    downloadQueue.Enqueue(new Tuple<Uri, TaskCompletionSource<IRandomAccessStream>>(uri, completedImageStream));
                }

                return await completedImageStream.Task;
            });
        }

        private static object _lock = new object();

        private static (bool, byte[]) DownloadImageFromUri(Uri uri)
        {
            try
            {
                WebClient client = new WebClient();
                var data = client.DownloadData(uri);
                return (true, data);
            }
            catch (WebException ex)
            {
                return (false, new byte[0]);
            }
        }

        private static void NetworkInformation_NetworkStatusChanged(object sender)
        {
            if (NetworkInformation.GetInternetConnectionProfile().GetNetworkConnectivityLevel() != NetworkConnectivityLevel.InternetAccess)
                return;

            bool downloadSuccess = false;

            do
            {
                Tuple<Uri, TaskCompletionSource<IRandomAccessStream>> downloadItem = null;
                if (downloadQueue.TryDequeue(out downloadItem))
                {
                    (bool, byte[]) result = DownloadImageFromUri(downloadItem.Item1);
                    if (result.Item1)
                    {
                        StoreImage(downloadItem.Item1, result.Item2);
                        downloadItem.Item2.SetResult(ExtractFromLocalStorage(downloadItem.Item1));
                        downloadSuccess = true;
                    }
                    else
                    {
                        downloadQueue.Enqueue(downloadItem);
                        downloadSuccess = false;

                    }
                }
            } while (downloadQueue.Count > 0 && downloadSuccess);
        }

        /// <summary>
        /// Gets the file name in isolated storage for the Uri specified. This name should be used to search in the isolated storage.
        /// </summary>
        /// <param name="uri">The URI.</param>
        /// <returns></returns>
        private static string GetFileNameInIsolatedStorage(Uri uri)
        {
            return $@"{imageStorageFolder}\{uri.Segments[uri.Segments.Length - 1]}";
        }

        private static bool InternalStorageContainsImage(Uri uri)
        {
            return IsolatedStorageFile.GetUserStoreForApplication().FileExists(GetFileNameInIsolatedStorage(uri));
        }



        public static IRandomAccessStream ExtractFromLocalStorage(Uri imageFileUri)
        {
            lock (_lock)
            {
                var isolatedAppStorageFile = IsolatedStorageFile.GetUserStoreForApplication();
                string isolatedStoragePath = GetFileNameInIsolatedStorage(imageFileUri);  //Load from local storage
                MemoryStream mem = new MemoryStream();
                using (var file = isolatedAppStorageFile.OpenFile(isolatedStoragePath, FileMode.Open))
                {
                    file.CopyTo(mem);
                    file.Close();
                }
                return mem.AsRandomAccessStream();
            }
        }
    }
}
