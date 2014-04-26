namespace OpenStack.Net
{
    using System;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using Rackspace.Threading;
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

        public StreamingHttpApiCall(HttpClient httpClient, HttpRequestMessage requestMessage, bool disposeMessage)
            : base(httpClient, requestMessage, HttpCompletionOption.ResponseHeadersRead, disposeMessage)
        {
        }

        public override Task<Tuple<HttpResponseMessage, Stream>> SendAsync(CancellationToken cancellationToken)
        {
            return HttpClient.SendAsync(RequestMessage, CompletionOption, cancellationToken)
                .Then(task => task.Result.Content.ReadAsStreamAsync().Select(innerTask => Tuple.Create(task.Result, innerTask.Result)));
        }
    }
}
