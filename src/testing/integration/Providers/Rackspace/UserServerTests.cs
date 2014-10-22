namespace Net.OpenStack.Testing.Integration.Providers.Rackspace
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Net;
    using net.openstack.Core.Domain;
    using net.openstack.Core.Exceptions.Response;
    using net.openstack.Core.Providers;
    using net.openstack.Providers.Rackspace;
    using Newtonsoft.Json;
    using Xunit;
    using Path = System.IO.Path;

    /// <summary>
    /// This class contains integration tests for the Rackspace Compute Provider
    /// (Cloud Servers) which are executed against an active server instance and
    /// can be run with user (non-admin) credentials.
    /// </summary>
    /// <seealso cref="IComputeProvider"/>
    /// <seealso cref="CloudServersProvider"/>
    public class UserServerTests : IUseFixture<T>
    {
        private static Server _server;
        private static string _password;

        internal static readonly string TestImageNameSubstring = "CentOS 6.4";

        [ClassInitialize]
        public static void ClassInitialize(TestContext testContext)
        {
            IComputeProvider provider = Bootstrapper.CreateComputeProvider();
            string serverName = UserComputeTests.UnitTestServerPrefix + Path.GetRandomFileName();

            Flavor flavor = UserComputeTests.ListAllFlavorsWithDetails(provider).OrderBy(i => i.RAMInMB).ThenBy(i => i.DiskSizeInGB).FirstOrDefault();
            if (flavor == null)
                Assert.False(true, "Couldn't find a flavor to use for the test server.");

            SimpleServerImage[] images = UserComputeTests.ListAllImages(provider).ToArray();
            SimpleServerImage image = images.FirstOrDefault(i => i.Name.IndexOf(TestImageNameSubstring, StringComparison.OrdinalIgnoreCase) >= 0);
            if (image == null)
                Assert.False(true, string.Format("Couldn't find the {0} image to use for the test server.", TestImageNameSubstring));

            Stopwatch timer = Stopwatch.StartNew();
            Console.Write("Creating server for image {0}...", image.Name);
            NewServer server = provider.CreateServer(serverName, image.Id, flavor.Id, attachToServiceNetwork: true);
            Assert.NotNull(server);
            Assert.False(string.IsNullOrEmpty(server.Id));

            _password = server.AdminPassword;

            _server = provider.WaitForServerActive(server.Id);
            Assert.NotNull(_server);
            Assert.Equal(server.Id, _server.Id);
            Assert.Equal(ServerState.Active, _server.Status);

            Console.WriteLine("done. {0} seconds", timer.Elapsed.TotalSeconds);
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            IComputeProvider provider = Bootstrapper.CreateComputeProvider();

            Stopwatch timer = Stopwatch.StartNew();
            Console.Write("  Deleting...");
            bool deleted = provider.DeleteServer(_server.Id);
            Assert.True(deleted);

            provider.WaitForServerDeleted(_server.Id);
            Console.Write("done. {0} seconds", timer.Elapsed.TotalSeconds);
        }

        [TestInitialize]
        public void TestInitialize()
        {
            IComputeProvider provider = Bootstrapper.CreateComputeProvider();
            Server server = provider.GetDetails(_server.Id);
            if (server.Status != ServerState.Active)
                Assert.False(true, "Could not run test because the server is in the '{0}' state (expected '{1}').", server.Status, ServerState.Active);
        }

        [Fact]
        [Trait(TestCategories.User, "")]
        [Trait(TestCategories.Compute, "")]
        public void TestListServers()
        {
            IComputeProvider provider = Bootstrapper.CreateComputeProvider();
            IEnumerable<SimpleServer> servers = UserComputeTests.ListAllServers(provider);
            Assert.NotNull(servers);
            if (!servers.Any())
                Assert.False(true, "The test could not proceed because the specified account and/or region does not appear to contain any configured servers.");

            Console.WriteLine("Servers");
            foreach (SimpleServer server in servers)
            {
                Assert.NotNull(server);

                Console.WriteLine("    {0}: {1}", server.Id, server.Name);

                Assert.False(string.IsNullOrEmpty(server.Id));
                Assert.False(string.IsNullOrEmpty(server.Name));
            }
        }

        [Fact]
        [Trait(TestCategories.User, "")]
        [Trait(TestCategories.Compute, "")]
        public void TestListServersWithDetails()
        {
            IComputeProvider provider = Bootstrapper.CreateComputeProvider();
            IEnumerable<Server> servers = UserComputeTests.ListAllServersWithDetails(provider);
            Assert.NotNull(servers);
            if (!servers.Any())
                Assert.False(true, "The test could not proceed because the specified account and/or region does not appear to contain any configured servers.");

            Console.WriteLine("Servers (with details)");
            foreach (Server server in servers)
            {
                Assert.NotNull(server);

                Console.WriteLine("    {0}: {1}", server.Id, server.Name);
                Console.WriteLine(JsonConvert.SerializeObject(server, Formatting.Indented));

                Assert.False(string.IsNullOrEmpty(server.Id));
                Assert.False(string.IsNullOrEmpty(server.Name));
            }
        }

        [Fact]
        [Trait(TestCategories.User, "")]
        [Trait(TestCategories.Compute, "")]
        public void TestGetDetails()
        {
            IComputeProvider provider = Bootstrapper.CreateComputeProvider();
            IEnumerable<Server> servers = UserComputeTests.ListAllServersWithDetails(provider);
            Assert.NotNull(servers);
            if (!servers.Any())
                Assert.False(true, "The test could not proceed because the specified account and/or region does not appear to contain any configured servers.");

            foreach (Server server in servers)
            {
                Assert.NotNull(server);
                Server details = provider.GetDetails(server.Id);
                Assert.Equal(server.AccessIPv4, details.AccessIPv4);
                Assert.Equal(server.AccessIPv6, details.AccessIPv6);
                //Assert.Equal(server.Addresses, details.Addresses);
                Assert.Equal(server.Created, details.Created);
                Assert.Equal(server.DiskConfig, details.DiskConfig);
                Assert.Equal(server.Flavor.Id, details.Flavor.Id);
                Assert.Equal(server.HostId, details.HostId);
                Assert.Equal(server.Id, details.Id);
                Assert.Equal(server.Image.Id, details.Image.Id);
                //Assert.Equal(server.Links, details.Links);
                Assert.Equal(server.Name, details.Name);
                Assert.Equal(server.PowerState, details.PowerState);
                //Assert.Equal(server.Progress, details.Progress);
                //Assert.Equal(server.Status, details.Status);
                //Assert.Equal(server.TaskState, details.TaskState);
                Assert.Equal(server.TenantId, details.TenantId);
                Assert.Equal(server.Updated, details.Updated);
                Assert.Equal(server.UserId, details.UserId);
                //Assert.Equal(server.VMState, details.VMState);
            }
        }

        [Fact]
        [Trait(TestCategories.User, "")]
        [Trait(TestCategories.Compute, "")]
        public void TestChangeAdministratorPassword()
        {
            IComputeProvider provider = Bootstrapper.CreateComputeProvider();

            string password = Path.GetTempPath();
            bool changePasswordResult = provider.ChangeAdministratorPassword(_server.Id, password);
            Assert.True(changePasswordResult);
            _password = password;
            Server changePasswordServer = provider.WaitForServerActive(_server.Id);
            Assert.Equal(ServerState.Active, changePasswordServer.Status);
        }

        [Fact]
        [Trait(TestCategories.User, "")]
        [Trait(TestCategories.Compute, "")]
        public void TestHardRebootServer()
        {
            IComputeProvider provider = Bootstrapper.CreateComputeProvider();
            bool rebootResult = provider.RebootServer(_server.Id, RebootType.Hard);
            Assert.True(rebootResult);
            Server rebootServer = provider.WaitForServerActive(_server.Id);
            Assert.Equal(ServerState.Active, rebootServer.Status);
        }

        [Fact]
        [Trait(TestCategories.User, "")]
        [Trait(TestCategories.Compute, "")]
        public void TestSoftRebootServer()
        {
            IComputeProvider provider = Bootstrapper.CreateComputeProvider();
            bool rebootResult = provider.RebootServer(_server.Id, RebootType.Soft);
            Assert.True(rebootResult);
            Server rebootServer = provider.WaitForServerActive(_server.Id);
            Assert.Equal(ServerState.Active, rebootServer.Status);
        }

        [Fact]
        [Trait(TestCategories.User, "")]
        [Trait(TestCategories.Compute, "")]
        public void TestRescueServer()
        {
            IComputeProvider provider = Bootstrapper.CreateComputeProvider();

            string rescueResult = provider.RescueServer(_server.Id);
            Assert.False(string.IsNullOrEmpty(rescueResult));
            Server rescueServer = provider.WaitForServerState(_server.Id, ServerState.Rescue, new[] { ServerState.Active, ServerState.Error, ServerState.Unknown, ServerState.Suspended });
            Assert.Equal(ServerState.Rescue, rescueServer.Status);

            bool unrescueResult = provider.UnRescueServer(_server.Id);
            Assert.True(unrescueResult);
            Server unrescueServer = provider.WaitForServerActive(_server.Id);
            Assert.Equal(ServerState.Active, unrescueServer.Status);
        }

        [Fact]
        [Trait(TestCategories.User, "")]
        [Trait(TestCategories.Compute, "")]
        public void TestUpdateServer()
        {
            IComputeProvider provider = Bootstrapper.CreateComputeProvider();

            string newName = UserComputeTests.UnitTestServerPrefix + Path.GetRandomFileName() + "²";
            bool updated = provider.UpdateServer(_server.Id, name: newName);
            Assert.True(updated);
            Server updatedServer = provider.GetDetails(_server.Id);
            Assert.Equal(_server.Id, updatedServer.Id);
            Assert.Equal(newName, updatedServer.Name);
            Assert.NotEqual(_server.Name, updatedServer.Name);
            _server = updatedServer;
        }

        [Fact]
        [Trait(TestCategories.User, "")]
        [Trait(TestCategories.Compute, "")]
        public void TestListAddresses()
        {
            IComputeProvider provider = Bootstrapper.CreateComputeProvider();
            ServerAddresses serverAddresses = provider.ListAddresses(_server.Id);
            if (serverAddresses.Count == 0)
                Assert.False(true, "Couldn't find any addresses listed for the server.");

            bool foundAddress = false;
            foreach (KeyValuePair<string, IPAddressList> addresses in serverAddresses)
            {
                Console.WriteLine("Network: {0}", addresses.Key);
                foreach (IPAddress address in addresses.Value)
                {
                    foundAddress = true;
                    Console.WriteLine("  {0}", address);
                }
            }

            if (!foundAddress)
                Assert.False(true, "Couldn't find addresses on any network for the server.");
        }

        [Fact]
        [Trait(TestCategories.User, "")]
        [Trait(TestCategories.Compute, "")]
        public void TestListAddressesByNetwork()
        {
            IComputeProvider provider = Bootstrapper.CreateComputeProvider();
            INetworksProvider networksProvider = Bootstrapper.CreateNetworksProvider();
            IEnumerable<CloudNetwork> networks = networksProvider.ListNetworks();

            bool foundAddress = false;
            foreach (CloudNetwork network in networks)
            {
                Console.WriteLine("Network: {0}", network.Label);
                IEnumerable<IPAddress> addresses = provider.ListAddressesByNetwork(_server.Id, network.Label);
                bool foundAddressOnNetwork = false;
                foreach (IPAddress address in addresses)
                {
                    foundAddress = true;
                    foundAddressOnNetwork = true;
                    Console.WriteLine("  {0}", address);
                }

                if (!foundAddressOnNetwork)
                    Console.WriteLine("  Server is not attached to this network.");
            }

            if (!foundAddress)
                Assert.False(true, "Couldn't find addresses on any network for the server.");
        }

        [Fact]
        [Trait(TestCategories.User, "")]
        [Trait(TestCategories.Compute, "")]
        public void TestRebuildServer()
        {
            IComputeProvider provider = Bootstrapper.CreateComputeProvider();

            Flavor flavor = UserComputeTests.ListAllFlavorsWithDetails(provider).OrderBy(i => i.RAMInMB).ThenBy(i => i.DiskSizeInGB).FirstOrDefault();
            if (flavor == null)
                Assert.False(true, "Couldn't find a flavor to use for the test server.");

            SimpleServerImage[] images = UserComputeTests.ListAllImages(provider).ToArray();
            SimpleServerImage image = images.FirstOrDefault(i => i.Name.IndexOf(TestImageNameSubstring, StringComparison.OrdinalIgnoreCase) >= 0);
            if (image == null)
                Assert.False(true, string.Format("Couldn't find the {0} image to use for the test server.", TestImageNameSubstring));

            Server rebuilt = provider.RebuildServer(_server.Id, null, image.Id, flavor.Id, _password);
            Assert.NotNull(rebuilt);
            Server rebuiltServer = provider.WaitForServerActive(rebuilt.Id);
            Assert.Equal(ServerState.Active, rebuiltServer.Status);
            _server = rebuiltServer;
        }

        [Fact]
        [Trait(TestCategories.User, "")]
        [Trait(TestCategories.Compute, "")]
        public void TestConfirmServerResize()
        {
            IComputeProvider provider = Bootstrapper.CreateComputeProvider();

            string serverName = UserComputeTests.UnitTestServerPrefix + Path.GetRandomFileName();

            Flavor flavor = UserComputeTests.ListAllFlavorsWithDetails(provider).OrderBy(i => i.RAMInMB).ThenBy(i => i.DiskSizeInGB).FirstOrDefault(i => !i.Id.Equals(_server.Flavor.Id, StringComparison.OrdinalIgnoreCase));
            if (flavor == null)
                Assert.False(true, "Couldn't find a flavor to use for the test server.");

            bool resized = provider.ResizeServer(_server.Id, serverName, flavor.Id);
            Assert.True(resized);
            Server resizedServer = provider.WaitForServerState(_server.Id, ServerState.VerifyResize, new[] { ServerState.Active, ServerState.Error, ServerState.Unknown, ServerState.Suspended });
            Assert.Equal(ServerState.VerifyResize, resizedServer.Status);
            _server = resizedServer;

            bool confirmed = provider.ConfirmServerResize(resizedServer.Id);
            Assert.True(confirmed);
            Server confirmedServer = provider.WaitForServerActive(_server.Id);
            Assert.Equal(ServerState.Active, confirmedServer.Status);
            _server = confirmedServer;
        }

        [Fact]
        [Trait(TestCategories.User, "")]
        [Trait(TestCategories.Compute, "")]
        public void TestRevertServerResize()
        {
            IComputeProvider provider = Bootstrapper.CreateComputeProvider();

            string serverName = UserComputeTests.UnitTestServerPrefix + Path.GetRandomFileName();

            Flavor flavor = UserComputeTests.ListAllFlavorsWithDetails(provider).OrderBy(i => i.RAMInMB).ThenBy(i => i.DiskSizeInGB).FirstOrDefault(i => !i.Id.Equals(_server.Flavor.Id, StringComparison.OrdinalIgnoreCase));
            if (flavor == null)
                Assert.False(true, "Couldn't find a flavor to use for the test server.");

            bool resized = provider.ResizeServer(_server.Id, serverName, flavor.Id);
            Assert.True(resized);
            Server resizedServer = provider.WaitForServerState(_server.Id, ServerState.VerifyResize, new[] { ServerState.Active, ServerState.Error, ServerState.Unknown, ServerState.Suspended });
            Assert.Equal(ServerState.VerifyResize, resizedServer.Status);
            _server = resizedServer;

            bool reverted = provider.RevertServerResize(resizedServer.Id);
            Assert.True(reverted);
            Server revertedServer = provider.WaitForServerActive(_server.Id);
            Assert.Equal(ServerState.Active, revertedServer.Status);
            _server = revertedServer;
        }

        [Fact]
        [Trait(TestCategories.User, "")]
        [Trait(TestCategories.Compute, "")]
        public void TestCreateImage()
        {
            IComputeProvider provider = Bootstrapper.CreateComputeProvider();

            /* Create the image
             */
            string imageName = UserComputeTests.UnitTestImagePrefix + Path.GetRandomFileName();
            bool imaged = provider.CreateImage(_server.Id, imageName);
            Assert.True(imaged);
            ServerImage[] images = provider.ListImagesWithDetails(server: _server.Id, imageName: imageName).ToArray();
            Assert.NotNull(images);
            Assert.Equal(1, images.Length);

            ServerImage image = images[0];
            Assert.Equal(imageName, image.Name);
            Assert.False(string.IsNullOrEmpty(image.Id));

            Assert.Equal(ImageState.Active, provider.WaitForImageActive(image.Id).Status);

            /* Test metadata operations on the image
             */
            Assert.True(provider.SetImageMetadataItem(image.Id, "Item 1", "Value"));
            Assert.Equal("Value", provider.GetImageMetadataItem(image.Id, "Item 1"));
            Assert.True(provider.SetImageMetadataItem(image.Id, "Item 2", "Value ²"));
            Assert.Equal("Value ²", provider.GetImageMetadataItem(image.Id, "Item 2"));

            // setting the same key overwrites the previous value
            Assert.True(provider.SetImageMetadataItem(image.Id, "Item 1", "Value 1"));
            Assert.Equal("Value 1", provider.GetImageMetadataItem(image.Id, "Item 1"));

            Assert.True(provider.DeleteImageMetadataItem(image.Id, "Item 1"));
            Assert.False(provider.ListImageMetadata(image.Id).ContainsKey("Item 1"));

            Metadata metadata = new Metadata()
            {
                { "Different", "Variables" },
            };

            Assert.True(provider.UpdateImageMetadata(image.Id, metadata));
            Metadata actual = provider.ListImageMetadata(image.Id);
            Assert.NotNull(actual);
            Assert.Equal("Value ²", actual["Item 2"]);
            Assert.Equal("Variables", actual["Different"]);

            // a slight tweak
            metadata["Different"] = "Values";
            Assert.True(provider.SetImageMetadata(image.Id, metadata));
            actual = provider.ListImageMetadata(image.Id);
            Assert.NotNull(actual);
            Assert.Equal(1, actual.Count);
            Assert.Equal("Values", actual["Different"]);

            Assert.True(provider.SetImageMetadata(image.Id, new Metadata()));
            Assert.Equal(0, provider.ListImageMetadata(image.Id).Count);

            /* Cleanup
             */
            bool deleted = provider.DeleteImage(images[0].Id);
            Assert.True(deleted);
            provider.WaitForImageDeleted(images[0].Id);
        }

        [Fact]
        [Trait(TestCategories.User, "")]
        [Trait(TestCategories.Compute, "")]
        public void TestAttachServerVolume()
        {
            IComputeProvider provider = Bootstrapper.CreateComputeProvider();

            IBlockStorageProvider blockStorageProvider = Bootstrapper.CreateBlockStorageProvider();
            VolumeType volumeType = UserBlockStorageTests.GetSsdVolumeTypeOrDefault(blockStorageProvider);
            string volumeName = UserBlockStorageTests.UnitTestVolumePrefix + Path.GetRandomFileName();
            Volume volume = blockStorageProvider.CreateVolume(UserBlockStorageTests.MinimumVolumeSize, displayName: volumeName, volumeType: volumeType != null ? volumeType.Id : null);
            Assert.Equal(VolumeState.Available, blockStorageProvider.WaitForVolumeAvailable(volume.Id).Status);

            /* AttachServerVolume
             */
            ServerVolume serverVolume = provider.AttachServerVolume(_server.Id, volume.Id);
            Assert.NotNull(serverVolume);
            Assert.False(string.IsNullOrEmpty(serverVolume.Id));
            Assert.Equal(_server.Id, serverVolume.ServerId);
            Assert.Equal(volume.Id, serverVolume.VolumeId);

            Assert.Equal(VolumeState.InUse, blockStorageProvider.WaitForVolumeState(volume.Id, VolumeState.InUse, new[] { VolumeState.Available, VolumeState.Error }).Status);

            /* ListServerVolumes
             */
            ServerVolume[] serverVolumes = provider.ListServerVolumes(_server.Id).ToArray();
            Assert.NotNull(serverVolumes);
            Assert.Equal(1, serverVolumes.Length);
            Assert.Equal(serverVolume.Id, serverVolumes[0].Id);
            Assert.Equal(serverVolume.ServerId, serverVolumes[0].ServerId);
            Assert.Equal(serverVolume.VolumeId, serverVolumes[0].VolumeId);

            /* GetServerVolumeDetails
             */
            ServerVolume volumeDetails = provider.GetServerVolumeDetails(_server.Id, volume.Id);
            Assert.NotNull(volumeDetails);
            Assert.Equal(serverVolume.Id, volumeDetails.Id);
            Assert.Equal(serverVolume.ServerId, volumeDetails.ServerId);
            Assert.Equal(serverVolume.VolumeId, volumeDetails.VolumeId);

            bool detach = provider.DetachServerVolume(_server.Id, volume.Id);
            Assert.True(detach);
            provider.WaitForServerActive(_server.Id);
            ServerVolume[] remainingVolumes = provider.ListServerVolumes(_server.Id).ToArray();
            Assert.Equal(0, remainingVolumes.Length);

            Assert.Equal(VolumeState.Available, blockStorageProvider.WaitForVolumeAvailable(volume.Id).Status);
            bool deleted = blockStorageProvider.DeleteVolume(volume.Id);
            Assert.True(blockStorageProvider.WaitForVolumeDeleted(volume.Id));
        }

        [Fact]
        [Trait(TestCategories.User, "")]
        [Trait(TestCategories.Compute, "")]
        public void TestVirtualInterfaces()
        {
            IComputeProvider provider = Bootstrapper.CreateComputeProvider();
            INetworksProvider networksProvider = Bootstrapper.CreateNetworksProvider();
            CloudNetwork publicNetwork = networksProvider.ListNetworks().Single(i => i.Label.Equals("public", StringComparison.OrdinalIgnoreCase));

            VirtualInterface publicVirtualInterface = provider.CreateVirtualInterface(_server.Id, publicNetwork.Id);
            Assert.NotNull(publicVirtualInterface);
            Assert.False(string.IsNullOrEmpty(publicVirtualInterface.Id));
            Assert.NotNull(publicVirtualInterface.MACAddress);

            IEnumerable<VirtualInterface> virtualInterfaces = provider.ListVirtualInterfaces(_server.Id);
            Assert.NotNull(virtualInterfaces);
            Assert.True(virtualInterfaces.Where(i => i.Id.Equals(publicVirtualInterface.Id, StringComparison.OrdinalIgnoreCase)).Any());

            bool deleted;
            deleted = provider.DeleteVirtualInterface(_server.Id, publicVirtualInterface.Id);
            Assert.True(deleted);
        }

        [Fact]
        [Trait(TestCategories.User, "")]
        [Trait(TestCategories.Compute, "")]
        public void TestServerMetadata()
        {
            IComputeProvider provider = Bootstrapper.CreateComputeProvider();

            Metadata initialMetadata = provider.ListServerMetadata(_server.Id);
            if (initialMetadata.Count > 0)
            {
                Console.WriteLine("Actual metadata");
                foreach (KeyValuePair<string, string> meta in initialMetadata)
                    Console.WriteLine("  {0}: {1}", meta.Key, meta.Value);

                Assert.False(true, "Expected the server to not have any initial metadata.");
            }

            Assert.True(provider.SetServerMetadataItem(_server.Id, "Item 1", "Value"));
            Assert.Equal("Value", provider.GetServerMetadataItem(_server.Id, "Item 1"));
            Assert.True(provider.SetServerMetadataItem(_server.Id, "Item 2", "Value ²"));
            Assert.Equal("Value ²", provider.GetServerMetadataItem(_server.Id, "Item 2"));

            // setting the same key overwrites the previous value
            Assert.True(provider.SetServerMetadataItem(_server.Id, "Item 1", "Value 1"));
            Assert.Equal("Value 1", provider.GetServerMetadataItem(_server.Id, "Item 1"));

            Assert.True(provider.DeleteServerMetadataItem(_server.Id, "Item 1"));
            Assert.False(provider.ListServerMetadata(_server.Id).ContainsKey("Item 1"));

            Metadata metadata = new Metadata()
            {
                { "Different", "Variables" },
            };

            Assert.True(provider.UpdateServerMetadata(_server.Id, metadata));
            Metadata actual = provider.ListServerMetadata(_server.Id);
            Assert.NotNull(actual);
            Assert.Equal(2, actual.Count);
            Assert.Equal("Value ²", actual["Item 2"]);
            Assert.Equal("Variables", actual["Different"]);

            // a slight tweak
            metadata["Different"] = "Values";
            Assert.True(provider.SetServerMetadata(_server.Id, metadata));
            actual = provider.ListServerMetadata(_server.Id);
            Assert.NotNull(actual);
            Assert.Equal(1, actual.Count);
            Assert.Equal("Values", actual["Different"]);

            Assert.True(provider.SetServerMetadata(_server.Id, new Metadata()));
            Assert.Equal(0, provider.ListServerMetadata(_server.Id).Count);
        }
    }
}
