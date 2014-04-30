namespace Net.OpenStack.Testing.Integration.Providers.OpenStack
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Diagnostics;
    using System.Linq;
    using System.Net.Http.Headers;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Newtonsoft.Json.Linq;
    using global::OpenStack.Collections;
    using global::OpenStack.Net;
    using global::OpenStack.Security.Authentication;
    using global::OpenStack.Services.Identity.V2;
    using global::OpenStack.Services.ObjectStorage.V1;
    using global::Rackspace.Security.Authentication;
    using global::Rackspace.Threading;
    using Encoding = System.Text.Encoding;
    using HttpStatusCode = System.Net.HttpStatusCode;
    using MemoryStream = System.IO.MemoryStream;
    using Path = System.IO.Path;
    using Stream = System.IO.Stream;
    using StreamReader = System.IO.StreamReader;
    using TestHelpers = Net.OpenStack.Testing.Integration.Providers.Rackspace.TestHelpers;

    [TestClass]
    public class ObjectStorageTests
    {
        /// <summary>
        /// This prefix is used for metadata keys created by unit tests, to avoid
        /// overwriting metadata created by other applications.
        /// </summary>
        private const string TestKeyPrefix = "UnitTestMetadataKey-";

        /// <summary>
        /// This prefix is used for containers created by unit tests, to avoid
        /// overwriting containers created by other applications.
        /// </summary>
        private const string TestContainerPrefix = "UnitTestContainer-";

        /// <summary>
        /// The minimum character allowed in metadata keys. This is drawn from
        /// the HTTP/1.1 specification, which does not allow ASCII control
        /// characters in header keys.
        /// </summary>
        private const char MinHeaderKeyCharacter = (char)32;

        /// <summary>
        /// The maximum character allowed in metadata keys. This is drawn from
        /// the HTTP/1.1 specification, which restricts header keys to the 7-bit
        /// ASCII character set.
        /// </summary>
        private const char MaxHeaderKeyCharacter = (char)127;

        /// <summary>
        /// The HTTP/1.1 separator characters.
        /// </summary>
        private const string SeparatorCharacters = "()<>@,;:\\\"/[]?={} \t\x7F";

        /// <summary>
        /// Characters which are technically allowed by HTTP/1.1, but cannot be used in
        /// metadata keys for <see cref="CloudFilesProvider"/>.
        /// </summary>
        /// <remarks>
        /// The underscore is disallowed by the Cloud Files implementation, which silently
        /// converts it to a dash. The apostrophe is disallowed by <see cref="WebHeaderCollection"/>
        /// which is used by the implementation.
        /// </remarks>
        private const string NotSupportedCharacters = "_'";

        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.ObjectStorage)]
        public async Task TestGetObjectStorageInfo()
        {
            IObjectStorageService provider = CreateProvider();
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource(TestTimeout(TimeSpan.FromSeconds(10)));
            ReadOnlyDictionary<string, JObject> info = await provider.GetObjectStorageInfoAsync(cancellationTokenSource.Token);
            Assert.IsNotNull(info);
            // large objects
            Console.WriteLine("slo: {0}", info.ContainsKey("slo") ? "supported" : "not supported");
            // schedule objects for deletion
            Console.WriteLine("???: {0}", info.ContainsKey("???") ? "supported" : "not supported");
            // auto-extract archive
            Console.WriteLine("bulk_upload: {0}", info.ContainsKey("bulk_upload") ? "supported" : "not supported");
            // bulk delete
            Console.WriteLine("bulk_delete: {0}", info.ContainsKey("bulk_delete") ? "supported" : "not supported");
            // container synchronization
            Console.WriteLine("container_sync: {0}", info.ContainsKey("container_sync") ? "supported" : "not supported");
            // container quotas
            Console.WriteLine("container_quotas: {0}", info.ContainsKey("container_quotas") ? "supported" : "not supported");
            // temp url
            Console.WriteLine("tempurl: {0}", info.ContainsKey("tempurl") ? "supported" : "not supported");
            // form post
            Console.WriteLine("formpost: {0}", info.ContainsKey("formpost") ? "supported" : "not supported");
            // static website
            Console.WriteLine("staticweb: {0}", info.ContainsKey("staticweb") ? "supported" : "not supported");
        }

        #region Container

        /// <summary>
        /// This test can be used to clear all of the metadata associated with every container in the storage provider.
        /// </summary>
        /// <remarks>
        /// This test is normally disabled. To run the cleanup method, comment out or remove the
        /// <see cref="IgnoreAttribute"/>.
        /// </remarks>
        [TestMethod]
        [TestCategory(TestCategories.Cleanup)]
        [Ignore]
        public async Task CleanupAllContainerMetadata()
        {
            IObjectStorageService provider = CreateProvider();
            IEnumerable<Container> containers = await ListAllContainersAsync(provider, null, CancellationToken.None, null);
            foreach (Container container in containers)
            {
                ContainerMetadata metadata = await provider.GetContainerMetadataAsync(container.Name, CancellationToken.None);
                await provider.RemoveContainerMetadataAsync(container.Name, metadata.Metadata.Keys, CancellationToken.None);
            }
        }

        /// <summary>
        /// This unit test clears the metadata associated with every container which is
        /// created by the unit tests in this class.
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategories.Cleanup)]
        public async Task CleanupTestContainerMetadata()
        {
            IObjectStorageService provider = CreateProvider();
            IEnumerable<Container> containers = await ListAllContainersAsync(provider, null, CancellationToken.None, null);
            foreach (Container container in containers)
            {
                Dictionary<string, string> metadata = await GetContainerMetadataWithPrefix(provider, container, TestKeyPrefix, CancellationToken.None);
                await provider.RemoveContainerMetadataAsync(container.Name, metadata.Keys, CancellationToken.None);
            }
        }

        /// <summary>
        /// This unit test deletes all containers created by the unit tests, including all
        /// objects within those containers.
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategories.Cleanup)]
        public async Task CleanupTestContainers()
        {
            IObjectStorageService provider = CreateProvider();
            IEnumerable<Container> containers = await ListAllContainersAsync(provider, null, CancellationToken.None, null);
            foreach (Container container in containers)
            {
                if (container.Name.Value.StartsWith(TestContainerPrefix))
                {
                    bool retry = false;

                    try
                    {
                        await provider.RemoveContainerAsync(container.Name, CancellationToken.None);
                    }
                    catch (HttpWebException)
                    {
                        retry = true;
                    }

                    if (retry)
                    {
                        // this works around a bug in bulk delete, where files with trailing whitespace
                        // in the name do not get deleted
                        foreach (ContainerObject containerObject in await ListAllObjectsAsync(provider, container.Name, null, CancellationToken.None, null))
                            await provider.RemoveObjectAsync(container.Name, containerObject.Name, CancellationToken.None);

                        await provider.RemoveContainerAsync(container.Name, CancellationToken.None);
                    }
                }
                else if (container.Name.Equals(".CDN_ACCESS_LOGS"))
                {
                    foreach (ContainerObject containerObject in await ListAllObjectsAsync(provider, container.Name, null, CancellationToken.None, null))
                    {
                        if (containerObject.Name.Value.StartsWith(TestContainerPrefix))
                            await provider.RemoveObjectAsync(container.Name, containerObject.Name, CancellationToken.None);
                    }
                }
            }
        }

        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.ObjectStorage)]
        public async Task TestListContainers()
        {
            IObjectStorageService provider = CreateProvider();
            IEnumerable<Container> containers = await ListAllContainersAsync(provider, null, CancellationToken.None, null);
            if (!containers.Any())
                Assert.Inconclusive("The account does not have any containers in the region.");

            Console.WriteLine("Containers");
            foreach (Container container in containers)
            {
                Console.WriteLine("  {0}", container.Name);
                Console.WriteLine("    Objects: {0}", container.ObjectCount);
                Console.WriteLine("    Bytes: {0}", container.Size);
            }
        }

        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.ObjectStorage)]
        public async Task TestContainerProperties()
        {
            IObjectStorageService provider = CreateProvider();
            IEnumerable<Container> containers = await ListAllContainersAsync(provider, 2, CancellationToken.None, null);
            if (!containers.Any())
                Assert.Inconclusive("The account does not have any containers in the region.");

            int containersTested = 0;
            long objectsTested = 0;
            long totalSizeTested = 0;
            int nonEmptyContainersTested = 0;
            int nonEmptyBytesContainersTested = 0;
            foreach (Container container in containers)
            {
                Assert.IsTrue(container.ObjectCount >= 0);
                Assert.IsTrue(container.Size >= 0);

                containersTested++;
                if (container.ObjectCount > 0)
                    nonEmptyContainersTested++;
                if (container.Size > 0)
                    nonEmptyBytesContainersTested++;

                long objectCount = 0;
                long objectSize = 0;
                foreach (var obj in await ListAllObjectsAsync(provider, container.Name, null, CancellationToken.None, null))
                {
                    objectCount++;
                    objectSize += obj.Size.Value;
                }

                objectsTested += objectCount;
                totalSizeTested += objectSize;

                Assert.AreEqual(container.ObjectCount, objectCount);
                Assert.AreEqual(container.Size, objectSize);

                if (containersTested >= 5 && nonEmptyContainersTested >= 5 && nonEmptyBytesContainersTested >= 5)
                    break;
            }

            if (containersTested == 0 || nonEmptyContainersTested == 0 || nonEmptyBytesContainersTested == 0)
                Assert.Inconclusive("The account does not have any non-empty containers in the region.");

            Console.WriteLine("Verified container properties for:");
            Console.WriteLine("  {0} containers", containersTested);
            Console.WriteLine("  {0} objects", objectsTested);
            Console.WriteLine("  {0} bytes", totalSizeTested);
        }

        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.ObjectStorage)]
        public async Task TestCreateContainer()
        {
            IObjectStorageService provider = CreateProvider();
            ContainerName containerName = new ContainerName(TestContainerPrefix + Path.GetRandomFileName());

            await provider.CreateContainerAsync(containerName, CancellationToken.None);

            await provider.CreateContainerAsync(containerName, CancellationToken.None);

            await provider.RemoveContainerAsync(containerName, CancellationToken.None);
        }

        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.ObjectStorage)]
        public async Task TestVersionedContainer()
        {
            IObjectStorageService provider = CreateProvider();
            ContainerName containerName = new ContainerName(TestContainerPrefix + Path.GetRandomFileName());
            ContainerName versionsContainerName = new ContainerName(TestContainerPrefix + Path.GetRandomFileName());

            await provider.CreateContainerAsync(versionsContainerName, CancellationToken.None);

            var prepareCreateContainer = await provider.PrepareCreateContainerAsync(containerName, CancellationToken.None);
            prepareCreateContainer.RequestMessage.Headers.Add(net.openstack.Providers.Rackspace.CloudFilesProvider.VersionsLocation, versionsContainerName.Value);
            await prepareCreateContainer.SendAsync(CancellationToken.None);

            ContainerMetadata headers = await provider.GetContainerMetadataAsync(containerName, CancellationToken.None);
            string location;
            Assert.IsTrue(headers.Headers.TryGetValue(net.openstack.Providers.Rackspace.CloudFilesProvider.VersionsLocation, out location));
            location = UriUtility.UriDecode(location);
            Assert.AreEqual(versionsContainerName.Value, location);

            ObjectName objectName = new ObjectName(Path.GetRandomFileName());
            string fileData1 = "first-content";
            string fileData2 = "second-content";

            /*
             * Create the object
             */

            using (MemoryStream uploadStream = new MemoryStream(Encoding.UTF8.GetBytes(fileData1)))
            {
                await provider.CreateObjectAsync(containerName, objectName, uploadStream, CancellationToken.None, null);
            }

            string actualData = await ReadAllObjectTextAsync(provider, containerName, objectName, Encoding.UTF8, CancellationToken.None, verifyEtag: true);
            Assert.AreEqual(fileData1, actualData);

            /*
             * Overwrite the object
             */

            using (MemoryStream uploadStream = new MemoryStream(Encoding.UTF8.GetBytes(fileData2)))
            {
                await provider.CreateObjectAsync(containerName, objectName, uploadStream, CancellationToken.None, null);
            }

            actualData = await ReadAllObjectTextAsync(provider, containerName, objectName, Encoding.UTF8, CancellationToken.None, verifyEtag: true);
            Assert.AreEqual(fileData2, actualData);

            /*
             * Delete the object once
             */

            await provider.RemoveObjectAsync(containerName, objectName, CancellationToken.None);

            actualData = await ReadAllObjectTextAsync(provider, containerName, objectName, Encoding.UTF8, CancellationToken.None, verifyEtag: true);
            Assert.AreEqual(fileData1, actualData);

            /*
             * Cleanup
             */

            await RemoveContainerWithObjectsAsync(provider, versionsContainerName, CancellationToken.None);
            await RemoveContainerWithObjectsAsync(provider, containerName, CancellationToken.None);
        }

        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.ObjectStorage)]
        public async Task TestDeleteContainer()
        {
            IObjectStorageService provider = CreateProvider();
            ContainerName containerName = new ContainerName(TestContainerPrefix + Path.GetRandomFileName());
            ObjectName objectName = new ObjectName(Path.GetRandomFileName());
            string fileContents = "File contents!";

            await provider.CreateContainerAsync(containerName, CancellationToken.None);

            Stream stream = new MemoryStream(Encoding.UTF8.GetBytes(fileContents));
            await provider.CreateObjectAsync(containerName, objectName, stream, CancellationToken.None, null);

            try
            {
                await provider.RemoveContainerAsync(containerName, CancellationToken.None);
                Assert.Fail("Expected a HttpWebException");
            }
            catch (HttpWebException ex)
            {
                Assert.IsNotNull(ex.ResponseMessage);
                Assert.AreEqual(HttpStatusCode.Conflict, ex.ResponseMessage.StatusCode);
            }

            await RemoveContainerWithObjectsAsync(provider, containerName, CancellationToken.None);
        }

        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.ObjectStorage)]
        public async Task TestGetContainerHeader()
        {
            IObjectStorageService provider = CreateProvider();
            ContainerName containerName = new ContainerName(TestContainerPrefix + Path.GetRandomFileName());

            await provider.CreateContainerAsync(containerName, CancellationToken.None);

            ContainerMetadata headers = await provider.GetContainerMetadataAsync(containerName, CancellationToken.None);
            Console.WriteLine("Container Headers");
            foreach (KeyValuePair<string, string> pair in headers.Headers)
                Console.WriteLine("    {0}: {1}", pair.Key, pair.Value);

            await provider.RemoveContainerAsync(containerName, CancellationToken.None);
        }

        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.ObjectStorage)]
        public async Task TestGetContainerMetaData()
        {
            IObjectStorageService provider = CreateProvider();
            ContainerName containerName = new ContainerName(TestContainerPrefix + Path.GetRandomFileName());

            await provider.CreateContainerAsync(containerName, CancellationToken.None);

            Dictionary<string, string> headers = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            Dictionary<string, string> metadata = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                { "Key1", "Value 1" },
                { "Key2", "Value ²" },
            };

            await provider.UpdateContainerMetadataAsync(containerName, new ContainerMetadata(headers, metadata), CancellationToken.None);

            ContainerMetadata actualMetadata = await provider.GetContainerMetadataAsync(containerName, CancellationToken.None);
            Console.WriteLine("Container Metadata");
            foreach (KeyValuePair<string, string> pair in actualMetadata.Metadata)
                Console.WriteLine("    {0}: {1}", pair.Key, pair.Value);

            CheckMetadataCollections(metadata, actualMetadata);

            await provider.RemoveContainerAsync(containerName, CancellationToken.None);
        }

        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.ObjectStorage)]
        public async Task TestContainerHeaderKeyCharacters()
        {
            IObjectStorageService provider = CreateProvider();
            ContainerName containerName = new ContainerName(TestContainerPrefix + Path.GetRandomFileName());

            await provider.CreateContainerAsync(containerName, CancellationToken.None);

            List<char> keyCharList = new List<char>();
            for (char i = MinHeaderKeyCharacter; i <= MaxHeaderKeyCharacter; i++)
            {
                if (!SeparatorCharacters.Contains(i) && !NotSupportedCharacters.Contains(i))
                    keyCharList.Add(i);
            }

            string key = TestKeyPrefix + new string(keyCharList.ToArray());
            Console.WriteLine("Expected key: {0}", key);

            await provider.UpdateContainerMetadataAsync(
                containerName,
                new ContainerMetadata(
                    new Dictionary<string, string>(),
                    new Dictionary<string, string>
                    {
                        { key, "Value" }
                    }),
                CancellationToken.None);

            ContainerMetadata metadata = await provider.GetContainerMetadataAsync(containerName, CancellationToken.None);
            Assert.IsNotNull(metadata);

            string value;
            Assert.IsTrue(metadata.Metadata.TryGetValue(key, out value));
            Assert.AreEqual("Value", value);

            await provider.UpdateContainerMetadataAsync(
                containerName,
                new ContainerMetadata(
                    new Dictionary<string, string>(),
                    new Dictionary<string, string>
                    {
                        { key, null }
                    }),
                CancellationToken.None);

            metadata = await provider.GetContainerMetadataAsync(containerName, CancellationToken.None);
            Assert.IsNotNull(metadata);
            Assert.IsFalse(metadata.Metadata.TryGetValue(key, out value));

            await provider.RemoveContainerAsync(containerName, CancellationToken.None);
        }

        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.ObjectStorage)]
        public async Task TestContainerInvalidHeaderKeyCharacters()
        {
            IObjectStorageService provider = CreateProvider();
            ContainerName containerName = new ContainerName(TestContainerPrefix + Path.GetRandomFileName());

            await provider.CreateContainerAsync(containerName, CancellationToken.None);

            List<char> validKeyCharList = new List<char>();
            for (char i = MinHeaderKeyCharacter; i <= MaxHeaderKeyCharacter; i++)
            {
                if (!SeparatorCharacters.Contains(i) && !NotSupportedCharacters.Contains(i))
                    validKeyCharList.Add(i);
            }

            for (int i = char.MinValue; i <= char.MaxValue; i++)
            {
                if (validKeyCharList.BinarySearch((char)i) >= 0)
                    continue;

                string invalidKey = new string((char)i, 1);

                try
                {
                    await provider.UpdateContainerMetadataAsync(
                        containerName,
                        new ContainerMetadata(
                            new Dictionary<string, string>(),
                            new Dictionary<string, string>
                            {
                                { invalidKey, "Value" }
                            }),
                        CancellationToken.None);
                    Assert.Fail("Should throw an exception for invalid keys.");
                }
                catch (ArgumentException)
                {
                    if (i >= MinHeaderKeyCharacter && i <= MaxHeaderKeyCharacter)
                        StringAssert.Contains(SeparatorCharacters, invalidKey);
                }
                catch (NotSupportedException)
                {
                    StringAssert.Contains(NotSupportedCharacters, invalidKey);
                }
            }

            await provider.RemoveContainerAsync(containerName, CancellationToken.None);
        }

        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.ObjectStorage)]
        public async Task TestUpdateContainerMetadata()
        {
            IObjectStorageService provider = CreateProvider();
            ContainerName containerName = new ContainerName(TestContainerPrefix + Path.GetRandomFileName());

            await provider.CreateContainerAsync(containerName, CancellationToken.None);

            Dictionary<string, string> metadata = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                { "Key1", "Value 1" },
                { "Key2", "Value ²" },
            };

            await provider.UpdateContainerMetadataAsync(containerName, new ContainerMetadata(new Dictionary<string, string>(), new Dictionary<string, string>(metadata, StringComparer.OrdinalIgnoreCase)), CancellationToken.None);

            ContainerMetadata actualMetadata = await provider.GetContainerMetadataAsync(containerName, CancellationToken.None);
            Console.WriteLine("Container Metadata");
            foreach (KeyValuePair<string, string> pair in actualMetadata.Metadata)
                Console.WriteLine("    {0}: {1}", pair.Key, pair.Value);

            CheckMetadataCollections(metadata, actualMetadata);

            metadata["Key2"] = "Value 2";
            Dictionary<string, string> updatedMetadata = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                { "Key2", "Value 2" }
            };
            await provider.UpdateContainerMetadataAsync(containerName, new ContainerMetadata(new Dictionary<string, string>(), new Dictionary<string, string>(updatedMetadata, StringComparer.OrdinalIgnoreCase)), CancellationToken.None);

            actualMetadata = await provider.GetContainerMetadataAsync(containerName, CancellationToken.None);
            Console.WriteLine("Container Metadata");
            foreach (KeyValuePair<string, string> pair in actualMetadata.Metadata)
                Console.WriteLine("    {0}: {1}", pair.Key, pair.Value);

            CheckMetadataCollections(metadata, actualMetadata);

            await provider.RemoveContainerAsync(containerName, CancellationToken.None);
        }

        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.ObjectStorage)]
        public async Task TestDeleteContainerMetadata()
        {
            IObjectStorageService provider = CreateProvider();
            ContainerName containerName = new ContainerName(TestContainerPrefix + Path.GetRandomFileName());

            await provider.CreateContainerAsync(containerName, CancellationToken.None);

            Dictionary<string, string> metadata = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                { "Key1", "Value 1" },
                { "Key2", "Value ²" },
                { "Key3", "Value 3" },
                { "Key4", "Value 4" },
            };

            await provider.UpdateContainerMetadataAsync(containerName, new ContainerMetadata(new Dictionary<string, string>(), new Dictionary<string, string>(metadata, StringComparer.OrdinalIgnoreCase)), CancellationToken.None);

            Func<Task<ContainerMetadata>> getLatestMetadataAsync =
                () =>
                {
                    return
                        CoreTaskExtensions.Using(
                            () => provider.PrepareGetContainerMetadataAsync(containerName, CancellationToken.None),
                            task =>
                                {
                                    task.Result.RequestMessage.Headers.Add("X-Newest", "true");
                                    return task.Result.SendAsync(CancellationToken.None);
                                })
                        .Select(task => task.Result.Item2);
                };

            ContainerMetadata actualMetadata = await getLatestMetadataAsync();
            Console.WriteLine("Container Metadata");
            foreach (KeyValuePair<string, string> pair in actualMetadata.Metadata)
                Console.WriteLine("    {0}: {1}", pair.Key, pair.Value);

            CheckMetadataCollections(metadata, actualMetadata);

            /* Check the overload which takes a single key
             */
            // remove Key3 first to make sure we still have a ² character in a remaining value
            metadata.Remove("Key3");
            await provider.RemoveContainerMetadataAsync(containerName, new [] { "Key3" }, CancellationToken.None);

            actualMetadata = await getLatestMetadataAsync();
            Console.WriteLine("Container Metadata after removing Key3");
            foreach (KeyValuePair<string, string> pair in actualMetadata.Metadata)
                Console.WriteLine("    {0}: {1}", pair.Key, pair.Value);

            CheckMetadataCollections(metadata, actualMetadata);

            /* Check the overload which takes multiple keys
             */
            metadata.Remove("Key2");
            metadata.Remove("Key4");
            await provider.RemoveContainerMetadataAsync(containerName, new[] { "Key2", "Key4" }, CancellationToken.None);

            actualMetadata = await getLatestMetadataAsync();
            Console.WriteLine("Container Metadata after removing Key2, Key4");
            foreach (KeyValuePair<string, string> pair in actualMetadata.Metadata)
                Console.WriteLine("    {0}: {1}", pair.Key, pair.Value);

            CheckMetadataCollections(metadata, actualMetadata);

            /* Check that duplicate removal is a NOP
             */
            metadata.Remove("Key2");
            metadata.Remove("Key4");
            await provider.RemoveContainerMetadataAsync(containerName, new[] { "Key2", "Key4" }, CancellationToken.None);

            actualMetadata = await getLatestMetadataAsync();
            Console.WriteLine("Container Metadata after removing Key2, Key4");
            foreach (KeyValuePair<string, string> pair in actualMetadata.Metadata)
                Console.WriteLine("    {0}: {1}", pair.Key, pair.Value);

            CheckMetadataCollections(metadata, actualMetadata);

            /* Cleanup
             */
            await provider.RemoveContainerAsync(containerName, CancellationToken.None);
        }

#if false // rackspace specific
        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.ObjectStorage)]
        public async Task TestListCDNContainers()
        {
            IObjectStorageService provider = CreateProvider();
            IEnumerable<ContainerCDN> containers = ListAllCDNContainers(provider);

            Console.WriteLine("Containers");
            foreach (ContainerCDN container in containers)
            {
                Console.WriteLine("  {1}{0}", container.Name, container.CDNEnabled ? "*" : "");
            }
        }

        /// <summary>
        /// This test covers most of the CDN functionality exposed by <see cref="IObjectStorageService"/>.
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.ObjectStorage)]
        public async Task TestCDNOnContainer()
        {
            IObjectStorageService provider = CreateProvider();
            ContainerName containerName = new ContainerName(TestContainerPrefix + Path.GetRandomFileName());
            ObjectName objectName = new ObjectName(Path.GetRandomFileName());
            string fileContents = "File contents!";

            await provider.CreateContainerAsync(containerName, CancellationToken.None);
            Assert.AreEqual(ObjectStore.ContainerCreated, result);

            Stream stream = new MemoryStream(Encoding.UTF8.GetBytes(fileContents));
            provider.CreateObject(containerName, stream, objectName);

            Dictionary<string, string> cdnHeaders = provider.EnableCDNOnContainer(containerName, false);
            Assert.IsNotNull(cdnHeaders);
            Console.WriteLine("CDN Headers from EnableCDNOnContainer");
            foreach (var pair in cdnHeaders)
                Console.WriteLine("    {0}: {1}", pair.Key, pair.Value);

            ContainerCDN containerHeader = provider.GetContainerCDNHeader(containerName);
            Assert.IsNotNull(containerHeader);
            Console.WriteLine(JsonConvert.SerializeObject(containerHeader, Formatting.Indented));
            Assert.IsTrue(containerHeader.CDNEnabled);
            Assert.IsFalse(containerHeader.LogRetention);
            Assert.IsTrue(
                containerHeader.CDNUri != null
                || containerHeader.CDNIosUri != null
                || containerHeader.CDNSslUri != null
                || containerHeader.CDNStreamingUri != null);

            // Call the other overloads of EnableCDNOnContainer
            cdnHeaders = provider.EnableCDNOnContainer(containerName, containerHeader.Ttl);
            ContainerCDN updatedHeader = provider.GetContainerCDNHeader(containerName);
            Console.WriteLine(JsonConvert.SerializeObject(updatedHeader, Formatting.Indented));
            Assert.IsNotNull(updatedHeader);
            Assert.IsTrue(updatedHeader.CDNEnabled);
            Assert.IsFalse(updatedHeader.LogRetention);
            Assert.IsTrue(
                updatedHeader.CDNUri != null
                || updatedHeader.CDNIosUri != null
                || updatedHeader.CDNSslUri != null
                || updatedHeader.CDNStreamingUri != null);
            Assert.AreEqual(containerHeader.Ttl, updatedHeader.Ttl);

            cdnHeaders = provider.EnableCDNOnContainer(containerName, containerHeader.Ttl, true);
            updatedHeader = provider.GetContainerCDNHeader(containerName);
            Console.WriteLine(JsonConvert.SerializeObject(updatedHeader, Formatting.Indented));
            Assert.IsNotNull(updatedHeader);
            Assert.IsTrue(updatedHeader.CDNEnabled);
            Assert.IsTrue(updatedHeader.LogRetention);
            Assert.IsTrue(
                updatedHeader.CDNUri != null
                || updatedHeader.CDNIosUri != null
                || updatedHeader.CDNSslUri != null
                || updatedHeader.CDNStreamingUri != null);
            Assert.AreEqual(containerHeader.Ttl, updatedHeader.Ttl);

            // update the container CDN properties
            Dictionary<string, string> headers = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                { CloudFilesProvider.CdnTTL, (updatedHeader.Ttl + 1).ToString() },
                { CloudFilesProvider.CdnLogRetention, false.ToString() },
                //{ CloudFilesProvider.CdnEnabled, true.ToString() },
            };

            provider.UpdateContainerCdnHeaders(containerName, headers);
            updatedHeader = provider.GetContainerCDNHeader(containerName);
            Console.WriteLine(JsonConvert.SerializeObject(updatedHeader, Formatting.Indented));
            Assert.IsNotNull(updatedHeader);
            Assert.IsTrue(updatedHeader.CDNEnabled);
            Assert.IsFalse(updatedHeader.LogRetention);
            Assert.IsTrue(
                updatedHeader.CDNUri != null
                || updatedHeader.CDNIosUri != null
                || updatedHeader.CDNSslUri != null
                || updatedHeader.CDNStreamingUri != null);
            Assert.AreEqual(containerHeader.Ttl + 1, updatedHeader.Ttl);

            // attempt to access the container over the CDN
            if (containerHeader.CDNUri != null || containerHeader.CDNSslUri != null)
            {
                string baseUri = containerHeader.CDNUri ?? containerHeader.CDNSslUri;
                Uri uri = new Uri(containerHeader.CDNUri + '/' + objectName);
                WebRequest request = HttpWebRequest.Create(uri);
                using (WebResponse response = request.GetResponse())
                {
                    Stream cdnStream = response.GetResponseStream();
                    StreamReader reader = new StreamReader(cdnStream, Encoding.UTF8);
                    string text = reader.ReadToEnd();
                    Assert.AreEqual(fileContents, text);
                }
            }
            else
            {
                Assert.Inconclusive("This integration test relies on cdn_uri or cdn_ssl_uri.");
            }

            IEnumerable<ContainerCDN> containers = ListAllCDNContainers(provider);
            Console.WriteLine("Containers");
            foreach (ContainerCDN container in containers)
            {
                Console.WriteLine("    {1}{0}", container.Name, container.CDNEnabled ? "*" : "");
            }

            cdnHeaders = provider.DisableCDNOnContainer(containerName);
            Assert.IsNotNull(cdnHeaders);
            Console.WriteLine("CDN Headers from DisableCDNOnContainer");
            foreach (var pair in cdnHeaders)
                Console.WriteLine("    {0}: {1}", pair.Key, pair.Value);

            updatedHeader = provider.GetContainerCDNHeader(containerName);
            Console.WriteLine(JsonConvert.SerializeObject(updatedHeader, Formatting.Indented));
            Assert.IsNotNull(updatedHeader);
            Assert.IsFalse(updatedHeader.CDNEnabled);
            Assert.IsFalse(updatedHeader.LogRetention);
            Assert.IsTrue(
                updatedHeader.CDNUri != null
                || updatedHeader.CDNIosUri != null
                || updatedHeader.CDNSslUri != null
                || updatedHeader.CDNStreamingUri != null);
            Assert.AreEqual(containerHeader.Ttl + 1, updatedHeader.Ttl);

            provider.RemoveContainer(containerName, deleteObjects: true);
        }
#endif

#if false // extension
        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.ObjectStorage)]
        public async Task TestStaticWebOnContainer()
        {
            IObjectStorageService provider = CreateProvider();
            ContainerName containerName = new ContainerName(TestContainerPrefix + Path.GetRandomFileName());
            ObjectName objectName = new ObjectName(Path.GetRandomFileName());
            string fileContents = "File contents!";

            await provider.CreateContainerAsync(containerName, CancellationToken.None);

            Stream stream = new MemoryStream(Encoding.UTF8.GetBytes(fileContents));
            await provider.CreateObjectAsync(containerName, objectName, stream, CancellationToken.None, null);

            Dictionary<string, string> cdnHeaders = provider.EnableCDNOnContainer(containerName, false);
            Assert.IsNotNull(cdnHeaders);
            Console.WriteLine("CDN Headers");
            foreach (var pair in cdnHeaders)
                Console.WriteLine("    {0}: {1}", pair.Key, pair.Value);

            string index = objectName;
            string error = objectName;
            string css = objectName;
            provider.EnableStaticWebOnContainer(containerName, index: index, error: error, listing: false);

            provider.DisableStaticWebOnContainer(containerName);

            provider.RemoveContainer(containerName, deleteObjects: true);
        }
#endif

        private static async Task<ReadOnlyCollection<Container>> ListAllContainersAsync(IObjectStorageService provider, int? pageSize, CancellationToken cancellationToken, IProgress<ReadOnlyCollection<Container>> progress)
        {
            if (pageSize <= 0)
                throw new ArgumentOutOfRangeException("pageSize");

            var firstPage = await provider.ListContainersAsync(pageSize, cancellationToken);
            return await firstPage.Item2.GetAllPagesAsync(cancellationToken, progress);
        }

        private static async Task<ReadOnlyCollection<ContainerObject>> ListAllObjectsAsync(IObjectStorageService provider, ContainerName container, int? pageSize, CancellationToken cancellationToken, IProgress<ReadOnlyCollection<ContainerObject>> progress)
        {
            if (pageSize <= 0)
                throw new ArgumentOutOfRangeException("pageSize");

            var firstPage = await provider.ListObjectsAsync(container, pageSize, CancellationToken.None);
            return await firstPage.Item2.GetAllPagesAsync(cancellationToken, progress);
        }

#if false // Rackspace-specific
        private static IEnumerable<ContainerCDN> ListAllCDNContainers(IObjectStorageService provider, int? blockSize = null, bool cdnEnabled = false, string region = null, CloudIdentity identity = null)
        {
            if (blockSize <= 0)
                throw new ArgumentOutOfRangeException("blockSize");

            ContainerCDN lastContainer = null;

            do
            {
                string marker = lastContainer != null ? lastContainer.Name : null;
                IEnumerable<ContainerCDN> containers = provider.ListCDNContainers(blockSize, marker, null, cdnEnabled, region, identity);
                lastContainer = null;
                foreach (ContainerCDN container in containers)
                {
                    lastContainer = container;
                    yield return container;
                }
            } while (lastContainer != null);
        }
#endif

        private static async Task<Dictionary<string, string>> GetContainerMetadataWithPrefix(IObjectStorageService provider, Container container, string prefix, CancellationToken cancellationToken)
        {
            ContainerMetadata metadata = await provider.GetContainerMetadataAsync(container.Name, cancellationToken);
            return FilterMetadataPrefix(metadata.Metadata, prefix);
        }

        private static async Task<string> GetObjectContentTypeAsync(IObjectStorageService provider, ContainerName container, ObjectName @object, CancellationToken cancellationToken)
        {
            ObjectMetadata headers = await provider.GetObjectMetadataAsync(container, @object, cancellationToken);

            string contentType;
            if (!headers.Headers.TryGetValue("Content-Type", out contentType))
                return null;

            return contentType.ToLowerInvariant();
        }

        #endregion

        #region Objects

        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.ObjectStorage)]
        public async Task TestGetObjectHeaders()
        {
            IObjectStorageService provider = CreateProvider();
            ContainerName containerName = new ContainerName(TestContainerPrefix + Path.GetRandomFileName());
            ObjectName objectName = new ObjectName(Path.GetRandomFileName());
            string objectData = "";

            await provider.CreateContainerAsync(containerName, CancellationToken.None);

            Stream stream = new MemoryStream(Encoding.UTF8.GetBytes(objectData));
            await provider.CreateObjectAsync(containerName, objectName, stream, CancellationToken.None, null);
            ObjectMetadata headers = await provider.GetObjectMetadataAsync(containerName, objectName, CancellationToken.None);
            Assert.IsNotNull(headers);
            Console.WriteLine("Headers");
            foreach (var pair in headers.Headers)
            {
                Assert.IsFalse(string.IsNullOrEmpty(pair.Key));
                Console.WriteLine("  {0}: {1}", pair.Key, pair.Value);
            }

            await RemoveContainerWithObjectsAsync(provider, containerName, CancellationToken.None);
        }

        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.ObjectStorage)]
        public async Task TestGetObjectMetaData()
        {
            IObjectStorageService provider = CreateProvider();
            ContainerName containerName = new ContainerName(TestContainerPrefix + Path.GetRandomFileName());
            ObjectName objectName = new ObjectName(Path.GetRandomFileName());
            string objectData = "";

            await provider.CreateContainerAsync(containerName, CancellationToken.None);

            Stream stream = new MemoryStream(Encoding.UTF8.GetBytes(objectData));
            await provider.CreateObjectAsync(containerName, objectName, stream, CancellationToken.None, null);
            Dictionary<string, string> metadata = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                { "Key1", "Value 1" },
                { "Key2", "Value ²" },
            };

            await provider.UpdateObjectMetadataAsync(containerName, objectName, new ObjectMetadata(new Dictionary<string, string>(), new Dictionary<string, string>(metadata, StringComparer.OrdinalIgnoreCase)), CancellationToken.None);

            ObjectMetadata actualMetadata = await provider.GetObjectMetadataAsync(containerName, objectName, CancellationToken.None);
            Console.WriteLine("Object Metadata");
            foreach (KeyValuePair<string, string> pair in actualMetadata.Metadata)
                Console.WriteLine("    {0}: {1}", pair.Key, pair.Value);

            CheckMetadataCollections(metadata, actualMetadata);

            await RemoveContainerWithObjectsAsync(provider, containerName, CancellationToken.None);
        }

        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.ObjectStorage)]
        public async Task TestUpdateObjectMetaData()
        {
            IObjectStorageService provider = CreateProvider();
            ContainerName containerName = new ContainerName(TestContainerPrefix + Path.GetRandomFileName());
            ObjectName objectName = new ObjectName(Path.GetRandomFileName());
            string objectData = "";
            string contentType = "text/plain-jane";

            await provider.CreateContainerAsync(containerName, CancellationToken.None);

            Stream stream = new MemoryStream(Encoding.UTF8.GetBytes(objectData));
            var preparedRequest = await provider.PrepareCreateObjectAsync(containerName, objectName, stream, CancellationToken.None, null);
            preparedRequest.RequestMessage.Content.Headers.ContentType = new MediaTypeHeaderValue(contentType);
            await preparedRequest.SendAsync(CancellationToken.None);
            Assert.AreEqual(contentType, await GetObjectContentTypeAsync(provider, containerName, objectName, CancellationToken.None));

            Dictionary<string, string> metadata = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                { "Key1", "Value 1" },
                { "Key2", "Value ²" },
            };

            await provider.UpdateObjectMetadataAsync(containerName, objectName, new ObjectMetadata(new Dictionary<string, string>(), new Dictionary<string, string>(metadata, StringComparer.OrdinalIgnoreCase)), CancellationToken.None);
            Assert.AreEqual(contentType, await GetObjectContentTypeAsync(provider, containerName, objectName, CancellationToken.None));

            ObjectMetadata actualMetadata = await provider.GetObjectMetadataAsync(containerName, objectName, CancellationToken.None);
            Console.WriteLine("Object Metadata");
            foreach (KeyValuePair<string, string> pair in actualMetadata.Metadata)
                Console.WriteLine("    {0}: {1}", pair.Key, pair.Value);

            CheckMetadataCollections(metadata, actualMetadata);

            metadata["Key2"] = "Value 2";
            Dictionary<string, string> updatedMetadata = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                { "Key2", "Value 2" }
            };
            await provider.SetObjectMetadataAsync(containerName, objectName, new ObjectMetadata(new Dictionary<string, string>(), new Dictionary<string, string>(updatedMetadata, StringComparer.OrdinalIgnoreCase)), CancellationToken.None);
            Assert.AreEqual(contentType, await GetObjectContentTypeAsync(provider, containerName, objectName, CancellationToken.None));

            actualMetadata = await provider.GetObjectMetadataAsync(containerName, objectName, CancellationToken.None);
            Console.WriteLine("Object Metadata");
            foreach (KeyValuePair<string, string> pair in actualMetadata.Metadata)
                Console.WriteLine("    {0}: {1}", pair.Key, pair.Value);

            CheckMetadataCollections(updatedMetadata, actualMetadata);

            await RemoveContainerWithObjectsAsync(provider, containerName, CancellationToken.None);
        }

        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.ObjectStorage)]
        public async Task TestDeleteObjectMetaData()
        {
            IObjectStorageService provider = CreateProvider();
            ContainerName containerName = new ContainerName(TestContainerPrefix + Path.GetRandomFileName());
            ObjectName objectName = new ObjectName(Path.GetRandomFileName());
            string objectData = "";

            await provider.CreateContainerAsync(containerName, CancellationToken.None);

            Stream stream = new MemoryStream(Encoding.UTF8.GetBytes(objectData));
            await provider.CreateObjectAsync(containerName, objectName, stream, CancellationToken.None, null);
            Dictionary<string, string> metadata = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                { "Key1", "Value 1" },
                { "Key2", "Value ²" },
                { "Key3", "Value 3" },
                { "Key4", "Value 4" },
            };

            await provider.UpdateObjectMetadataAsync(containerName, objectName, new ObjectMetadata(new Dictionary<string, string>(), new Dictionary<string, string>(metadata, StringComparer.OrdinalIgnoreCase)), CancellationToken.None);

            ObjectMetadata actualMetadata = await provider.GetObjectMetadataAsync(containerName, objectName, CancellationToken.None);
            Console.WriteLine("Object Metadata");
            foreach (KeyValuePair<string, string> pair in actualMetadata.Metadata)
                Console.WriteLine("    {0}: {1}", pair.Key, pair.Value);

            CheckMetadataCollections(metadata, actualMetadata);

            /* Check the overload which takes a single key
             */
            // remove Key3 first to make sure we still have a ² character in a remaining value
            metadata.Remove("Key3");
            await provider.RemoveObjectMetadataAsync(containerName, objectName, new[] { "Key3" }, CancellationToken.None);

            actualMetadata = await provider.GetObjectMetadataAsync(containerName, objectName, CancellationToken.None);
            Console.WriteLine("Object Metadata after removing Key3");
            foreach (KeyValuePair<string, string> pair in actualMetadata.Metadata)
                Console.WriteLine("    {0}: {1}", pair.Key, pair.Value);

            CheckMetadataCollections(metadata, actualMetadata);

            /* Check the overload which takes multiple keys
             */
            metadata.Remove("Key2");
            metadata.Remove("Key4");
            await provider.RemoveObjectMetadataAsync(containerName, objectName, new[] { "Key2", "Key4" }, CancellationToken.None);

            actualMetadata = await provider.GetObjectMetadataAsync(containerName, objectName, CancellationToken.None);
            Console.WriteLine("Object Metadata after removing Key2, Key4");
            foreach (KeyValuePair<string, string> pair in actualMetadata.Metadata)
                Console.WriteLine("    {0}: {1}", pair.Key, pair.Value);

            CheckMetadataCollections(metadata, actualMetadata);

            /* Check that duplicate removal is a NOP
             */
            metadata.Remove("Key2");
            metadata.Remove("Key4");
            await provider.RemoveObjectMetadataAsync(containerName, objectName, new[] { "Key2", "Key4" }, CancellationToken.None);

            actualMetadata = await provider.GetObjectMetadataAsync(containerName, objectName, CancellationToken.None);
            Console.WriteLine("Object Metadata after removing Key2, Key4");
            foreach (KeyValuePair<string, string> pair in actualMetadata.Metadata)
                Console.WriteLine("    {0}: {1}", pair.Key, pair.Value);

            CheckMetadataCollections(metadata, actualMetadata);

            /* Cleanup
             */
            await RemoveContainerWithObjectsAsync(provider, containerName, CancellationToken.None);
        }

        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.ObjectStorage)]
        public async Task TestListObjects()
        {
            IObjectStorageService provider = CreateProvider();
            ContainerName containerName = new ContainerName(TestContainerPrefix + Path.GetRandomFileName());
            ObjectName[] objectNames = { new ObjectName(Path.GetRandomFileName()), new ObjectName(Path.GetRandomFileName()) };
            // another random name counts as random content
            string fileData = Path.GetRandomFileName();

            await provider.CreateContainerAsync(containerName, CancellationToken.None);

            foreach (ObjectName objectName in objectNames)
            {
                using (MemoryStream uploadStream = new MemoryStream(Encoding.UTF8.GetBytes(fileData)))
                {
                    await provider.CreateObjectAsync(containerName, objectName, uploadStream, CancellationToken.None, null);
                }
            }

            Console.WriteLine("Objects in container {0}", containerName);
            foreach (ContainerObject containerObject in await ListAllObjectsAsync(provider, containerName, null, CancellationToken.None, null))
            {
                Console.WriteLine("  {0}", containerObject.Name);
            }

            /* Cleanup
             */
            await RemoveContainerWithObjectsAsync(provider, containerName, CancellationToken.None);
        }

        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.ObjectStorage)]
        public async Task TestSpecialCharacters()
        {
            IObjectStorageService provider = CreateProvider();
            string[] specialNames = { "#", " ", " lead", "trail ", "%", "x//x" };
            // another random name counts as random content
            string fileData = Path.GetRandomFileName();

            foreach (string containerSuffix in specialNames)
            {
                if (containerSuffix.IndexOf('/') >= 0)
                    continue;

                ContainerName containerName = new ContainerName(TestContainerPrefix + Path.GetRandomFileName() + containerSuffix);

                await provider.CreateContainerAsync(containerName, CancellationToken.None);

                foreach (string objectName in specialNames)
                {
                    using (MemoryStream uploadStream = new MemoryStream(Encoding.UTF8.GetBytes(fileData)))
                    {
                        await provider.CreateObjectAsync(containerName, new ObjectName(objectName), uploadStream, CancellationToken.None, null);
                    }
                }

                Console.WriteLine("Objects in container {0}", containerName);
                foreach (ContainerObject containerObject in await ListAllObjectsAsync(provider, containerName, null, CancellationToken.None, null))
                {
                    Console.WriteLine("  {0}", containerObject.Name);
                }

                /* Cleanup
                 */
                await RemoveContainerWithObjectsAsync(provider, containerName, CancellationToken.None);
            }
        }

#if false // API no longer included
        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.ObjectStorage)]
        public async Task TestCreateObjectFromFile_UseFileName()
        {
            IObjectStorageService provider = CreateProvider();
            ContainerName containerName = new ContainerName(TestContainerPrefix + Path.GetRandomFileName());
            ObjectName objectName = new ObjectName(Path.GetRandomFileName());
            // another random name counts as random content
            string fileData = Path.GetRandomFileName();
            string tempFilePath = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());

            await provider.CreateContainerAsync(containerName, CancellationToken.None);

            try
            {
                File.WriteAllText(tempFilePath, fileData, Encoding.UTF8);
                provider.CreateObjectFromFile(containerName, tempFilePath);

                // it's ok to create the same file twice
                ProgressMonitor progressMonitor = new ProgressMonitor(new FileInfo(tempFilePath).Length);
                provider.CreateObjectFromFile(containerName, tempFilePath, progressUpdated: progressMonitor.Updated);
                Assert.IsTrue(progressMonitor.IsComplete, "Failed to notify progress monitor callback of status update.");
            }
            finally
            {
                File.Remove(tempFilePath);
            }

            string actualData = ReadAllObjectText(provider, containerName, Path.GetFileName(tempFilePath), Encoding.UTF8, verifyEtag: true);
            Assert.AreEqual(fileData, actualData);

            /* Cleanup
             */
            provider.RemoveContainer(containerName, deleteObjects: true);
        }

        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.ObjectStorage)]
        public async Task TestCreateObjectFromFile_UseCustomObjectName()
        {
            IObjectStorageService provider = CreateProvider();
            ContainerName containerName = new ContainerName(TestContainerPrefix + Path.GetRandomFileName());
            ObjectName objectName = new ObjectName(Path.GetRandomFileName());
            // another random name counts as random content
            string fileData = Path.GetRandomFileName();
            string tempFilePath = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());

            ObjectStore containerResult = await provider.CreateContainerAsync(containerName, CancellationToken.None);
            Assert.AreEqual(ObjectStore.ContainerCreated, containerResult);

            try
            {
                File.WriteAllText(tempFilePath, fileData, Encoding.UTF8);
                provider.CreateObjectFromFile(containerName, tempFilePath, objectName);

                // it's ok to create the same file twice
                ProgressMonitor progressMonitor = new ProgressMonitor(new FileInfo(tempFilePath).Length);
                provider.CreateObjectFromFile(containerName, tempFilePath, objectName, progressUpdated: progressMonitor.Updated);
                Assert.IsTrue(progressMonitor.IsComplete, "Failed to notify progress monitor callback of status update.");
            }
            finally
            {
                File.Remove(tempFilePath);
            }

            string actualData = ReadAllObjectText(provider, containerName, objectName, Encoding.UTF8, verifyEtag: true);
            Assert.AreEqual(fileData, actualData);

            /* Cleanup
             */
            provider.RemoveContainer(containerName, deleteObjects: true);
        }
#endif

        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.ObjectStorage)]
        public async Task TestCreateObject()
        {
            IObjectStorageService provider = CreateProvider();
            ContainerName containerName = new ContainerName(TestContainerPrefix + Path.GetRandomFileName());
            ObjectName objectName = new ObjectName(Path.GetRandomFileName());
            // another random name counts as random content
            string fileData = Path.GetRandomFileName();

            await provider.CreateContainerAsync(containerName, CancellationToken.None);

            using (MemoryStream uploadStream = new MemoryStream(Encoding.UTF8.GetBytes(fileData)))
            {
                await provider.CreateObjectAsync(containerName, objectName, uploadStream, CancellationToken.None, null);
            }

            using (MemoryStream uploadStream = new MemoryStream(Encoding.UTF8.GetBytes(fileData)))
            {
                // it's ok to create the same file twice
                ProgressMonitor progressMonitor = new ProgressMonitor(uploadStream.Length);
                await provider.CreateObjectAsync(containerName, objectName, uploadStream, CancellationToken.None, progressMonitor);
                Assert.IsTrue(progressMonitor.IsComplete, "Failed to notify progress monitor callback of status update.");
            }

            string actualData = await ReadAllObjectTextAsync(provider, containerName, objectName, Encoding.UTF8, CancellationToken.None, verifyEtag: true);
            Assert.AreEqual(fileData, actualData);

            /* Cleanup
             */
            await RemoveContainerWithObjectsAsync(provider, containerName, CancellationToken.None);
        }

        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.ObjectStorage)]
        public async Task TestCreateObjectIfNoneMatch()
        {
            IObjectStorageService provider = CreateProvider();
            ContainerName containerName = new ContainerName(TestContainerPrefix + Path.GetRandomFileName());
            ObjectName objectName = new ObjectName(Path.GetRandomFileName());
            // another random name counts as random content
            char[] fileDataChars = new char[5000];
            for (int i = 0; i < fileDataChars.Length; i++)
                fileDataChars[i] = (char)i;

            string fileData = new string(fileDataChars);

            await provider.CreateContainerAsync(containerName, CancellationToken.None);

            using (MemoryStream uploadStream = new MemoryStream(Encoding.UTF8.GetBytes(fileData)))
            {
                await provider.CreateObjectAsync(containerName, objectName, uploadStream, CancellationToken.None, null);
            }

            using (MemoryStream uploadStream = new MemoryStream(Encoding.UTF8.GetBytes(fileData)))
            {
                ProgressMonitor progressMonitor = new ProgressMonitor(uploadStream.Length);
                try
                {
                    HttpApiCall prepared = await provider.PrepareCreateObjectAsync(containerName, objectName, uploadStream, CancellationToken.None, progressMonitor);
                    prepared.RequestMessage.Headers.Add("If-None-Match", "*");
                    await prepared.SendAsync(CancellationToken.None);
                    Assert.Fail("Expected a 412 (Precondition Failed)");
                }
                catch (HttpWebException ex)
                {
                    Assert.IsNotNull(ex.ResponseMessage);
                    Assert.AreEqual(HttpStatusCode.PreconditionFailed, ex.ResponseMessage.StatusCode);
                    Assert.AreEqual(0, progressMonitor.CurrentValue);
                }
            }

            using (MemoryStream uploadStream = new MemoryStream(Encoding.UTF8.GetBytes(fileData)))
            {
                ProgressMonitor progressMonitor = new ProgressMonitor(uploadStream.Length);
                try
                {
                    HttpApiCall prepared = await provider.PrepareCreateObjectAsync(containerName, objectName, uploadStream, CancellationToken.None, progressMonitor);
                    prepared.RequestMessage.Headers.Add("If-None-Match", "*");
                    await prepared.SendAsync(CancellationToken.None);
                    Assert.Fail("Expected a 412 (Precondition Failed)");
                }
                catch (HttpWebException ex)
                {
                    Assert.IsNotNull(ex.ResponseMessage);
                    Assert.AreEqual(HttpStatusCode.PreconditionFailed, ex.ResponseMessage.StatusCode);
                    Assert.AreEqual(0, progressMonitor.CurrentValue);
                }
            }

            string actualData = await ReadAllObjectTextAsync(provider, containerName, objectName, Encoding.UTF8, CancellationToken.None, verifyEtag: true);
            Assert.AreEqual(fileData, actualData);

            /* Cleanup
             */
            await RemoveContainerWithObjectsAsync(provider, containerName, CancellationToken.None);
        }

        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.ObjectStorage)]
        public async Task TestCreateObjectWithMetadata()
        {
            IObjectStorageService provider = CreateProvider();
            ContainerName containerName = new ContainerName(TestContainerPrefix + Path.GetRandomFileName());
            ObjectName objectName = new ObjectName(Path.GetRandomFileName());
            // another random name counts as random content
            string fileData = Path.GetRandomFileName();

            await provider.CreateContainerAsync(containerName, CancellationToken.None);

            using (MemoryStream uploadStream = new MemoryStream(Encoding.UTF8.GetBytes(fileData)))
            {
                Dictionary<string, string> headers = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
                {
                    { "X-Object-Meta-Projectcode", "ProjectCode" },
                    { "X-Object-Meta-Filedesc", "FileDescription" },
                    { "X-Object-Meta-Usercode", "User Code" },
                };
                var prepared = await provider.PrepareCreateObjectAsync(containerName, objectName, uploadStream, CancellationToken.None, null);
                foreach (var pair in headers)
                    prepared.RequestMessage.Headers.Add(pair.Key, pair.Value);
                await prepared.SendAsync(CancellationToken.None);
            }

            string actualData = await ReadAllObjectTextAsync(provider, containerName, objectName, Encoding.UTF8, CancellationToken.None, verifyEtag: true);
            Assert.AreEqual(fileData, actualData);

            ObjectMetadata metadata = await provider.GetObjectMetadataAsync(containerName, objectName, CancellationToken.None);
            Assert.AreEqual("ProjectCode", metadata.Metadata["projectcode"]);
            Assert.AreEqual("FileDescription", metadata.Metadata["fileDesc"]);
            Assert.AreEqual("User Code", metadata.Metadata["usercode"]);

            /* Cleanup
             */
            await RemoveContainerWithObjectsAsync(provider, containerName, CancellationToken.None);
        }

#if false // API not yet defined
        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.ObjectStorage)]
        [DeploymentItem("DarkKnightRises.jpg")]
        public async Task TestCreateLargeObject()
        {
            IObjectStorageService provider = CreateProvider();
            ((CloudFilesProvider)provider).LargeFileBatchThreshold = 81920;

            ContainerName containerName = new ContainerName(TestContainerPrefix + Path.GetRandomFileName());
            string sourceFileName = "DarkKnightRises.jpg";
            byte[] content = File.ReadAllBytes("DarkKnightRises.jpg");

            await provider.CreateContainerAsync(containerName, CancellationToken.None);

            ProgressMonitor progressMonitor = new ProgressMonitor(content.Length);
            provider.CreateObjectFromFile(containerName, sourceFileName, progressUpdated: progressMonitor.Updated);
            Assert.IsTrue(progressMonitor.IsComplete, "Failed to notify progress monitor callback of status update.");

            using (MemoryStream downloadStream = new MemoryStream())
            {
                provider.GetObject(containerName, sourceFileName, downloadStream);
                Assert.AreEqual(content.Length, GetContainerObjectSize(provider, containerName, sourceFileName));

                downloadStream.Position = 0;
                byte[] actualData = new byte[downloadStream.Length];
                downloadStream.Read(actualData, 0, actualData.Length);
                Assert.AreEqual(content.Length, actualData.Length);
                using (MD5 md5 = MD5.Create())
                {
                    byte[] contentMd5 = md5.ComputeHash(content);
                    byte[] actualMd5 = md5.ComputeHash(actualData);
                    Assert.AreEqual(BitConverter.ToString(contentMd5), BitConverter.ToString(actualMd5));
                }
            }

            /* Cleanup
             */
            provider.RemoveContainer(containerName, deleteObjects: true);
        }

        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.ObjectStorage)]
        [DeploymentItem("DarkKnightRises.jpg")]
        public async Task TestVerifyLargeObjectETag()
        {
            IObjectStorageService provider = CreateProvider();
            ((CloudFilesProvider)provider).LargeFileBatchThreshold = 81920;

            ContainerName containerName = new ContainerName(TestContainerPrefix + Path.GetRandomFileName());
            string sourceFileName = "DarkKnightRises.jpg";
            byte[] content = File.ReadAllBytes("DarkKnightRises.jpg");

            ObjectStore containerResult = await provider.CreateContainerAsync(containerName, CancellationToken.None);
            Assert.AreEqual(ObjectStore.ContainerCreated, containerResult);

            ProgressMonitor progressMonitor = new ProgressMonitor(content.Length);
            provider.CreateObjectFromFile(containerName, sourceFileName, progressUpdated: progressMonitor.Updated);
            Assert.IsTrue(progressMonitor.IsComplete, "Failed to notify progress monitor callback of status update.");

            try
            {
                using (MemoryStream downloadStream = new MemoryStream())
                {
                    provider.GetObject(containerName, sourceFileName, downloadStream, verifyEtag: true);

                    Assert.AreEqual(content.Length, GetContainerObjectSize(provider, containerName, sourceFileName));

                    downloadStream.Position = 0;
                    byte[] actualData = new byte[downloadStream.Length];
                    downloadStream.Read(actualData, 0, actualData.Length);
                    Assert.AreEqual(content.Length, actualData.Length);
                    using (MD5 md5 = MD5.Create())
                    {
                        byte[] contentMd5 = md5.ComputeHash(content);
                        byte[] actualMd5 = md5.ComputeHash(actualData);
                        Assert.AreEqual(BitConverter.ToString(contentMd5), BitConverter.ToString(actualMd5));
                    }
                }

                /* Cleanup
                 */
                provider.RemoveContainer(containerName, deleteObjects: true);
            }
            catch (NotSupportedException)
            {
                Assert.Inconclusive("The provider does not support verifying ETags for large objects.");
            }
        }

        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.ObjectStorage)]
        [DeploymentItem("DarkKnightRises.jpg")]
        public async Task TestExtractArchiveTar()
        {
            CloudFilesProvider provider = (CloudFilesProvider)Bootstrapper.CreateObjectStorageProvider();
            ContainerName containerName = new ContainerName(TestContainerPrefix + Path.GetRandomFileName());
            string sourceFileName = "DarkKnightRises.jpg";
            byte[] content = File.ReadAllBytes("DarkKnightRises.jpg");
            using (MemoryStream outputStream = new MemoryStream())
            {
                using (TarArchive archive = TarArchive.CreateOutputTarArchive(outputStream))
                {
                    archive.IsStreamOwner = false;
                    archive.RootPath = Path.GetDirectoryName(Path.GetFullPath(sourceFileName)).Replace('\\', '/');
                    TarEntry entry = TarEntry.CreateEntryFromFile(sourceFileName);
                    archive.WriteEntry(entry, true);
                    archive.Close();
                }

                outputStream.Flush();
                outputStream.Position = 0;
                ExtractArchiveResponse response = provider.ExtractArchive(outputStream, containerName, ArchiveFormat.Tar);
                Assert.IsNotNull(response);
                Assert.AreEqual(1, response.CreatedFiles);
                Assert.IsNotNull(response.Errors);
                Assert.AreEqual(0, response.Errors.Count);
            }

            using (MemoryStream downloadStream = new MemoryStream())
            {
                provider.GetObject(containerName, sourceFileName, downloadStream, verifyEtag: true);
                Assert.AreEqual(content.Length, GetContainerObjectSize(provider, containerName, sourceFileName));

                downloadStream.Position = 0;
                byte[] actualData = new byte[downloadStream.Length];
                downloadStream.Read(actualData, 0, actualData.Length);
                Assert.AreEqual(content.Length, actualData.Length);
                using (MD5 md5 = MD5.Create())
                {
                    byte[] contentMd5 = md5.ComputeHash(content);
                    byte[] actualMd5 = md5.ComputeHash(actualData);
                    Assert.AreEqual(BitConverter.ToString(contentMd5), BitConverter.ToString(actualMd5));
                }
            }

            /* Cleanup
             */
            provider.RemoveContainer(containerName, deleteObjects: true);
        }

        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.ObjectStorage)]
        [DeploymentItem("DarkKnightRises.jpg")]
        public async Task TestExtractArchiveTarGz()
        {
            CloudFilesProvider provider = (CloudFilesProvider)Bootstrapper.CreateObjectStorageProvider();
            ContainerName containerName = new ContainerName(TestContainerPrefix + Path.GetRandomFileName());
            string sourceFileName = "DarkKnightRises.jpg";
            byte[] content = File.ReadAllBytes("DarkKnightRises.jpg");
            using (MemoryStream outputStream = new MemoryStream())
            {
                using (GZipOutputStream gzoStream = new GZipOutputStream(outputStream))
                {
                    gzoStream.IsStreamOwner = false;
                    gzoStream.SetLevel(9);
                    using (TarArchive archive = TarArchive.CreateOutputTarArchive(gzoStream))
                    {
                        archive.IsStreamOwner = false;
                        archive.RootPath = Path.GetDirectoryName(Path.GetFullPath(sourceFileName)).Replace('\\', '/');
                        TarEntry entry = TarEntry.CreateEntryFromFile(sourceFileName);
                        archive.WriteEntry(entry, true);
                        archive.Close();
                    }
                }

                outputStream.Flush();
                outputStream.Position = 0;
                ExtractArchiveResponse response = provider.ExtractArchive(outputStream, containerName, ArchiveFormat.TarGz);
                Assert.IsNotNull(response);
                Assert.AreEqual(1, response.CreatedFiles);
                Assert.IsNotNull(response.Errors);
                Assert.AreEqual(0, response.Errors.Count);
            }

            using (MemoryStream downloadStream = new MemoryStream())
            {
                provider.GetObject(containerName, sourceFileName, downloadStream, verifyEtag: true);
                Assert.AreEqual(content.Length, GetContainerObjectSize(provider, containerName, sourceFileName));

                downloadStream.Position = 0;
                byte[] actualData = new byte[downloadStream.Length];
                downloadStream.Read(actualData, 0, actualData.Length);
                Assert.AreEqual(content.Length, actualData.Length);
                using (MD5 md5 = MD5.Create())
                {
                    byte[] contentMd5 = md5.ComputeHash(content);
                    byte[] actualMd5 = md5.ComputeHash(actualData);
                    Assert.AreEqual(BitConverter.ToString(contentMd5), BitConverter.ToString(actualMd5));
                }
            }

            /* Cleanup
             */
            provider.RemoveContainer(containerName, deleteObjects: true);
        }

        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.ObjectStorage)]
        [DeploymentItem("DarkKnightRises.jpg")]
        public async Task TestExtractArchiveTarBz2()
        {
            CloudFilesProvider provider = (CloudFilesProvider)Bootstrapper.CreateObjectStorageProvider();
            ContainerName containerName = new ContainerName(TestContainerPrefix + Path.GetRandomFileName());
            string sourceFileName = "DarkKnightRises.jpg";
            byte[] content = File.ReadAllBytes("DarkKnightRises.jpg");
            using (MemoryStream outputStream = new MemoryStream())
            {
                using (BZip2OutputStream bz2Stream = new BZip2OutputStream(outputStream))
                {
                    bz2Stream.IsStreamOwner = false;
                    using (TarArchive archive = TarArchive.CreateOutputTarArchive(bz2Stream))
                    {
                        archive.IsStreamOwner = false;
                        archive.RootPath = Path.GetDirectoryName(Path.GetFullPath(sourceFileName)).Replace('\\', '/');
                        TarEntry entry = TarEntry.CreateEntryFromFile(sourceFileName);
                        archive.WriteEntry(entry, true);
                        archive.Close();
                    }
                }

                outputStream.Flush();
                outputStream.Position = 0;
                ExtractArchiveResponse response = provider.ExtractArchive(outputStream, containerName, ArchiveFormat.TarBz2);
                Assert.IsNotNull(response);
                Assert.AreEqual(1, response.CreatedFiles);
                Assert.IsNotNull(response.Errors);
                Assert.AreEqual(0, response.Errors.Count);
            }

            using (MemoryStream downloadStream = new MemoryStream())
            {
                provider.GetObject(containerName, sourceFileName, downloadStream, verifyEtag: true);
                Assert.AreEqual(content.Length, GetContainerObjectSize(provider, containerName, sourceFileName));

                downloadStream.Position = 0;
                byte[] actualData = new byte[downloadStream.Length];
                downloadStream.Read(actualData, 0, actualData.Length);
                Assert.AreEqual(content.Length, actualData.Length);
                using (MD5 md5 = MD5.Create())
                {
                    byte[] contentMd5 = md5.ComputeHash(content);
                    byte[] actualMd5 = md5.ComputeHash(actualData);
                    Assert.AreEqual(BitConverter.ToString(contentMd5), BitConverter.ToString(actualMd5));
                }
            }

            /* Cleanup
             */
            provider.RemoveContainer(containerName, deleteObjects: true);
        }

        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.ObjectStorage)]
        [DeploymentItem("DarkKnightRises.jpg")]
        public async Task TestExtractArchiveTarGzCreateContainer()
        {
            CloudFilesProvider provider = (CloudFilesProvider)Bootstrapper.CreateObjectStorageProvider();
            ContainerName containerName = new ContainerName(TestContainerPrefix + Path.GetRandomFileName());
            string sourceFileName = "DarkKnightRises.jpg";
            byte[] content = File.ReadAllBytes("DarkKnightRises.jpg");
            using (MemoryStream outputStream = new MemoryStream())
            {
                using (GZipOutputStream gzoStream = new GZipOutputStream(outputStream))
                {
                    gzoStream.IsStreamOwner = false;
                    gzoStream.SetLevel(9);
                    using (TarOutputStream tarOutputStream = new TarOutputStream(gzoStream))
                    {
                        tarOutputStream.IsStreamOwner = false;
                        TarEntry entry = TarEntry.CreateTarEntry(containerName + '/' + sourceFileName);
                        entry.Size = content.Length;
                        tarOutputStream.PutNextEntry(entry);
                        tarOutputStream.Write(content, 0, content.Length);
                        tarOutputStream.CloseEntry();
                        tarOutputStream.Close();
                    }
                }

                outputStream.Flush();
                outputStream.Position = 0;
                ExtractArchiveResponse response = provider.ExtractArchive(outputStream, "", ArchiveFormat.TarGz);
                Assert.IsNotNull(response);
                Assert.AreEqual(1, response.CreatedFiles);
                Assert.IsNotNull(response.Errors);
                Assert.AreEqual(0, response.Errors.Count);
            }

            using (MemoryStream downloadStream = new MemoryStream())
            {
                provider.GetObject(containerName, sourceFileName, downloadStream, verifyEtag: true);
                Assert.AreEqual(content.Length, GetContainerObjectSize(provider, containerName, sourceFileName));

                downloadStream.Position = 0;
                byte[] actualData = new byte[downloadStream.Length];
                downloadStream.Read(actualData, 0, actualData.Length);
                Assert.AreEqual(content.Length, actualData.Length);
                using (MD5 md5 = MD5.Create())
                {
                    byte[] contentMd5 = md5.ComputeHash(content);
                    byte[] actualMd5 = md5.ComputeHash(actualData);
                    Assert.AreEqual(BitConverter.ToString(contentMd5), BitConverter.ToString(actualMd5));
                }
            }

            /* Cleanup
             */
            provider.RemoveContainer(containerName, deleteObjects: true);
        }
#endif

        private static async Task<long> GetContainerObjectSize(IObjectStorageService provider, ContainerName container, ObjectName @object, CancellationToken cancellationToken)
        {
            ObjectMetadata headers = await provider.GetObjectMetadataAsync(container, @object, cancellationToken);
            return long.Parse(headers.Headers["Content-Length"]);
        }

        private class ProgressMonitor : IProgress<long>
        {
            private readonly long _maxValue;
            private long _currentValue;

            public ProgressMonitor(long totalSize)
            {
                _maxValue = totalSize;
            }

            public long CurrentValue
            {
                get
                {
                    return _currentValue;
                }
            }

            public long MaxValue
            {
                get
                {
                    return _maxValue;
                }
            }

            public bool IsComplete
            {
                get
                {
                    return _currentValue == _maxValue;
                }
            }

            public void Report(long value)
            {
                Assert.IsTrue(value >= 0);
                Assert.IsTrue(value <= _maxValue);
                Assert.IsTrue(value >= _currentValue);
                _currentValue = value;
            }
        }

#if false // no API yet
        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.ObjectStorage)]
        public async Task TestGetObjectSaveToFile()
        {
            IObjectStorageService provider = CreateProvider();
            ContainerName containerName = new ContainerName(TestContainerPrefix + Path.GetRandomFileName());
            ObjectName objectName = new ObjectName(Path.GetRandomFileName());
            // another random name counts as random content
            string fileData = Path.GetRandomFileName();

            ObjectStore containerResult = await provider.CreateContainerAsync(containerName, CancellationToken.None);
            Assert.AreEqual(ObjectStore.ContainerCreated, containerResult);

            using (MemoryStream uploadStream = new MemoryStream(Encoding.UTF8.GetBytes(fileData)))
            {
                provider.CreateObject(containerName, uploadStream, objectName);
            }

            try
            {
                provider.GetObjectSaveToFile(containerName, Path.GetTempPath(), objectName, verifyEtag: true);
                Assert.AreEqual(fileData, File.ReadAllText(Path.Combine(Path.GetTempPath(), objectName), Encoding.UTF8));

                // it's ok to download the same file twice
                ProgressMonitor progressMonitor = new ProgressMonitor(GetContainerObjectSize(provider, containerName, objectName));
                provider.GetObjectSaveToFile(containerName, Path.GetTempPath(), objectName, progressUpdated: progressMonitor.Updated, verifyEtag: true);
                Assert.IsTrue(progressMonitor.IsComplete, "Failed to notify progress monitor callback of status update.");
            }
            finally
            {
                File.Remove(Path.Combine(Path.GetTempPath(), objectName));
            }

            string tempFileName = Path.GetRandomFileName();
            try
            {
                provider.GetObjectSaveToFile(containerName, Path.GetTempPath(), objectName, tempFileName, verifyEtag: true);
                Assert.AreEqual(fileData, File.ReadAllText(Path.Combine(Path.GetTempPath(), tempFileName), Encoding.UTF8));

                // it's ok to download the same file twice
                ProgressMonitor progressMonitor = new ProgressMonitor(GetContainerObjectSize(provider, containerName, objectName));
                provider.GetObjectSaveToFile(containerName, Path.GetTempPath(), objectName, progressUpdated: progressMonitor.Updated, verifyEtag: true);
                Assert.IsTrue(progressMonitor.IsComplete, "Failed to notify progress monitor callback of status update.");
            }
            finally
            {
                File.Remove(Path.Combine(Path.GetTempPath(), tempFileName));
            }

            /* Cleanup
             */
            provider.RemoveContainer(containerName, deleteObjects: true);
        }
#endif

        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.ObjectStorage)]
        public async Task TestCopyObject()
        {
            IObjectStorageService provider = CreateProvider();
            ContainerName containerName = new ContainerName(TestContainerPrefix + Path.GetRandomFileName());
            ObjectName objectName = new ObjectName(Path.GetRandomFileName());
            ObjectName copiedName = new ObjectName(Path.GetRandomFileName());
            // another random name counts as random content
            string fileData = Path.GetRandomFileName();
            string contentType = "text/plain-jane";

            await provider.CreateContainerAsync(containerName, CancellationToken.None);

            using (MemoryStream uploadStream = new MemoryStream(Encoding.UTF8.GetBytes(fileData)))
            {
                var prepared = await provider.PrepareCreateObjectAsync(containerName, objectName, uploadStream, CancellationToken.None, null);
                prepared.RequestMessage.Content.Headers.ContentType = new MediaTypeHeaderValue(contentType);
                await prepared.SendAsync(CancellationToken.None);
            }

            string actualData = await ReadAllObjectTextAsync(provider, containerName, objectName, Encoding.UTF8, CancellationToken.None, verifyEtag: true);
            Assert.AreEqual(fileData, actualData);

            await provider.CopyObjectAsync(containerName, objectName, containerName, copiedName, CancellationToken.None);

            // make sure the item is available at the copied location
            actualData = await ReadAllObjectTextAsync(provider, containerName, copiedName, Encoding.UTF8, CancellationToken.None, verifyEtag: true);
            Assert.AreEqual(fileData, actualData);

            // make sure the original object still exists
            actualData = await ReadAllObjectTextAsync(provider, containerName, objectName, Encoding.UTF8, CancellationToken.None, verifyEtag: true);
            Assert.AreEqual(fileData, actualData);

            // make sure the content type was not changed by the copy operation
            Assert.AreEqual(contentType, await GetObjectContentTypeAsync(provider, containerName, objectName, CancellationToken.None));
            Assert.AreEqual(contentType, await GetObjectContentTypeAsync(provider, containerName, copiedName, CancellationToken.None));

            /* Cleanup
             */
            await RemoveContainerWithObjectsAsync(provider, containerName, CancellationToken.None);
        }

#if false // no API yet
        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.ObjectStorage)]
        public async Task TestMoveObject()
        {
            IObjectStorageService provider = CreateProvider();
            ContainerName containerName = new ContainerName(TestContainerPrefix + Path.GetRandomFileName());
            ObjectName objectName = new ObjectName(Path.GetRandomFileName());
            ObjectName movedName = new ObjectName(Path.GetRandomFileName());
            // another random name counts as random content
            string fileData = Path.GetRandomFileName();

            await provider.CreateContainerAsync(containerName, CancellationToken.None);

            using (MemoryStream uploadStream = new MemoryStream(Encoding.UTF8.GetBytes(fileData)))
            {
                await provider.CreateObjectAsync(containerName, objectName, uploadStream, CancellationToken.None, null);
            }

            string actualData = await ReadAllObjectTextAsync(provider, containerName, objectName, Encoding.UTF8, CancellationToken.None, verifyEtag: true);
            Assert.AreEqual(fileData, actualData);

            provider.MoveObjectAsync(containerName, objectName, containerName, movedName, CancellationToken.None);

            try
            {
                using (MemoryStream downloadStream = new MemoryStream())
                {
                    var data = await provider.GetObjectAsync(containerName, objectName, CancellationToken.None);
                    await data.Item2.CopyToAsync(downloadStream);
                }

                Assert.Fail("Expected an exception (object should not exist)");
            }
            catch (HttpWebException)
            {
            }

            actualData = await ReadAllObjectTextAsync(provider, containerName, movedName, Encoding.UTF8, CancellationToken.None, verifyEtag: true);
            Assert.AreEqual(fileData, actualData);

            /* Cleanup
             */
            await provider.RemoveContainerAsync(containerName, CancellationToken.None);
        }
#endif

        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.ObjectStorage)]
        public async Task TestDeleteObject()
        {
            IObjectStorageService provider = CreateProvider();
            ContainerName containerName = new ContainerName(TestContainerPrefix + Path.GetRandomFileName());
            ObjectName objectName = new ObjectName(Path.GetRandomFileName());
            // another random name counts as random content
            string fileData = Path.GetRandomFileName();

            await provider.CreateContainerAsync(containerName, CancellationToken.None);

            using (MemoryStream uploadStream = new MemoryStream(Encoding.UTF8.GetBytes(fileData)))
            {
                await provider.CreateObjectAsync(containerName, objectName, uploadStream, CancellationToken.None, null);
            }

            string actualData = await ReadAllObjectTextAsync(provider, containerName, objectName, Encoding.UTF8, CancellationToken.None, verifyEtag: true);
            Assert.AreEqual(fileData, actualData);

            await provider.RemoveObjectAsync(containerName, objectName, CancellationToken.None);

            try
            {
                using (MemoryStream downloadStream = new MemoryStream())
                {
                    var data = await provider.GetObjectAsync(containerName, objectName, CancellationToken.None);
                    await data.Item2.CopyToAsync(downloadStream, 4096, CancellationToken.None);
                }

                Assert.Fail("Expected an exception (object should not exist)");
            }
            catch (HttpWebException)
            {
            }

            /* Cleanup
             */
            await provider.RemoveContainerAsync(containerName, CancellationToken.None);
        }

        #endregion

        #region Accounts

        /// <summary>
        /// This test can be used to clear all of the metadata associated with an account.
        /// </summary>
        /// <remarks>
        /// This test is normally disabled. To run the cleanup method, comment out or remove the
        /// <see cref="IgnoreAttribute"/>.
        /// </remarks>
        [TestMethod]
        [TestCategory(TestCategories.Cleanup)]
        [Ignore]
        public async Task CleanupAllAccountMetadata()
        {
            IObjectStorageService provider = CreateProvider();
            AccountMetadata metadata = await provider.GetAccountMetadataAsync(CancellationToken.None);
            await provider.RemoveAccountMetadataAsync(metadata.Metadata.Keys, CancellationToken.None);
        }

        /// <summary>
        /// This unit test clears the metadata associated with the account which is
        /// created by the unit tests in this class.
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategories.Cleanup)]
        public async Task CleanupTestAccountMetadata()
        {
            IObjectStorageService provider = CreateProvider();
            Dictionary<string, string> metadata = await GetAccountMetadataWithPrefixAsync(provider, TestKeyPrefix, CancellationToken.None);
            await provider.RemoveAccountMetadataAsync(metadata.Keys, CancellationToken.None);
        }

        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.ObjectStorage)]
        public async Task TestGetAccountHeaders()
        {
            IObjectStorageService provider = CreateProvider();
            AccountMetadata headers = await provider.GetAccountMetadataAsync(CancellationToken.None);
            Assert.IsNotNull(headers);

            Console.WriteLine("Account Headers:");
            foreach (var pair in headers.Headers)
            {
                Assert.IsNotNull(pair.Key);
                Assert.IsNotNull(pair.Value);
                Assert.IsFalse(string.IsNullOrEmpty(pair.Key));
                Console.WriteLine("    {0}: {1}", pair.Key, pair.Value);
            }

            string containerCountText;
            Assert.IsTrue(headers.Headers.TryGetValue(net.openstack.Providers.Rackspace.CloudFilesProvider.AccountContainerCount, out containerCountText));
            long containerCount;
            Assert.IsTrue(long.TryParse(containerCountText, out containerCount));
            Assert.IsTrue(containerCount >= 0);

            string accountBytesText;
            Assert.IsTrue(headers.Headers.TryGetValue(net.openstack.Providers.Rackspace.CloudFilesProvider.AccountBytesUsed, out accountBytesText));
            long accountBytes;
            Assert.IsTrue(long.TryParse(accountBytesText, out accountBytes));
            Assert.IsTrue(accountBytes >= 0);

            string objectCountText;
            if (headers.Headers.TryGetValue(net.openstack.Providers.Rackspace.CloudFilesProvider.AccountObjectCount, out objectCountText))
            {
                // the X-Account-Object-Count header is optional, but when included should be a non-negative integer
                long objectCount;
                Assert.IsTrue(long.TryParse(objectCountText, out objectCount));
                Assert.IsTrue(objectCount >= 0);
            }
        }

        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.ObjectStorage)]
        public async Task TestGetAccountMetaData()
        {
            IObjectStorageService provider = CreateProvider();
            AccountMetadata metadata = await provider.GetAccountMetadataAsync(CancellationToken.None);
            Assert.IsNotNull(metadata);

            Console.WriteLine("Account MetaData:");
            foreach (var pair in metadata.Metadata)
            {
                Assert.IsNotNull(pair.Key);
                Assert.IsNotNull(pair.Value);
                Assert.IsFalse(string.IsNullOrEmpty(pair.Key));
                Console.WriteLine("    {0}: {1}", pair.Key, pair.Value);
            }
        }

        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.ObjectStorage)]
        public async Task TestAccountHeaderKeyCharacters()
        {
            IObjectStorageService provider = CreateProvider();

            List<char> keyCharList = new List<char>();
            for (char i = MinHeaderKeyCharacter; i <= MaxHeaderKeyCharacter; i++)
            {
                if (!SeparatorCharacters.Contains(i) && !NotSupportedCharacters.Contains(i))
                    keyCharList.Add(i);
            }

            string key = TestKeyPrefix + new string(keyCharList.ToArray());
            Console.WriteLine("Expected key: {0}", key);

            await provider.UpdateAccountMetadataAsync(
                new AccountMetadata(
                    new Dictionary<string, string>(),
                    new Dictionary<string, string>
                    {
                        { key, "Value" }
                    }),
                CancellationToken.None);

            AccountMetadata metadata = await provider.GetAccountMetadataAsync(CancellationToken.None);
            Assert.IsNotNull(metadata);

            string value;
            Assert.IsTrue(metadata.Metadata.TryGetValue(key, out value));
            Assert.AreEqual("Value", value);

            await provider.UpdateAccountMetadataAsync(
                new AccountMetadata(
                    new Dictionary<string, string>(),
                    new Dictionary<string, string>
                    {
                        { key, null }
                    }),
                CancellationToken.None);

            metadata = await provider.GetAccountMetadataAsync(CancellationToken.None);
            Assert.IsNotNull(metadata);
            Assert.IsFalse(metadata.Metadata.TryGetValue(key, out value));
        }

        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.ObjectStorage)]
        public async Task TestAccountInvalidHeaderKeyCharacters()
        {
            IObjectStorageService provider = CreateProvider();

            List<char> validKeyCharList = new List<char>();
            for (char i = MinHeaderKeyCharacter; i <= MaxHeaderKeyCharacter; i++)
            {
                if (!SeparatorCharacters.Contains(i) && !NotSupportedCharacters.Contains(i))
                    validKeyCharList.Add(i);
            }

            for (int i = char.MinValue; i <= char.MaxValue; i++)
            {
                if (validKeyCharList.BinarySearch((char)i) >= 0)
                    continue;

                string invalidKey = new string((char)i, 1);

                try
                {
                    await provider.UpdateAccountMetadataAsync(
                        new AccountMetadata(
                            new Dictionary<string, string>(),
                            new Dictionary<string, string>
                            {
                                { invalidKey, "Value" }
                            }),
                        CancellationToken.None);
                    Assert.Fail("Should throw an exception for invalid keys.");
                }
                catch (FormatException)
                {
                    if (i >= MinHeaderKeyCharacter && i <= MaxHeaderKeyCharacter)
                        StringAssert.Contains(SeparatorCharacters, invalidKey);
                }
                catch (NotSupportedException)
                {
                    StringAssert.Contains(NotSupportedCharacters, invalidKey);
                }
            }
        }

        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.ObjectStorage)]
        public async Task TestUpdateAccountMetadata()
        {
            IObjectStorageService provider = CreateProvider();
            AccountMetadata metadata = await provider.GetAccountMetadataAsync(CancellationToken.None);
            if (metadata.Metadata.Any(i => i.Key.StartsWith(TestKeyPrefix, StringComparison.OrdinalIgnoreCase)))
            {
                Assert.Inconclusive("The account contains metadata from a previous unit test run. Run CleanupTestAccountMetadata and try again.");
                return;
            }

            // test add metadata
            await TestGetAccountMetaData();
            await provider.UpdateAccountMetadataAsync(
                new AccountMetadata(
                    new Dictionary<string, string>(),
                    new Dictionary<string, string>
                    {
                        { TestKeyPrefix + "1", "Value ĳ" },
                        { TestKeyPrefix + "2", "Value ²" },
                    }),
                CancellationToken.None);
            await TestGetAccountMetaData();

            Dictionary<string, string> expected =
                new Dictionary<string, string>
            {
                { TestKeyPrefix + "1", "Value ĳ" },
                { TestKeyPrefix + "2", "Value ²" },
            };
            CheckMetadataCollections(expected, new AccountMetadata(new Dictionary<string, string>(), await GetAccountMetadataWithPrefixAsync(provider, TestKeyPrefix, CancellationToken.None)));

            // test update metadata
            await provider.UpdateAccountMetadataAsync(
                new AccountMetadata(
                    new Dictionary<string, string>(),
                    new Dictionary<string, string>
                    {
                        { TestKeyPrefix + "1", "Value 1" },
                    }),
                CancellationToken.None);

            expected = new Dictionary<string, string>
            {
                { TestKeyPrefix + "1", "Value 1" },
                { TestKeyPrefix + "2", "Value ²" },
            };
            CheckMetadataCollections(expected, new AccountMetadata(new Dictionary<string, string>(), await GetAccountMetadataWithPrefixAsync(provider, TestKeyPrefix, CancellationToken.None)));

            // test remove metadata
            await provider.UpdateAccountMetadataAsync(
                new AccountMetadata(
                    new Dictionary<string, string>(),
                    new Dictionary<string, string>
                    {
                        { TestKeyPrefix + "1", null },
                        { TestKeyPrefix + "2", string.Empty },
                    }),
                CancellationToken.None);

            expected = new Dictionary<string, string>();
            CheckMetadataCollections(expected, new AccountMetadata(new Dictionary<string, string>(), await GetAccountMetadataWithPrefixAsync(provider, TestKeyPrefix, CancellationToken.None)));
        }

        private static async Task<Dictionary<string, string>> GetAccountMetadataWithPrefixAsync(IObjectStorageService provider, string prefix, CancellationToken cancellationToken)
        {
            AccountMetadata metadata = await provider.GetAccountMetadataAsync(cancellationToken);
            return FilterMetadataPrefix(metadata.Metadata, prefix);
        }

        #endregion

        private static async Task<string> ReadAllObjectTextAsync(IObjectStorageService provider, ContainerName container, ObjectName @object, Encoding encoding, CancellationToken cancellationToken, bool verifyEtag = false, Action<long> progressUpdated = null)
        {
            using (MemoryStream downloadStream = new MemoryStream())
            {
                var result = await provider.GetObjectAsync(container, @object, cancellationToken).ConfigureAwait(false);
                if (verifyEtag)
                {
                    Console.WriteLine("ETag verification is not yet supported.");
                }

                StreamReader reader = new StreamReader(result.Item2, encoding);
                return reader.ReadToEnd();
            }
        }

        private static async Task RemoveContainerWithObjectsAsync(IObjectStorageService provider, ContainerName container, CancellationToken cancellationToken)
        {
            if (provider == null)
                throw new ArgumentNullException("provider");
            if (container == null)
                throw new ArgumentNullException("container");

            await RemoveAllObjectsAsync(provider, container, cancellationToken);
            await provider.RemoveContainerAsync(container, cancellationToken);
        }

        private static async Task RemoveAllObjectsAsync(IObjectStorageService provider, ContainerName container, CancellationToken cancellationToken)
        {
            if (provider == null)
                throw new ArgumentNullException("provider");
            if (container == null)
                throw new ArgumentNullException("container");

            while (true)
            {
                var request = await provider.PrepareListObjectsAsync(container, null, cancellationToken);
                request.RequestMessage.Headers.Add("X-Newest", "true");
                var response = await request.SendAsync(cancellationToken);
                ReadOnlyCollectionPage<ContainerObject> objects = response.Item2.Item2;
                if (objects.Count == 0)
                    break;

                List<Task> tasks = new List<Task>();
                tasks.AddRange(objects.Select(containerObject => RemoveObjectIfFoundAsync(provider, container, containerObject.Name, cancellationToken)));
                await Task.WhenAll(tasks);
            }
        }

        private static async Task RemoveObjectIfFoundAsync(IObjectStorageService provider, ContainerName container, ObjectName @object, CancellationToken cancellationToken)
        {
            if (provider == null)
                throw new ArgumentNullException("provider");
            if (container == null)
                throw new ArgumentNullException("container");
            if (@object == null)
                throw new ArgumentNullException("object");

            try
            {
                await provider.RemoveObjectAsync(container, @object, cancellationToken);
            }
            catch (HttpWebException ex)
            {
                if (ex.ResponseMessage == null || ex.ResponseMessage.StatusCode != HttpStatusCode.NotFound)
                    throw;
            }
        }

        private static Dictionary<string, string> FilterMetadataPrefix(IDictionary<string, string> metadata, string prefix)
        {
            Dictionary<string, string> result = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            foreach (KeyValuePair<string, string> pair in metadata)
            {
                if (pair.Key.StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
                    result.Add(pair.Key, pair.Value);
            }

            return result;
        }

        private static void CheckMetadataCollections(Dictionary<string, string> expected, StorageMetadata actual)
        {
            Assert.AreEqual(expected.Count, actual.Metadata.Count);
            CheckMetadataSubset(expected, actual);
        }

        private static void CheckMetadataSubset(Dictionary<string, string> expected, StorageMetadata actual)
        {
            foreach (var pair in expected)
                Assert.IsTrue(actual.Metadata.Contains(pair), "Expected metadata item {{ {0} : {1} }} not found.", pair.Key, pair.Value);
        }

        private TimeSpan TestTimeout(TimeSpan timeout)
        {
            if (Debugger.IsAttached)
                return TimeSpan.FromDays(1);

            return timeout;
        }

        internal static IObjectStorageService CreateProvider()
        {
            var provider = new ObjectStorageClient(CreateAuthenticationService(), Bootstrapper.Settings.DefaultRegion, false);
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

                AuthenticationRequest authenticationRequest =
                    new AuthenticationRequest(
                        new JProperty("auth", new JObject(
                            new JProperty("RAX-KSKEY:apiKeyCredentials", new JObject(
                                new JProperty("username", Bootstrapper.Settings.TestIdentity.Username),
                                new JProperty("apiKey", Bootstrapper.Settings.TestIdentity.APIKey))))));
                IAuthenticationService authenticationService = new RackspaceAuthenticationClient(identityService, authenticationRequest);
                return authenticationService;
            }, true);

        internal static IAuthenticationService CreateAuthenticationService()
        {
            return _testAuthenticationService.Value;
        }
    }
}
