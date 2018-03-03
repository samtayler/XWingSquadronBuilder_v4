using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.Networking.Connectivity;
using Windows.UI.Core;

namespace XWingSquadronBuilder_v5.Presentation.Services
{
    
    public class InternetConnectionWatcher //: IDisposable
    {
        //class RerunableTask<T>
        //{
        //    public RerunableTask(Task<T> operation)
        //    {
        //        Operation = operation;
        //        Result = new TaskCompletionSource<T>();
        //    }

        //    public Task<T> Operation { get; private set; }
        //    public TaskCompletionSource<T> Result { get; private set; }

        //    private void SetResult(Task<T> completedAction)
        //    {
        //        if(completedAction.IsCompletedSuccessfully)
        //        {
        //            Result.SetResult(completedAction.Result);
        //        }
        //        else
        //        {
                   
        //        }
        //    }

        //}
        //private bool HasInternetConnection;
        //private HttpClient webClient;
        //private Dictionary<Uri, Task<HttpResponseMessage>> pendingRequests;

        //public InternetConnectionWatcher()
        //{
        //    pendingRequests = new Dictionary<Uri, Task<HttpResponseMessage>>();
        //    webClient = new HttpClient();
        //    HasInternetConnection = NetworkInformation.GetInternetConnectionProfile().GetNetworkConnectivityLevel() == NetworkConnectivityLevel.InternetAccess;
        //    NetworkInformation.NetworkStatusChanged += OnNetworkStatusChange;
        //}

        //public void Dispose()
        //{
        //    NetworkInformation.NetworkStatusChanged -= OnNetworkStatusChange;
        //}

        //public TaskCompletionSource<HttpResponseMessage> SubmitGetOperation(Uri uri)
        //{
        //    Task<HttpResponseMessage> t = new Task<HttpResponseMessage>(() =>
        //    {
        //        return webClient.GetAsync(uri).Result;
        //    });
        //    pendingRequests.Add(uri, t);
        //    if (HasInternetConnection)
        //    {
        //        //;                
        //    }
        //    return pendingRequests[uri];
        //}

        //private async HttpResponseMessage RunGetOperation(Uri uri)
        //{

        //}

        //private void CompleteWebRequest(Task<HttpResponseMessage> action)
        //{

        //}

        //private void OnNetworkStatusChange(object sender)
        //{
        //    if (!HasInternetConnection && NetworkInformation.GetInternetConnectionProfile().GetNetworkConnectivityLevel() == NetworkConnectivityLevel.InternetAccess)
        //    {
        //        HasInternetConnection = true;
        //    }
        //    else if (HasInternetConnection && NetworkInformation.GetInternetConnectionProfile().GetNetworkConnectivityLevel() != NetworkConnectivityLevel.InternetAccess)
        //    {
        //        HasInternetConnection = false;
        //    }

        //}
    }
}
