namespace Net.OpenStack.Testing.Integration.Providers.Rackspace
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using net.openstack.Core.Domain;
    using net.openstack.Core.Exceptions.Response;
    using net.openstack.Core.Providers;
    using net.openstack.Providers.Rackspace;
    using net.openstack.Providers.Rackspace.Validators;
    using Path = System.IO.Path;

    /// <summary>
    /// This class contains integration tests for the Rackspace Networks Provider
    /// (Cloud Networks) that can be run with user (non-admin) credentials.
    /// </summary>
    /// <seealso cref="INetworksProvider"/>
    /// <seealso cref="CloudNetworksProvider"/>
    [TestClass]
    public class UserNetworksTests
    {
        /// <summary>
        /// This prefix is used for networks created by unit tests, to avoid
        /// overwriting networks created by other applications.
        /// </summary>
        private const string UnitTestNetworkPrefix = "UnitTestNetwork-";

        [Fact]
        [Trait(TestCategories.User, "")]
        [Trait(TestCategories.Networks, "")]
        public void TestListNetworks()
        {
            INetworksProvider provider = Bootstrapper.CreateNetworksProvider();
            IEnumerable<CloudNetwork> networks = provider.ListNetworks();
            Assert.NotNull(networks);
            if (!networks.Any())
                Assert.False(true, "The test could not proceed because the specified account and/or region does not appear to contain any configured networks.");

            Console.WriteLine("Networks");
            foreach (CloudNetwork network in networks)
            {
                Assert.NotNull(network);

                Console.WriteLine("    {0}: {1} ({2})", network.Id, network.Label, network.Cidr);

                Assert.False(string.IsNullOrEmpty(network.Id));
                Assert.False(string.IsNullOrEmpty(network.Label));

                if (!string.IsNullOrEmpty(network.Cidr))
                    CloudNetworksValidator.Default.ValidateCidr(network.Cidr);
            }
        }

        /// <summary>
        /// This test covers the basic functionality of the <see cref="INetworksProvider.CreateNetwork"/>,
        /// <see cref="INetworksProvider.ShowNetwork"/>, and <see cref="INetworksProvider.DeleteNetwork"/>
        /// methods.
        /// </summary>
        [Fact]
        [Trait(TestCategories.User, "")]
        [Trait(TestCategories.Networks, "")]
        public void TestBasicFunctionality()
        {
            INetworksProvider provider = Bootstrapper.CreateNetworksProvider();
            string networkName = UnitTestNetworkPrefix + Path.GetRandomFileName();
            string cidr = "192.0.2.0/24";

            CloudNetwork network;
            try
            {
                network = provider.CreateNetwork(cidr, networkName);
            }
            catch (BadServiceRequestException ex)
            {
                if (ex.Message == "Quota exceeded, too many networks.")
                    Assert.False(true, "The required test network could not be created due to a quota.");

                throw;
            }

            Assert.NotNull(network);

            CloudNetwork showNetwork = provider.ShowNetwork(network.Id);
            Assert.NotNull(showNetwork);
            Assert.Equal(network.Id, showNetwork.Id);
            Assert.Equal(network.Label, showNetwork.Label);
            Assert.Equal(network.Cidr, showNetwork.Cidr);
        }

        /// <summary>
        /// This unit test deletes all the networks created by the unit tests in this class.
        /// These are identified by the prefix <see cref="UnitTestNetworkPrefix"/>.
        /// </summary>
        [Fact]
        [Trait(TestCategories.Cleanup, "")]
        public void CleanupTestNetworks()
        {
            INetworksProvider provider = Bootstrapper.CreateNetworksProvider();
            IEnumerable<CloudNetwork> networks = provider.ListNetworks();
            Assert.NotNull(networks);

            foreach (CloudNetwork network in networks)
            {
                Assert.NotNull(network);
                if (!network.Label.StartsWith(UnitTestNetworkPrefix))
                    continue;

                Console.WriteLine("Removing network... {0}: {1}", network.Id, network.Label);
                provider.DeleteNetwork(network.Id);
            }
        }
    }
}
