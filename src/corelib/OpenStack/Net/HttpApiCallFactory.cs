namespace OpenStack.Net
{
    using System;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using Stream = System.IO.Stream;

    public class HttpApiCallFactory : IHttpApiCallFactory
    {
        private readonly HttpClient _httpClient;

        public HttpApiCallFactory()
            : this(new HttpClient())
        {
        }

        public HttpApiCallFactory(HttpClient httpClient)
        {
            if (httpClient == null)
                throw new ArgumentNullException("httpClient");

            _httpClient = httpClient;
        }

        protected HttpClient HttpClient
        {
            get
            {
                return _httpClient;
            }
        }

        public virtual HttpApiCall<T> CreateJsonApiCall<T>(HttpRequestMessage requestMessage)
        {
            return new JsonHttpApiCall<T>(HttpClient, requestMessage, HttpCompletionOption.ResponseContentRead);
        }

        public virtual HttpApiCall<Stream> CreateStreamingApiCall(HttpRequestMessage requestMessage)
        {
            return new StreamingHttpApiCall(HttpClient, requestMessage);
        }

        public HttpApiCall CreateBasicApiCall(HttpRequestMessage requestMessage)
        {
            return CreateBasicApiCall(requestMessage, HttpCompletionOption.ResponseContentRead);
        }

        public virtual HttpApiCall CreateBasicApiCall(HttpRequestMessage requestMessage, HttpCompletionOption completionOption)
        {
            return new HttpApiCall(HttpClient, requestMessage, completionOption);
        }

        public HttpApiCall<T> CreateCustomApiCall<T>(HttpRequestMessage requestMessage, HttpCompletionOption completionOption, Func<HttpResponseMessage, CancellationToken, Task<T>> deserializeResult)
        {
            return new CustomHttpApiCall<T>(HttpClient, requestMessage, completionOption, deserializeResult);
        }
    }
}
