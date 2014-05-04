namespace OpenStack.Net
{
    using System;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;

#if !NET40PLUS
    using OpenStack.Compat;
#endif

    public class DelegatingHttpApiCall<T> : IHttpApiCall<T>
    {
        private readonly IHttpApiCall<T> _httpApiCall;

        public event EventHandler<HttpRequestEventArgs> BeforeAsyncWebRequest;

        public event EventHandler<HttpResponseEventArgs> AfterAsyncWebResponse;

        public DelegatingHttpApiCall(IHttpApiCall<T> httpApiCall)
        {
            if (httpApiCall == null)
                throw new ArgumentNullException("httpApiCall");

            _httpApiCall = httpApiCall;
            _httpApiCall.BeforeAsyncWebRequest += HandleBeforeAsyncWebRequest;
            _httpApiCall.AfterAsyncWebResponse += HandleAfterAsyncWebResponse;
        }

        public HttpCompletionOption CompletionOption
        {
            get
            {
                return _httpApiCall.CompletionOption;
            }

            set
            {
                _httpApiCall.CompletionOption = value;
            }
        }

        public HttpClient HttpClient
        {
            get
            {
                return _httpApiCall.HttpClient;
            }

            set
            {
                _httpApiCall.HttpClient = value;
            }
        }

        public HttpRequestMessage RequestMessage
        {
            get
            {
                return _httpApiCall.RequestMessage;
            }

            set
            {
                _httpApiCall.RequestMessage = value;
            }
        }

        protected IHttpApiCall<T> HttpApiCall
        {
            get
            {
                return _httpApiCall;
            }
        }

        public Task<Tuple<HttpResponseMessage, T>> SendAsync(CancellationToken cancellationToken)
        {
            return _httpApiCall.SendAsync(cancellationToken);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
                _httpApiCall.Dispose();
        }

        protected virtual void HandleBeforeAsyncWebRequest(object sender, HttpRequestEventArgs e)
        {
            var handler = BeforeAsyncWebRequest;
            if (handler != null)
                handler(this, e);
        }

        protected virtual void HandleAfterAsyncWebResponse(object sender, HttpResponseEventArgs e)
        {
            var handler = AfterAsyncWebResponse;
            if (handler != null)
                handler(this, e);
        }
    }
}
