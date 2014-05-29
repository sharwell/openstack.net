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

    /// <summary>
    /// This class defines an authentication service based on the OpenStack Identity Service V2.
    /// </summary>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
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

        /// <summary>
        /// This field caches the result of authenticating a user, improving the efficiency of
        /// locating multiple services in the service catalog.
        /// </summary>
        private UserAccess _userAccess;

        /// <summary>
        /// This is the backing field for the <see cref="ExpirationOverlap"/> property.
        /// </summary>
        private TimeSpan _expirationOverlap = TimeSpan.FromMinutes(5);

        /// <summary>
        /// Initializes a new instance of the <see cref="IdentityV2AuthenticationClient"/> class
        /// with the specified identity service and prepared authentication request.
        /// </summary>
        /// <param name="identityService">The <see cref="IIdentityService"/> instance to use for authentication purposes.</param>
        /// <param name="authenticationRequest">The authentication request, which contains the credentials to use for authenticating with the Identity Service.</param>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="identityService"/> is <see langword="null"/>.
        /// <para>-or-</para>
        /// <para>If <paramref name="authenticationRequest"/> is <see langword="null"/>.</para>
        /// </exception>
        public IdentityV2AuthenticationClient(IIdentityService identityService, AuthenticationRequest authenticationRequest)
        {
            if (identityService == null)
                throw new ArgumentNullException("identityService");
            if (authenticationRequest == null)
                throw new ArgumentNullException("authenticationRequest");

            _identityService = identityService;
            _authenticationRequest = authenticationRequest;
        }

        /// <summary>
        /// Gets or sets the overlap to consider for reauthenticating tokens that are about to expire.
        /// The default value is 5 minutes.
        /// </summary>
        /// <returns>
        /// The expiration overlap for tokens provided by the Identity Service. If the time until a
        /// token expires is less than this value, it will be treated as already expired and the
        /// Identity API will be used to reauthenticate the user.
        /// </returns>
        /// <exception cref="ArgumentNullException">If <paramref name="value"/> is negative.</exception>
        public TimeSpan ExpirationOverlap
        {
            get
            {
                return _expirationOverlap;
            }

            set
            {
                if (value < TimeSpan.Zero)
                    throw new ArgumentOutOfRangeException("value");

                _expirationOverlap = value;
            }
        }

        /// <summary>
        /// Gets the <see cref="IIdentityService"/> instance to use for authentication requests.
        /// </summary>
        /// <value>The <see cref="IIdentityService"/> instance to use for authentication requests.</value>
        protected IIdentityService IdentityService
        {
            get
            {
                return _identityService;
            }
        }

        /// <summary>
        /// Gets the prepared <see cref="OpenStack.Services.Identity.V2.AuthenticationRequest"/> to use
        /// for authentication purposes.
        /// </summary>
        /// <value>The prepared <see cref="OpenStack.Services.Identity.V2.AuthenticationRequest"/> to use
        /// for authentication purposes.</value>
        protected AuthenticationRequest AuthenticationRequest
        {
            get
            {
                return _authenticationRequest;
            }
        }

        #region IAuthenticationService Members

        /// <inheritdoc/>
        /// <remarks>
        /// The base implementation authenticates with the Identity Service V2 if necessary, followed
        /// by calling <see cref="GetBaseAddressImpl"/> to locate a service endpoint within the user's
        /// service catalog.
        /// </remarks>
        public virtual Task<Uri> GetBaseAddressAsync(string serviceType, string serviceName, string region, bool internalAddress, CancellationToken cancellationToken)
        {
            return
                AuthenticateAsync(cancellationToken)
                .Select(task => GetBaseAddressImpl(task.Result, serviceType, serviceName, region, internalAddress));
        }

        /// <inheritdoc/>
        /// <remarks>
        /// The base implementation of this authentication client sets the <c>X-Auth-Token</c>
        /// HTTP header of requests to the token obtained from the Identity Service V2.
        /// </remarks>
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

        /// <summary>
        /// Authenticates the credentials provided in <see cref="AuthenticationRequest"/> with
        /// the Identity Service V2.
        /// </summary>
        /// <remarks>
        /// This method caches the authentication result to avoid unnecessary calls to the
        /// Identity API. If a cached authentication result is available but has either expired
        /// or will expire soon (see <see cref="ExpirationOverlap"/>), the cached result is
        /// discarded and the credentials are reauthenticated with the Identity Service.
        /// </remarks>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> that the task will observe.</param>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation. When the task completes
        /// successfully, the <see cref="Task{TResult}.Result"/> property will contain a
        /// <see cref="UserAccess"/> instance providing the details for the authenticated user.
        /// </returns>
        protected virtual Task<UserAccess> AuthenticateAsync(CancellationToken cancellationToken)
        {
            if (_userAccess != null && _userAccess.Token != null && _userAccess.Token.Expiration > DateTimeOffset.Now + ExpirationOverlap)
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

#warning Need to define the exception(s) for cases where the requested service could not be found, and/or did not contain an endpoint in the specified region
        /// <summary>
        /// This method provides the core implementation of <see cref="GetBaseAddressAsync"/> after a <see cref="UserAccess"/>
        /// is obtained from the Identity Service.
        /// </summary>
        /// <param name="userAccess">A <see cref="UserAccess"/> object containing the details for the authenticated user.</param>
        /// <param name="serviceType">The service type to locate.</param>
        /// <param name="serviceName">The preferred name of the service.</param>
        /// <param name="region">The preferred region for the service. This method calls <see cref="GetEffectiveRegion"/> with this value to obtain the actual region to consider for this algorithm.</param>
        /// <param name="internalAddress"><see langword="true"/> to return a base address for accessing the service over a local network; otherwise, <see langword="false"/> to return a base address for accessing the service over a public network (the internet).</param>
        /// <returns>A <see cref="Uri"/> containing the base address for accessing the service.</returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="userAccess"/> is <see langword="null"/>.
        /// <para>-or-</para>
        /// <para>If <paramref name="serviceType"/> is <see langword="null"/>.</para>
        /// </exception>
        /// <exception cref="ArgumentException">If <paramref name="serviceType"/> is empty.</exception>
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

        /// <summary>
        /// Gets the effective region to use for locating services in the service catalog, for the specified
        /// <see cref="UserAccess"/> and preferred region.
        /// </summary>
        /// <remarks>
        /// The default implementation simply returns <paramref name="region"/>. Specific vendors
        /// may extend this functionality to provide a default value when applicable to their users.
        /// </remarks>
        /// <param name="userAccess">The <see cref="UserAccess"/> object providing details for the authenticated user.</param>
        /// <param name="region">The preferred region, as specified in the call to <see cref="GetBaseAddressAsync"/>.</param>
        /// <returns>The effective region to use for service location in <see cref="GetBaseAddressImpl"/>.</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="userAccess"/> is <see langword="null"/>.</exception>
        protected virtual string GetEffectiveRegion(UserAccess userAccess, string region)
        {
            return region;
        }
    }
}
