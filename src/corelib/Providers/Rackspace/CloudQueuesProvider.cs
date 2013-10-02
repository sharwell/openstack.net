namespace net.openstack.Providers.Rackspace
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Threading.Tasks;
    using net.openstack.Core;
    using net.openstack.Core.Domain;
    using net.openstack.Core.Exceptions;
    using net.openstack.Core.Providers;
    using net.openstack.Providers.Rackspace.Objects.Request;
    using net.openstack.Providers.Rackspace.Objects.Response;
    using net.openstack.Providers.Rackspace.Validators;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using CancellationToken = System.Threading.CancellationToken;
    using Encoding = System.Text.Encoding;
    using HttpStatusCode = System.Net.HttpStatusCode;
    using IRestService = JSIStudios.SimpleRESTServices.Client.IRestService;
    using JsonRequestSettings = JSIStudios.SimpleRESTServices.Client.Json.JsonRequestSettings;

    /// <summary>
    /// Provides an implementation of <see cref="IQueueingService"/> for operating
    /// with Rackspace's Cloud Queues product.
    /// </summary>
    /// <seealso href="https://wiki.openstack.org/w/index.php?title=Marconi/specs/api/v1">OpenStack Marconi API v1 Blueprint</seealso>
    /// <threadsafety static="true" instance="false"/>
    public class CloudQueuesProvider : ProviderBase<IQueueingService>, IQueueingService
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
        /// This field caches the base URI used for accessing the Cloud Queues service.
        /// </summary>
        /// <seealso cref="GetBaseUriAsync"/>
        private Uri _baseUri;

        /// <summary>
        /// This field caches the home document returned by <see cref="GetHomeAsync"/>.
        /// </summary>
        private HomeDocument _homeDocument;

        private HttpClient _httpClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="CloudQueuesProvider"/> class with
        /// the specified values.
        /// </summary>
        /// <param name="defaultIdentity">The default identity to use for calls that do not explicitly specify an identity. If this value is <c>null</c>, no default identity is available so all calls must specify an explicit identity.</param>
        /// <param name="defaultRegion">The default region to use for calls that do not explicitly specify a region. If this value is <c>null</c>, the default region for the user will be used; otherwise if the service uses region-specific endpoints all calls must specify an explicit region.</param>
        /// <param name="clientId">The value of the <strong>Client-Id</strong> header to send with message requests from this service.</param>
        /// <param name="internalUrl"><c>true</c> to use the endpoint's <see cref="Endpoint.InternalURL"/>; otherwise <c>false</c> to use the endpoint's <see cref="Endpoint.PublicURL"/>.</param>
        /// <param name="identityProvider">The identity provider to use for authenticating requests to this provider. If this value is <c>null</c>, a new instance of <see cref="CloudIdentityProvider"/> is created using <paramref name="defaultIdentity"/> as the default identity.</param>
        /// <param name="restService">The implementation of <see cref="IRestService"/> to use for executing REST requests. If this value is <c>null</c>, the provider will use a new instance of <see cref="JsonRestServices"/>.</param>
        public CloudQueuesProvider(CloudIdentity defaultIdentity, string defaultRegion, Guid clientId, bool internalUrl, IIdentityProvider identityProvider, IRestService restService)
            : base(defaultIdentity, defaultRegion, identityProvider, restService, HttpResponseCodeValidator.Default)
        {
            _clientId = clientId;
            _internalUrl = internalUrl;

            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Add("Accept", JsonRequestSettings.JsonContentType);
            _httpClient.DefaultRequestHeaders.Add("Client-Id", _clientId.ToString("B"));
            _httpClient.DefaultRequestHeaders.Add("User-Agent", UserAgentGenerator.UserAgent);
        }

        #region IQueueingService Members

        /// <inheritdoc/>
        public async Task<HomeDocument> GetHomeAsync(CancellationToken cancellationToken)
        {
            if (_homeDocument != null)
            {
                return _homeDocument;
            }

            Tuple<IdentityToken, Uri> authentication = await AuthenticateServiceAsync(cancellationToken);

            UriTemplate template = new UriTemplate("/v1/");
            Uri uri = template.BindByName(authentication.Item2, new Dictionary<string, string>());

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, uri);
            request.Headers.Add("X-Auth-Token", authentication.Item1.Id);

            HttpResponseMessage response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, cancellationToken);
            response.EnsureSuccessStatusCode();

            string body = await response.Content.ReadAsStringAsync();
            HomeDocument document = await JsonConvert.DeserializeObjectAsync<HomeDocument>(body);
            _homeDocument = document;
            return document;
        }

        /// <inheritdoc/>
        public async Task GetNodeHealthAsync(CancellationToken cancellationToken)
        {
            Tuple<IdentityToken, Uri> authentication = await AuthenticateServiceAsync(cancellationToken);

            UriTemplate template = new UriTemplate("/v1/health");
            Uri uri = template.BindByName(authentication.Item2, new Dictionary<string, string>());

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Head, uri);
            request.Headers.Add("X-Auth-Token", authentication.Item1.Id);

            HttpResponseMessage response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken);
            response.EnsureSuccessStatusCode();
        }

        /// <inheritdoc/>
        public async Task CreateQueueAsync(string queueName, CancellationToken cancellationToken)
        {
            if (queueName == null)
                throw new ArgumentNullException("queueName");
            if (string.IsNullOrEmpty(queueName))
                throw new ArgumentException("queueName cannot be empty");

            Tuple<IdentityToken, Uri> authentication = await AuthenticateServiceAsync(cancellationToken);

            UriTemplate template = new UriTemplate("/v1/queues/{queue_name}");
            var parameters = new Dictionary<string, string>
                {
                    { "queue_name", queueName }
                };
            Uri uri = template.BindByName(authentication.Item2, parameters);

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Put, uri);
            request.Headers.Add("X-Auth-Token", authentication.Item1.Id);

            HttpResponseMessage response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, cancellationToken);
            response.EnsureSuccessStatusCode();
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<CloudQueue>> ListQueuesAsync(string marker, int? limit, bool detailed, CancellationToken cancellationToken)
        {
            if (limit <= 0)
                throw new ArgumentOutOfRangeException("limit");

            Tuple<IdentityToken, Uri> authentication = await AuthenticateServiceAsync(cancellationToken);

            UriTemplate template = new UriTemplate("/v1/queues?marker={marker}&limit={limit}&detailed={detailed}");
            var parameters = new Dictionary<string, string>
                {
                    { "detailed", detailed.ToString().ToLowerInvariant() },
                };
            if (marker != null)
                parameters.Add("marker", marker);
            if (limit.HasValue)
                parameters.Add("limit", limit.ToString());
            Uri uri = template.BindByName(authentication.Item2, parameters);

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, uri);
            request.Headers.Add("X-Auth-Token", authentication.Item1.Id);

            HttpResponseMessage response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, cancellationToken);
            response.EnsureSuccessStatusCode();

            string body = await response.Content.ReadAsStringAsync();
            ListCloudQueuesResponse result = await JsonConvert.DeserializeObjectAsync<ListCloudQueuesResponse>(body);
            if (result == null || result.Queues == null)
                return Enumerable.Empty<CloudQueue>();

            return result.Queues;
        }

        /// <inheritdoc/>
        public async Task<bool> QueueExistsAsync(string queueName, CancellationToken cancellationToken)
        {
            if (queueName == null)
                throw new ArgumentNullException("queueName");
            if (string.IsNullOrEmpty(queueName))
                throw new ArgumentException("queueName cannot be empty");

            Tuple<IdentityToken, Uri> authentication = await AuthenticateServiceAsync(cancellationToken);

            UriTemplate template = new UriTemplate("/v1/queues/{queue_name}");
            var parameters = new Dictionary<string, string>() { { "queue_name", queueName } };
            Uri uri = template.BindByName(authentication.Item2, parameters);

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Head, uri);
            request.Headers.Add("X-Auth-Token", authentication.Item1.Id);

            HttpResponseMessage response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, cancellationToken);
            switch (response.StatusCode)
            {
            case HttpStatusCode.NoContent:
                return true;

            case HttpStatusCode.NotFound:
                return false;

            default:
                response.EnsureSuccessStatusCode();
                return true;
            }
        }

        /// <inheritdoc/>
        public async Task DeleteQueueAsync(string queueName, CancellationToken cancellationToken)
        {
            if (queueName == null)
                throw new ArgumentNullException("queueName");
            if (string.IsNullOrEmpty(queueName))
                throw new ArgumentException("queueName cannot be empty");

            Tuple<IdentityToken, Uri> authentication = await AuthenticateServiceAsync(cancellationToken);

            UriTemplate template = new UriTemplate("/v1/queues/{queue_name}");
            var parameters = new Dictionary<string, string>
                {
                    { "queue_name", queueName }
                };
            Uri uri = template.BindByName(authentication.Item2, parameters);
            
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Delete, uri);
            request.Headers.Add("X-Auth-Token", authentication.Item1.Id);

            HttpResponseMessage response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, cancellationToken);
            response.EnsureSuccessStatusCode();
        }

        /// <inheritdoc/>
        public async Task SetQueueMetadataAsync<T>(string queueName, T metadata, CancellationToken cancellationToken)
            where T : class
        {
            if (queueName == null)
                throw new ArgumentNullException("queueName");
            if (string.IsNullOrEmpty(queueName))
                throw new ArgumentException("queueName cannot be empty");

            Tuple<IdentityToken, Uri> authentication = await AuthenticateServiceAsync(cancellationToken);

            UriTemplate template = new UriTemplate("/v1/queues/{queue_name}/metadata");
            var parameters = new Dictionary<string, string>
                {
                    { "queue_name", queueName }
                };
            Uri uri = template.BindByName(authentication.Item2, parameters);

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Put, uri);
            request.Headers.Add("X-Auth-Token", authentication.Item1.Id);
            string body = await JsonConvert.SerializeObjectAsync(metadata);
            request.Content = new StringContent(body, Encoding.UTF8, JsonRequestSettings.JsonContentType);

            HttpResponseMessage response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, cancellationToken);
            response.EnsureSuccessStatusCode();
        }

        /// <inheritdoc/>
        public Task<JObject> GetQueueMetadataAsync(string queueName, CancellationToken cancellationToken)
        {
            return GetQueueMetadataAsync<JObject>(queueName, cancellationToken);
        }

        /// <inheritdoc/>
        public async Task<T> GetQueueMetadataAsync<T>(string queueName, CancellationToken cancellationToken)
            where T : class
        {
            if (queueName == null)
                throw new ArgumentNullException("queueName");
            if (string.IsNullOrEmpty(queueName))
                throw new ArgumentException("queueName cannot be empty");

            Tuple<IdentityToken, Uri> authentication = await AuthenticateServiceAsync(cancellationToken);

            UriTemplate template = new UriTemplate("/v1/queues/{queue_name}/metadata");
            var parameters = new Dictionary<string, string>
                {
                    { "queue_name", queueName }
                };
            Uri uri = template.BindByName(authentication.Item2, parameters);

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, uri);
            request.Headers.Add("X-Auth-Token", authentication.Item1.Id);

            HttpResponseMessage response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, cancellationToken);
            response.EnsureSuccessStatusCode();

            string body = await response.Content.ReadAsStringAsync();
            return await JsonConvert.DeserializeObjectAsync<T>(body);
        }

        /// <inheritdoc/>
        public async Task<QueueStatistics> GetQueueStatisticsAsync(string queueName, CancellationToken cancellationToken)
        {
            if (queueName == null)
                throw new ArgumentNullException("queueName");
            if (string.IsNullOrEmpty(queueName))
                throw new ArgumentException("queueName cannot be empty");

            Tuple<IdentityToken, Uri> authentication = await AuthenticateServiceAsync(cancellationToken);

            UriTemplate template = new UriTemplate("/v1/queues/{queue_name}/stats");
            var parameters = new Dictionary<string, string>() { { "queue_name", queueName } };
            Uri uri = template.BindByName(authentication.Item2, parameters);

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, uri);
            request.Headers.Add("X-Auth-Token", authentication.Item1.Id);

            HttpResponseMessage response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, cancellationToken);
            response.EnsureSuccessStatusCode();

            string body = await response.Content.ReadAsStringAsync();
            return await JsonConvert.DeserializeObjectAsync<QueueStatistics>(body);
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<QueuedMessage>> ListMessagesAsync(string queueName, string marker, int? limit, bool echo, bool includeClaimed, CancellationToken cancellationToken)
        {
            if (queueName == null)
                throw new ArgumentNullException("queueName");
            if (string.IsNullOrEmpty(queueName))
                throw new ArgumentException("queueName cannot be empty");
            if (limit <= 0)
                throw new ArgumentOutOfRangeException("limit");

            Tuple<IdentityToken, Uri> authentication = await AuthenticateServiceAsync(cancellationToken);

            UriTemplate template = new UriTemplate("/v1/queues/{queue_name}/messages?marker={marker}&limit={limit}&echo={echo}&include_claimed={include_claimed}");

            var parameters =
                new Dictionary<string, string>()
                {
                    { "queue_name", queueName },
                    { "echo", echo.ToString() },
                    { "include_claimed", includeClaimed.ToString() }
                };
            if (marker != null)
                parameters["marker"] = marker;
            if (limit != null)
                parameters["limit"] = limit.ToString();
            Uri uri = template.BindByName(authentication.Item2, parameters);

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, uri);
            request.Headers.Add("X-Auth-Token", authentication.Item1.Id);

            HttpResponseMessage response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, cancellationToken);
            response.EnsureSuccessStatusCode();

            string body = await response.Content.ReadAsStringAsync();
            ListCloudQueueMessagesResponse result = await JsonConvert.DeserializeObjectAsync<ListCloudQueueMessagesResponse>(body);
            return result.Messages;
        }

        /// <inheritdoc/>
        public async Task<QueuedMessage> GetMessageAsync(string queueName, string messageId, CancellationToken cancellationToken)
        {
            if (queueName == null)
                throw new ArgumentNullException("queueName");
            if (string.IsNullOrEmpty(queueName))
                throw new ArgumentException("queueName cannot be empty");

            Tuple<IdentityToken, Uri> authentication = await AuthenticateServiceAsync(cancellationToken);

            UriTemplate template = new UriTemplate("/v1/queues/{queue_name}/messages/{message_id}");

            var parameters =
                new Dictionary<string, string>()
                {
                    { "queue_name", queueName },
                    { "message_id", messageId },
                };
            Uri uri = template.BindByName(authentication.Item2, parameters);

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, uri);
            request.Headers.Add("X-Auth-Token", authentication.Item1.Id);

            HttpResponseMessage response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, cancellationToken);
            response.EnsureSuccessStatusCode();

            string body = await response.Content.ReadAsStringAsync();
            return await JsonConvert.DeserializeObjectAsync<QueuedMessage>(body);
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<QueuedMessage>> GetMessagesAsync(string queueName, IEnumerable<string> messageIds, CancellationToken cancellationToken)
        {
            if (queueName == null)
                throw new ArgumentNullException("queueName");
            if (messageIds == null)
                throw new ArgumentNullException("messageIds");
            if (string.IsNullOrEmpty(queueName))
                throw new ArgumentException("queueName cannot be empty");

            Tuple<IdentityToken, Uri> authentication = await AuthenticateServiceAsync(cancellationToken);

            UriTemplate template = new UriTemplate("/v1/queues/{queue_name}/messages?ids={ids}");

            var parameters =
                new Dictionary<string, string>()
                {
                    { "queue_name", queueName },
                    { "ids", string.Join(",", messageIds.ToArray()) },
                };
            Uri uri = template.BindByName(authentication.Item2, parameters);

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, uri);
            request.Headers.Add("X-Auth-Token", authentication.Item1.Id);

            HttpResponseMessage response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, cancellationToken);
            response.EnsureSuccessStatusCode();

            string body = await response.Content.ReadAsStringAsync();
            return await JsonConvert.DeserializeObjectAsync<IEnumerable<QueuedMessage>>(body);
        }

        /// <inheritdoc/>
        public Task PostMessagesAsync(string queueName, IEnumerable<Message> messages, CancellationToken cancellationToken)
        {
            return PostMessagesAsync(queueName, cancellationToken, messages.ToArray());
        }

        /// <inheritdoc/>
        public async Task PostMessagesAsync(string queueName, CancellationToken cancellationToken, params Message[] messages)
        {
            if (queueName == null)
                throw new ArgumentNullException("queueName");
            if (messages == null)
                throw new ArgumentNullException("messages");
            if (string.IsNullOrEmpty(queueName))
                throw new ArgumentException("queueName cannot be empty");

            Tuple<IdentityToken, Uri> authentication = await AuthenticateServiceAsync(cancellationToken);

            UriTemplate template = new UriTemplate("/v1/queues/{queue_name}/messages");

            var parameters =
                new Dictionary<string, string>()
                {
                    { "queue_name", queueName },
                };
            Uri uri = template.BindByName(authentication.Item2, parameters);

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, uri);
            request.Headers.Add("X-Auth-Token", authentication.Item1.Id);
            string body = await JsonConvert.SerializeObjectAsync(messages);
            request.Content = new StringContent(body, Encoding.UTF8, JsonRequestSettings.JsonContentType);

            HttpResponseMessage response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, cancellationToken);
            response.EnsureSuccessStatusCode();
        }

        /// <inheritdoc/>
        public Task PostMessagesAsync<T>(string queueName, IEnumerable<Message<T>> messages, CancellationToken cancellationToken)
        {
            return PostMessagesAsync(queueName, cancellationToken, messages.ToArray());
        }

        /// <inheritdoc/>
        public async Task PostMessagesAsync<T>(string queueName, CancellationToken cancellationToken, params Message<T>[] messages)
        {
            if (queueName == null)
                throw new ArgumentNullException("queueName");
            if (messages == null)
                throw new ArgumentNullException("messages");
            if (string.IsNullOrEmpty(queueName))
                throw new ArgumentException("queueName cannot be empty");

            Tuple<IdentityToken, Uri> authentication = await AuthenticateServiceAsync(cancellationToken);

            UriTemplate template = new UriTemplate("/v1/queues/{queue_name}/messages");

            var parameters =
                new Dictionary<string, string>()
                {
                    { "queue_name", queueName },
                };
            Uri uri = template.BindByName(authentication.Item2, parameters);

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, uri);
            request.Headers.Add("X-Auth-Token", authentication.Item1.Id);
            string body = await JsonConvert.SerializeObjectAsync(messages);
            request.Content = new StringContent(body, Encoding.UTF8, JsonRequestSettings.JsonContentType);

            HttpResponseMessage response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, cancellationToken);
            response.EnsureSuccessStatusCode();
        }

        /// <inheritdoc/>
        public async Task DeleteMessageAsync(string queueName, string messageId, Claim claim, CancellationToken cancellationToken)
        {
            if (queueName == null)
                throw new ArgumentNullException("queueName");
            if (string.IsNullOrEmpty(queueName))
                throw new ArgumentException("queueName cannot be empty");

            Tuple<IdentityToken, Uri> authentication = await AuthenticateServiceAsync(cancellationToken);

            UriTemplate template = new UriTemplate("/v1/queues/{queue_name}/messages/{message_id}?claim_id={claim_id}");

            var parameters =
                new Dictionary<string, string>()
                {
                    { "queue_name", queueName },
                    { "message_id", messageId },
                };
            if (claim != null)
                parameters["claim_id"] = claim.Id;
            Uri uri = template.BindByName(authentication.Item2, parameters);

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Delete, uri);
            request.Headers.Add("X-Auth-Token", authentication.Item1.Id);

            HttpResponseMessage response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, cancellationToken);
            response.EnsureSuccessStatusCode();
        }

        /// <inheritdoc/>
        public async Task DeleteMessagesAsync(string queueName, IEnumerable<string> messageIds, CancellationToken cancellationToken)
        {
            if (queueName == null)
                throw new ArgumentNullException("queueName");
            if (messageIds == null)
                throw new ArgumentNullException("messageIds");
            if (string.IsNullOrEmpty(queueName))
                throw new ArgumentException("queueName cannot be empty");

            Tuple<IdentityToken, Uri> authentication = await AuthenticateServiceAsync(cancellationToken);

            UriTemplate template = new UriTemplate("/v1/queues/{queue_name}/messages?ids={ids}");

            var parameters =
                new Dictionary<string, string>()
                {
                    { "queue_name", queueName },
                    { "ids", string.Join(",", messageIds.ToArray()) },
                };
            Uri uri = template.BindByName(authentication.Item2, parameters);

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Delete, uri);
            request.Headers.Add("X-Auth-Token", authentication.Item1.Id);

            HttpResponseMessage response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, cancellationToken);
            response.EnsureSuccessStatusCode();
        }

        /// <inheritdoc/>
        public async Task<Claim> ClaimMessageAsync(string queueName, int? limit, TimeSpan timeToLive, TimeSpan gracePeriod, CancellationToken cancellationToken)
        {
            if (queueName == null)
                throw new ArgumentNullException("queueName");
            if (string.IsNullOrEmpty(queueName))
                throw new ArgumentException("queueName cannot be empty");
            if (limit < 0)
                throw new ArgumentOutOfRangeException("limit");

            Tuple<IdentityToken, Uri> authentication = await AuthenticateServiceAsync(cancellationToken);

            UriTemplate template = new UriTemplate("/v1/queues/{queue_name}/claims?limit={limit}");

            var parameters =
                new Dictionary<string, string>()
                {
                    { "queue_name", queueName },
                };
            if (limit != null)
                parameters["limit"] = limit.ToString();
            Uri uri = template.BindByName(authentication.Item2, parameters);

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, uri);
            request.Headers.Add("X-Auth-Token", authentication.Item1.Id);
            var body = new ClaimMessagesRequest(timeToLive, gracePeriod);
            string bodyText = await JsonConvert.SerializeObjectAsync(body);
            request.Content = new StringContent(bodyText, Encoding.UTF8, JsonRequestSettings.JsonContentType);

            HttpResponseMessage response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, cancellationToken);
            response.EnsureSuccessStatusCode();

            Uri location = response.Headers.Location;
            if (location != null && !location.IsAbsoluteUri)
                location = new Uri(authentication.Item2, location.ToString());

            IEnumerable<QueuedMessage> messages;
            if (response.StatusCode == HttpStatusCode.NoContent)
            {
                messages = Enumerable.Empty<QueuedMessage>();
            }
            else
            {
                messages = await JsonConvert.DeserializeObjectAsync<IEnumerable<QueuedMessage>>(await response.Content.ReadAsStringAsync());
            }

            return new Claim(this, queueName, location, timeToLive, TimeSpan.Zero, messages);
        }

        /// <inheritdoc/>
        public Task<Claim> QueryClaimAsync(string queueName, Claim claim, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public Task UpdateClaimAsync(string queueName, Claim claim, TimeSpan timeToLive, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public async Task ReleaseClaimAsync(string queueName, Claim claim, CancellationToken cancellationToken)
        {
            if (queueName == null)
                throw new ArgumentNullException("queueName");
            if (claim == null)
                throw new ArgumentNullException("claim");
            if (string.IsNullOrEmpty(queueName))
                throw new ArgumentException("queueName cannot be empty");

            Tuple<IdentityToken, Uri> authentication = await AuthenticateServiceAsync(cancellationToken);

            UriTemplate template = new UriTemplate("/v1/queues/{queue_name}/claims/{claim_id}");

            var parameters =
                new Dictionary<string, string>()
                {
                    { "queue_name", queueName },
                    { "claim_id", claim.Id },
                };
            Uri uri = template.BindByName(authentication.Item2, parameters);

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Delete, uri);
            request.Headers.Add("X-Auth-Token", authentication.Item1.Id);

            HttpResponseMessage response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, cancellationToken);
            response.EnsureSuccessStatusCode();
        }

        #endregion

        /// <summary>
        /// Gets the base URI for accessing this API.
        /// </summary>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> which may be used to cancel the asynchronous operation.</param>
        /// <returns>A <see cref="Task"/> object representing the asynchronous operation.</returns>
        private Task<Uri> GetBaseUriAsync(CancellationToken cancellationToken)
        {
            if (_baseUri != null)
            {
                return Task.FromResult(_baseUri);
            }

            return Task.Factory.StartNew(
                () =>
                {
                    try
                    {
                        Endpoint endpoint = GetServiceEndpoint(null, "queues", "cloudQueues", null);
                        Uri baseUri = new Uri(_internalUrl ? endpoint.InternalURL : endpoint.PublicURL);
                        _baseUri = baseUri;
                        return baseUri;
                    }
                    catch (UserAuthorizationException)
                    {
                        string effectiveRegion = null;
                        if (!string.IsNullOrEmpty(DefaultRegion))
                        {
                            effectiveRegion = DefaultRegion;
                        }
                        else
                        {
                            UserAccess userAccess = IdentityProvider.GetUserAccess(GetDefaultIdentity(null));
                            if (!string.IsNullOrEmpty(userAccess.User.DefaultRegion))
                                effectiveRegion = userAccess.User.DefaultRegion;
                        }

                        Uri baseUri;
                        switch (effectiveRegion.ToUpperInvariant())
                        {
                        case "ORD":
                            if (_internalUrl)
                                baseUri = new Uri("https://snet-ord.queues.api.rackspacecloud.com/");
                            else
                                baseUri = new Uri("https://preview.queue.api.rackspacecloud.com/");
                            break;

                        case "LON":
                            if (_internalUrl)
                                baseUri = new Uri("https://snet-lon.queues.api.rackspacecloud.com/");
                            else
                                baseUri = new Uri("https://lon.queues.api.rackspacecloud.com/");
                            break;

                        default:
                            throw;
                        }

                        _baseUri = baseUri;
                        return baseUri;
                    }
                });
        }

        /// <summary>
        /// Authenticates the user and gets the base URI for accessing this API.
        /// </summary>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> which may be used to cancel the asynchronous operation.</param>
        /// <returns>A <see cref="Task"/> object representing the asynchronous operation.</returns>
        private async Task<Tuple<IdentityToken, Uri>> AuthenticateServiceAsync(CancellationToken cancellationToken)
        {
            IdentityToken token;
            IIdentityService identityService = IdentityProvider as IIdentityService;
            if (identityService != null)
                token = await identityService.GetTokenAsync(GetDefaultIdentity(null));
            else
                token = await Task.Factory.StartNew(() => IdentityProvider.GetToken(GetDefaultIdentity(null)));

            Uri baseUri = await GetBaseUriAsync(cancellationToken);
            return Tuple.Create(token, baseUri);
        }
    }
}
