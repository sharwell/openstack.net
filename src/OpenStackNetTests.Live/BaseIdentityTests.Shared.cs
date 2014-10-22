namespace OpenStackNetTests.Live
{
    using System;
    using System.Diagnostics;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using OpenStack.Collections;
    using OpenStack.Services.Identity;
    using Rackspace.Services.Identity.V2;
    using Xunit;

    partial class BaseIdentityTests
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
        public async Task TestListApiVersions()
        {
            using (CancellationTokenSource cancellationTokenSource = new CancellationTokenSource())
            {
                cancellationTokenSource.CancelAfter(TestTimeout(TimeSpan.FromSeconds(10)));

                using (IBaseIdentityService service = CreateService())
                {
                    ListApiVersionsApiCall apiCall = await service.PrepareListApiVersionsAsync(cancellationTokenSource.Token);
                    Tuple<HttpResponseMessage, ReadOnlyCollectionPage<ApiVersion>> response = await apiCall.SendAsync(cancellationTokenSource.Token);

                    Assert.NotNull(response);
                    Assert.NotNull(response.Item1);

                    ReadOnlyCollectionPage<ApiVersion> versions = response.Item2;
                    Assert.NotNull(versions);
                    Assert.NotEqual(0, versions.Count);
                    Assert.False(versions.CanHaveNextPage);
                    Assert.False(versions.Contains(null));

                    foreach (ApiVersion version in versions)
                    {
                        Assert.NotNull(version);
                        Assert.NotNull(version.Id);
                        Assert.NotNull(version.LastModified);
                        Assert.NotNull(version.MediaTypes);
                        Assert.NotNull(version.Links);
                        Assert.NotNull(version.Status);

                        Assert.NotEqual(0, version.MediaTypes.Count);
                        foreach (MediaType mediaType in version.MediaTypes)
                        {
                            Assert.NotNull(mediaType);
                            Assert.NotNull(mediaType.Base);
                            Assert.NotNull(mediaType.Type);
                        }

                        Assert.NotEqual(0, version.Links.Count);
                        foreach (Link link in version.Links)
                        {
                            Assert.NotNull(link);
                            Assert.NotNull(link.Target);
                            Assert.NotNull(link.Relation);
                            Assert.True(link.Target.IsAbsoluteUri);
                        }
                    }
                }
            }
        }

        [Fact]
        [Trait(TestCategories.User, "")]
        [Trait(TestCategories.Identity, "")]
        public async Task TestListApiVersionsSimple()
        {
            using (CancellationTokenSource cancellationTokenSource = new CancellationTokenSource())
            {
                cancellationTokenSource.CancelAfter(TestTimeout(TimeSpan.FromSeconds(10)));

                using (IBaseIdentityService service = CreateService())
                {
                    // test using the simple extension method
                    ReadOnlyCollectionPage<ApiVersion> versions = await service.ListApiVersionsAsync(cancellationTokenSource.Token);
                    Assert.NotNull(versions);
                    Assert.NotEqual(0, versions.Count);
                    Assert.False(versions.CanHaveNextPage);
                    Assert.False(versions.Contains(null));
                }
            }
        }

        [Fact]
        [Trait(TestCategories.User, "")]
        [Trait(TestCategories.Identity, "")]
        public async Task TestGetApiVersion2()
        {
            using (CancellationTokenSource cancellationTokenSource = new CancellationTokenSource())
            {
                cancellationTokenSource.CancelAfter(TestTimeout(TimeSpan.FromSeconds(10)));

                using (IBaseIdentityService service = CreateService())
                {
                    ApiVersionId version2 = new ApiVersionId("v2.0");

                    // test using the simple extension method
                    ApiVersion version = await service.GetApiVersionAsync(version2, cancellationTokenSource.Token);
                    Assert.NotNull(version);
                    Assert.Equal(version2, version.Id);
                }
            }
        }

        protected TimeSpan TestTimeout(TimeSpan timeSpan)
        {
            if (Debugger.IsAttached)
                return TimeSpan.FromDays(6);

            return timeSpan;
        }

        protected IBaseIdentityService CreateService()
        {
            BaseIdentityClient client;
            switch (Vendor)
            {
            case "HP":
                // currently HP does not have a vendor-specific IBaseIdentityService
                goto default;

            case "Rackspace":
                client = new RackspaceIdentityClient(BaseAddress);
                break;

            case "OpenStack":
            default:
                client = new BaseIdentityClient(BaseAddress);
                break;
            }

            TestProxy.ConfigureService(client, Proxy);
            client.BeforeAsyncWebRequest += TestHelpers.HandleBeforeAsyncWebRequest;
            client.AfterAsyncWebResponse += TestHelpers.HandleAfterAsyncWebResponse;

            return client;
        }
    }
}
