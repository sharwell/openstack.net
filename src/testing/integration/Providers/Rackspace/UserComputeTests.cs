namespace Net.OpenStack.Testing.Integration.Providers.Rackspace
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using net.openstack.Core.Domain;
    using net.openstack.Core.Providers;
    using net.openstack.Providers.Rackspace;
    using Newtonsoft.Json;
    using Xunit;


    /// <summary>
    /// This class contains integration tests for the Rackspace Compute Provider
    /// (Cloud Servers) which do not require an active server instance and can be
    /// run with user (non-admin) credentials.
    /// </summary>
    /// <seealso cref="IComputeProvider"/>
    /// <seealso cref="CloudServersProvider"/>
    public class UserComputeTests
    {
        /// <summary>
        /// This prefix is used for servers created by unit tests, to avoid
        /// overwriting servers created by other applications.
        /// </summary>
        public const string UnitTestServerPrefix = "UnitTestServer-";

        /// <summary>
        /// This prefix is used for images created by unit tests, to avoid
        /// overwriting images created by other applications.
        /// </summary>
        public const string UnitTestImagePrefix = "UnitTestImage-";

        /// <summary>
        /// This prefix is used for virtual interfaces created by unit tests, to avoid
        /// overwriting virtual interfaces created by other applications.
        /// </summary>
        public const string UnitTestInterfacePrefix = "UnitTestInterface-";

        public static IEnumerable<SimpleServer> ListAllServers(IComputeProvider provider, int? blockSize = null, string imageId = null, string flavorId = null, string name = null, ServerState status = null, DateTimeOffset? changesSince = null, string region = null, CloudIdentity identity = null)
        {
            if (blockSize <= 0)
                throw new ArgumentOutOfRangeException("blockSize");

            SimpleServer lastServer = null;

            do
            {
                string marker = lastServer != null ? lastServer.Id : null;
                IEnumerable<SimpleServer> servers = provider.ListServers(imageId, flavorId, name, status, marker, blockSize, changesSince, region, identity);
                lastServer = null;
                foreach (SimpleServer server in servers)
                {
                    lastServer = server;
                    yield return server;
                }
            } while (lastServer != null);
        }

        public static IEnumerable<Server> ListAllServersWithDetails(IComputeProvider provider, int? blockSize = null, string imageId = null, string flavorId = null, string name = null, ServerState status = null, DateTimeOffset? changesSince = null, string region = null, CloudIdentity identity = null)
        {
            if (blockSize <= 0)
                throw new ArgumentOutOfRangeException("blockSize");

            Server lastServer = null;

            do
            {
                string marker = lastServer != null ? lastServer.Id : null;
                IEnumerable<Server> servers = provider.ListServersWithDetails(imageId, flavorId, name, status, marker, blockSize, changesSince, region, identity);
                lastServer = null;
                foreach (Server server in servers)
                {
                    lastServer = server;
                    yield return server;
                }
            } while (lastServer != null);
        }

        public static IEnumerable<SimpleServerImage> ListAllImages(IComputeProvider provider, int? blockSize = null, string server = null, string imageName = null, ImageState imageStatus = null, DateTimeOffset? changesSince = null, ImageType imageType = null, string region = null, CloudIdentity identity = null)
        {
            if (blockSize <= 0)
                throw new ArgumentOutOfRangeException("blockSize");

            SimpleServerImage lastImage = null;

            do
            {
                string marker = lastImage != null ? lastImage.Id : null;
                IEnumerable<SimpleServerImage> images = provider.ListImages(server, imageName, imageStatus, changesSince, marker, blockSize, imageType, region, identity);
                lastImage = null;
                foreach (SimpleServerImage image in images)
                {
                    lastImage = image;
                    yield return image;
                }
            } while (lastImage != null);
        }

        public static IEnumerable<ServerImage> ListAllImagesWithDetails(IComputeProvider provider, int? blockSize = null, string server = null, string imageName = null, ImageState imageStatus = null, DateTimeOffset? changesSince = null, ImageType imageType = null, string region = null, CloudIdentity identity = null)
        {
            if (blockSize <= 0)
                throw new ArgumentOutOfRangeException("blockSize");

            ServerImage lastImage = null;

            do
            {
                string marker = lastImage != null ? lastImage.Id : null;
                IEnumerable<ServerImage> images = provider.ListImagesWithDetails(server, imageName, imageStatus, changesSince, marker, blockSize, imageType, region, identity);
                lastImage = null;
                foreach (ServerImage image in images)
                {
                    lastImage = image;
                    yield return image;
                }
            } while (lastImage != null);
        }

        public static IEnumerable<Flavor> ListAllFlavors(IComputeProvider provider, int? blockSize = null, int? minDiskInGB = null, int? minRamInMB = null, string region = null, CloudIdentity identity = null)
        {
            if (blockSize <= 0)
                throw new ArgumentOutOfRangeException("blockSize");

            Flavor lastFlavor = null;

            do
            {
                string marker = lastFlavor != null ? lastFlavor.Id : null;
                IEnumerable<Flavor> flavors = provider.ListFlavors(minDiskInGB, minRamInMB, marker, blockSize, region, identity);
                lastFlavor = null;
                foreach (Flavor flavor in flavors)
                {
                    lastFlavor = flavor;
                    yield return flavor;
                }
            } while (lastFlavor != null);
        }

        public static IEnumerable<FlavorDetails> ListAllFlavorsWithDetails(IComputeProvider provider, int? blockSize = null, int? minDiskInGB = null, int? minRamInMB = null, string region = null, CloudIdentity identity = null)
        {
            if (blockSize <= 0)
                throw new ArgumentOutOfRangeException("blockSize");

            FlavorDetails lastFlavor = null;

            do
            {
                string marker = lastFlavor != null ? lastFlavor.Id : null;
                IEnumerable<FlavorDetails> flavors = provider.ListFlavorsWithDetails(minDiskInGB, minRamInMB, marker, blockSize, region, identity);
                lastFlavor = null;
                foreach (FlavorDetails flavor in flavors)
                {
                    lastFlavor = flavor;
                    yield return flavor;
                }
            } while (lastFlavor != null);
        }

        [Fact]
        [Trait(TestCategories.Cleanup, "")]
        public void CleanupTestServers()
        {
            IComputeProvider provider = Bootstrapper.CreateComputeProvider();
            IEnumerable<SimpleServer> servers = ListAllServers(provider).ToArray();
            foreach (SimpleServer server in servers)
            {
                if (string.IsNullOrEmpty(server.Name))
                    continue;

                if (!server.Name.StartsWith(UnitTestServerPrefix))
                    continue;

                Console.WriteLine("Deleting unit test server... {0} ({1})", server.Name, server.Id);
                bool deleted = provider.DeleteServer(server.Id);
                Assert.True(deleted);
                provider.WaitForServerDeleted(server.Id);
            }
        }

        [Fact]
        [Trait(TestCategories.Cleanup, "")]
        public void CleanupTestImages()
        {
            IComputeProvider provider = Bootstrapper.CreateComputeProvider();
            IEnumerable<SimpleServerImage> images = ListAllImages(provider).ToArray();
            foreach (SimpleServerImage image in images)
            {
                if (string.IsNullOrEmpty(image.Name))
                    continue;

                if (!image.Name.StartsWith(UnitTestImagePrefix))
                    continue;

                Console.WriteLine("Deleting unit test image... {0} ({1})", image.Name, image.Id);
                bool deleted = provider.DeleteImage(image.Id);
                Assert.True(deleted);
                provider.WaitForImageDeleted(image.Id);
            }
        }

        [Fact]
        [Trait(TestCategories.Cleanup, "")]
        public void CleanupTestVirtualInterfaces()
        {
            // virtual interfaces are only added to unit test servers; they are automatically
            // cleaned up when the servers are cleaned up.
        }

        [Fact]
        [Trait(TestCategories.User, "")]
        [Trait(TestCategories.Compute, "")]
        public void TestListFlavors()
        {
            IComputeProvider provider = Bootstrapper.CreateComputeProvider();
            IEnumerable<Flavor> flavors = ListAllFlavors(provider);
            Assert.NotNull(flavors);
            if (!flavors.Any())
                Assert.False(true, "The test could not proceed because the specified account and/or region does not appear to contain any configured flavors.");

            Console.WriteLine("Flavors");
            foreach (Flavor flavor in flavors)
            {
                Assert.NotNull(flavor);

                Console.WriteLine("    {0}: {1}", flavor.Id, flavor.Name);

                Assert.False(string.IsNullOrEmpty(flavor.Id));
                Assert.False(string.IsNullOrEmpty(flavor.Name));
            }
        }

        [Fact]
        [Trait(TestCategories.User, "")]
        [Trait(TestCategories.Compute, "")]
        public void TestListFlavorsWithDetails()
        {
            IComputeProvider provider = Bootstrapper.CreateComputeProvider();
            IEnumerable<FlavorDetails> flavors = ListAllFlavorsWithDetails(provider);
            Assert.NotNull(flavors);
            if (!flavors.Any())
                Assert.False(true, "The test could not proceed because the specified account and/or region does not appear to contain any configured flavors.");

            Console.WriteLine("Flavors (with details)");
            foreach (FlavorDetails flavor in flavors)
            {
                Assert.NotNull(flavor);

                Console.WriteLine("    {0}: {1}", flavor.Id, flavor.Name);
                Console.WriteLine(JsonConvert.SerializeObject(flavor, Formatting.Indented));

                Assert.False(string.IsNullOrEmpty(flavor.Id));
                Assert.False(string.IsNullOrEmpty(flavor.Name));
            }
        }

        [Fact]
        [Trait(TestCategories.User, "")]
        [Trait(TestCategories.Compute, "")]
        public void TestGetFlavor()
        {
            IComputeProvider provider = Bootstrapper.CreateComputeProvider();
            IEnumerable<FlavorDetails> flavors = ListAllFlavorsWithDetails(provider);
            Assert.NotNull(flavors);
            if (!flavors.Any())
                Assert.False(true, "The test could not proceed because the specified account and/or region does not appear to contain any configured flavors.");

            foreach (FlavorDetails flavor in flavors)
            {
                Assert.NotNull(flavor);
                FlavorDetails details = provider.GetFlavor(flavor.Id);
                Assert.Equal(flavor.Disabled, details.Disabled);
                Assert.Equal(flavor.DiskSizeInGB, details.DiskSizeInGB);
                Assert.Equal(flavor.Id, details.Id);
                //Assert.Equal(flavor.Links, details.Links);
                Assert.Equal(flavor.Name, details.Name);
                Assert.Equal(flavor.RAMInMB, details.RAMInMB);
                Assert.Equal(flavor.VirtualCPUCount, details.VirtualCPUCount);
            }
        }

        [Fact]
        [Trait(TestCategories.User, "")]
        [Trait(TestCategories.Compute, "")]
        public void TestListImages()
        {
            IComputeProvider provider = Bootstrapper.CreateComputeProvider();
            IEnumerable<SimpleServerImage> images = ListAllImages(provider);
            Assert.NotNull(images);
            if (!images.Any())
                Assert.False(true, "The test could not proceed because the specified account and/or region does not appear to contain any images.");

            Console.WriteLine("Images");
            foreach (SimpleServerImage image in images)
            {
                Assert.NotNull(image);

                Console.WriteLine("    {0}: {1}", image.Id, image.Name);

                Assert.False(string.IsNullOrEmpty(image.Id));
                Assert.False(string.IsNullOrEmpty(image.Name));
            }
        }

        [Fact]
        [Trait(TestCategories.User, "")]
        [Trait(TestCategories.Compute, "")]
        public void TestListImagesWithDetails()
        {
            IComputeProvider provider = Bootstrapper.CreateComputeProvider();
            IEnumerable<ServerImage> images = ListAllImagesWithDetails(provider);
            Assert.NotNull(images);
            if (!images.Any())
                Assert.False(true, "The test could not proceed because the specified account and/or region does not appear to contain any images.");

            Console.WriteLine("Images (with details)");
            foreach (ServerImage image in images)
            {
                Assert.NotNull(image);

                Console.WriteLine("    {0}: {1}", image.Id, image.Name);
                Console.WriteLine(JsonConvert.SerializeObject(image, Formatting.Indented));

                Assert.False(string.IsNullOrEmpty(image.Id));
                Assert.False(string.IsNullOrEmpty(image.Name));
            }
        }

        [Fact]
        [Trait(TestCategories.User, "")]
        [Trait(TestCategories.Compute, "")]
        public void TestGetImage()
        {
            IComputeProvider provider = Bootstrapper.CreateComputeProvider();
            IEnumerable<ServerImage> images = ListAllImagesWithDetails(provider);
            Assert.NotNull(images);
            if (!images.Any())
                Assert.False(true, "The test could not proceed because the specified account and/or region does not appear to contain any images.");

            foreach (ServerImage image in images)
            {
                Assert.NotNull(image);
                ServerImage details = provider.GetImage(image.Id);
                Assert.Equal(image.Created, details.Created);
                Assert.Equal(image.DiskConfig, details.DiskConfig);
                Assert.Equal(image.Id, details.Id);

                Assert.NotNull(details.Links);
                Assert.Equal(image.Links.Length, details.Links.Length);
                for (int i = 0; i < image.Links.Length; i++)
                {
                    // this could start to fail if the server reorders links; if that occurs the test should be rewritten to allow it
                    Assert.Equal(image.Links[i].Href, details.Links[i].Href);
                    Assert.Equal(image.Links[i].Rel, details.Links[i].Rel);
                }

                Assert.Equal(image.MinDisk, details.MinDisk);
                Assert.Equal(image.MinRAM, details.MinRAM);
                Assert.Equal(image.Name, details.Name);

                Assert.True(details.Progress >= 0 && details.Progress <= 100);

                if (image.Server != null)
                {
                    Assert.NotNull(details.Server);
                    Assert.Equal(image.Server.Id, details.Server.Id);
                }

                Assert.NotNull(details.Status);

                Assert.Equal(image.Updated, details.Updated);
            }
        }

        [Fact]
        [Trait(TestCategories.User, "")]
        [Trait(TestCategories.Compute, "")]
        public void TestListImageMetadata()
        {
            IComputeProvider provider = Bootstrapper.CreateComputeProvider();
            IEnumerable<ServerImage> images = ListAllImagesWithDetails(provider);
            Assert.NotNull(images);
            if (!images.Any())
                Assert.False(true, "The test could not proceed because the specified account and/or region does not appear to contain any images.");

            Console.WriteLine("Image metadata");
            Console.WriteLine();

            bool hadMetadata = false;
            foreach (ServerImage image in images)
            {
                Assert.NotNull(image);
                Metadata metadata = provider.ListImageMetadata(image.Id);
                if (metadata.Count == 0)
                    continue;

                hadMetadata = true;
                Console.WriteLine("Image: {0}", image.Name);
                foreach (KeyValuePair<string, string> pair in metadata)
                    Console.WriteLine("  {0}: {1}", pair.Key, pair.Value);
            }

            if (!hadMetadata)
                Assert.False(true, "None of the images contained metadata.");
        }

        [Fact]
        [Trait(TestCategories.User, "")]
        [Trait(TestCategories.Compute, "")]
        public void TestGetImageMetadataItem()
        {
            IComputeProvider provider = Bootstrapper.CreateComputeProvider();
            IEnumerable<ServerImage> images = ListAllImagesWithDetails(provider);
            Assert.NotNull(images);
            if (!images.Any())
                Assert.False(true, "The test could not proceed because the specified account and/or region does not appear to contain any images.");

            int hadMetadata = 0;
            int metadataCount = 0;
            foreach (ServerImage image in images)
            {
                Assert.NotNull(image);
                Metadata metadata = provider.ListImageMetadata(image.Id);
                if (metadata.Count == 0)
                    continue;

                hadMetadata++;
                metadataCount += metadata.Count;
                Console.WriteLine("Checking {0} metadata items for image '{1}'...", metadata.Count, image.Name);
                foreach (KeyValuePair<string, string> pair in metadata)
                    Assert.Equal(pair.Value, provider.GetImageMetadataItem(image.Id, pair.Key));

                if (hadMetadata >= 3 && metadataCount >= 30)
                {
                    // this can be slow for a large number of images and metadata items,
                    // so stop after several are tested
                    break;
                }
            }

            if (hadMetadata == 0)
                Assert.False(true, "None of the images contained metadata.");
        }
    }
}
