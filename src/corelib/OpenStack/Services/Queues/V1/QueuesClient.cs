﻿namespace OpenStack.Services.Queues.V1
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
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
    using OpenStack.ObjectModel.JsonHome;
    using OpenStack.Security.Authentication;
    using Rackspace.Net;
    using Rackspace.Threading;
    using Link = OpenStack.Services.Compute.V2.Link;

    /// <summary>
    /// Provides an implementation of <see cref="IQueuesService"/> for operating
    /// with Rackspace's Cloud Queues product.
    /// </summary>
    /// <seealso href="https://wiki.openstack.org/w/index.php?title=Marconi/specs/api/v1">OpenStack Marconi API v1 Blueprint</seealso>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    public class QueuesClient : ServiceClient, IQueuesService
    {
        /// <summary>
        /// Specifies whether the <see cref="Endpoint.PublicURL"/> or <see cref="Endpoint.InternalURL"/>
        /// should be used for accessing the Cloud Queues API.
        /// </summary>
        private readonly bool _internalUrl;

        /// <summary>
        /// The value of the Client-Id header to send with message requests from this service.
        /// </summary>
        private readonly Guid _clientId;

        /// <summary>
        /// Initializes a new instance of the <see cref="QueuesClient"/> class with
        /// the specified values.
        /// </summary>
        /// <remarks>
        /// <note type="inherit">
        /// The default implementation does not rely on <paramref name="restService"/> or
        /// <paramref name="httpStatusCodeValidator"/> for any services. If a derived class
        /// uses synchronous methods for any web API calls, the constructor for the derived
        /// class can either specify or expose these parameters.
        /// </note>
        /// </remarks>
        /// <param name="defaultIdentity">The default identity to use for calls that do not explicitly specify an identity. If this value is <see langword="null"/>, the <paramref name="identityProvider"/> provides the default identity.</param>
        /// <param name="defaultRegion">The default region to use for calls that do not explicitly specify a region. If this value is <see langword="null"/>, the default region for the user will be used; otherwise if the service uses region-specific endpoints all calls must specify an explicit region.</param>
        /// <param name="clientId">The value of the <strong>Client-Id</strong> header to send with message requests from this service.</param>
        /// <param name="internalUrl"><see langword="true"/> to use the endpoint's <see cref="Endpoint.InternalURL"/>; otherwise <see langword="false"/> to use the endpoint's <see cref="Endpoint.PublicURL"/>.</param>
        /// <param name="identityProvider">The identity provider to use for authenticating requests to this provider. If this value is <see langword="null"/>, a new instance of <see cref="CloudIdentityProvider"/> is created using <paramref name="defaultIdentity"/> as the default identity.</param>
        /// <param name="restService">The implementation of <see cref="IRestService"/> to use for executing synchronous REST requests. If this value is <see langword="null"/>, the provider will use a new instance of <see cref="JsonRestServices"/>.</param>
        /// <param name="httpStatusCodeValidator">The HTTP status code validator to use for synchronous REST requests. If this value is <see langword="null"/>, the provider will use <see cref="HttpResponseCodeValidator.Default"/>.</param>
        /// <exception cref="ArgumentException">If both <paramref name="defaultIdentity"/> and <paramref name="identityProvider"/> are <see langword="null"/>.</exception>
        public QueuesClient(IAuthenticationService authenticationService, string defaultRegion, Guid clientId, bool internalUrl)
            : base(authenticationService, defaultRegion)
        {
            _clientId = clientId;
            _internalUrl = internalUrl;
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
        public Task<ReadOnlyCollectionPage<Queue>> ListQueuesAsync(QueueName marker, int? limit, bool detailed, CancellationToken cancellationToken)
        {
            if (limit <= 0)
                throw new ArgumentOutOfRangeException("limit");

            UriTemplate template = new UriTemplate("queues{?marker,limit,detailed}");
            var parameters = new Dictionary<string, string>
                {
                    { "detailed", detailed.ToString().ToLowerInvariant() },
                };
            if (marker != null)
                parameters.Add("marker", marker.Value);
            if (limit.HasValue)
                parameters.Add("limit", limit.ToString());

            Func<Task<Uri>, Task<HttpRequestMessage>> prepareRequest =
                PrepareRequestAsyncFunc(HttpMethod.Get, template, parameters, cancellationToken);

            Func<Task<HttpRequestMessage>, Task<ListCloudQueuesResponse>> requestResource =
                GetResponseAsyncFunc<ListCloudQueuesResponse>(cancellationToken);

            Func<Task<ListCloudQueuesResponse>, ReadOnlyCollectionPage<Queue>> resultSelector =
                task =>
                {
                    ReadOnlyCollectionPage<Queue> page = null;
                    if (task.Result != null && task.Result.Queues != null)
                    {
                        Queue lastQueue = task.Result.Queues.LastOrDefault();
                        QueueName nextMarker = lastQueue != null ? lastQueue.Name : marker;
                        Func<CancellationToken, Task<ReadOnlyCollectionPage<Queue>>> getNextPageAsync =
                            nextCancellationToken => ListQueuesAsync(nextMarker, limit, detailed, nextCancellationToken);
                        page = new BasicReadOnlyCollectionPage<Queue>(task.Result.Queues, getNextPageAsync);
                    }

                    return page ?? ReadOnlyCollectionPage<Queue>.Empty;
                };

            return GetBaseUriAsync(cancellationToken)
                .Then(prepareRequest)
                .Then(requestResource)
                .Select(resultSelector);
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
        /// request from the <see cref="QueueExistsAsync"/> method.
        /// </summary>
        /// <remarks>
        /// This method is used by <see cref="ValidateResultImplAsync"/> to support specialized handling
        /// for the HTTP response to the <see cref="QueueExistsAsync"/> call. Unlike most calls, the
        /// status code <see cref="HttpStatusCode.NotFound"/> indicates a successful response from this
        /// call.
        /// </remarks>
        /// <param name="message">The request message, which may be <see langword="null"/>.</param>
        /// <returns><see langword="true"/> if <paramref name="message"/> has the form of a message created by <see cref="QueueExistsAsync"/>; otherwise, <see langword="false"/>.</returns>
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
        public Task<QueuedMessageList> ListMessagesAsync(QueueName queueName, QueuedMessageListId marker, int? limit, bool echo, bool includeClaimed, CancellationToken cancellationToken)
        {
            if (queueName == null)
                throw new ArgumentNullException("queueName");
            if (limit <= 0)
                throw new ArgumentOutOfRangeException("limit");

            UriTemplate template = new UriTemplate("queues/{queue_name}/messages{?marker,limit,echo,include_claimed}");

            var parameters =
                new Dictionary<string, string>()
                {
                    { "queue_name", queueName.Value },
                    { "echo", echo.ToString() },
                    { "include_claimed", includeClaimed.ToString() }
                };
            if (marker != null)
                parameters["marker"] = marker.Value;
            if (limit != null)
                parameters["limit"] = limit.ToString();

            Func<Task<Uri>, Task<HttpRequestMessage>> prepareRequest =
                PrepareRequestAsyncFunc(HttpMethod.Get, template, parameters, cancellationToken);

            Func<Task<HttpRequestMessage>, Task<ListCloudQueueMessagesResponse>> requestResource =
                GetResponseAsyncFunc<ListCloudQueueMessagesResponse>(cancellationToken);

            Func<Task<ListCloudQueueMessagesResponse>, QueuedMessageList> resultSelector =
                task =>
                {
                    ReadOnlyCollection<QueuedMessage> messages = null;
                    if (task.Result != null)
                        messages = task.Result.Messages;

                    QueuedMessageListId nextMarker = null;
                    if (task.Result != null && task.Result.Links != null)
                    {
                        Link nextLink = task.Result.Links.FirstOrDefault(i => string.Equals(i.Relation, "next", StringComparison.OrdinalIgnoreCase));
                        if (nextLink != null)
                        {
                            Uri baseUri = new Uri("https://example.com");
                            Uri absoluteUri;
                            if (nextLink.Target.OriginalString.StartsWith("/v1"))
                                absoluteUri = new Uri(baseUri, nextLink.Target.OriginalString.Substring("/v1".Length));
                            else
                                absoluteUri = new Uri(baseUri, nextLink.Target);

                            UriTemplateMatch match = template.Match(baseUri, absoluteUri);
                            if (match != null && !string.IsNullOrEmpty((string)match.Bindings["marker"].Value))
                                nextMarker = new QueuedMessageListId((string)match.Bindings["marker"].Value);
                        }
                    }

                    if (messages == null || messages.Count == 0)
                    {
                        // use the same marker again
                        messages = messages ?? new ReadOnlyCollection<QueuedMessage>(new QueuedMessage[0]);
                        nextMarker = marker;
                    }

                    Func<CancellationToken, Task<ReadOnlyCollectionPage<QueuedMessage>>> getNextPageAsync = null;
                    if (nextMarker != null || messages.Count == 0)
                    {
                        getNextPageAsync =
                            nextCancellationToken => ListMessagesAsync(queueName, nextMarker, limit, echo, includeClaimed, nextCancellationToken)
                                .Select(t => (ReadOnlyCollectionPage<QueuedMessage>)t.Result);
                    }

                    return new QueuedMessageList(messages, getNextPageAsync, nextMarker);
                };

            return GetBaseUriAsync(cancellationToken)
                .Then(prepareRequest)
                .Then(requestResource)
                .Select(resultSelector);
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
        public Task<ReadOnlyCollection<QueuedMessage>> GetMessagesAsync(QueueName queueName, IEnumerable<MessageId> messageIds, CancellationToken cancellationToken)
        {
            if (queueName == null)
                throw new ArgumentNullException("queueName");
            if (messageIds == null)
                throw new ArgumentNullException("messageIds");
            if (messageIds.Contains(null))
                throw new ArgumentException("messageIds cannot contain any null values");

            UriTemplate template = new UriTemplate("queues/{queue_name}/messages{?ids}");

            var parameters =
                new Dictionary<string, string>()
                {
                    { "queue_name", queueName.Value },
                    { "ids", string.Join(",", messageIds.Select(i => i.Value).ToArray()) },
                };

            Func<Uri, Uri> uriTransform =
                uri =>
                {
                    UriBuilder uriBuilder = new UriBuilder(uri);
                    uriBuilder.Query = null;
                    uriBuilder.Fragment = null;
                    return new Uri(uriBuilder.Uri.AbsoluteUri + uri.Query.Replace("%2c", ",").Replace("%2C", ","));
                };

            Func<Task<Uri>, Task<HttpRequestMessage>> prepareRequest =
                PrepareRequestAsyncFunc(HttpMethod.Get, template, parameters, cancellationToken, uriTransform);

            Func<Task<HttpRequestMessage>, Task<ReadOnlyCollection<QueuedMessage>>> requestResource =
                GetResponseAsyncFunc<ReadOnlyCollection<QueuedMessage>>(cancellationToken);

            return GetBaseUriAsync(cancellationToken)
                .Then(prepareRequest)
                .Then(requestResource);
        }

        /// <inheritdoc/>
        public Task PostMessagesAsync(QueueName queueName, IEnumerable<Message> messages, CancellationToken cancellationToken)
        {
            if (queueName == null)
                throw new ArgumentNullException("queueName");
            if (messages == null)
                throw new ArgumentNullException("messages");

            return PostMessagesAsync(queueName, cancellationToken, messages.ToArray());
        }

        /// <inheritdoc/>
        public Task PostMessagesAsync(QueueName queueName, CancellationToken cancellationToken, params Message[] messages)
        {
            if (queueName == null)
                throw new ArgumentNullException("queueName");
            if (messages == null)
                throw new ArgumentNullException("messages");
            if (messages.Contains(null))
                throw new ArgumentException("messages cannot contain any null values");

            if (messages.Length == 0)
                return this.QueueExistsAsync(queueName, cancellationToken);

            UriTemplate template = new UriTemplate("queues/{queue_name}/messages");

            var parameters =
                new Dictionary<string, string>()
                {
                    { "queue_name", queueName.Value },
                };

            Func<Task<Uri>, Task<HttpRequestMessage>> prepareRequest =
                PrepareRequestAsyncFunc(HttpMethod.Post, template, parameters, messages, cancellationToken);

            Func<Task<HttpRequestMessage>, Task<string>> requestResource =
                GetResponseAsyncFunc(cancellationToken);

            return GetBaseUriAsync(cancellationToken)
                .Then(prepareRequest)
                .Then(requestResource);
        }

        /// <inheritdoc/>
        public Task PostMessagesAsync<T>(QueueName queueName, IEnumerable<Message<T>> messages, CancellationToken cancellationToken)
        {
            if (queueName == null)
                throw new ArgumentNullException("queueName");
            if (messages == null)
                throw new ArgumentNullException("messages");

            return PostMessagesAsync(queueName, cancellationToken, messages.ToArray());
        }

        /// <inheritdoc/>
        public Task PostMessagesAsync<T>(QueueName queueName, CancellationToken cancellationToken, params Message<T>[] messages)
        {
            if (queueName == null)
                throw new ArgumentNullException("queueName");
            if (messages == null)
                throw new ArgumentNullException("messages");
            if (messages.Contains(null))
                throw new ArgumentException("messages cannot contain any null values");

            if (messages.Length == 0)
                return this.QueueExistsAsync(queueName, cancellationToken);

            UriTemplate template = new UriTemplate("queues/{queue_name}/messages");

            var parameters =
                new Dictionary<string, string>()
                {
                    { "queue_name", queueName.Value },
                };

            Func<Task<Uri>, Task<HttpRequestMessage>> prepareRequest =
                PrepareRequestAsyncFunc(HttpMethod.Post, template, parameters, messages, cancellationToken);

            Func<Task<HttpRequestMessage>, Task<string>> requestResource =
                GetResponseAsyncFunc(cancellationToken);

            return GetBaseUriAsync(cancellationToken)
                .Then(prepareRequest)
                .Then(requestResource);
        }

        /// <inheritdoc/>
        public Task DeleteMessageAsync(QueueName queueName, MessageId messageId, Claim claim, CancellationToken cancellationToken)
        {
            if (queueName == null)
                throw new ArgumentNullException("queueName");
            if (messageId == null)
                throw new ArgumentNullException("messageId");

            UriTemplate template = new UriTemplate("queues/{queue_name}/messages/{message_id}{?claim_id}");

            var parameters =
                new Dictionary<string, string>()
                {
                    { "queue_name", queueName.Value },
                    { "message_id", messageId.Value },
                };
            if (claim != null)
                parameters["claim_id"] = claim.Id.Value;

            Func<Task<Uri>, Task<HttpRequestMessage>> prepareRequest =
                PrepareRequestAsyncFunc(HttpMethod.Delete, template, parameters, cancellationToken);

            Func<Task<HttpRequestMessage>, Task<string>> requestResource =
                GetResponseAsyncFunc(cancellationToken);

            return GetBaseUriAsync(cancellationToken)
                .Then(prepareRequest)
                .Then(requestResource);
        }

        /// <inheritdoc/>
        public Task DeleteMessagesAsync(QueueName queueName, IEnumerable<MessageId> messageIds, CancellationToken cancellationToken)
        {
            if (queueName == null)
                throw new ArgumentNullException("queueName");
            if (messageIds == null)
                throw new ArgumentNullException("messageIds");
            if (messageIds.Contains(null))
                throw new ArgumentException("messageIds cannot contain any null values");

            UriTemplate template = new UriTemplate("queues/{queue_name}/messages{?ids}");

            var parameters =
                new Dictionary<string, string>()
                {
                    { "queue_name", queueName.Value },
                    { "ids", string.Join(",", messageIds.Select(i => i.Value).ToArray()) },
                };

            Func<Uri, Uri> uriTransform =
                uri =>
                {
                    UriBuilder uriBuilder = new UriBuilder(uri);
                    uriBuilder.Query = null;
                    uriBuilder.Fragment = null;
                    return new Uri(uriBuilder.Uri.AbsoluteUri + uri.Query.Replace("%2c", ",").Replace("%2C", ","));
                };

            Func<Task<Uri>, Task<HttpRequestMessage>> prepareRequest =
                PrepareRequestAsyncFunc(HttpMethod.Delete, template, parameters, cancellationToken, uriTransform);

            Func<Task<HttpRequestMessage>, Task<string>> requestResource =
                GetResponseAsyncFunc(cancellationToken);

            return GetBaseUriAsync(cancellationToken)
                .Then(prepareRequest)
                .Then(requestResource);
        }

        /// <inheritdoc/>
        public Task<Claim> ClaimMessageAsync(QueueName queueName, int? limit, TimeSpan timeToLive, TimeSpan gracePeriod, CancellationToken cancellationToken)
        {
            if (queueName == null)
                throw new ArgumentNullException("queueName");
            if (limit < 0)
                throw new ArgumentOutOfRangeException("limit");
            if (timeToLive <= TimeSpan.Zero)
                throw new ArgumentOutOfRangeException("timeToLive");
            if (gracePeriod < TimeSpan.Zero)
                throw new ArgumentOutOfRangeException("gracePeriod");

            UriTemplate template = new UriTemplate("queues/{queue_name}/claims{?limit}");

            var parameters =
                new Dictionary<string, string>()
                {
                    { "queue_name", queueName.Value },
                };
            if (limit != null)
                parameters["limit"] = limit.ToString();

            var request =
                new JObject(
                    new JProperty("ttl", (long)timeToLive.TotalSeconds),
                    new JProperty("grace", (long)gracePeriod.TotalSeconds));

            Func<Task<Uri>, Task<HttpRequestMessage>> prepareRequest =
                PrepareRequestAsyncFunc(HttpMethod.Post, template, parameters, request, cancellationToken);

            Func<Task<Tuple<HttpResponseMessage, string>>, Task<Tuple<Uri, IEnumerable<QueuedMessage>>>> parseResult =
                task => GetBaseUriAsync(cancellationToken).Then(
                    baseUriTask =>
                    {
                        Uri relativeLocation = task.Result.Item1.Headers.Location;
                        Uri location = relativeLocation != null ? new Uri(baseUriTask.Result, relativeLocation) : null;
                        if (task.Result.Item1.StatusCode == HttpStatusCode.NoContent)
                        {
                            // the queue did not contain any messages to claim
                            Tuple<Uri, IEnumerable<QueuedMessage>> result = Tuple.Create(location, Enumerable.Empty<QueuedMessage>());
                            return CompletedTask.FromResult(result);
                        }

                        IEnumerable<QueuedMessage> messages = JsonConvert.DeserializeObject<IEnumerable<QueuedMessage>>(task.Result.Item2);

                        return CompletedTask.FromResult(Tuple.Create(location, messages));
                    });
            Func<Task<HttpRequestMessage>, Task<Tuple<Uri, IEnumerable<QueuedMessage>>>> requestResource =
                GetResponseAsyncFunc<Tuple<Uri, IEnumerable<QueuedMessage>>>(cancellationToken, parseResult);

            Func<Task<Tuple<Uri, IEnumerable<QueuedMessage>>>, Claim> resultSelector =
                task => new Claim(this, queueName, task.Result.Item1, timeToLive, TimeSpan.Zero, true, task.Result.Item2);

            return GetBaseUriAsync(cancellationToken)
                .Then(prepareRequest)
                .Then(requestResource)
                .Select(resultSelector);
        }

        /// <inheritdoc/>
        public Task<Claim> QueryClaimAsync(QueueName queueName, Claim claim, CancellationToken cancellationToken)
        {
            if (queueName == null)
                throw new ArgumentNullException("queueName");
            if (claim == null)
                throw new ArgumentNullException("claim");

            UriTemplate template = new UriTemplate("queues/{queue_name}/claims/{claim_id}");

            var parameters =
                new Dictionary<string, string>()
                {
                    { "queue_name", queueName.Value },
                    { "claim_id", claim.Id.Value }
                };

            Func<Task<Uri>, Task<HttpRequestMessage>> prepareRequest =
                PrepareRequestAsyncFunc(HttpMethod.Get, template, parameters, cancellationToken);

            Func<Task<Tuple<HttpResponseMessage, string>>, Task<Tuple<Uri, TimeSpan, TimeSpan, IEnumerable<QueuedMessage>>>> parseResult =
                task => GetBaseUriAsync(cancellationToken).Then(
                    baseUriTask =>
                    {
                        // this response uses ContentLocation instead of Location
                        Uri relativeLocation = task.Result.Item1.Content.Headers.ContentLocation;
                        Uri location = relativeLocation != null ? new Uri(baseUriTask.Result, relativeLocation) : null;

                        JObject result = JsonConvert.DeserializeObject<JObject>(task.Result.Item2);
                        TimeSpan age = TimeSpan.FromSeconds((int)result["age"]);
                        TimeSpan ttl = TimeSpan.FromSeconds((int)result["ttl"]);
                        IEnumerable<QueuedMessage> messages = result["messages"].ToObject<IEnumerable<QueuedMessage>>();
                        return CompletedTask.FromResult(Tuple.Create(location, ttl, age, messages));
                    });
            Func<Task<HttpRequestMessage>, Task<Tuple<Uri, TimeSpan, TimeSpan, IEnumerable<QueuedMessage>>>> requestResource =
                GetResponseAsyncFunc(cancellationToken, parseResult);

            Func<Task<Tuple<Uri, TimeSpan, TimeSpan, IEnumerable<QueuedMessage>>>, Claim> resultSelector =
                task => new Claim(this, queueName, task.Result.Item1, task.Result.Item2, task.Result.Item3, false, task.Result.Item4);

            return GetBaseUriAsync(cancellationToken)
                .Then(prepareRequest)
                .Then(requestResource)
                .Select(resultSelector);
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
        /// This method calls <see cref="ProviderBase{TProvider}.PrepareRequestImpl"/> to create the
        /// initial <see cref="HttpRequestMessage"/>, and then sets the <c>Client-Id</c> header according
        /// to the Marconi (Cloud Queues) specification before returning.
        /// </remarks>
        protected override HttpRequestMessage PrepareRequestImpl<T>(HttpMethod method, UriTemplate template, Uri baseUri, IDictionary<string, T> parameters)
        {
            HttpRequestMessage request = base.PrepareRequestImpl(method, template, baseUri, parameters);
            request.Headers.Add("Client-Id", _clientId.ToString("D"));
            return request;
        }

        /// <threadsafety static="true" instance="false"/>
        /// <preliminary/>
        [JsonObject(MemberSerialization.OptIn)]
        protected class ListCloudQueuesResponse
        {
#pragma warning disable 649 // Field 'fieldName' is never assigned to, and will always have its default value {value}
            [JsonProperty("links")]
            private Link[] _links;

            [JsonProperty("queues")]
            private Queue[] _queues;
#pragma warning restore 649

            /// <summary>
            /// Initializes a new instance of the <see cref="ListCloudQueuesResponse"/> class
            /// during JSON deserialization.
            /// </summary>
            [JsonConstructor]
            protected ListCloudQueuesResponse()
            {
            }

            public Link[] Links
            {
                get
                {
                    return _links;
                }
            }

            public Queue[] Queues
            {
                get
                {
                    return _queues;
                }
            }
        }

        /// <threadsafety static="true" instance="false"/>
        /// <preliminary/>
        [JsonObject(MemberSerialization.OptIn)]
        protected class ListCloudQueueMessagesResponse
        {
#pragma warning disable 649 // Field 'fieldName' is never assigned to, and will always have its default value {value}
            [JsonProperty("links")]
            private Link[] _links;

            [JsonProperty("messages")]
            private QueuedMessage[] _messages;
#pragma warning restore 649

            /// <summary>
            /// Initializes a new instance of the <see cref="ListCloudQueueMessagesResponse"/> class
            /// during JSON deserialization.
            /// </summary>
            [JsonConstructor]
            protected ListCloudQueueMessagesResponse()
            {
            }

            public ReadOnlyCollection<Link> Links
            {
                get
                {
                    if (_links == null)
                        return null;

                    return new ReadOnlyCollection<Link>(_links);
                }
            }

            public ReadOnlyCollection<QueuedMessage> Messages
            {
                get
                {
                    if (_messages == null)
                        return null;

                    return new ReadOnlyCollection<QueuedMessage>(_messages);
                }
            }
        }
    }
}
