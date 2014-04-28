namespace OpenStack.Security.Authentication
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using OpenStack.Services.Identity.V2;
    using Rackspace.Threading;
    using NoDefaultRegionSetException = net.openstack.Core.Exceptions.NoDefaultRegionSetException;
    using UserAuthenticationException = net.openstack.Core.Exceptions.UserAuthenticationException;
    using UserAuthorizationException = net.openstack.Core.Exceptions.UserAuthorizationException;

#if !NET40PLUS
    using OpenStack.Compat;
#endif

    public class IdentityV2AuthenticationClient : IAuthenticationService
    {
        /// <summary>
        /// This is the backing field for the <see cref="IdentityService"/> property.
        /// </summary>
        private readonly IIdentityService _identityService;

        /// <summary>
        /// This is the backing field for the <see cref="AuthenticationRequest"/> property.
        /// </summary>
        private readonly AuthenticationRequest _authenticationRequest;

        private UserAccess _userAccess;

        public IdentityV2AuthenticationClient(IIdentityService identityService, AuthenticationRequest authenticationRequest)
        {
            if (identityService == null)
                throw new ArgumentNullException("identityService");
            if (authenticationRequest == null)
                throw new ArgumentNullException("authenticationRequest");

            _identityService = identityService;
            _authenticationRequest = authenticationRequest;
        }

        protected IIdentityService IdentityService
        {
            get
            {
                return _identityService;
            }
        }

        protected AuthenticationRequest AuthenticationRequest
        {
            get
            {
                return _authenticationRequest;
            }
        }

        #region IAuthenticationService Members

        public virtual Task<Uri> GetBaseAddressAsync(string serviceType, string serviceName, string region, bool internalAddress, CancellationToken cancellationToken)
        {
            return
                AuthenticateAsync(cancellationToken)
                .Select(task => GetBaseAddressImpl(task.Result, serviceType, serviceName, region, internalAddress));
        }

        public virtual Task AuthenticateRequestAsync(HttpRequestMessage requestMessage, CancellationToken cancellationToken)
        {
            return
                AuthenticateAsync(cancellationToken)
                .Select(
                    task =>
                    {
                        UserAccess userAccess = task.Result;
                        if (userAccess == null)
                            return;

                        IdentityToken token = userAccess.Token;
                        if (token == null)
                            return;

                        if (token.Id == null)
                            return;

                        requestMessage.Headers.Add("X-Auth-Token", token.Id.Value);
                    });
        }

        #endregion

        protected virtual Task<UserAccess> AuthenticateAsync(CancellationToken cancellationToken)
        {
            if (_userAccess != null && _userAccess.Token != null && _userAccess.Token.Expiration > DateTimeOffset.Now + TimeSpan.FromMinutes(5))
                return CompletedTask.FromResult(_userAccess);

            return
                IdentityService.AuthenticateAsync(AuthenticationRequest, cancellationToken)
                .Select(
                    task =>
                    {
                        _userAccess = task.Result;
                        return task.Result;
                    });
        }

        protected virtual Uri GetBaseAddressImpl(UserAccess userAccess, string serviceType, string serviceName, string region, bool internalAddress)
        {
            if (serviceType == null)
                throw new ArgumentNullException("serviceType");
            if (string.IsNullOrEmpty(serviceType))
                throw new ArgumentException("serviceType cannot be empty");

            if (userAccess == null || userAccess.ServiceCatalog == null)
                throw new UserAuthenticationException("Unable to authenticate user and retrieve authorized service endpoints.");

            IEnumerable<ServiceCatalogEntry> services = userAccess.ServiceCatalog.Where(sc => string.Equals(sc.Type, serviceType, StringComparison.OrdinalIgnoreCase));

            if (serviceName != null)
            {
                IEnumerable<ServiceCatalogEntry> namedServices = services.Where(sc => string.Equals(sc.Name, serviceName, StringComparison.OrdinalIgnoreCase));
                if (namedServices.Any())
                    services = namedServices;
            }

            IEnumerable<Tuple<ServiceCatalogEntry, Endpoint>> endpoints =
                services.SelectMany(service => service.Endpoints.Select(endpoint => Tuple.Create(service, endpoint)));

            string effectiveRegion = GetEffectiveRegion(userAccess, region);

            IEnumerable<Tuple<ServiceCatalogEntry, Endpoint>> regionEndpoints =
                endpoints.Where(i => string.Equals(i.Item2.Region ?? string.Empty, effectiveRegion ?? string.Empty, StringComparison.OrdinalIgnoreCase));

            if (regionEndpoints.Any())
                endpoints = regionEndpoints;
            else
                endpoints = endpoints.Where(i => string.IsNullOrEmpty(i.Item2.Region));

            if (effectiveRegion == null && !endpoints.Any())
                throw new NoDefaultRegionSetException("No region was provided, the service does not provide a region-independent endpoint, and there is no default region set for the user's account.");

            Tuple<ServiceCatalogEntry, Endpoint> serviceEndpoint = endpoints.FirstOrDefault();
            if (serviceEndpoint == null)
                throw new UserAuthorizationException("The user does not have access to the requested service or region.");

            Uri baseAddress;
            if (internalAddress)
                baseAddress = serviceEndpoint.Item2.InternalUri;
            else
                baseAddress = serviceEndpoint.Item2.PublicUri;

            if (baseAddress != null && baseAddress.IsAbsoluteUri && !baseAddress.AbsoluteUri.EndsWith("/"))
                baseAddress = new Uri(baseAddress.AbsoluteUri + "/", UriKind.Absolute);

            return baseAddress;
        }

        protected virtual string GetEffectiveRegion(UserAccess userAccess, string region)
        {
            return region;
        }
    }
}
