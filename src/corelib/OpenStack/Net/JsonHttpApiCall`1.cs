namespace OpenStack.Net
{
    using System;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using Newtonsoft.Json;
    using Rackspace.Threading;

#if !NET40PLUS
    using OpenStack.Compat;
#endif

    public class JsonHttpApiCall<T> : HttpApiCall<T>
    {
        public JsonHttpApiCall(HttpClient httpClient, HttpRequestMessage requestMessage, HttpCompletionOption completionOption)
            : base(httpClient, requestMessage, completionOption)
        {
        }

        public JsonHttpApiCall(HttpClient httpClient, HttpRequestMessage requestMessage, HttpCompletionOption completionOption, bool disposeMessage)
            : base(httpClient, requestMessage, completionOption, disposeMessage)
        {
        }

        public override Task<Tuple<HttpResponseMessage, T>> SendAsync(CancellationToken cancellationToken)
        {
            return HttpClient.SendAsync(RequestMessage, CompletionOption, cancellationToken)
                .Then(
                    task =>
                    {
                        return DeserializeResultAsync(task.Result, cancellationToken)
                            .Select(innerTask => Tuple.Create(task.Result, innerTask.Result));
                    });
        }

        protected virtual Task<T> DeserializeResultAsync(HttpResponseMessage responseMessage, CancellationToken cancellationToken)
        {
            return responseMessage.Content.ReadAsStringAsync()
                .Select(task => JsonConvert.DeserializeObject<T>(task.Result));
        }
    }
}
