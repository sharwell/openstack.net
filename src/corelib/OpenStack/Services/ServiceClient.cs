namespace OpenStack.Services
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;
    using Newtonsoft.Json;
    using OpenStack.Net;
    using OpenStack.Security.Authentication;
    using OpenStack.Threading;
    using Rackspace.Net;
    using Rackspace.Threading;
    using CancellationToken = System.Threading.CancellationToken;
    using Encoding = System.Text.Encoding;
    using Stream = System.IO.Stream;

#if !NET40PLUS
    using OpenStack.Compat;
#endif

    /// <summary>
    /// Adds common functionality for all Rackspace Providers.
    /// </summary>
    /// <typeparam name="TProvider">The service provider interface this object implements.</typeparam>
    /// <threadsafety static="true" instance="false"/>
    public abstract class ServiceClient : IHttpApiCallFactory
    {
        /// <summary>
        /// The <see cref="IAuthenticationService"/> to use for authenticating requests to this provider.
        /// </summary>
        private readonly IAuthenticationService _authenticationService;

#if !PORTABLE
        /// <summary>
        /// This is the backing field for the <see cref="ConnectionLimit"/> property.
        /// </summary>
        private int? _connectionLimit;
#endif

        /// <summary>
        /// This is the backing field for the <see cref="DefaultRegion"/> property.
        /// </summary>
        private string _defaultRegion;

        /// <summary>
        /// This is the backing field for the <see cref="BackoffPolicy"/> property.
        /// </summary>
        private IBackoffPolicy _backoffPolicy;

        /// <summary>
        /// This is the backing field for the <see cref="HttpClient"/> property.
        /// </summary>
        private HttpClient _httpClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProviderBase{TProvider}"/> class using
        /// the specified default identity, default region, identity provider, and REST service
        /// implementation, and the default HTTP response code validator.
        /// </summary>
        /// <param name="defaultIdentity">The default identity to use for calls that do not explicitly specify an identity. If this value is <see langword="null"/>, no default identity is available so all calls must specify an explicit identity.</param>
        /// <param name="defaultRegion">The default region to use for calls that do not explicitly specify a region. If this value is <see langword="null"/>, the default region for the user will be used; otherwise if the service uses region-specific endpoints all calls must specify an explicit region.</param>
        /// <param name="identityProvider">The identity provider to use for authenticating requests to this provider. If this value is <see langword="null"/>, a new instance of <see cref="CloudIdentityProvider"/> is created using <paramref name="defaultIdentity"/> as the default identity.</param>
        protected ServiceClient(IAuthenticationService authenticationService, string defaultRegion)
        {
            _authenticationService = authenticationService;
            _defaultRegion = defaultRegion;
            _httpClient = new HttpClient();
        }

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

#if !PORTABLE
        /// <summary>
        /// Gets or sets the maximum number of connections allowed on the <see cref="ServicePoint"/>
        /// objects used for requests. If the value is <see langword="null"/>, the connection limit value for the
        /// <see cref="ServicePoint"/> object is not altered.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">If <paramref name="value"/> is less than or equal to 0.</exception>
        public int? ConnectionLimit
        {
            get
            {
                return _connectionLimit;
            }

            set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException("value");

                _connectionLimit = value;
            }
        }
#endif

        /// <summary>
        /// Gets the default region for this provider instance, if one was specified.
        /// </summary>
        /// <value>
        /// The default region to use for API calls where an explicit region is not specified in the call;
        /// or <see langword="null"/> to use the default region associated with the identity making the call.
        /// </value>
        public string DefaultRegion
        {
            get
            {
                return _defaultRegion;
            }
        }

        /// <summary>
        /// Gets or sets the back-off policy to use for polling operations.
        /// </summary>
        /// <remarks>
        /// If this value is set to <see langword="null"/>, the default back-off policy for the current
        /// provider will be used.
        /// </remarks>
        /// <preliminary/>
        public IBackoffPolicy BackoffPolicy
        {
            get
            {
                return _backoffPolicy ?? DefaultBackoffPolicy;
            }

            set
            {
                _backoffPolicy = value;
            }
        }

        /// <summary>
        /// Gets the default HTTP response validation method for requests sent to this service.
        /// </summary>
        /// <remarks>
        /// This property is intended to support extension methods which prepare <see cref="HttpApiCall{T}"/>
        /// instances which are not supported by the default client. The validation behavior itself may be
        /// customized by overriding <see cref="ValidateResultImplAsync"/> in a particular service client.
        /// </remarks>
        public Func<Task<HttpResponseMessage>, CancellationToken, Task<HttpResponseMessage>> DefaultResponseValidator
        {
            get
            {
                return ValidateResultImplAsync;
            }
        }

        protected IAuthenticationService AuthenticationService
        {
            get
            {
                return _authenticationService;
            }
        }

        /// <summary>
        /// Gets the default back-off policy for the current provider.
        /// </summary>
        /// <remarks>
        /// The default implementation returns <see cref="OpenStack.Threading.BackoffPolicy.Default"/>.
        /// Providers may override this property to change the default back-off policy.
        /// </remarks>
        /// <preliminary/>
        protected virtual IBackoffPolicy DefaultBackoffPolicy
        {
            get
            {
                return OpenStack.Threading.BackoffPolicy.Default;
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="HttpClient"/> to use for sending requests to the server.
        /// </summary>
        /// <value>
        /// The <see cref="HttpClient"/> to use for sending HTTP requests.
        /// </value>
        /// <exception cref="ArgumentNullException">If <paramref name="value"/> is <see langword="null"/>.</exception>
        public HttpClient HttpClient
        {
            get
            {
                return _httpClient;
            }

            protected set
            {
                if (value == null)
                    throw new ArgumentNullException("value");

                _httpClient = value;
            }
        }

        /// <summary>
        /// Creates a task continuation function responsible for creating an <see cref="HttpRequestMessage"/> for use
        /// in asynchronous REST API calls. The input to the continuation function is a completed task which
        /// computes an <see cref="IdentityToken"/> for an authenticated user and a base URI for use in binding
        /// the URI templates for REST API calls. The continuation function calls <see cref="PrepareRequestImpl"/>
        /// to create and prepare the resulting <see cref="HttpRequestMessage"/>.
        /// </summary>
        /// <param name="method">The <see cref="HttpMethod"/> to use for the request.</param>
        /// <param name="template">The <see cref="UriTemplate"/> for the target URI.</param>
        /// <param name="parameters">A collection of parameters for binding the URI template in a call to <see cref="UriTemplate.BindByName(Uri, IDictionary{string, string})"/>.</param>
        /// <param name="uriTransform">An optional transformation to apply to the bound URI for the request. If this value is <see langword="null"/>, the result of binding the <paramref name="template"/> with <paramref name="parameters"/> will be used as the absolute request URI.</param>
        /// <returns>A task continuation delegate which can be used to create an <see cref="HttpRequestMessage"/> following the completion of a task that obtains an <see cref="IdentityToken"/> and the base URI for a service.</returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="template"/> is <see langword="null"/>.
        /// <para>-or-</para>
        /// <para>If <paramref name="parameters"/> is <see langword="null"/>.</para>
        /// </exception>
        /// <preliminary/>
        protected Func<Task<Uri>, Task<HttpRequestMessage>> PrepareRequestAsyncFunc(HttpMethod method, UriTemplate template, IDictionary<string, string> parameters, CancellationToken cancellationToken, Func<Uri, Uri> uriTransform = null)
        {
            if (template == null)
                throw new ArgumentNullException("template");
            if (parameters == null)
                throw new ArgumentNullException("parameters");

            return
                task =>
                {
                    Uri baseUri = task.Result;
                    HttpRequestMessage request = PrepareRequestImpl(method, template, baseUri, parameters, uriTransform);

                    Func<Task<HttpRequestMessage>, Task<HttpRequestMessage>> authenticateRequest =
                        task2 => _authenticationService.AuthenticateRequestAsync(task2.Result, cancellationToken).Select(_ => task2.Result);
                    return CompletedTask.FromResult(request).Then(authenticateRequest);
                };
        }

        /// <summary>
        /// Creates a task continuation function responsible for creating an <see cref="HttpRequestMessage"/> for use
        /// in asynchronous REST API calls. The input to the continuation function is a completed task which
        /// computes an <see cref="IdentityToken"/> for an authenticated user and a base URI for use in binding
        /// the URI templates for REST API calls. The continuation function calls <see cref="PrepareRequestImpl"/>
        /// to create and prepare the resulting <see cref="HttpRequestMessage"/>, and then asynchronously obtains
        /// the request stream for the request and writes the specified <paramref name="body"/> in JSON notation.
        /// </summary>
        /// <typeparam name="TBody">The type modeling the body of the request.</typeparam>
        /// <param name="method">The <see cref="HttpMethod"/> to use for the request.</param>
        /// <param name="template">The <see cref="UriTemplate"/> for the target URI.</param>
        /// <param name="parameters">A collection of parameters for binding the URI template in a call to <see cref="UriTemplate.BindByName(Uri, IDictionary{string, string})"/>.</param>
        /// <param name="body">A object modeling the body of the web request. The object is serialized in JSON notation for inclusion in the request.</param>
        /// <param name="uriTransform">An optional transformation to apply to the bound URI for the request. If this value is <see langword="null"/>, the result of binding the <paramref name="template"/> with <paramref name="parameters"/> will be used as the absolute request URI.</param>
        /// <returns>A task continuation delegate which can be used to create an <see cref="HttpRequestMessage"/> following the completion of a task that obtains an <see cref="IdentityToken"/> and the base URI for a service.</returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="template"/> is <see langword="null"/>.
        /// <para>-or-</para>
        /// <para>If <paramref name="parameters"/> is <see langword="null"/>.</para>
        /// </exception>
        /// <preliminary/>
        protected Func<Task<Uri>, Task<HttpRequestMessage>> PrepareRequestAsyncFunc<TBody>(HttpMethod method, UriTemplate template, IDictionary<string, string> parameters, TBody body, CancellationToken cancellationToken, Func<Uri, Uri> uriTransform = null)
        {
            return
                task =>
                {
                    Uri baseUri = task.Result;
                    HttpRequestMessage request = PrepareRequestImpl(method, template, baseUri, parameters, uriTransform);
                    request.Content = EncodeRequestBodyImpl(request, body);

                    Func<Task<HttpRequestMessage>, Task<HttpRequestMessage>> authenticateRequest =
                        task2 => _authenticationService.AuthenticateRequestAsync(task2.Result, cancellationToken).Select(_ => task2.Result);
                    return CompletedTask.FromResult(request).Then(authenticateRequest);
                };
        }

        /// <summary>
        /// Encode the body of a request, and update the <see cref="HttpRequestMessage"/> properties
        /// as necessary to support the encoded body.
        /// </summary>
        /// <remarks>
        /// The default implementation uses <see cref="JsonConvert"/> to convert <paramref name="body"/>
        /// to JSON notation, and then uses <see cref="Encoding.UTF8"/> to encode the text. The
        /// <see cref="HttpContentHeaders.ContentType"/> and <see cref="HttpContentHeaders.ContentLength"/>
        /// properties are updated to reflect the JSON content.
        /// </remarks>
        /// <typeparam name="TBody">The type modeling the body of the request.</typeparam>
        /// <param name="request">The <see cref="HttpRequestMessage"/> object for the request.</param>
        /// <param name="body">The object modeling the body of the request.</param>
        /// <returns>The encoded content to send with the <see cref="HttpRequestMessage"/>.</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="request"/> is <see langword="null"/>.</exception>
        /// <preliminary/>
        protected virtual HttpContent EncodeRequestBodyImpl<TBody>(HttpRequestMessage request, TBody body)
        {
            if (request == null)
                throw new ArgumentNullException("request");

            string bodyText = JsonConvert.SerializeObject(body);
            byte[] encodedBody = Encoding.UTF8.GetBytes(bodyText);
            ByteArrayContent content = new ByteArrayContent(encodedBody);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json") { CharSet = "UTF-8" };

            content.Headers.ContentLength = encodedBody.Length;

            return content;
        }

        /// <summary>
        /// Creates and prepares an <see cref="HttpRequestMessage"/> for an asynchronous REST API call.
        /// </summary>
        /// <remarks>
        /// The base implementation sets the following properties of the web request.
        ///
        /// <list type="table">
        /// <listheader>
        /// <term>Property</term>
        /// <term>Value</term>
        /// </listheader>
        /// <item>
        /// <description><see cref="WebRequest.Method"/></description>
        /// <description><paramref name="method"/></description>
        /// </item>
        /// <item>
        /// <description><see cref="HttpRequestHeaders.Accept"/></description>
        /// <description><c>application/json</c></description>
        /// </item>
        /// <item>
        /// <description><see cref="WebRequest.Headers"/><literal>["X-Auth-Token"]</literal></description>
        /// <description><see name="IdentityToken.Id"/></description>
        /// </item>
        /// <item>
        /// <description><see cref="HttpRequestHeaders.UserAgent"/></description>
        /// <description><see cref="UserAgentGenerator.UserAgent"/></description>
        /// </item>
        /// <item>
        /// <description><see cref="ServicePoint.ConnectionLimit"/></description>
        /// <description><see cref="ConnectionLimit"/></description>
        /// </item>
        /// </list>
        /// </remarks>
        /// <param name="method">The <see cref="HttpMethod"/> to use for the request.</param>
        /// <param name="identityToken">The <see cref="IdentityToken"/> to use for making an authenticated REST API call.</param>
        /// <param name="template">The <see cref="UriTemplate"/> for the target URI.</param>
        /// <param name="baseUri">The base URI to use for binding the URI template.</param>
        /// <param name="parameters">A collection of parameters for binding the URI template in a call to <see cref="UriTemplate.BindByName(Uri, IDictionary{string, string})"/>.</param>
        /// <param name="uriTransform">An optional transformation to apply to the bound URI for the request. If this value is <see langword="null"/>, the result of binding the <paramref name="template"/> with <paramref name="parameters"/> will be used as the absolute request URI.</param>
        /// <returns>An <see cref="HttpRequestMessage"/> to use for making the asynchronous REST API call.</returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="identityToken"/> is <see langword="null"/>.
        /// <para>-or-</para>
        /// <para>If <paramref name="template"/> is <see langword="null"/>.</para>
        /// <para>-or-</para>
        /// <para>If <paramref name="baseUri"/> is <see langword="null"/>.</para>
        /// <para>-or-</para>
        /// <para>If <paramref name="parameters"/> is <see langword="null"/>.</para>
        /// </exception>
        /// <exception cref="ArgumentException">If <paramref name="baseUri"/> is not an absolute URI.</exception>
        /// <preliminary/>
        protected virtual HttpRequestMessage PrepareRequestImpl(HttpMethod method, UriTemplate template, Uri baseUri, IDictionary<string, string> parameters, Func<Uri, Uri> uriTransform)
        {
            Uri boundUri = template.BindByName(baseUri, parameters);
            if (uriTransform != null)
                boundUri = uriTransform(boundUri);

            HttpRequestMessage request = new HttpRequestMessage(method, boundUri);
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            request.Headers.UserAgent.Add(new ProductInfoHeaderValue(AssemblyInfo.AssemblyProduct, AssemblyInfo.AssemblyInformationalVersion));
#if !PORTABLE
            if (ConnectionLimit.HasValue)
            {
                ServicePoint servicePoint = ServicePointManager.FindServicePoint(boundUri);
                servicePoint.ConnectionLimit = ConnectionLimit.Value;
            }
#endif

            return request;
        }

        /// <summary>
        /// Gets the base absolute URI to use for making asynchronous REST API calls to this service.
        /// </summary>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> that the task will observe.</param>
        /// <returns>
        /// A <see cref="Task"/> object representing the asynchronous operation. When the task
        /// completes successfully, the <see cref="Task{TResult}.Result"/> property will contain
        /// a <see cref="Uri"/> representing the base absolute URI for the service.
        /// </returns>
        /// <preliminary/>
        protected abstract Task<Uri> GetBaseUriAsync(CancellationToken cancellationToken);

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

        /// <summary>
        /// Gets the response from an asynchronous web request, with the body of the response (if any) returned as a string.
        /// </summary>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> that the task will observe.</param>
        /// <returns>
        /// A continuation function delegate which takes an asynchronously prepared <see cref="HttpRequestMessage"/>
        /// and returns the resulting body of the operation, if any, as a string.
        /// </returns>
        /// <preliminary/>
        protected virtual Func<Task<HttpRequestMessage>, Task<string>> GetResponseAsyncFunc(CancellationToken cancellationToken)
        {
            Func<Task<HttpRequestMessage>, Task<HttpResponseMessage>> requestResource =
                task => RequestResourceImplAsync(task, cancellationToken);

            Func<Task<HttpResponseMessage>, Task<Tuple<HttpResponseMessage, string>>> readResult =
                task => ReadResultImpl(task, cancellationToken);

            Func<Task<Tuple<HttpResponseMessage, string>>, string> parseResult =
                task => task.Result.Item2;

            Func<Task<HttpRequestMessage>, Task<string>> result =
                task =>
                {
                    return task.Then(requestResource)
                        .Then(readResult)
                        .Select(parseResult);
                };

            return result;
        }

        /// <summary>
        /// Gets the response from an asynchronous web request, with the body of the response (if any) returned as an object of type <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The type for the response object.</typeparam>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> that the task will observe.</param>
        /// <param name="parseResult">
        /// A continuation function delegate which parses the body of the <see cref="HttpResponseMessage"/>
        /// and returns an object of type <typeparamref name="T"/>, as an asynchronous operation. If
        /// this value is <see langword="null"/>, the conversion will be performed by calling <see cref="ParseJsonResultImplAsync{T}"/>.
        /// </param>
        /// <returns>
        /// A continuation function delegate which takes an asynchronously prepared <see cref="HttpRequestMessage"/>
        /// and returns the resulting body of the operation, if any, as an instance of type <typeparamref name="T"/>.
        /// </returns>
        /// <preliminary/>
        protected virtual Func<Task<HttpRequestMessage>, Task<T>> GetResponseAsyncFunc<T>(CancellationToken cancellationToken, Func<Task<Tuple<HttpResponseMessage, string>>, Task<T>> parseResult = null)
        {
            Func<Task<HttpRequestMessage>, Task<HttpResponseMessage>> requestResource =
                task => RequestResourceImplAsync(task, cancellationToken);

            Func<Task<HttpResponseMessage>, Task<Tuple<HttpResponseMessage, string>>> readResult =
                task => ReadResultImpl(task, cancellationToken);

            if (parseResult == null)
            {
                parseResult = task => ParseJsonResultImplAsync<T>(task, cancellationToken);
            }

            Func<Task<HttpRequestMessage>, Task<T>> result =
                task =>
                {
                    return task.Then(requestResource)
                        .Then(readResult)
                        .Then(parseResult);
                };

            return result;
        }

        /// <summary>
        /// This method calls <see cref="OnBeforeAsyncWebRequest"/> and then asynchronously gets the response
        /// to the web request.
        /// </summary>
        /// <remarks>
        /// This method is the first step of implementing <see cref="GetResponseAsyncFunc"/> and <see cref="GetResponseAsyncFunc{T}"/>.
        /// </remarks>
        /// <param name="task">A task which created and prepared the <see cref="HttpRequestMessage"/>.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> that the task will observe.</param>
        /// <returns>A <see cref="Task"/> object representing the asynchronous operation.</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="task"/> is <see langword="null"/>.</exception>
        /// <preliminary/>
        protected virtual Task<HttpResponseMessage> RequestResourceImplAsync(Task<HttpRequestMessage> task, CancellationToken cancellationToken)
        {
            if (task == null)
                throw new ArgumentNullException("task");

            OnBeforeAsyncWebRequest(task.Result);
            return _httpClient.SendAsync(task.Result, HttpCompletionOption.ResponseContentRead, cancellationToken);
        }

        /// <summary>
        /// This method reads the complete body of an asynchronous <see cref="HttpResponseMessage"/> as a string.
        /// </summary>
        /// <param name="task">A <see cref="Task"/> object representing the asynchronous operation to get the <see cref="HttpResponseMessage"/>.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> that the task will observe.</param>
        /// <returns>A <see cref="Tuple{T1, T2}"/> object. The first element of the tuple contains the
        /// <see cref="HttpResponseMessage"/> provided by <paramref name="task"/> as an <see cref="HttpResponseMessage"/>.
        /// The second element of the tuple contains the complete body of the response as a string.
        /// </returns>
        /// <exception cref="ArgumentNullException">If <paramref name="task"/> is <see langword="null"/>.</exception>
        /// <preliminary/>
        protected virtual Task<Tuple<HttpResponseMessage, string>> ReadResultImpl(Task<HttpResponseMessage> task, CancellationToken cancellationToken)
        {
            if (task == null)
                throw new ArgumentNullException("task");

            HttpResponseMessage response = task.Result;
            OnAfterAsyncWebResponse(task);
            return ValidateResultImplAsync(task, cancellationToken)
                .Then(innerTask => innerTask.Result.Content.ReadAsStringAsync())
                .Select(innerTask => Tuple.Create(response, innerTask.Result));
        }

        /// <summary>
        /// This method validates the response to an asynchronous web request was successful.
        /// </summary>
        /// <remarks>
        /// The default implementation determines if the call was successful by checking the
        /// <see cref="HttpResponseMessage.IsSuccessStatusCode"/> property of the result
        /// message. This validation is performed synchronously, and upon success the method
        /// simply returns <paramref name="task"/>, which has already completed.
        ///
        /// <note type="implement">
        /// Most overriding implementations will not need to transform the resulting
        /// <see cref="HttpResponseMessage"/>, and may simply return <paramref name="task"/>
        /// for efficiency. Another option in this case is to perform custom validation first,
        /// and if that validation succeeds return the result of the base implementation.
        /// </note>
        /// </remarks>
        /// <param name="task">The antecedent task, which provides the <see cref="HttpResponseMessage"/> from the service.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> that the task will observe.</param>
        /// <returns>
        /// A <see cref="Task"/> object representing the asynchronous operation. When the operation
        /// completes successfully, the <see cref="Task{TResult}.Result"/> property will contain the
        /// <see cref="HttpResponseMessage"/> providing information about the result of the call.
        /// </returns>
        /// <exception cref="HttpWebException">
        /// If the <see cref="HttpResponseMessage"/> provided by the antecedent <paramref name="task"/>
        /// indicates that the request failed.
        /// </exception>
        protected virtual Task<HttpResponseMessage> ValidateResultImplAsync(Task<HttpResponseMessage> task, CancellationToken cancellationToken)
        {
            if (task.Result.IsSuccessStatusCode)
                return task;

            throw new HttpWebException(task.Result);
        }

        /// <summary>
        /// Provides a default object parser for <see cref="GetResponseAsyncFunc{T}"/> which converts the
        /// body of an <see cref="HttpResponseMessage"/> to an object of type <typeparamref name="T"/> by calling
        /// <see cref="JsonConvert.DeserializeObject{T}(String)"/>
        /// </summary>
        /// <typeparam name="T">The type for the response object.</typeparam>
        /// <param name="task">A <see cref="Task"/> object representing the asynchronous operation to get the <see cref="HttpResponseMessage"/>.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> that the task will observe.</param>
        /// <returns>
        /// A <see cref="Task"/> object representing the asynchronous operation. When the operation
        /// completes successfully, the <see cref="Task{TResult}.Result"/> property will contain an
        /// object of type <typeparamref name="T"/> representing the serialized body of the response.
        /// </returns>
        /// <exception cref="ArgumentNullException">If <paramref name="task"/> is <see langword="null"/>.</exception>
        /// <preliminary/>
        protected virtual Task<T> ParseJsonResultImplAsync<T>(Task<Tuple<HttpResponseMessage, string>> task, CancellationToken cancellationToken)
        {
            return CompletedTask.FromResult(JsonConvert.DeserializeObject<T>(task.Result.Item2));
        }

        /// <summary>
        /// This method creates an instance of <see cref="HttpApiCall{T}"/> representing a call
        /// to an HTTP API that returns a JSON response. The response body is deserialized to an
        /// instance of <typeparamref name="T"/> using <see cref="JsonConvert"/>.
        /// </summary>
        /// <typeparam name="T">The type modeling the JSON body of the response.</typeparam>
        /// <param name="requestMessage">The request message.</param>
        /// <returns>
        /// An instance of <see cref="HttpApiCall{T}"/> representing an HTTP API call that
        /// returns a JSON body.
        /// </returns>
        /// <exception cref="ArgumentNullException">If <paramref name="requestMessage"/> is <see langword="null"/>.</exception>
        protected virtual HttpApiCall<T> CreateJsonApiCall<T>(HttpRequestMessage requestMessage)
        {
            var result = new JsonHttpApiCall<T>(HttpClient, requestMessage, HttpCompletionOption.ResponseContentRead, ValidateResultImplAsync);
            return RegisterApiCall(result);
        }

        /// <summary>
        /// This method creates an instance of <see cref="HttpApiCall{T}"/> representing a call
        /// to an HTTP API that returns streaming content. An instance of <see cref="Stream"/>
        /// is provided for reading the content.
        /// </summary>
        /// <param name="requestMessage">The request message.</param>
        /// <returns>
        /// An instance of <see cref="HttpApiCall{T}"/> representing an HTTP API call that
        /// returns a <see cref="Stream"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">If <paramref name="requestMessage"/> is <see langword="null"/>.</exception>
        protected virtual HttpApiCall<Stream> CreateStreamingApiCall(HttpRequestMessage requestMessage)
        {
            var result = new StreamingHttpApiCall(HttpClient, requestMessage, ValidateResultImplAsync);
            return RegisterApiCall(result);
        }

        protected virtual HttpApiCall CreateBasicApiCall(HttpRequestMessage requestMessage)
        {
            return CreateBasicApiCall(requestMessage, HttpCompletionOption.ResponseContentRead);
        }

        protected virtual HttpApiCall CreateBasicApiCall(HttpRequestMessage requestMessage, HttpCompletionOption completionOption)
        {
            var result = new HttpApiCall(HttpClient, requestMessage, completionOption, ValidateResultImplAsync);
            return RegisterApiCall(result);
        }

        protected virtual HttpApiCall<T> CreateCustomApiCall<T>(HttpRequestMessage requestMessage, HttpCompletionOption completionOption, Func<HttpResponseMessage, CancellationToken, Task<T>> deserializeResult)
        {
            var result = new CustomHttpApiCall<T>(HttpClient, requestMessage, completionOption, ValidateResultImplAsync, deserializeResult);
            return RegisterApiCall(result);
        }

        protected virtual T RegisterApiCall<T>(T call)
            where T : IHttpApiRequest
        {
            if (call == null)
                throw new ArgumentNullException("call");

            call.BeforeAsyncWebRequest += (sender, e) => OnBeforeAsyncWebRequest(e.Request);
            call.AfterAsyncWebResponse += (sender, e) => OnAfterAsyncWebResponse(e.Response);
            return call;
        }

        #region IHttpApiCallFactory Members

        HttpApiCall<T> IHttpApiCallFactory.CreateJsonApiCall<T>(HttpRequestMessage requestMessage)
        {
            return CreateJsonApiCall<T>(requestMessage);
        }

        HttpApiCall<Stream> IHttpApiCallFactory.CreateStreamingApiCall(HttpRequestMessage requestMessage)
        {
            return CreateStreamingApiCall(requestMessage);
        }

        HttpApiCall IHttpApiCallFactory.CreateBasicApiCall(HttpRequestMessage requestMessage, HttpCompletionOption completionOption)
        {
            return CreateBasicApiCall(requestMessage, completionOption);
        }

        HttpApiCall<T> IHttpApiCallFactory.CreateCustomApiCall<T>(HttpRequestMessage requestMessage, HttpCompletionOption completionOption, Func<HttpResponseMessage, CancellationToken, Task<T>> deserializeResult)
        {
            return CreateCustomApiCall<T>(requestMessage, completionOption, deserializeResult);
        }

        #endregion
    }
}
