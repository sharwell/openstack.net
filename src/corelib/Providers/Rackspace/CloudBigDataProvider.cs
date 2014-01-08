namespace net.openstack.Providers.Rackspace
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Net;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using JSIStudios.SimpleRESTServices.Client;
    using net.openstack.Core;
    using net.openstack.Core.Domain;
    using net.openstack.Core.Exceptions;
    using net.openstack.Core.Providers;
    using net.openstack.Core.Validators;
    using net.openstack.Providers.Rackspace.Objects.Hadoop;
    using Newtonsoft.Json.Linq;
    using FlavorId = net.openstack.Providers.Rackspace.Objects.Databases.FlavorId;

    public class CloudBigDataProvider : ProviderBase<IHadoopService>, IHadoopService
    {
        /// <summary>
        /// This field caches the base URI used for accessing the Cloud Big Data service.
        /// </summary>
        /// <seealso cref="GetBaseUriAsync"/>
        private Uri _baseUri;

        /// <summary>
        /// Initializes a new instance of the <see cref="CloudBigDataProvider"/> class with
        /// the specified values.
        /// </summary>
        /// <param name="defaultIdentity">The default identity to use for calls that do not explicitly specify an identity. If this value is <see langword="null"/>, no default identity is available so all calls must specify an explicit identity.</param>
        /// <param name="defaultRegion">The default region to use for calls that do not explicitly specify a region. If this value is <see langword="null"/>, the default region for the user will be used; otherwise if the service uses region-specific endpoints all calls must specify an explicit region.</param>
        /// <param name="identityProvider">The identity provider to use for authenticating requests to this provider. If this value is <see langword="null"/>, a new instance of <see cref="CloudIdentityProvider"/> is created using <paramref name="defaultIdentity"/> as the default identity.</param>
        public CloudBigDataProvider(CloudIdentity defaultIdentity, string defaultRegion, IIdentityProvider identityProvider)
            : base(defaultIdentity, defaultRegion, identityProvider, null, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CloudBigDataProvider"/> class with
        /// the specified values.
        /// </summary>
        /// <param name="defaultIdentity">The default identity to use for calls that do not explicitly specify an identity. If this value is <see langword="null"/>, no default identity is available so all calls must specify an explicit identity.</param>
        /// <param name="defaultRegion">The default region to use for calls that do not explicitly specify a region. If this value is <see langword="null"/>, the default region for the user will be used; otherwise if the service uses region-specific endpoints all calls must specify an explicit region.</param>
        /// <param name="identityProvider">The identity provider to use for authenticating requests to this provider. If this value is <see langword="null"/>, a new instance of <see cref="CloudIdentityProvider"/> is created using <paramref name="defaultIdentity"/> as the default identity.</param>
        /// <param name="restService">The implementation of <see cref="IRestService"/> to use for executing synchronous REST requests. If this value is <see langword="null"/>, the provider will use a new instance of <see cref="JsonRestServices"/>.</param>
        /// <param name="httpStatusCodeValidator">The HTTP status code validator to use for synchronous REST requests. If this value is <see langword="null"/>, the provider will use <see cref="HttpResponseCodeValidator.Default"/>.</param>
        protected CloudBigDataProvider(CloudIdentity defaultIdentity, string defaultRegion, IIdentityProvider identityProvider, IRestService restService, IHttpResponseCodeValidator httpStatusCodeValidator)
            : base(defaultIdentity, defaultRegion, identityProvider, restService, httpStatusCodeValidator)
        {
        }

        #region IHadoopService Members

        /// <inheritdoc/>
        public Task GetProfileAsync()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public Task UpdateProfileAsync()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public Task CreateClusterAsync()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public Task ListClustersAsync()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public Task GetClusterAsync()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public Task RemoveClusterAsync()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public Task ResizeClusterAsync(ClusterId clusterId, ResizeClusterConfiguration configuration, AsyncCompletionOption completionOption, CancellationToken cancellationToken, IProgress<Cluster> progress)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public Task<ReadOnlyCollection<HadoopNode>> ListNodesAsync(ClusterId clusterId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public Task<HadoopNode> GetNodeAsync(ClusterId clusterId, HadoopNodeId nodeId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public Task<ReadOnlyCollection<HadoopFlavor>> ListFlavorsAsync(CancellationToken cancellationToken)
        {
            UriTemplate template = new UriTemplate("/flavors");
            var parameters = new Dictionary<string, string>();

            Func<Task<Tuple<IdentityToken, Uri>>, HttpWebRequest> prepareRequest =
                PrepareRequestAsyncFunc(HttpMethod.GET, template, parameters);

            Func<Task<HttpWebRequest>, Task<JObject>> requestResource =
                GetResponseAsyncFunc<JObject>(cancellationToken);

            Func<Task<JObject>, ReadOnlyCollection<HadoopFlavor>> resultSelector =
                task =>
                {
                    JObject result = task.Result;
                    if (result == null)
                        return null;

                    JToken records = result["flavors"];
                    if (records == null)
                        return null;

                    return records.ToObject<ReadOnlyCollection<HadoopFlavor>>();
                };

            return AuthenticateServiceAsync(cancellationToken)
                .ContinueWith(prepareRequest)
                .ContinueWith(requestResource).Unwrap()
                .ContinueWith(resultSelector);
        }

        /// <inheritdoc/>
        public Task<HadoopFlavor> GetFlavorAsync(FlavorId flavorId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public Task<ClusterType> GetSupportedTypesAsync(FlavorId flavorId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public Task<ReadOnlyCollection<ClusterType>> ListClusterTypesAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public Task<ClusterType> GetClusterTypeAsync(ClusterTypeId clusterTypeId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public Task<ReadOnlyCollection<HadoopFlavor>> GetSupportedFlavorsAsync(ClusterTypeId clusterTypeId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public Task GetResourceLimitsAsync()
        {
            throw new NotImplementedException();
        }

        #endregion

        /// <inheritdoc/>
        /// <remarks>
        /// This method returns a cached base address if one is available. If no cached address is
        /// available, <see cref="ProviderBase{TProvider}.GetServiceEndpoint"/> is called to obtain
        /// an <see cref="Endpoint"/> with the type <c>rax:bigdata</c> and preferred type <c>cloudBigData</c>.
        /// </remarks>
        protected override Task<Uri> GetBaseUriAsync(CancellationToken cancellationToken)
        {
            if (_baseUri != null)
            {
                return InternalTaskExtensions.CompletedTask(_baseUri);
            }

            return Task.Factory.StartNew(
                () =>
                {
                    try
                    {
                        Endpoint endpoint = GetServiceEndpoint(null, "rax:bigdata", "cloudBigData", null);
                        _baseUri = new Uri(endpoint.PublicURL);
                        return _baseUri;
                    }
                    catch (UserAuthorizationException)
                    {
                        var userAccess = IdentityProvider.GetUserAccess(GetDefaultIdentity(null));

                        string effectiveRegion;
                        if (!string.IsNullOrEmpty(DefaultRegion))
                            effectiveRegion = DefaultRegion;
                        else
                            effectiveRegion = userAccess.User.DefaultRegion;

                        return new Uri(string.Format("https://{0}.bigdata.api.rackspacecloud.com/v1.0/{1}", effectiveRegion.ToLowerInvariant(), userAccess.Token.Tenant.Id));
                    }
                });
        }
    }
}
