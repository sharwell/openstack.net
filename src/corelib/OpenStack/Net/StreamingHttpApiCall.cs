namespace OpenStack.Net
{
    using System;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using Stream = System.IO.Stream;

#if !NET40PLUS
    using OpenStack.Compat;
#endif

    public class StreamingHttpApiCall : HttpApiCall<Stream>
    {
        public StreamingHttpApiCall(HttpClient httpClient, HttpRequestMessage requestMessage)
            : base(httpClient, requestMessage, HttpCompletionOption.ResponseHeadersRead)
        {
        }

        public StreamingHttpApiCall(HttpClient httpClient, HttpRequestMessage requestMessage, Func<Task<HttpResponseMessage>, CancellationToken, Task<HttpResponseMessage>> validate)
            : base(httpClient, requestMessage, HttpCompletionOption.ResponseHeadersRead, validate)
        {
        }

        protected override Task<Stream> DeserializeResultImplAsync(HttpResponseMessage response, CancellationToken cancellationToken)
        {
            return response.Content.ReadAsStreamAsync();
        }
    }
}
