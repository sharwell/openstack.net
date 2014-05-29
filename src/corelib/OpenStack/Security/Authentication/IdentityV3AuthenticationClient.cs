namespace OpenStack.Security.Authentication
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using OpenStack.Collections;
    using OpenStack.Services.Identity.V3;
    using Rackspace.Threading;
    using NoDefaultRegionSetException = net.openstack.Core.Exceptions.NoDefaultRegionSetException;
    using UserAuthorizationException = net.openstack.Core.Exceptions.UserAuthorizationException;

#if !NET40PLUS
    using OpenStack.Compat;
#endif

    /// <summary>
    /// This class defines an authentication service based on the OpenStack Identity Service V3.
    /// </summary>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    public class IdentityV3AuthenticationClient : IAuthenticationService
    {
        /// <summary>
        /// This is the backing field for the <see cref="IdentityService"/> property.
        /// </summary>
        private readonly IIdentityService _identityService;

        /// <summary>
        /// This is the backing field for the <see cref="AuthenticateRequest"/> property.
        /// </summary>
        private readonly AuthenticateRequest _authenticateRequest;

        /// <summary>
        /// This field caches the result of authenticating a user, reducing the number of
        /// calls required to the Identity Service API.
        /// </summary>
        private Tuple<TokenId, AuthenticateResponse> _authenticatedToken;

        /// <summary>
        /// This is the backing field for the <see cref="ExpirationOverlap"/> property.
        /// </summary>
        private TimeSpan _expirationOverlap = TimeSpan.FromMinutes(5);

        /// <summary>
        /// Initializes a new instance of the <see cref="IdentityV3AuthenticationClient"/> class
        /// with the specified identity service and prepared authentication request.
        /// </summary>
        /// <param name="identityService">The <see cref="IIdentityService"/> instance to use for authentication purposes.</param>
        /// <param name="authenticateRequest">The authentication request, which contains the credentials to use for authenticating with the Identity Service.</param>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="identityService"/> is <see langword="null"/>.
        /// <para>-or-</para>
        /// <para>If <paramref name="authenticateRequest"/> is <see langword="null"/>.</para>
        /// </exception>
        public IdentityV3AuthenticationClient(IIdentityService identityService, AuthenticateRequest authenticateRequest)
        {
            if (identityService == null)
                throw new ArgumentNullException("identityService");
            if (authenticateRequest == null)
                throw new ArgumentNullException("authenticateRequest");

            _identityService = identityService;
            _authenticateRequest = authenticateRequest;
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
        /// Gets the prepared <see cref="OpenStack.Services.Identity.V3.AuthenticateRequest"/> to use
        /// for authentication purposes.
        /// </summary>
        /// <value>The prepared <see cref="OpenStack.Services.Identity.V3.AuthenticateRequest"/> to use
        /// for authentication purposes.</value>
        protected AuthenticateRequest AuthenticateRequest
        {
            get
            {
                return _authenticateRequest;
            }
        }

        #region IAuthenticationService Members

        /// <inheritdoc/>
        /// <remarks>
        /// The base implementation authenticates with the Identity Service V3 if necessary, followed
        /// by calling <see cref="GetBaseAddressAsyncImpl"/> to locate a service base address.
        /// </remarks>
        public virtual Task<Uri> GetBaseAddressAsync(string serviceType, string serviceName, string region, bool internalAddress, CancellationToken cancellationToken)
        {
            return
                AuthenticateAsync(cancellationToken)
                .Then(task => GetBaseAddressAsyncImpl(task.Result, serviceType, serviceName, region, internalAddress, cancellationToken));
        }

        /// <inheritdoc/>
        /// <remarks>
        /// The base implementation of this authentication client sets the <c>X-Auth-Token</c>
        /// HTTP header of requests to the token obtained from the Identity Service V3.
        /// </remarks>
        public virtual Task AuthenticateRequestAsync(HttpRequestMessage requestMessage, CancellationToken cancellationToken)
        {
            return
                AuthenticateAsync(cancellationToken)
                .Select(
                    task =>
                    {
                        Tuple<TokenId, AuthenticateResponse> userAccess = task.Result;
                        if (userAccess == null)
                            return;

                        TokenId token = userAccess.Item1;
                        if (token == null)
                            return;

                        requestMessage.Headers.Add("X-Auth-Token", token.Value);
                    });
        }

        #endregion

        /// <summary>
        /// Authenticates the credentials provided in <see cref="AuthenticateRequest"/> with
        /// the Identity Service V3.
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
        /// successfully, the <see cref="Task{TResult}.Result"/> property will contain the result
        /// provided by the Identity Service.
        /// </returns>
        protected virtual Task<Tuple<TokenId, AuthenticateResponse>> AuthenticateAsync(CancellationToken cancellationToken)
        {
            if (_authenticatedToken != null && _authenticatedToken.Item2 != null && _authenticatedToken.Item2.Token != null && _authenticatedToken.Item2.Token.ExpiresAt > DateTimeOffset.Now + TimeSpan.FromMinutes(5))
                return CompletedTask.FromResult(_authenticatedToken);

            return
                CoreTaskExtensions.Using(
                    () => IdentityService.PrepareAuthenticateAsync(AuthenticateRequest, cancellationToken),
                    task => task.Result.SendAsync(cancellationToken))
                .Select(
                    task =>
                    {
                        _authenticatedToken = task.Result.Item2;
                        return task.Result.Item2;
                    });
        }

#warning Need to define the exception(s) for cases where the requested service could not be found, and/or did not contain an endpoint in the specified region
        /// <summary>
        /// This method provides the core implementation of <see cref="GetBaseAddressAsync"/> after authentication
        /// details are obtained from the Identity Service.
        /// </summary>
        /// <param name="authenticatedToken">The authentication details provided by the Identity Service.</param>
        /// <param name="serviceType">The service type to locate.</param>
        /// <param name="serviceName">The preferred name of the service.</param>
        /// <param name="region">The preferred region for the service. This method calls <see cref="GetEffectiveRegion"/> with this value to obtain the actual region to consider for this algorithm.</param>
        /// <param name="internalAddress"><see langword="true"/> to return a base address for accessing the service over a local network; otherwise, <see langword="false"/> to return a base address for accessing the service over a public network (the internet).</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> that the task will observe.</param>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation. When the task completes
        /// successfully, the <see cref="Task{TResult}.Result"/> property will contain a
        ///  <see cref="Uri"/> containing the base address for accessing the service.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="authenticatedToken"/> is <see langword="null"/>.
        /// <para>-or-</para>
        /// <para>If <paramref name="serviceType"/> is <see langword="null"/>.</para>
        /// </exception>
        /// <exception cref="ArgumentException">If <paramref name="serviceType"/> is empty.</exception>
        protected virtual Task<Uri> GetBaseAddressAsyncImpl(Tuple<TokenId, AuthenticateResponse> authenticatedToken, string serviceType, string serviceName, string region, bool internalAddress, CancellationToken cancellationToken)
        {
            if (authenticatedToken == null)
                throw new ArgumentNullException("authenticatedToken");
            if (serviceType == null)
                throw new ArgumentNullException("serviceType");
            if (string.IsNullOrEmpty(serviceType))
                throw new ArgumentException("serviceType cannot be empty");

            // first locate the correct service
            Task<ReadOnlyCollection<Service>> services =
                CoreTaskExtensions.Using(
                    () => IdentityService.PrepareListServicesAsync(cancellationToken).WithType(serviceType),
                    task => task.Result.SendAsync(cancellationToken))
                .Then(task => task.Result.Item2.GetAllPagesAsync(cancellationToken, null));

            // get the endpoints for each service
            Task<List<Tuple<Service, ReadOnlyCollection<Endpoint>>>> serviceEndpoints =
                services.Then(
                    task =>
                    {
                        ReadOnlyCollection<Service> servicesResult = task.Result;
                        List<Task<ReadOnlyCollection<Endpoint>>> listEndpoints = new List<Task<ReadOnlyCollection<Endpoint>>>();
                        foreach (Service service in servicesResult)
                        {
                            Task<ReadOnlyCollection<Endpoint>> endpoints =
                                CoreTaskExtensions.Using(
                                    () => IdentityService.PrepareListEndpointsAsync(cancellationToken).WithService(service.Id),
                                    innerTask => innerTask.Result.SendAsync(cancellationToken))
                                .Then(innerTask => innerTask.Result.Item2.GetAllPagesAsync(cancellationToken, null));
                            listEndpoints.Add(endpoints);
                        }

                        return Task.Factory.WhenAll(listEndpoints.ToArray())
                            .Select(tasks => tasks.Result.Select((endpoints, index) => Tuple.Create(servicesResult[index], endpoints.Result)).ToList());
                    });

            Task<Uri> finalResult =
                serviceEndpoints.Select(
                    task =>
                    {
                        IList<Tuple<Service, ReadOnlyCollection<Endpoint>>> applicableServices = task.Result;
                        if (serviceName != null)
                        {
                            var namedServices = applicableServices.Where(service => string.Equals(service.Item1.Name, serviceName, StringComparison.OrdinalIgnoreCase));
                            if (namedServices.Any())
                                applicableServices = namedServices.ToList();
                        }

                        IList<Tuple<Service, Endpoint>> endpoints =
                            applicableServices.SelectMany(pair => pair.Item2.Select(endpoint => Tuple.Create(pair.Item1, endpoint))).ToList();

                        string effectiveRegion = GetEffectiveRegion(authenticatedToken, region);

                        IList<Tuple<Service, Endpoint>> regionEndpoints =
                            endpoints.Where(i => string.Equals(i.Item2.Region ?? string.Empty, effectiveRegion ?? string.Empty, StringComparison.OrdinalIgnoreCase)).ToList();

                        if (regionEndpoints.Any())
                            endpoints = regionEndpoints;
                        else
                            endpoints = endpoints.Where(i => string.IsNullOrEmpty(i.Item2.Region)).ToList();

                        if (effectiveRegion == null && !endpoints.Any())
                            throw new NoDefaultRegionSetException("No region was provided, the service does not provide a region-independent endpoint, and there is no default region set for the user's account.");

                        EndpointInterface interfaceType = internalAddress ? EndpointInterface.Internal : EndpointInterface.Public;
                        Tuple<Service, Endpoint> serviceEndpoint = endpoints.FirstOrDefault(i => i.Item2.Interface == interfaceType);
                        if (serviceEndpoint == null)
                            throw new UserAuthorizationException("The user does not have access to the requested service or region.");

                        Uri baseAddress = serviceEndpoint.Item2.Uri;
                        if (baseAddress != null && baseAddress.IsAbsoluteUri && !baseAddress.AbsoluteUri.EndsWith("/"))
                            baseAddress = new Uri(baseAddress.AbsoluteUri + "/", UriKind.Absolute);

                        return baseAddress;
                    });

            return finalResult;
        }

        /// <summary>
        /// Gets the effective region to use for locating services in the service catalog, for the specified
        /// authenticated token and preferred region.
        /// </summary>
        /// <remarks>
        /// The default implementation simply returns <paramref name="region"/>. Specific vendors
        /// may extend this functionality to provide a default value when applicable to their users.
        /// </remarks>
        /// <param name="authenticatedToken">The authenticated token information provided by the Identity Service for the authenticated user.</param>
        /// <param name="region">The preferred region, as specified in the call to <see cref="GetBaseAddressAsync"/>.</param>
        /// <returns>The effective region to use for service location in <see cref="GetBaseAddressAsyncImpl"/>.</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="authenticatedToken"/> is <see langword="null"/>.</exception>
        protected virtual string GetEffectiveRegion(Tuple<TokenId, AuthenticateResponse> authenticatedToken, string region)
        {
            return region;
        }
    }
}
