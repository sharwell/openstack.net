namespace OpenStack.Services.Queues.V1
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading;
    using System.Threading.Tasks;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using OpenStack.Collections;
    using OpenStack.Compat;
    using OpenStack.Net;
    using OpenStack.ObjectModel.JsonHome;
    using OpenStack.Security.Authentication;
    using Rackspace.Net;
    using Rackspace.Threading;
    using Link = OpenStack.Services.Compute.V2.Link;

    /// <summary>
    /// This class provides a default implementation of <see cref="IQueuesService"/> suitable for
    /// connecting to OpenStack-compatible installations of the Queues Service V1.
    /// </summary>
    /// <seealso href="https://wiki.openstack.org/w/index.php?title=Marconi/specs/api/v1">OpenStack Marconi API v1 Blueprint</seealso>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    public class QueuesClient : ServiceClient, IQueuesService
    {
        /// <summary>
        /// Specifies whether the public or internal base address
        /// should be used for accessing the object storage service.
        /// </summary>
        private readonly bool _internalUrl;

        /// <summary>
        /// This is the backing field for the <see cref="ClientId"/> property.
        /// </summary>
        private readonly Guid _clientId;

        /// <summary>
        /// Initializes a new instance of the <see cref="QueuesClient"/> class with the
        /// specified authentication service, default region, client ID, and value
        /// indicating whether an internal or public endpoint should be used for
        /// communicating with the service.
        /// </summary>
        /// <param name="authenticationService">The authentication service to use for authenticating requests made to this service.</param>
        /// <param name="defaultRegion">The preferred region for the service. Unless otherwise specified for a specific client, derived service clients will not use a default region if this value is <see langword="null"/> (i.e. only regionless or global service endpoints will be considered acceptable).</param>
        /// <param name="clientId">The value of the <strong>Client-Id</strong> header to send with message requests from this service.</param>
        /// <param name="internalUrl"><see langword="true"/> to access the service over a local network; otherwise, <see langword="false"/> to access the service over a public network (the internet).</param>
        /// <exception cref="ArgumentNullException">If <paramref name="authenticationService"/> is <see langword="null"/>.</exception>
        public QueuesClient(IAuthenticationService authenticationService, string defaultRegion, Guid clientId, bool internalUrl)
            : base(authenticationService, defaultRegion)
        {
            _clientId = clientId;
            _internalUrl = internalUrl;
        }

        /// <summary>
        /// Gets a value indicating whether the service should be accessed over a local or public network.
        /// </summary>
        /// <value>
        /// <see langword="true"/> to access the service over a local network.
        /// <para>-or-</para>
        /// <para><see langword="false"/> to access the service over a public network (the internet).</para>
        /// </value>
        protected bool InternalUrl
        {
            get
            {
                return _internalUrl;
            }
        }

        /// <summary>
        /// Gets the value of the <strong>Client-Id</strong> header to send with message requests from this service.
        /// </summary>
        /// <value>
        /// The value of the <strong>Client-Id</strong> header to send with message requests from this service.
        /// </value>
        protected Guid ClientId
        {
            get
            {
                return _clientId;
            }
        }

        #region IQueuesService Members

        /// <inheritdoc/>
        public Task<GetHomeApiCall> PrepareGetHomeAsync(CancellationToken cancellationToken)
        {
            UriTemplate template = new UriTemplate("/v1");
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            return GetBaseUriAsync(cancellationToken)
                .Then(PrepareRequestAsyncFunc(HttpMethod.Get, template, parameters, cancellationToken))
                .Select(
                    task =>
                    {
                        /* Do not clear the Accept header. We are intentionally accepting both application/json
                         * and application/json-home to account for known cases where the service doesn't properly
                         * distinguish between these values, resulting in request/response content type
                         * mismatches.
                         */
                        //task.Result.Headers.Accept.Clear();
                        task.Result.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json-home"));
                        return new GetHomeApiCall(CreateJsonApiCall<HomeDocument>(task.Result));
                    });
        }

        /// <inheritdoc/>
        public Task<GetNodeHealthApiCall> PrepareGetNodeHealthAsync(CancellationToken cancellationToken)
        {
            UriTemplate template = new UriTemplate("health");
            Dictionary<string, string> parameters = new Dictionary<string, string>();

            Func<HttpResponseMessage, CancellationToken, Task<bool>> deserializeResult =
                (responseMessage, _) =>
                {
                    return CompletedTask.FromResult(responseMessage.StatusCode >= (HttpStatusCode)200 && responseMessage.StatusCode < (HttpStatusCode)300);
                };

            return GetBaseUriAsync(cancellationToken)
                .Then(PrepareRequestAsyncFunc(HttpMethod.Get, template, parameters, cancellationToken))
                .Select(task => new GetNodeHealthApiCall(CreateCustomApiCall(task.Result, HttpCompletionOption.ResponseContentRead, deserializeResult)));
        }

        /// <summary>
        /// This helper method determines if a specified <see cref="HttpRequestMessage"/> represents a
        /// request from the <see cref="PrepareGetNodeHealthAsync"/> method.
        /// </summary>
        /// <remarks>
        /// This method is used by <see cref="ValidateResultImplAsync"/> to support specialized handling
        /// for the HTTP response to the <see cref="PrepareGetNodeHealthAsync"/> call. Unlike most calls,
        /// the status code <see cref="HttpStatusCode.ServiceUnavailable"/> indicates a successful response
        /// from this call.
        /// </remarks>
        /// <param name="message">The request message, which may be <see langword="null"/>.</param>
        /// <returns><see langword="true"/> if <paramref name="message"/> has the form of a message created by <see cref="PrepareGetNodeHealthAsync"/>; otherwise, <see langword="false"/>.</returns>
        protected virtual bool IsNodeHealthMessage(HttpRequestMessage message)
        {
            if (message == null)
                return false;

            if (message.Method != HttpMethod.Get)
                return false;

            string[] uriSegments = message.RequestUri.GetSegments();
            if (uriSegments.Length == 0 || !string.Equals("health", uriSegments[uriSegments.Length - 1], StringComparison.OrdinalIgnoreCase))
                return false;

            return true;
        }

        /// <inheritdoc/>
        public Task<CreateQueueApiCall> PrepareCreateQueueAsync(QueueName queueName, CancellationToken cancellationToken)
        {
            if (queueName == null)
                throw new ArgumentNullException("queueName");

            UriTemplate template = new UriTemplate("queues/{queue_name}");
            var parameters = new Dictionary<string, string> { { "queue_name", queueName.Value } };

            Func<HttpResponseMessage, CancellationToken, Task<bool>> deserializeResult =
                (responseMessage, _) =>
                {
                    if (responseMessage.StatusCode == HttpStatusCode.Created)
                        return CompletedTask.FromResult(true);

                    return CompletedTask.FromResult(false);
                };

            return GetBaseUriAsync(cancellationToken)
                .Then(PrepareRequestAsyncFunc(HttpMethod.Put, template, parameters, cancellationToken))
                .Select(task => new CreateQueueApiCall(CreateCustomApiCall(task.Result, HttpCompletionOption.ResponseContentRead, deserializeResult)));
        }

        /// <inheritdoc/>
        public Task<ListQueuesApiCall> PrepareListQueuesAsync(CancellationToken cancellationToken)
        {
            UriTemplate template = new UriTemplate("queues");
            var parameters = new Dictionary<string, string>();

            Func<HttpResponseMessage, CancellationToken, Task<ReadOnlyCollectionPage<Queue>>> deserializeResult =
                (responseMessage, _) =>
                {
                    if (responseMessage.StatusCode == HttpStatusCode.NoContent)
                        return CompletedTask.FromResult(ReadOnlyCollectionPage<Queue>.Empty);

                    if (!HttpApiCall.IsAcceptable(responseMessage))
                        throw new HttpWebException(responseMessage);

                    Uri originalUri = responseMessage.RequestMessage.RequestUri;
                    return responseMessage.Content.ReadAsStringAsync()
                        .Select(
                            innerTask =>
                            {
                                if (string.IsNullOrEmpty(innerTask.Result))
                                    return null;

                                JObject responseObject = JsonConvert.DeserializeObject<JObject>(innerTask.Result);
                                JArray queuesArray = responseObject["queues"] as JArray;
                                if (queuesArray == null)
                                    return null;

                                IList<Queue> list = queuesArray.ToObject<Queue[]>();
                                Func<CancellationToken, Task<ReadOnlyCollectionPage<Queue>>> getNextPageAsync =
                                    CreateGetNextPageAsyncDelegate<ListQueuesApiCall, Queue>(PrepareListQueuesAsync, responseObject);

                                ReadOnlyCollectionPage<Queue> results = new BasicReadOnlyCollectionPage<Queue>(list, getNextPageAsync);
                                return results;
                            });
                };

            return GetBaseUriAsync(cancellationToken)
                .Then(PrepareRequestAsyncFunc(HttpMethod.Get, template, parameters, cancellationToken))
                .Select(task => new ListQueuesApiCall(CreateCustomApiCall(task.Result, HttpCompletionOption.ResponseContentRead, deserializeResult)));
        }

        /// <inheritdoc/>
        public Task<QueueExistsApiCall> PrepareQueueExistsAsync(QueueName queueName, CancellationToken cancellationToken)
        {
            if (queueName == null)
                throw new ArgumentNullException("queueName");

            UriTemplate template = new UriTemplate("queues/{queue_name}");
            var parameters = new Dictionary<string, string> { { "queue_name", queueName.Value } };

            Func<HttpResponseMessage, CancellationToken, Task<bool>> deserializeResult =
                (responseMessage, _) => CompletedTask.FromResult(responseMessage.IsSuccessStatusCode);

            return GetBaseUriAsync(cancellationToken)
                .Then(PrepareRequestAsyncFunc(HttpMethod.Head, template, parameters, cancellationToken))
                .Select(task => new QueueExistsApiCall(CreateCustomApiCall(task.Result, HttpCompletionOption.ResponseContentRead, deserializeResult)));
        }

        /// <summary>
        /// This helper method determines if a specified <see cref="HttpRequestMessage"/> represents a
        /// request from the <see cref="PrepareQueueExistsAsync"/> method.
        /// </summary>
        /// <remarks>
        /// This method is used by <see cref="ValidateResultImplAsync"/> to support specialized handling
        /// for the HTTP response to the <see cref="PrepareQueueExistsAsync"/> call. Unlike most calls, the
        /// status code <see cref="HttpStatusCode.NotFound"/> indicates a successful response from this
        /// call.
        /// </remarks>
        /// <param name="message">The request message, which may be <see langword="null"/>.</param>
        /// <returns><see langword="true"/> if <paramref name="message"/> has the form of a message created by <see cref="PrepareQueueExistsAsync"/>; otherwise, <see langword="false"/>.</returns>
        protected virtual bool IsQueueExistsMessage(HttpRequestMessage message)
        {
            if (message == null)
                return false;

            if (message.Method != HttpMethod.Head)
                return false;

            string[] uriSegments = message.RequestUri.GetSegments();
            if (uriSegments.Length < 2 || !string.Equals("queues/", uriSegments[uriSegments.Length - 2], StringComparison.Ordinal))
                return false;

            return true;
        }

        /// <inheritdoc/>
        public Task<RemoveQueueApiCall> PrepareRemoveQueueAsync(QueueName queueName, CancellationToken cancellationToken)
        {
            if (queueName == null)
                throw new ArgumentNullException("queueName");

            UriTemplate template = new UriTemplate("queues/{queue_name}");
            var parameters = new Dictionary<string, string> { { "queue_name", queueName.Value } };
            return GetBaseUriAsync(cancellationToken)
                .Then(PrepareRequestAsyncFunc(HttpMethod.Delete, template, parameters, cancellationToken))
                .Select(task => new RemoveQueueApiCall(CreateBasicApiCall(task.Result)));
        }

        /// <inheritdoc/>
        public Task<SetQueueMetadataApiCall> PrepareSetQueueMetadataAsync<T>(QueueName queueName, T metadata, CancellationToken cancellationToken)
        {
            if (queueName == null)
                throw new ArgumentNullException("queueName");
            if (metadata == null)
                throw new ArgumentNullException("metadata");

            UriTemplate template = new UriTemplate("queues/{queue_name}/metadata");
            var parameters = new Dictionary<string, string> { { "queue_name", queueName.Value } };
            return GetBaseUriAsync(cancellationToken)
                .Then(PrepareRequestAsyncFunc(HttpMethod.Put, template, parameters, metadata, cancellationToken))
                .Select(task => new SetQueueMetadataApiCall(CreateBasicApiCall(task.Result)));
        }

        /// <inheritdoc/>
        public Task<GetQueueMetadataApiCall<T>> PrepareGetQueueMetadataAsync<T>(QueueName queueName, CancellationToken cancellationToken)
        {
            if (queueName == null)
                throw new ArgumentNullException("queueName");

            UriTemplate template = new UriTemplate("queues/{queue_name}/metadata");
            var parameters = new Dictionary<string, string> { { "queue_name", queueName.Value } };
            return GetBaseUriAsync(cancellationToken)
                .Then(PrepareRequestAsyncFunc(HttpMethod.Get, template, parameters, cancellationToken))
                .Select(task => new GetQueueMetadataApiCall<T>(CreateJsonApiCall<T>(task.Result)));
        }

        /// <inheritdoc/>
        public Task<GetQueueStatisticsApiCall> PrepareGetQueueStatisticsAsync(QueueName queueName, CancellationToken cancellationToken)
        {
            if (queueName == null)
                throw new ArgumentNullException("queueName");

            UriTemplate template = new UriTemplate("queues/{queue_name}/stats");
            var parameters = new Dictionary<string, string> { { "queue_name", queueName.Value } };
            return GetBaseUriAsync(cancellationToken)
                .Then(PrepareRequestAsyncFunc(HttpMethod.Get, template, parameters, cancellationToken))
                .Select(task => new GetQueueStatisticsApiCall(CreateJsonApiCall<QueueStatistics>(task.Result)));
        }

        /// <inheritdoc/>
        public Task<ListMessagesApiCall> PrepareListMessagesAsync(QueueName queueName, CancellationToken cancellationToken)
        {
            if (queueName == null)
                throw new ArgumentNullException("queueName");

            UriTemplate template = new UriTemplate("queues/{queue_name}/messages");
            var parameters = new Dictionary<string, string> { { "queue_name", queueName.Value } };

            Func<HttpResponseMessage, CancellationToken, Task<ReadOnlyCollectionPage<QueuedMessage>>> deserializeResult =
                (responseMessage, _) =>
                {
                    if (responseMessage.StatusCode == HttpStatusCode.NoContent)
                        return CompletedTask.FromResult(ReadOnlyCollectionPage<QueuedMessage>.Empty);

                    if (!HttpApiCall.IsAcceptable(responseMessage))
                        throw new HttpWebException(responseMessage);

                    Uri originalUri = responseMessage.RequestMessage.RequestUri;
                    return responseMessage.Content.ReadAsStringAsync()
                        .Select(
                            innerTask =>
                            {
                                if (string.IsNullOrEmpty(innerTask.Result))
                                    return null;

                                JObject responseObject = JsonConvert.DeserializeObject<JObject>(innerTask.Result);
                                JArray queuesArray = responseObject["messages"] as JArray;
                                if (queuesArray == null)
                                    return null;

                                IList<QueuedMessage> list = queuesArray.ToObject<QueuedMessage[]>();
                                Func<CancellationToken, Task<ReadOnlyCollectionPage<QueuedMessage>>> getNextPageAsync =
                                    CreateGetNextPageAsyncDelegate<ListMessagesApiCall, QueuedMessage>(innerCancellationToken => PrepareListMessagesAsync(queueName, innerCancellationToken), responseObject);

                                ReadOnlyCollectionPage<QueuedMessage> results = new BasicReadOnlyCollectionPage<QueuedMessage>(list, getNextPageAsync);
                                return results;
                            });
                };

            return GetBaseUriAsync(cancellationToken)
                .Then(PrepareRequestAsyncFunc(HttpMethod.Get, template, parameters, cancellationToken))
                .Select(task => new ListMessagesApiCall(CreateCustomApiCall(task.Result, HttpCompletionOption.ResponseContentRead, deserializeResult)));
        }

        /// <inheritdoc/>
        public Task<GetMessageApiCall> PrepareGetMessageAsync(QueueName queueName, MessageId messageId, CancellationToken cancellationToken)
        {
            if (queueName == null)
                throw new ArgumentNullException("queueName");
            if (messageId == null)
                throw new ArgumentNullException("messageId");

            UriTemplate template = new UriTemplate("queues/{queue_name}/messages/{message_id}");
            var parameters = new Dictionary<string, string> { { "queue_name", queueName.Value }, { "message_id", messageId.Value } };
            return GetBaseUriAsync(cancellationToken)
                .Then(PrepareRequestAsyncFunc(HttpMethod.Get, template, parameters, cancellationToken))
                .Select(task => new GetMessageApiCall(CreateJsonApiCall<QueuedMessage>(task.Result)));
        }

        /// <inheritdoc/>
        public Task<GetMessagesApiCall> PrepareGetMessagesAsync(QueueName queueName, IEnumerable<MessageId> messageIds, CancellationToken cancellationToken)
        {
            if (queueName == null)
                throw new ArgumentNullException("queueName");
            if (messageIds == null)
                throw new ArgumentNullException("messageIds");
            if (messageIds.Contains(null))
                throw new ArgumentException("messageIds cannot contain any null values");

            UriTemplate template = new UriTemplate("queues/{queue_name}/messages{?ids}");

            var parameters =
                new Dictionary<string, object>()
                {
                    { "queue_name", queueName },
                    { "ids", messageIds.ToArray() },
                };

            Func<HttpResponseMessage, CancellationToken, Task<ReadOnlyCollectionPage<QueuedMessage>>> deserializeResult =
                (responseMessage, _) =>
                {
                    return responseMessage.Content.ReadAsStringAsync()
                        .Select(
                            innerTask =>
                            {
                                QueuedMessage[] messages = JsonConvert.DeserializeObject<QueuedMessage[]>(innerTask.Result);
                                ReadOnlyCollectionPage<QueuedMessage> result = new BasicReadOnlyCollectionPage<QueuedMessage>(messages, null);
                                return result;
                            });
                };

            return GetBaseUriAsync(cancellationToken)
                .Then(PrepareRequestAsyncFunc(HttpMethod.Get, template, parameters, cancellationToken))
                .Select(task => new GetMessagesApiCall(CreateCustomApiCall(task.Result, HttpCompletionOption.ResponseContentRead, deserializeResult)));
        }

        /// <inheritdoc/>
        public Task<PostMessagesApiCall> PreparePostMessagesAsync(QueueName queueName, IEnumerable<Message> messages, CancellationToken cancellationToken)
        {
            if (queueName == null)
                throw new ArgumentNullException("queueName");
            if (messages == null)
                throw new ArgumentNullException("messages");

            return PreparePostMessagesAsync(queueName, messages.Cast<Message<JObject>>(), cancellationToken);
        }

        /// <inheritdoc/>
        public Task<PostMessagesApiCall> PreparePostMessagesAsync<T>(QueueName queueName, IEnumerable<Message<T>> messages, CancellationToken cancellationToken)
        {
            if (queueName == null)
                throw new ArgumentNullException("queueName");
            if (messages == null)
                throw new ArgumentNullException("messages");
            if (messages.Contains(null))
                throw new ArgumentException("messages cannot contain any null values");

            UriTemplate template = new UriTemplate("queues/{queue_name}/messages");
            var parameters = new Dictionary<string, object> { { "queue_name", queueName } };

            return GetBaseUriAsync(cancellationToken)
                .Then(PrepareRequestAsyncFunc(HttpMethod.Post, template, parameters, messages.ToArray(), cancellationToken))
                .Select(task => new PostMessagesApiCall(CreateJsonApiCall<PostMessagesResponse>(task.Result)));
        }

        /// <inheritdoc/>
        public Task<RemoveMessageApiCall> PrepareRemoveMessageAsync(QueueName queueName, MessageId messageId, ClaimId claimId, CancellationToken cancellationToken)
        {
            if (queueName == null)
                throw new ArgumentNullException("queueName");
            if (messageId == null)
                throw new ArgumentNullException("messageId");

            UriTemplate template = new UriTemplate("queues/{queue_name}/messages/{message_id}{?claim_id}");

            var parameters = new Dictionary<string, string> { { "queue_name", queueName.Value }, { "message_id", messageId.Value } };
            if (claimId != null)
                parameters["claim_id"] = claimId.Value;

            return GetBaseUriAsync(cancellationToken)
                .Then(PrepareRequestAsyncFunc(HttpMethod.Delete, template, parameters, cancellationToken))
                .Select(task => new RemoveMessageApiCall(CreateBasicApiCall(task.Result)));
        }

        /// <inheritdoc/>
        public Task<RemoveMessagesApiCall> PrepareRemoveMessagesAsync(QueueName queueName, IEnumerable<MessageId> messageIds, CancellationToken cancellationToken)
        {
            if (queueName == null)
                throw new ArgumentNullException("queueName");
            if (messageIds == null)
                throw new ArgumentNullException("messageIds");
            if (messageIds.Contains(null))
                throw new ArgumentException("messageIds cannot contain any null values");

            UriTemplate template = new UriTemplate("queues/{queue_name}/messages{?ids}");

            var parameters =
                new Dictionary<string, object>()
                {
                    { "queue_name", queueName },
                    { "ids", messageIds.ToArray() },
                };

            return GetBaseUriAsync(cancellationToken)
                .Then(PrepareRequestAsyncFunc(HttpMethod.Delete, template, parameters, cancellationToken))
                .Select(task => new RemoveMessagesApiCall(CreateBasicApiCall(task.Result)));
        }

        /// <inheritdoc/>
        public Task<ClaimMessagesApiCall> PrepareClaimMessagesAsync(QueueName queueName, ClaimData claimData, CancellationToken cancellationToken)
        {
            UriTemplate template = new UriTemplate("queues/{queue_name}/claims");
            var parameters = new Dictionary<string, string> { { "queue_name", queueName.Value } };

            Func<HttpResponseMessage, CancellationToken, Task<Tuple<Uri, Claim>>> deserializeResult =
                (responseMessage, _) =>
                {
                    Uri location = responseMessage.Headers.Location;
                    if (responseMessage.StatusCode == HttpStatusCode.NoContent)
                    {
                        Claim claim = new Claim(claimData.TimeToLive, null, TimeSpan.Zero, new QueuedMessage[0]);
                        Tuple<Uri, Claim> result = Tuple.Create(location, claim);
                        return CompletedTask.FromResult(result);
                    }

                    return responseMessage.Content.ReadAsStringAsync()
                        .Select(
                            innerTask =>
                            {
                                QueuedMessage[] messages = JsonConvert.DeserializeObject<QueuedMessage[]>(innerTask.Result);
                                Claim claim = new Claim(claimData.TimeToLive, null, TimeSpan.Zero, messages);
                                return Tuple.Create(location, claim);
                            });
                };

            return GetBaseUriAsync(cancellationToken)
                .Then(PrepareRequestAsyncFunc(HttpMethod.Post, template, parameters, claimData, cancellationToken))
                .Select(task => new ClaimMessagesApiCall(CreateCustomApiCall(task.Result, HttpCompletionOption.ResponseContentRead, deserializeResult)));
        }

        /// <inheritdoc/>
        public Task<QueryClaimApiCall> PrepareQueryClaimAsync(QueueName queueName, ClaimId claimId, CancellationToken cancellationToken)
        {
            UriTemplate template = new UriTemplate("queues/{queue_name}/claims/{claim_id}");
            var parameters = new Dictionary<string, string> { { "queue_name", queueName.Value }, { "claim_id", claimId.Value } };

            Func<HttpResponseMessage, CancellationToken, Task<Tuple<Uri, Claim>>> deserializeResult =
                (responseMessage, _) =>
                {
                    return responseMessage.Content.ReadAsStringAsync()
                        .Select(
                            innerTask =>
                            {
                                Uri location = responseMessage.Content.Headers.ContentLocation;
                                Claim claim = JsonConvert.DeserializeObject<Claim>(innerTask.Result);
                                return Tuple.Create(location, claim);
                            });
                };

            return GetBaseUriAsync(cancellationToken)
                .Then(PrepareRequestAsyncFunc(HttpMethod.Get, template, parameters, cancellationToken))
                .Select(task => new QueryClaimApiCall(CreateCustomApiCall(task.Result, HttpCompletionOption.ResponseContentRead, deserializeResult)));
        }

        /// <inheritdoc/>
        public Task<UpdateClaimApiCall> PrepareUpdateClaimAsync(QueueName queueName, ClaimId claimId, ClaimData claimData, CancellationToken cancellationToken)
        {
            if (queueName == null)
                throw new ArgumentNullException("queueName");
            if (claimId == null)
                throw new ArgumentNullException("claimId");
            if (claimData == null)
                throw new ArgumentNullException("claimData");

            UriTemplate template = new UriTemplate("queues/{queue_name}/claims/{claim_id}");
            var parameters = new Dictionary<string, string> { { "queue_name", queueName.Value }, { "claim_id", claimId.Value } };
            return GetBaseUriAsync(cancellationToken)
                .Then(PrepareRequestAsyncFunc(new HttpMethod("PATCH"), template, parameters, claimData, cancellationToken))
                .Select(task => new UpdateClaimApiCall(CreateBasicApiCall(task.Result)));
        }

        /// <inheritdoc/>
        public Task<ReleaseClaimApiCall> PrepareReleaseClaimAsync(QueueName queueName, ClaimId claimId, CancellationToken cancellationToken)
        {
            if (queueName == null)
                throw new ArgumentNullException("queueName");
            if (claimId == null)
                throw new ArgumentNullException("claimId");

            UriTemplate template = new UriTemplate("queues/{queue_name}/claims/{claim_id}");
            var parameters = new Dictionary<string, string> { { "queue_name", queueName.Value }, { "claim_id", claimId.Value } };
            return GetBaseUriAsync(cancellationToken)
                .Then(PrepareRequestAsyncFunc(HttpMethod.Delete, template, parameters, cancellationToken))
                .Select(task => new ReleaseClaimApiCall(CreateBasicApiCall(task.Result)));
        }

        #endregion

        /// <inheritdoc/>
        /// <remarks>
        /// This method extends the base implementation by providing special support for the
        /// result of calls to <see cref="PrepareQueueExistsAsync"/> and <see cref="PrepareGetNodeHealthAsync"/>.
        /// </remarks>
        /// <seealso cref="IsQueueExistsMessage"/>
        /// <seealso cref="IsNodeHealthMessage"/>
        protected override Task<HttpResponseMessage> ValidateResultImplAsync(Task<HttpResponseMessage> task, CancellationToken cancellationToken)
        {
            // special handling for QueueExistsAsync
            if (task.Result.StatusCode == HttpStatusCode.NotFound && IsQueueExistsMessage(task.Result.RequestMessage))
                return task;

            // special handling for checking the node health
            if (task.Result.StatusCode == HttpStatusCode.ServiceUnavailable && IsNodeHealthMessage(task.Result.RequestMessage))
                return task;

            // all other cases defer to the base implementation
            return base.ValidateResultImplAsync(task, cancellationToken);
        }

        /// <inheritdoc/>
        /// <remarks>
        /// This method calls <see cref="IAuthenticationService.GetBaseAddressAsync"/> to obtain a URI
        /// for the type <c>queues</c>. The preferred name is not specified.
        /// </remarks>
        protected override Task<Uri> GetBaseUriAsyncImpl(CancellationToken cancellationToken)
        {
            const string serviceType = "queues";
            const string serviceName = "";
            return AuthenticationService.GetBaseAddressAsync(serviceType, serviceName, DefaultRegion, _internalUrl, cancellationToken);
        }

        /// <inheritdoc/>
        /// <remarks>
        /// This method calls <see cref="ServiceClient.PrepareRequestImpl"/> to create the
        /// initial <see cref="HttpRequestMessage"/>, and then sets the <c>Client-Id</c> header according
        /// to the Marconi (OpenStack Queues Service) specification before returning.
        /// </remarks>
        protected override HttpRequestMessage PrepareRequestImpl<T>(HttpMethod method, UriTemplate template, Uri baseUri, IDictionary<string, T> parameters)
        {
            HttpRequestMessage request = base.PrepareRequestImpl(method, template, baseUri, parameters);
            request.Headers.Add("Client-Id", _clientId.ToString("D"));
            return request;
        }

        private static Func<CancellationToken, Task<ReadOnlyCollectionPage<TElement>>> CreateGetNextPageAsyncDelegate<TCall, TElement>(Func<CancellationToken, Task<TCall>> prepareApiCall, JObject responseObject)
            where TCall : IHttpApiCall<ReadOnlyCollectionPage<TElement>>
        {
            if (responseObject == null)
                return null;

            JArray linksArray = responseObject["links"] as JArray;
            if (linksArray == null)
                return null;

            Link[] links = linksArray.ToObject<Link[]>();
            Link nextLink = links.FirstOrDefault(i => string.Equals("next", i.Relation, StringComparison.OrdinalIgnoreCase));
            if (nextLink == null)
                return null;

            return
                cancellationToken =>
                {
                    return
                        CoreTaskExtensions.Using(
                            () => prepareApiCall(cancellationToken)
                                .Select(
                                    _ =>
                                    {
                                        _.Result.RequestMessage.RequestUri = new Uri(_.Result.RequestMessage.RequestUri, nextLink.Target);
                                        return _.Result;
                                    }),
                            _ => _.Result.SendAsync(cancellationToken))
                        .Select(_ => _.Result.Item2);
                };
        }
    }
}
