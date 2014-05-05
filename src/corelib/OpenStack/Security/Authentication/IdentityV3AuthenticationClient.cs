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

        private Tuple<TokenId, AuthenticateResponse> _userAccess;

        public IdentityV3AuthenticationClient(IIdentityService identityService, AuthenticateRequest authenticateRequest)
        {
            if (identityService == null)
                throw new ArgumentNullException("identityService");
            if (authenticateRequest == null)
                throw new ArgumentNullException("authenticateRequest");

            _identityService = identityService;
            _authenticateRequest = authenticateRequest;
        }

        protected IIdentityService IdentityService
        {
            get
            {
                return _identityService;
            }
        }

        protected AuthenticateRequest AuthenticateRequest
        {
            get
            {
                return _authenticateRequest;
            }
        }

        #region IAuthenticationService Members

        public virtual Task<Uri> GetBaseAddressAsync(string serviceType, string serviceName, string region, bool internalAddress, CancellationToken cancellationToken)
        {
            return
                AuthenticateAsync(cancellationToken)
                .Then(task => GetBaseAddressAsyncImpl(task.Result, serviceType, serviceName, region, internalAddress, cancellationToken));
        }

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

        protected virtual Task<Tuple<TokenId, AuthenticateResponse>> AuthenticateAsync(CancellationToken cancellationToken)
        {
            if (_userAccess != null && _userAccess.Item2 != null && _userAccess.Item2.Token != null && _userAccess.Item2.Token.ExpiresAt > DateTimeOffset.Now + TimeSpan.FromMinutes(5))
                return CompletedTask.FromResult(_userAccess);

            return
                CoreTaskExtensions.Using(
                    () => IdentityService.PrepareAuthenticateAsync(AuthenticateRequest, cancellationToken),
                    task => task.Result.SendAsync(cancellationToken))
                .Select(
                    task =>
                    {
                        _userAccess = task.Result.Item2;
                        return task.Result.Item2;
                    });
        }

        protected virtual Task<Uri> GetBaseAddressAsyncImpl(Tuple<TokenId, AuthenticateResponse> userAccess, string serviceType, string serviceName, string region, bool internalAddress, CancellationToken cancellationToken)
        {
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

                        string effectiveRegion = GetEffectiveRegion(userAccess, region);

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

        protected virtual string GetEffectiveRegion(Tuple<TokenId, AuthenticateResponse> userAccess, string region)
        {
            return region;
        }
    }
}
