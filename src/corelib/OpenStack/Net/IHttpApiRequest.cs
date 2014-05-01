namespace OpenStack.Net
{
    using System;
    using System.Net.Http;

    public interface IHttpApiRequest : IDisposable
    {
        /// <summary>
        /// This event is fired immediately before sending an asynchronous web request.
        /// </summary>
        event EventHandler<HttpRequestEventArgs> BeforeAsyncWebRequest;

        /// <summary>
        /// This event is fired when the result of an asynchronous web request is received.
        /// </summary>
        event EventHandler<HttpResponseEventArgs> AfterAsyncWebResponse;

        HttpClient HttpClient
        {
            get;
            set;
        }

        HttpRequestMessage RequestMessage
        {
            get;
            set;
        }

        HttpCompletionOption CompletionOption
        {
            get;
            set;
        }
    }
}
