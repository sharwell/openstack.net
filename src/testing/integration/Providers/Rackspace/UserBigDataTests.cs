namespace Net.OpenStack.Testing.Integration.Providers.Rackspace
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Diagnostics;
    using System.Linq;
    using System.Net;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using net.openstack.Core.Domain;
    using net.openstack.Core.Providers;
    using net.openstack.Providers.Rackspace;
    using net.openstack.Providers.Rackspace.Objects.Hadoop;
    using Newtonsoft.Json;
    using Path = System.IO.Path;

    [TestClass]
    public class UserBigDataTests
    {
        /// <summary>
        /// The prefix to use for names of clusters created during integration testing.
        /// </summary>
        public static readonly string TestClusterPrefix = "UnitTestCluster-";

        [TestMethod]
        [TestCategory(TestCategories.Cleanup)]
        public async Task CleanupClusters()
        {
            IHadoopService provider = CreateProvider();
            using (CancellationTokenSource cancellationTokenSource = new CancellationTokenSource(TestTimeout(TimeSpan.FromSeconds(300))))
            {
                Assert.Inconclusive("Not yet implemented");
            }
        }

        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.BigData)]
        public async Task TestCreateProfile()
        {
            IHadoopService provider = CreateProvider();
            using (CancellationTokenSource cancellationTokenSource = new CancellationTokenSource(TestTimeout(TimeSpan.FromSeconds(300))))
            {
                Assert.Inconclusive("Not yet implemented");
            }
        }

        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.BigData)]
        public async Task TestUpdateProfile()
        {
            IHadoopService provider = CreateProvider();
            using (CancellationTokenSource cancellationTokenSource = new CancellationTokenSource(TestTimeout(TimeSpan.FromSeconds(300))))
            {
                Assert.Inconclusive("Not yet implemented");
            }
        }

        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.BigData)]
        public async Task TestGetProfile()
        {
            IHadoopService provider = CreateProvider();
            using (CancellationTokenSource cancellationTokenSource = new CancellationTokenSource(TestTimeout(TimeSpan.FromSeconds(300))))
            {
                Assert.Inconclusive("Not yet implemented");
            }
        }

        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.BigData)]
        public async Task TestCreateCluster()
        {
            IHadoopService provider = CreateProvider();
            using (CancellationTokenSource cancellationTokenSource = new CancellationTokenSource(TestTimeout(TimeSpan.FromSeconds(300))))
            {
                Assert.Inconclusive("Not yet implemented");
            }
        }

        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.BigData)]
        public async Task TestListClusters()
        {
            IHadoopService provider = CreateProvider();
            using (CancellationTokenSource cancellationTokenSource = new CancellationTokenSource(TestTimeout(TimeSpan.FromSeconds(300))))
            {
                Assert.Inconclusive("Not yet implemented");
            }
        }

        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.BigData)]
        public async Task TestGetCluster()
        {
            IHadoopService provider = CreateProvider();
            using (CancellationTokenSource cancellationTokenSource = new CancellationTokenSource(TestTimeout(TimeSpan.FromSeconds(300))))
            {
                Assert.Inconclusive("Not yet implemented");
            }
        }

        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.BigData)]
        public async Task TestRemoveCluster()
        {
            IHadoopService provider = CreateProvider();
            using (CancellationTokenSource cancellationTokenSource = new CancellationTokenSource(TestTimeout(TimeSpan.FromSeconds(300))))
            {
                Assert.Inconclusive("Not yet implemented");
            }
        }

        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.BigData)]
        public async Task TestResizeCluster()
        {
            IHadoopService provider = CreateProvider();
            using (CancellationTokenSource cancellationTokenSource = new CancellationTokenSource(TestTimeout(TimeSpan.FromSeconds(300))))
            {
                Assert.Inconclusive("Not yet implemented");
            }
        }

        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.BigData)]
        public async Task TestListNodes()
        {
            IHadoopService provider = CreateProvider();
            using (CancellationTokenSource cancellationTokenSource = new CancellationTokenSource(TestTimeout(TimeSpan.FromSeconds(300))))
            {
                Assert.Inconclusive("Not yet implemented");
            }
        }

        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.BigData)]
        public async Task TestGetNode()
        {
            IHadoopService provider = CreateProvider();
            using (CancellationTokenSource cancellationTokenSource = new CancellationTokenSource(TestTimeout(TimeSpan.FromSeconds(300))))
            {
                Assert.Inconclusive("Not yet implemented");
            }
        }

        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.BigData)]
        public async Task TestListFlavors()
        {
            IHadoopService provider = CreateProvider();
            using (CancellationTokenSource cancellationTokenSource = new CancellationTokenSource(TestTimeout(TimeSpan.FromSeconds(300))))
            {
                ReadOnlyCollection<HadoopFlavor> flavors = await provider.ListFlavorsAsync(cancellationTokenSource.Token);
                if (flavors.Count == 0)
                    Assert.Inconclusive("The service did not return any flavors");

                foreach (HadoopFlavor flavor in flavors)
                {
                    Assert.IsNotNull(flavor);
                    Assert.IsNotNull(flavor.Id);
                    Assert.IsNotNull(flavor.Name);
                    Assert.IsNotNull(flavor.Memory);
                    Assert.IsNotNull(flavor.VirtualProcessorCount);
                    Assert.IsNotNull(flavor.Disk);
                    Assert.IsNotNull(flavor.Links);
                    Assert.IsNotNull(flavor.Href);
                }
            }
        }

        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.BigData)]
        public async Task TestGetFlavor()
        {
            IHadoopService provider = CreateProvider();
            using (CancellationTokenSource cancellationTokenSource = new CancellationTokenSource(TestTimeout(TimeSpan.FromSeconds(300))))
            {
                Assert.Inconclusive("Not yet implemented");
            }
        }

        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.BigData)]
        public async Task TestGetSupportedTypes()
        {
            IHadoopService provider = CreateProvider();
            using (CancellationTokenSource cancellationTokenSource = new CancellationTokenSource(TestTimeout(TimeSpan.FromSeconds(300))))
            {
                Assert.Inconclusive("Not yet implemented");
            }
        }

        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.BigData)]
        public async Task TestListClusterTypes()
        {
            IHadoopService provider = CreateProvider();
            using (CancellationTokenSource cancellationTokenSource = new CancellationTokenSource(TestTimeout(TimeSpan.FromSeconds(300))))
            {
                Assert.Inconclusive("Not yet implemented");
            }
        }

        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.BigData)]
        public async Task TestGetClusterType()
        {
            IHadoopService provider = CreateProvider();
            using (CancellationTokenSource cancellationTokenSource = new CancellationTokenSource(TestTimeout(TimeSpan.FromSeconds(300))))
            {
                Assert.Inconclusive("Not yet implemented");
            }
        }

        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.BigData)]
        public async Task TestGetSupportedFlavors()
        {
            IHadoopService provider = CreateProvider();
            using (CancellationTokenSource cancellationTokenSource = new CancellationTokenSource(TestTimeout(TimeSpan.FromSeconds(300))))
            {
                Assert.Inconclusive("Not yet implemented");
            }
        }

        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.BigData)]
        public async Task TestGetResourceLimits()
        {
            IHadoopService provider = CreateProvider();
            using (CancellationTokenSource cancellationTokenSource = new CancellationTokenSource(TestTimeout(TimeSpan.FromSeconds(300))))
            {
                Assert.Inconclusive("Not yet implemented");
            }
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

        /// <summary>
        /// Creates a random cluster name with the proper prefix for integration testing.
        /// </summary>
        /// <returns>A unique, randomly-generated cluster name.</returns>
        internal static string CreateRandomClusterName()
        {
            return TestClusterPrefix + Path.GetRandomFileName();
        }

        /// <summary>
        /// Creates an instance of <see cref="IBigDataService"/> for testing using
        /// the <see cref="OpenstackNetSetings.TestIdentity"/>.
        /// </summary>
        /// <returns>An instance of <see cref="IBigDataService"/> for integration testing.</returns>
        internal static IHadoopService CreateProvider()
        {
            var provider = new TestCloudBigDataProvider(Bootstrapper.Settings.TestIdentity, Bootstrapper.Settings.DefaultRegion, null);
            provider.BeforeAsyncWebRequest +=
                (sender, e) =>
                {
                    Console.Error.WriteLine("{0} (Request) {1} {2}", DateTime.Now, e.Request.Method, e.Request.RequestUri);
                };
            provider.AfterAsyncWebResponse +=
                (sender, e) =>
                {
                    Console.Error.WriteLine("{0} (Result {1}) {2}", DateTime.Now, e.Response.StatusCode, e.Response.ResponseUri);
                };

            provider.ConnectionLimit = 20;
            return provider;
        }

        internal class TestCloudBigDataProvider : CloudBigDataProvider
        {
            public TestCloudBigDataProvider(CloudIdentity defaultIdentity, string defaultRegion, IIdentityProvider identityProvider)
                : base(defaultIdentity, defaultRegion, identityProvider)
            {
            }

            protected override byte[] EncodeRequestBodyImpl<TBody>(HttpWebRequest request, TBody body)
            {
                byte[] encoded = base.EncodeRequestBodyImpl<TBody>(request, body);
                Console.Error.WriteLine("<== " + Encoding.UTF8.GetString(encoded));
                return encoded;
            }

            protected override Tuple<HttpWebResponse, string> ReadResultImpl(Task<WebResponse> task, CancellationToken cancellationToken)
            {
                try
                {
                    Tuple<HttpWebResponse, string> result = base.ReadResultImpl(task, cancellationToken);
                    LogResult(result.Item1, result.Item2, true);
                    return result;
                }
                catch (WebException ex)
                {
                    HttpWebResponse response = ex.Response as HttpWebResponse;
                    if (response != null && response.ContentLength != 0)
                        LogResult(response, ex.Message, true);

                    throw;
                }
            }

            private void LogResult(HttpWebResponse response, string rawBody, bool reformat)
            {
                foreach (string header in response.Headers)
                {
                    Console.Error.WriteLine(string.Format("{0}: {1}", header, response.Headers[header]));
                }

                if (!string.IsNullOrEmpty(rawBody))
                {
                    if (reformat)
                    {
                        object parsed = JsonConvert.DeserializeObject(rawBody);
                        Console.Error.WriteLine("==> " + JsonConvert.SerializeObject(parsed, Formatting.Indented));
                    }
                    else
                    {
                        Console.Error.WriteLine("==> " + rawBody);
                    }
                }
            }
        }
    }
}
