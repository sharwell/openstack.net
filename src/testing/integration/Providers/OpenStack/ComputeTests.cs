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
    using Newtonsoft.Json.Linq;
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
                ReadOnlyCollection<Flavor> flavors = await ListAllFlavorsAsync(client, null, cancellationTokenSource.Token);
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
                ReadOnlyCollection<Flavor> flavors = await ListAllFlavorsAsync(client, 2, cancellationTokenSource.Token);
                Console.WriteLine("Flavors");
                foreach (var flavor in flavors)
                {
                    Console.WriteLine("  {0} ({1})", flavor.Name, flavor.Id);
                }
            }
        }

        private static async Task<ReadOnlyCollection<Flavor>> ListAllFlavorsAsync(IComputeService client, int? pageSize, CancellationToken cancellationToken, IProgress<ReadOnlyCollectionPage<Flavor>> progress = null)
        {
            if (!pageSize.HasValue)
                return await (await client.ListFlavorsAsync(cancellationToken)).GetAllPagesAsync(cancellationToken, progress);

            ListFlavorsApiCall apiCall = await client.PrepareListFlavorsAsync(cancellationToken).WithPageSize(pageSize.Value);
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
