namespace OpenStack.Net
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using Rackspace.Threading;

#if !NET40PLUS
    using OpenStack.Compat;
#endif

    public class HttpApiCall : HttpApiCall<string>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HttpApiCall"/> class
        /// with the specified request message.
        /// </summary>
        /// <param name="requestMessage">The <see cref="HttpRequestMessage"/> representing the HTTP API request.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="requestMessage"/> is <see langword="null"/>.</exception>
        public HttpApiCall(HttpClient httpClient, HttpRequestMessage requestMessage, HttpCompletionOption completionOption)
            : base(httpClient, requestMessage, completionOption)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpApiCall"/> class
        /// with the specified request message.
        /// </summary>
        /// <param name="requestMessage">The <see cref="HttpRequestMessage"/> representing the HTTP API request.</param>
        /// <param name="disposeMessage"><see langword="true"/> to call <see cref="IDisposable.Dispose"/> on the <paramref name="requestMessage"/> object when this object is disposed; otherwise, <see langword="false"/>. The default value is <see langword="true"/>.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="requestMessage"/> is <see langword="null"/>.</exception>
        public HttpApiCall(HttpClient httpClient, HttpRequestMessage requestMessage, HttpCompletionOption completionOption, bool disposeMessage)
            : base(httpClient, requestMessage, completionOption, disposeMessage)
        {
        }

        public override Task<Tuple<HttpResponseMessage, string>> SendAsync(CancellationToken cancellationToken)
        {
            return HttpClient.SendAsync(RequestMessage, CompletionOption, cancellationToken)
                .Then(
                    task =>
                    {
                        if (task.Result.Content == null)
                            return CompletedTask.FromResult(Tuple.Create(task.Result, default(string)));

                        return task.Result.Content.ReadAsStringAsync()
                            .Select(innerTask => Tuple.Create(task.Result, innerTask.Result));
                    });
        }
    }
}
