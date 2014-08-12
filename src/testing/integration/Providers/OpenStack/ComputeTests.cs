namespace Net.OpenStack.Testing.Integration.Providers.OpenStack
{
    using System;
    using System.Collections.ObjectModel;
    using System.Diagnostics;
    using System.Threading;
    using System.Threading.Tasks;
    using global::OpenStack.Collections;
    using global::OpenStack.Security.Authentication;
    using global::OpenStack.Services.Compute.V2;
    using global::OpenStack.Services.Identity.V2;
    using global::Rackspace.Security.Authentication;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using TestHelpers = Rackspace.TestHelpers;

    [TestClass]
    public class ComputeTests
    {
        [TestMethod]
        public async Task TestListFlavors()
        {
            using (CancellationTokenSource cancellationTokenSource = new CancellationTokenSource(TestTimeout(TimeSpan.FromSeconds(20))))
            {
                IComputeService client = CreateProvider();
                ReadOnlyCollection<Flavor> flavors = await ListAllFlavorsAsync(client, cancellationTokenSource.Token);
                Console.WriteLine("Flavors");
                foreach (var flavor in flavors)
                {
                    Console.WriteLine("  {0} ({1})", flavor.Name, flavor.Id);
                }
            }
        }

        [TestMethod]
        public async Task TestListFlavorsWithPageSize()
        {
            using (CancellationTokenSource cancellationTokenSource = new CancellationTokenSource(TestTimeout(TimeSpan.FromSeconds(20))))
            {
                IComputeService client = CreateProvider();

                int pageSize = 2;
                Action<ReadOnlyCollectionPage<Flavor>> handler =
                    page => Assert.IsTrue(page.Count <= pageSize);
                IProgress<ReadOnlyCollectionPage<Flavor>> progressValidator = new Progress<ReadOnlyCollectionPage<Flavor>>(handler);

                ReadOnlyCollection<Flavor> flavors = await ListAllFlavorsAsync(client, pageSize, null, cancellationTokenSource.Token, progressValidator);
                Console.WriteLine("Flavors");
                foreach (var flavor in flavors)
                {
                    Console.WriteLine("  {0} ({1})", flavor.Name, flavor.Id);
                }
            }
        }

        [TestMethod]
        public async Task TestListFlavorsWithChangesSince()
        {
            using (CancellationTokenSource cancellationTokenSource = new CancellationTokenSource(TestTimeout(TimeSpan.FromSeconds(20))))
            {
                IComputeService client = CreateProvider();
                ReadOnlyCollection<Flavor> flavors = await ListAllFlavorsAsync(client, null, DateTimeOffset.Now, cancellationTokenSource.Token);
                Console.WriteLine("Flavors");
                foreach (var flavor in flavors)
                {
                    Console.WriteLine("  {0} ({1})", flavor.Name, flavor.Id);
                }

                Assert.AreEqual(0, flavors.Count, "Expected changes-since filter with current date/time to filter out all flavors");
            }
        }

        private static Task<ReadOnlyCollection<Flavor>> ListAllFlavorsAsync(IComputeService client, CancellationToken cancellationToken, IProgress<ReadOnlyCollectionPage<Flavor>> progress = null)
        {
            return ListAllFlavorsAsync(client, null, null, cancellationToken, progress);
        }

        private static async Task<ReadOnlyCollection<Flavor>> ListAllFlavorsAsync(IComputeService client, int? pageSize, DateTimeOffset? changesSince, CancellationToken cancellationToken, IProgress<ReadOnlyCollectionPage<Flavor>> progress = null)
        {
            if (!pageSize.HasValue && !changesSince.HasValue)
                return await (await client.ListFlavorsAsync(cancellationToken)).GetAllPagesAsync(cancellationToken, progress);

            Task<ListFlavorsApiCall> apiCallTask = client.PrepareListFlavorsAsync(cancellationToken);
            if (pageSize.HasValue)
                apiCallTask = apiCallTask.WithPageSize(pageSize.Value);
            if (changesSince.HasValue)
                apiCallTask = apiCallTask.WithChangesSince(changesSince.Value);

            ListFlavorsApiCall apiCall = await apiCallTask;
            return await (await apiCall.SendAsync(cancellationToken)).Item2.GetAllPagesAsync(cancellationToken, progress);
        }

        private TimeSpan TestTimeout(TimeSpan timeout)
        {
            if (Debugger.IsAttached)
            {
                Console.WriteLine("Using extended timeout due to attached debugger.");
                return TimeSpan.FromDays(1);
            }

            return timeout;
        }

        internal static IComputeService CreateProvider()
        {
            var provider = new ComputeClient(CreateAuthenticationService(), Bootstrapper.Settings.DefaultRegion, false);
            provider.BeforeAsyncWebRequest += TestHelpers.HandleBeforeAsyncWebRequest;
            provider.AfterAsyncWebResponse += TestHelpers.HandleAfterAsyncWebRequest;
#if !PORTABLE
            provider.ConnectionLimit = 80;
#endif
            return provider;
        }

        private static Lazy<IAuthenticationService> _testAuthenticationService =
            new Lazy<IAuthenticationService>(() =>
            {
                IdentityClient identityService = new IdentityClient(new Uri("https://identity.api.rackspacecloud.com"));
                identityService.BeforeAsyncWebRequest += TestHelpers.HandleBeforeAsyncWebRequest;
                identityService.AfterAsyncWebResponse += TestHelpers.HandleAfterAsyncWebRequest;

                string username = Bootstrapper.Settings.TestIdentity.Username;
                string apiKey = Bootstrapper.Settings.TestIdentity.APIKey;
                AuthenticationRequest authenticationRequest = RackspaceAuthentication.ApiKey(username, apiKey);
                IAuthenticationService authenticationService = new RackspaceAuthenticationClient(identityService, authenticationRequest);
                return authenticationService;
            }, true);

        internal static IAuthenticationService CreateAuthenticationService()
        {
            return _testAuthenticationService.Value;
        }
    }
}
