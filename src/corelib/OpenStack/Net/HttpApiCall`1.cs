namespace OpenStack.Net
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;

#if NET35
    using OpenStack.Compat;
#endif

    /// <summary>
    /// This class represents a call to an HTTP API. It provides clients the opportunity to inspect
    /// and/or modify the <see cref="RequestMessage"/> prior to sending the request, without losing
    /// the ability to obtain a strongly-typed result from <see cref="SendAsync"/>.
    /// </summary>
    /// <typeparam name="T">The class modeling the response returned by the call.</typeparam>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    public abstract class HttpApiCall<T> : IDisposable
    {
        /// <summary>
        /// This is the backing field for the <see cref="HttpClient"/> property.
        /// </summary>
        private HttpClient _httpClient;

        /// <summary>
        /// This is the backing field for the <see cref="RequestMessage"/> property.
        /// </summary>
        private readonly HttpRequestMessage _requestMessage;

        /// <summary>
        /// This is the backing field for the <see cref="CompletionOption"/> property.
        /// </summary>
        private HttpCompletionOption _completionOption;

        /// <summary>
        /// This value controls whether or not <see cref="IDisposable.Dispose"/> is call on the
        /// <see cref="RequestMessage"/> object when the current object is disposed.
        /// </summary>
        private readonly bool _disposeMessage;

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpApiCall{T}"/> class
        /// with the specified request message.
        /// </summary>
        /// <param name="requestMessage">The <see cref="HttpRequestMessage"/> representing the HTTP API request.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="requestMessage"/> is <see langword="null"/>.</exception>
        public HttpApiCall(HttpClient httpClient, HttpRequestMessage requestMessage, HttpCompletionOption completionOption)
            : this(httpClient, requestMessage, completionOption, true)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpApiCall{T}"/> class
        /// with the specified request message.
        /// </summary>
        /// <param name="requestMessage">The <see cref="HttpRequestMessage"/> representing the HTTP API request.</param>
        /// <param name="disposeMessage"><see langword="true"/> to call <see cref="IDisposable.Dispose"/> on the <paramref name="requestMessage"/> object when this object is disposed; otherwise, <see langword="false"/>. The default value is <see langword="true"/>.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="requestMessage"/> is <see langword="null"/>.</exception>
        public HttpApiCall(HttpClient httpClient, HttpRequestMessage requestMessage, HttpCompletionOption completionOption, bool disposeMessage)
        {
            if (httpClient == null)
                throw new ArgumentNullException("httpClient");
            if (requestMessage == null)
                throw new ArgumentNullException("requestMessage");

            _httpClient = httpClient;
            _requestMessage = requestMessage;
            _disposeMessage = disposeMessage;
            _completionOption = completionOption;
        }

        /// <summary>
        /// Gets or sets the <see cref="HttpClient"/> to use for sending the request.
        /// </summary>
        public HttpClient HttpClient
        {
            get
            {
                return _httpClient;
            }

            set
            {
                if (value == null)
                    throw new ArgumentNullException("value");

                _httpClient = value;
            }
        }

        /// <summary>
        /// Gets the <see cref="HttpRequestMessage"/> used by this HTTP API call.
        /// </summary>
        public HttpRequestMessage RequestMessage
        {
            get
            {
                return _requestMessage;
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="HttpCompletionOption"/> to use for the HTTP request.
        /// </summary>
        public HttpCompletionOption CompletionOption
        {
            get
            {
                return _completionOption;
            }

            set
            {
                _completionOption = value;
            }
        }

        /// <summary>
        /// Asynchronously send the HTTP API request, and return the result as a strongly-typed
        /// deserialized value.
        /// </summary>
        /// <param name="httpClient">The <see cref="HttpClient"/> to use for sending the request.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> the task will observe.</param>
        /// <returns>
        /// A <see cref="Task"/> instance representing the asynchronous operation. When the task
        /// completes successfully, the <see cref="Task{TResult}.Result"/> property will contain
        /// the a tuple; the first element is the <see cref="HttpResponseMessage"/> and the second
        /// element is the strongly-typed result of the HTTP API call.
        /// </returns>
        /// <exception cref="WebException">If the HTTP API request does not return successfully.</exception>
        public abstract Task<Tuple<HttpResponseMessage, T>> SendAsync(CancellationToken cancellationToken);

        /// <inheritdoc/>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases the managed and unmanaged resources owned by this instance.
        /// </summary>
        /// <param name="disposing"><see langword="true"/> if this method is called from <see cref="Dispose()"/>; otherwise, <see langword="false"/> if this method is called from a finalizer.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing && _disposeMessage)
            {
                _requestMessage.Dispose();
            }
        }
    }
}
