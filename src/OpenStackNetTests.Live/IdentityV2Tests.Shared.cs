namespace OpenStackNetTests.Live
{
    using System;
    using System.Collections.ObjectModel;
    using System.Diagnostics;
    using System.Linq;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using Newtonsoft.Json;
    using OpenStack.Collections;
    using OpenStack.Security.Authentication;
    using OpenStack.Services.Identity;
    using OpenStack.Services.Identity.V2;
    using Rackspace.Security.Authentication;
    using Rackspace.Services.Identity.V2;
    using Xunit;

    partial class IdentityV2Tests
    {
        protected Uri BaseAddress
        {
            get
            {
                TestCredentials credentials = Credentials;
                if (credentials == null)
                    return null;

                return credentials.BaseAddress;
            }
        }

        protected TestProxy Proxy
        {
            get
            {
                TestCredentials credentials = Credentials;
                if (credentials == null)
                    return null;

                return credentials.Proxy;
            }
        }

        protected string Vendor
        {
            get
            {
                TestCredentials credentials = Credentials;
                if (credentials == null)
                    return "OpenStack";

                return credentials.Vendor;
            }
        }

        [Fact]
        [Trait(TestCategories.User, "")]
        [Trait(TestCategories.Identity, "")]
        public async Task TestListExtensions()
        {
            using (CancellationTokenSource cancellationTokenSource = new CancellationTokenSource())
            {
                cancellationTokenSource.CancelAfter(TestTimeout(TimeSpan.FromSeconds(10)));

                using (IIdentityService service = CreateService())
                {
                    ListExtensionsApiCall apiCall = await service.PrepareListExtensionsAsync(cancellationTokenSource.Token);
                    Tuple<HttpResponseMessage, ReadOnlyCollectionPage<Extension>> response = await apiCall.SendAsync(cancellationTokenSource.Token);

                    Assert.NotNull(response);
                    Assert.NotNull(response.Item2);

                    ReadOnlyCollectionPage<Extension> extensions = response.Item2;
                    Assert.NotNull(extensions);
                    Assert.NotEqual(0, extensions.Count);
                    Assert.False(extensions.CanHaveNextPage);

                    foreach (Extension extension in extensions)
                        CheckExtension(extension);
                }
            }
        }

        [Fact]
        [Trait(TestCategories.User, "")]
        [Trait(TestCategories.Identity, "")]
        public async Task TestListExtensionsSimple()
        {
            using (CancellationTokenSource cancellationTokenSource = new CancellationTokenSource())
            {
                cancellationTokenSource.CancelAfter(TestTimeout(TimeSpan.FromSeconds(10)));

                using (IIdentityService service = CreateService())
                {
                    ReadOnlyCollectionPage<Extension> extensions = await service.ListExtensionsAsync(cancellationTokenSource.Token);
                    Assert.NotNull(extensions);
                    Assert.NotEqual(0, extensions.Count);
                    Assert.False(extensions.CanHaveNextPage);

                    foreach (Extension extension in extensions)
                        CheckExtension(extension);
                }
            }
        }

        [Fact]
        [Trait(TestCategories.User, "")]
        [Trait(TestCategories.Identity, "")]
        public async Task TestGetExtension()
        {
            using (CancellationTokenSource cancellationTokenSource = new CancellationTokenSource())
            {
                cancellationTokenSource.CancelAfter(TestTimeout(TimeSpan.FromSeconds(10)));

                using (IIdentityService service = CreateService())
                {
                    ListExtensionsApiCall apiCall = await service.PrepareListExtensionsAsync(cancellationTokenSource.Token);
                    Tuple<HttpResponseMessage, ReadOnlyCollectionPage<Extension>> response = await apiCall.SendAsync(cancellationTokenSource.Token);

                    Assert.NotNull(response);
                    Assert.NotNull(response.Item2);

                    ReadOnlyCollectionPage<Extension> extensions = response.Item2;
                    Assert.NotNull(extensions);
                    Assert.NotEqual(0, extensions.Count);
                    Assert.False(extensions.CanHaveNextPage);

                    foreach (Extension listedExtension in extensions)
                    {
                        Assert.NotNull(listedExtension);
                        Assert.NotNull(listedExtension.Alias);

                        GetExtensionApiCall getApiCall = await service.PrepareGetExtensionAsync(listedExtension.Alias, cancellationTokenSource.Token);
                        Tuple<HttpResponseMessage, ExtensionResponse> getResponse = await getApiCall.SendAsync(cancellationTokenSource.Token);

                        Extension extension = getResponse.Item2.Extension;
                        CheckExtension(extension);
                        Assert.Equal(listedExtension.Alias, extension.Alias);
                    }
                }
            }
        }

        [Fact]
        [Trait(TestCategories.User, "")]
        [Trait(TestCategories.Identity, "")]
        public async Task TestGetExtensionSimple()
        {
            using (CancellationTokenSource cancellationTokenSource = new CancellationTokenSource())
            {
                cancellationTokenSource.CancelAfter(TestTimeout(TimeSpan.FromSeconds(10)));

                using (IIdentityService service = CreateService())
                {
                    ReadOnlyCollectionPage<Extension> extensions = await service.ListExtensionsAsync(cancellationTokenSource.Token);

                    Assert.NotNull(extensions);
                    Assert.NotEqual(0, extensions.Count);
                    Assert.False(extensions.CanHaveNextPage);

                    foreach (Extension listedExtension in extensions)
                    {
                        Assert.NotNull(listedExtension);
                        Assert.NotNull(listedExtension.Alias);

                        Extension extension = await service.GetExtensionAsync(listedExtension.Alias, cancellationTokenSource.Token);
                        CheckExtension(extension);
                        Assert.Equal(listedExtension.Alias, extension.Alias);
                    }
                }
            }
        }

        [Fact]
        [Trait(TestCategories.User, "")]
        [Trait(TestCategories.Identity, "")]
        public async Task TestAuthenticate()
        {
            using (CancellationTokenSource cancellationTokenSource = new CancellationTokenSource())
            {
                cancellationTokenSource.CancelAfter(TestTimeout(TimeSpan.FromSeconds(10)));

                using (IIdentityService service = CreateService())
                {
                    TestCredentials credentials = Credentials;
                    Assert.NotNull(credentials);

                    AuthenticationRequest request = credentials.AuthenticationRequest;
                    Assert.NotNull(request);

                    AuthenticateApiCall apiCall = await service.PrepareAuthenticateAsync(request, cancellationTokenSource.Token);
                    Tuple<HttpResponseMessage, AccessResponse> response = await apiCall.SendAsync(cancellationTokenSource.Token);

                    Assert.NotNull(response);
                    Assert.NotNull(response.Item2);

                    Access access = response.Item2.Access;
                    Assert.NotNull(access);
                    Assert.NotNull(access.Token);
                    Assert.NotNull(access.ServiceCatalog);
                    Assert.NotNull(access.User);

                    // check the token
                    Token token = access.Token;
                    Assert.NotNull(token);
                    Assert.NotNull(token.Id);
                    Assert.NotNull(token.Tenant);

                    // Rackspace does not return this property, and it doesn't seem to be particularly useful.
                    //Assert.NotNull(token.IssuedAt);

                    Assert.NotNull(token.ExpiresAt);

                    // check the user
                    User user = access.User;
                    Assert.NotNull(user);
                    Assert.NotNull(user.Id);
                    Assert.NotNull(user.Name);

                    // If the Username is null, it's presumed to be the same as the Name. (Rackspace does not return
                    // this property.)
                    //Assert.NotNull(user.Username);

                    Assert.NotNull(user.Roles);

                    Assert.NotEqual(0, user.Roles.Count);
                    foreach (Role role in user.Roles)
                    {
                        Assert.NotNull(role);
                        Assert.NotNull(role.Name);
                        Assert.NotEqual(string.Empty, role.Name);
                    }

                    // At least Rackspace does not return the roles_links property.
                    if (user.RolesLinks != null)
                    {
                        foreach (Link link in user.RolesLinks)
                        {
                            Assert.NotNull(link);
                            Assert.NotNull(link.Relation);
                            Assert.NotEqual(string.Empty, link.Relation);
                            Assert.NotNull(link.Target);
                            Assert.True(link.Target.IsAbsoluteUri);
                        }
                    }

                    // check the service catalog
                    ReadOnlyCollection<ServiceCatalogEntry> serviceCatalog = access.ServiceCatalog;
                    Assert.NotNull(serviceCatalog);
                    Assert.NotEqual(0, serviceCatalog.Count);
                    foreach (ServiceCatalogEntry entry in serviceCatalog)
                    {
                        Assert.NotNull(entry);
                        Assert.NotNull(entry.Name);
                        Assert.NotNull(entry.Type);
                        Assert.NotNull(entry.Endpoints);

                        Assert.NotEqual(0, entry.Endpoints.Count);
                        foreach (Endpoint endpoint in entry.Endpoints)
                        {
                            Assert.NotNull(endpoint);

                            // At least Rackspace does not return an ID with their endpoints. The ID doesn't seem
                            // necessary for API calls.
                            //Assert.NotNull(endpoint.Id);

                            // Region-independent endpoints may have a null Region value.
                            //Assert.NotNull(endpoint.Region);

                            Assert.False(endpoint.PublicUrl == null && endpoint.InternalUrl == null && endpoint.AdminUrl == null);
                        }

                        // At least Rackspace does not return the endpoints_links property.
                        if (entry.EndpointsLinks != null)
                        {
                            foreach (Link link in entry.EndpointsLinks)
                            {
                                Assert.NotNull(link);
                                Assert.NotNull(link.Relation);
                                Assert.NotEqual(string.Empty, link.Relation);
                                Assert.NotNull(link.Target);
                                Assert.True(link.Target.IsAbsoluteUri);
                            }
                        }
                    }
                }
            }
        }

        [Fact]
        [Trait(TestCategories.User, "")]
        [Trait(TestCategories.Identity, "")]
        public async Task TestListTenants()
        {
            using (CancellationTokenSource cancellationTokenSource = new CancellationTokenSource())
            {
                cancellationTokenSource.CancelAfter(TestTimeout(TimeSpan.FromSeconds(10)));

                TestCredentials credentials = Credentials;
                Assert.NotNull(credentials);

                AuthenticationRequest request = credentials.AuthenticationRequest;
                Assert.NotNull(request);

                IdentityV2AuthenticationService authenticationService = new IdentityV2AuthenticationService(CreateService(), request);

                using (IIdentityService service = CreateService(authenticationService))
                {
                    ListTenantsApiCall apiCall = await service.PrepareListTenantsAsync(cancellationTokenSource.Token);
                    Tuple<HttpResponseMessage, ReadOnlyCollectionPage<Tenant>> response = await apiCall.SendAsync(cancellationTokenSource.Token);

                    Assert.NotNull(response);
                    Assert.NotNull(response.Item2);

                    ReadOnlyCollectionPage<Tenant> tenants = response.Item2;
                    Assert.NotNull(tenants);
                    Assert.NotEqual(0, tenants.Count);
                    Assert.False(tenants.CanHaveNextPage);

                    foreach (Tenant tenant in tenants)
                        CheckTenant(tenant);

                    Assert.True(tenants.Any(i => i.Enabled ?? false));
                }
            }
        }

        protected void CheckExtension(Extension extension)
        {
            Assert.NotNull(extension);
            Assert.NotNull(extension.Alias);
            Assert.NotNull(extension.Description);
            Assert.NotNull(extension.Name);
            Assert.NotNull(extension.Namespace);
            try
            {
                Assert.NotNull(extension.LastModified);
            }
            catch (JsonException)
            {
                // OpenStack is known to return an unrecognized (and undefined) timestamp format for this
                // property...
            }
        }

        protected void CheckTenant(Tenant tenant)
        {
            Assert.NotNull(tenant);
            Assert.NotNull(tenant.Id);
            Assert.NotNull(tenant.Name);
            Assert.False(string.IsNullOrEmpty(tenant.Name));

            // Caller should check for at least one enabled Tenant
            //Assert.Equal(true, tenant.Enabled);

            // At least Rackspace does not send back this property.
            //Assert.NotNull(tenant.Description);
            //Assert.False(string.IsNullOrEmpty(tenant.Description));
        }

        protected TimeSpan TestTimeout(TimeSpan timeSpan)
        {
            if (Debugger.IsAttached)
                return TimeSpan.FromDays(6);

            return timeSpan;
        }

        internal static IIdentityService CreateService(TestCredentials credentials)
        {
            if (credentials == null)
                throw new ArgumentNullException("credentials");

            IdentityClient client;
            switch (credentials.Vendor)
            {
            case "HP":
                // currently HP does not have a vendor-specific IIdentityService
                goto default;

            case "Rackspace":
                client = new RackspaceIdentityClient(credentials.BaseAddress);
                break;

            case "OpenStack":
            default:
                client = new IdentityClient(credentials.BaseAddress);
                break;
            }

            TestProxy.ConfigureService(client, credentials.Proxy);
            client.BeforeAsyncWebRequest += TestHelpers.HandleBeforeAsyncWebRequest;
            client.AfterAsyncWebResponse += TestHelpers.HandleAfterAsyncWebResponse;

            return client;
        }

        internal static IAuthenticationService CreateAuthenticationService(TestCredentials credentials)
        {
            if (credentials == null)
                throw new ArgumentNullException("credentials");

            IIdentityService identityService = CreateService(credentials);
            IAuthenticationService authenticationService;
            switch (credentials.Vendor)
            {
            case "HP":
                // currently HP does not have a vendor-specific IIdentityService
                goto default;

            case "Rackspace":
                authenticationService = new RackspaceAuthenticationService(identityService, credentials.AuthenticationRequest);
                break;

            case "OpenStack":
            default:
                authenticationService = new IdentityV2AuthenticationService(identityService, credentials.AuthenticationRequest);
                break;
            }

            return authenticationService;
        }

        protected IIdentityService CreateService()
        {
            return CreateService(Credentials);
        }

        protected IIdentityService CreateService(IAuthenticationService authenticationService)
        {
            IdentityClient client;
            switch (Vendor)
            {
            case "HP":
                // currently HP does not have a vendor-specific IIdentityService
                goto default;

            case "Rackspace":
                client = new RackspaceIdentityClient(authenticationService, BaseAddress);
                break;

            case "OpenStack":
            default:
                client = new IdentityClient(authenticationService, BaseAddress);
                break;
            }

            TestProxy.ConfigureService(client, Proxy);
            client.BeforeAsyncWebRequest += TestHelpers.HandleBeforeAsyncWebRequest;
            client.AfterAsyncWebResponse += TestHelpers.HandleAfterAsyncWebResponse;

            return client;
        }
    }
}
