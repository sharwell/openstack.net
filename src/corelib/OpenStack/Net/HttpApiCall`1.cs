namespace OpenStack.Net
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using Rackspace.Threading;

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
        private static readonly Func<Task<HttpResponseMessage>, CancellationToken, Task<HttpResponseMessage>> DefaultResponseValidator =
            (task, cancellationToken) => task;

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

        private readonly Func<Task<HttpResponseMessage>, CancellationToken, Task<HttpResponseMessage>> _validate;

        /// <summary>
        /// This event is fired immediately before sending an asynchronous web request.
        /// </summary>
        /// <preliminary/>
        public event EventHandler<HttpRequestEventArgs> BeforeAsyncWebRequest;

        /// <summary>
        /// This event is fired when the result of an asynchronous web request is received.
        /// </summary>
        /// <preliminary/>
        public event EventHandler<HttpResponseEventArgs> AfterAsyncWebResponse;

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpApiCall{T}"/> class
        /// with the specified request message.
        /// </summary>
        /// <param name="requestMessage">The <see cref="HttpRequestMessage"/> representing the HTTP API request.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="requestMessage"/> is <see langword="null"/>.</exception>
        public HttpApiCall(HttpClient httpClient, HttpRequestMessage requestMessage, HttpCompletionOption completionOption)
            : this(httpClient, requestMessage, completionOption, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpApiCall{T}"/> class
        /// with the specified request message.
        /// </summary>
        /// <param name="requestMessage">The <see cref="HttpRequestMessage"/> representing the HTTP API request.</param>
        /// <param name="disposeMessage"><see langword="true"/> to call <see cref="IDisposable.Dispose"/> on the <paramref name="requestMessage"/> object when this object is disposed; otherwise, <see langword="false"/>. The default value is <see langword="true"/>.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="requestMessage"/> is <see langword="null"/>.</exception>
        public HttpApiCall(HttpClient httpClient, HttpRequestMessage requestMessage, HttpCompletionOption completionOption, Func<Task<HttpResponseMessage>, CancellationToken, Task<HttpResponseMessage>> validate)
        {
            if (httpClient == null)
                throw new ArgumentNullException("httpClient");
            if (requestMessage == null)
                throw new ArgumentNullException("requestMessage");

            _httpClient = httpClient;
            _requestMessage = requestMessage;
            _completionOption = completionOption;
            _validate = validate ?? DefaultResponseValidator;
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

        protected Func<Task<HttpResponseMessage>, CancellationToken, Task<HttpResponseMessage>> ValidateCallback
        {
            get
            {
                return _validate;
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
        public Task<Tuple<HttpResponseMessage, T>> SendAsync(CancellationToken cancellationToken)
        {
            return SendImplAsync(cancellationToken)
                .Then(task => ValidateCallback(task, cancellationToken))
                .Then(
                    task =>
                    {
                        return DeserializeResultImplAsync(task.Result, cancellationToken)
                            .Select(innerTask => Tuple.Create(task.Result, innerTask.Result));
                    });
        }

        /// <summary>
        /// Invokes the <see cref="BeforeAsyncWebRequest"/> event for the specified <paramref name="request"/>.
        /// </summary>
        /// <param name="request">The web request.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="request"/> is <see langword="null"/>.</exception>
        /// <preliminary/>
        protected virtual void OnBeforeAsyncWebRequest(HttpRequestMessage request)
        {
            var handler = BeforeAsyncWebRequest;
            if (handler != null)
                handler(this, new HttpRequestEventArgs(request));
        }

        /// <summary>
        /// Invokes the <see cref="AfterAsyncWebResponse"/> event for the specified <paramref name="response"/>.
        /// </summary>
        /// <param name="response">The web response.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="response"/> is <see langword="null"/>.</exception>
        /// <preliminary/>
        protected virtual void OnAfterAsyncWebResponse(Task<HttpResponseMessage> response)
        {
            if (response == null)
                throw new ArgumentNullException("response");

            var handler = AfterAsyncWebResponse;
            if (handler != null)
                handler(this, new HttpResponseEventArgs(response));
        }

        protected virtual Task<HttpResponseMessage> SendImplAsync(CancellationToken cancellationToken)
        {
            OnBeforeAsyncWebRequest(RequestMessage);
            return HttpClient.SendAsync(RequestMessage, CompletionOption, cancellationToken)
                .Finally(task => OnAfterAsyncWebResponse(task));
        }

        protected abstract Task<T> DeserializeResultImplAsync(HttpResponseMessage response, CancellationToken cancellationToken);

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
            if (disposing)
            {
                _requestMessage.Dispose();
            }
        }
    }
}
