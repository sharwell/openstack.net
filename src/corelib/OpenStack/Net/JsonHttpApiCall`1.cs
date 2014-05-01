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

        public JsonHttpApiCall(HttpClient httpClient, HttpRequestMessage requestMessage, HttpCompletionOption completionOption, Func<Task<HttpResponseMessage>, CancellationToken, Task<HttpResponseMessage>> validate)
            : base(httpClient, requestMessage, completionOption, validate)
        {
        }

        protected override Task<T> DeserializeResultImplAsync(HttpResponseMessage responseMessage, CancellationToken cancellationToken)
        {
            bool acceptable = HttpApiCall.IsAcceptable(responseMessage);
            return responseMessage.Content.ReadAsStringAsync()
                .Select(task => acceptable ? JsonConvert.DeserializeObject<T>(task.Result) : default(T));
        }
    }
}
