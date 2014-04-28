namespace OpenStack.Net
{
    using System;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;

    public class CustomHttpApiCall<T> : HttpApiCall<T>
    {
        private readonly Func<HttpResponseMessage, CancellationToken, Task<T>> _deserializeResult;

        public CustomHttpApiCall(HttpClient httpClient, HttpRequestMessage requestMessage, HttpCompletionOption completionOption, Func<HttpResponseMessage, CancellationToken, Task<T>> deserializeResult)
            : base(httpClient, requestMessage, completionOption)
        {
            if (deserializeResult == null)
                throw new ArgumentNullException("deserializeResult");

            _deserializeResult = deserializeResult;
        }

        public CustomHttpApiCall(HttpClient httpClient, HttpRequestMessage requestMessage, HttpCompletionOption completionOption, Func<Task<HttpResponseMessage>, CancellationToken, Task<HttpResponseMessage>> validate, Func<HttpResponseMessage, CancellationToken, Task<T>> deserializeResult)
            : base(httpClient, requestMessage, completionOption, validate)
        {
            if (deserializeResult == null)
                throw new ArgumentNullException("deserializeResult");

            _deserializeResult = deserializeResult;
        }

        public CustomHttpApiCall(HttpClient httpClient, HttpRequestMessage requestMessage, HttpCompletionOption completionOption, Func<Task<HttpResponseMessage>, CancellationToken, Task<HttpResponseMessage>> validate, bool disposeMessage, Func<HttpResponseMessage, CancellationToken, Task<T>> deserializeResult)
            : base(httpClient, requestMessage, completionOption, validate, disposeMessage)
        {
            if (deserializeResult == null)
                throw new ArgumentNullException("deserializeResult");

            _deserializeResult = deserializeResult;
        }

        protected override Task<T> DeserializeResultImplAsync(HttpResponseMessage response, CancellationToken cancellationToken)
        {
            return _deserializeResult(response, cancellationToken);
        }
    }
}
