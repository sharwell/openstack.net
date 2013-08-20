namespace Net.OpenStack.Testing.Integration.Providers.Rackspace
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using net.openstack.Core.Domain;
    using net.openstack.Core.Exceptions;
    using net.openstack.Core.Providers;
    using net.openstack.Providers.Rackspace;
    using Newtonsoft.Json;
    using Container = net.openstack.Core.Domain.Container;
    using HttpWebRequest = System.Net.HttpWebRequest;
    using MemoryStream = System.IO.MemoryStream;
    using Path = System.IO.Path;
    using Stream = System.IO.Stream;
    using StreamReader = System.IO.StreamReader;
    using WebRequest = System.Net.WebRequest;
    using WebResponse = System.Net.WebResponse;

    /// <summary>
    /// This class contains integration tests for the Rackspace Object Storage Provider
    /// (Cloud Files) that can be run with user (non-admin) credentials.
    /// </summary>
    /// <seealso cref="CloudFilesProvider"/>
    [TestClass]
    public class UserObjectStorageTests
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

        #region Container

        /// <summary>
        /// This test can be used to clear all of the metadata associated with every container in the storage provider.
        /// </summary>
        /// <remarks>
        /// This test is normally disabled. To run the cleanup method, comment out or remove the
        /// <see cref="IgnoreAttribute"/>.
        /// </remarks>
        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.ObjectStorage)]
        [Ignore]
        public void CleanupAllContainerMetadata()
        {
            IObjectStorageProvider provider = new CloudFilesProvider(Bootstrapper.Settings.TestIdentity);
            IEnumerable<Container> containers = ListAllContainers(provider);
            foreach (Container container in containers)
            {
                Dictionary<string, string> metadata = provider.GetContainerMetaData(container.Name);
                provider.DeleteContainerMetadata(container.Name, metadata.Keys);
            }
        }

        /// <summary>
        /// This unit test clears the metadata associated with every container which is
        /// created by the unit tests in this class.
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.ObjectStorage)]
        public void CleanupTestContainerMetadata()
        {
            IObjectStorageProvider provider = new CloudFilesProvider(Bootstrapper.Settings.TestIdentity);
            IEnumerable<Container> containers = ListAllContainers(provider);
            foreach (Container container in containers)
            {
                Dictionary<string, string> metadata = GetContainerMetadataWithPrefix(provider, container, TestKeyPrefix);
                provider.DeleteContainerMetadata(container.Name, metadata.Keys);
            }
        }

        /// <summary>
        /// This unit test deletes all containers created by the unit tests, including all
        /// objects within those containers.
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.ObjectStorage)]
        public void CleanupTestContainers()
        {
            IObjectStorageProvider provider = new CloudFilesProvider(Bootstrapper.Settings.TestIdentity);
            IEnumerable<Container> containers = ListAllContainers(provider);
            foreach (Container container in containers)
            {
                if (container.Name.StartsWith(TestContainerPrefix))
                    provider.DeleteContainer(container.Name);
            }
        }

        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.ObjectStorage)]
        public void TestListContainers()
        {
            IObjectStorageProvider provider = new CloudFilesProvider(Bootstrapper.Settings.TestIdentity);
            IEnumerable<Container> containers = ListAllContainers(provider);

            Console.WriteLine("Containers");
            foreach (Container container in containers)
            {
                Console.WriteLine("  {0}", container.Name);
            }
        }

        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.ObjectStorage)]
        public void TestCreateContainer()
        {
            IObjectStorageProvider provider = new CloudFilesProvider(Bootstrapper.Settings.TestIdentity);
            string containerName = TestContainerPrefix + Path.GetRandomFileName();

            ObjectStore result = provider.CreateContainer(containerName);
            Assert.AreEqual(ObjectStore.ContainerCreated, result);

            result = provider.CreateContainer(containerName);
            Assert.AreEqual(ObjectStore.ContainerExists, result);

            provider.DeleteContainer(containerName);
        }

        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.ObjectStorage)]
        public void TestDeleteContainer()
        {
            IObjectStorageProvider provider = new CloudFilesProvider(Bootstrapper.Settings.TestIdentity);
            string containerName = TestContainerPrefix + Path.GetRandomFileName();
            string objectName = Path.GetRandomFileName();
            string fileContents = "File contents!";

            ObjectStore result = provider.CreateContainer(containerName);
            Assert.AreEqual(ObjectStore.ContainerCreated, result);

            Stream stream = new MemoryStream(Encoding.UTF8.GetBytes(fileContents));
            provider.CreateObject(containerName, stream, objectName);

            try
            {
                provider.DeleteContainer(containerName, false);
                Assert.Fail("Expected a ContainerNotEmptyException");
            }
            catch (ContainerNotEmptyException)
            {
            }

            provider.DeleteContainer(containerName, true);
        }

        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.ObjectStorage)]
        public void TestGetContainerHeader()
        {
            IObjectStorageProvider provider = new CloudFilesProvider(Bootstrapper.Settings.TestIdentity);
            string containerName = TestContainerPrefix + Path.GetRandomFileName();

            ObjectStore result = provider.CreateContainer(containerName);
            Assert.AreEqual(ObjectStore.ContainerCreated, result);

            Dictionary<string, string> headers = provider.GetContainerHeader(containerName);
            Console.WriteLine("Container Headers");
            foreach (KeyValuePair<string, string> pair in headers)
                Console.WriteLine("    {0}: {1}", pair.Key, pair.Value);

            provider.DeleteContainer(containerName);
        }

        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.ObjectStorage)]
        public void TestGetContainerMetaData()
        {
            IObjectStorageProvider provider = new CloudFilesProvider(Bootstrapper.Settings.TestIdentity);
            string containerName = TestContainerPrefix + Path.GetRandomFileName();

            ObjectStore result = provider.CreateContainer(containerName);
            Assert.AreEqual(ObjectStore.ContainerCreated, result);

            Dictionary<string, string> metadata = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                { "Key1", "Value 1" },
                { "Key2", "Value ²" },
            };

            provider.UpdateContainerMetadata(containerName, new Dictionary<string, string>(metadata, StringComparer.OrdinalIgnoreCase));

            Dictionary<string, string> actualMetadata = provider.GetContainerMetaData(containerName);
            Console.WriteLine("Container Metadata");
            foreach (KeyValuePair<string, string> pair in actualMetadata)
                Console.WriteLine("    {0}: {1}", pair.Key, pair.Value);

            CheckMetadataCollections(metadata, actualMetadata);

            provider.DeleteContainer(containerName);
        }

        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.ObjectStorage)]
        public void TestContainerHeaderKeyCharacters()
        {
            IObjectStorageProvider provider = new CloudFilesProvider(Bootstrapper.Settings.TestIdentity);
            string containerName = TestContainerPrefix + Path.GetRandomFileName();

            ObjectStore result = provider.CreateContainer(containerName);
            Assert.AreEqual(ObjectStore.ContainerCreated, result);

            List<char> keyCharList = new List<char>();
            for (char i = MinHeaderKeyCharacter; i <= MaxHeaderKeyCharacter; i++)
            {
                if (!SeparatorCharacters.Contains(i) && !NotSupportedCharacters.Contains(i))
                    keyCharList.Add(i);
            }

            string key = TestKeyPrefix + new string(keyCharList.ToArray());
            Console.WriteLine("Expected key: {0}", key);

            provider.UpdateContainerMetadata(
                containerName,
                new Dictionary<string, string>
                {
                    { key, "Value" }
                });

            Dictionary<string, string> metadata = provider.GetContainerMetaData(containerName);
            Assert.IsNotNull(metadata);

            string value;
            Assert.IsTrue(metadata.TryGetValue(key, out value));
            Assert.AreEqual("Value", value);

            provider.UpdateContainerMetadata(
                containerName,
                new Dictionary<string, string>
                {
                    { key, null }
                });

            metadata = provider.GetContainerMetaData(containerName);
            Assert.IsNotNull(metadata);
            Assert.IsFalse(metadata.TryGetValue(key, out value));

            provider.DeleteContainer(containerName);
        }

        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.ObjectStorage)]
        public void TestContainerInvalidHeaderKeyCharacters()
        {
            IObjectStorageProvider provider = new CloudFilesProvider(Bootstrapper.Settings.TestIdentity);
            string containerName = TestContainerPrefix + Path.GetRandomFileName();

            ObjectStore result = provider.CreateContainer(containerName);
            Assert.AreEqual(ObjectStore.ContainerCreated, result);

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
                    provider.UpdateContainerMetadata(
                        containerName,
                        new Dictionary<string, string>
                        {
                            { invalidKey, "Value" }
                        });
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

            provider.DeleteContainer(containerName);
        }

        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.ObjectStorage)]
        public void TestUpdateContainerMetadata()
        {
            IObjectStorageProvider provider = new CloudFilesProvider(Bootstrapper.Settings.TestIdentity);
            string containerName = TestContainerPrefix + Path.GetRandomFileName();

            ObjectStore result = provider.CreateContainer(containerName);
            Assert.AreEqual(ObjectStore.ContainerCreated, result);

            Dictionary<string, string> metadata = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                { "Key1", "Value 1" },
                { "Key2", "Value ²" },
            };

            provider.UpdateContainerMetadata(containerName, new Dictionary<string, string>(metadata, StringComparer.OrdinalIgnoreCase));

            Dictionary<string, string> actualMetadata = provider.GetContainerMetaData(containerName);
            Console.WriteLine("Container Metadata");
            foreach (KeyValuePair<string, string> pair in actualMetadata)
                Console.WriteLine("    {0}: {1}", pair.Key, pair.Value);

            CheckMetadataCollections(metadata, actualMetadata);

            metadata["Key2"] = "Value 2";
            Dictionary<string, string> updatedMetadata = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                { "Key2", "Value 2" }
            };
            provider.UpdateContainerMetadata(containerName, new Dictionary<string, string>(updatedMetadata, StringComparer.OrdinalIgnoreCase));

            actualMetadata = provider.GetContainerMetaData(containerName);
            Console.WriteLine("Container Metadata");
            foreach (KeyValuePair<string, string> pair in actualMetadata)
                Console.WriteLine("    {0}: {1}", pair.Key, pair.Value);

            CheckMetadataCollections(metadata, actualMetadata);

            provider.DeleteContainer(containerName);
        }

        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.ObjectStorage)]
        public void TestDeleteContainerMetadata()
        {
            IObjectStorageProvider provider = new CloudFilesProvider(Bootstrapper.Settings.TestIdentity);
            string containerName = TestContainerPrefix + Path.GetRandomFileName();

            ObjectStore result = provider.CreateContainer(containerName);
            Assert.AreEqual(ObjectStore.ContainerCreated, result);

            Dictionary<string, string> metadata = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                { "Key1", "Value 1" },
                { "Key2", "Value ²" },
            };

            provider.UpdateContainerMetadata(containerName, new Dictionary<string, string>(metadata, StringComparer.OrdinalIgnoreCase));

            Dictionary<string, string> actualMetadata = provider.GetContainerMetaData(containerName);
            Console.WriteLine("Container Metadata");
            foreach (KeyValuePair<string, string> pair in actualMetadata)
                Console.WriteLine("    {0}: {1}", pair.Key, pair.Value);

            CheckMetadataCollections(metadata, actualMetadata);

            metadata.Remove("Key2");
            Dictionary<string, string> updatedMetadata = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                { "Key2", null }
            };
            provider.UpdateContainerMetadata(containerName, new Dictionary<string, string>(updatedMetadata, StringComparer.OrdinalIgnoreCase));

            actualMetadata = provider.GetContainerMetaData(containerName);
            Console.WriteLine("Container Metadata");
            foreach (KeyValuePair<string, string> pair in actualMetadata)
                Console.WriteLine("    {0}: {1}", pair.Key, pair.Value);

            CheckMetadataCollections(metadata, actualMetadata);

            provider.DeleteContainer(containerName);
        }

        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.ObjectStorage)]
        public void TestListCDNContainers()
        {
            IObjectStorageProvider provider = new CloudFilesProvider(Bootstrapper.Settings.TestIdentity);
            IEnumerable<ContainerCDN> containers = ListAllCDNContainers(provider);

            Console.WriteLine("Containers");
            foreach (ContainerCDN container in containers)
            {
                Console.WriteLine("  {1}{0}", container.Name, container.CDNEnabled ? "*" : "");
            }
        }

        /// <summary>
        /// This test covers most of the CDN functionality exposed by <see cref="IObjectStorageProvider"/>.
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.ObjectStorage)]
        public void TestCDNOnContainer()
        {
            IObjectStorageProvider provider = new CloudFilesProvider(Bootstrapper.Settings.TestIdentity);
            string containerName = TestContainerPrefix + Path.GetRandomFileName();
            string objectName = Path.GetRandomFileName();
            string fileContents = "File contents!";

            ObjectStore result = provider.CreateContainer(containerName);
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

            provider.DeleteContainer(containerName, true);
        }

        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.ObjectStorage)]
        public void TestStaticWebOnContainer()
        {
            IObjectStorageProvider provider = new CloudFilesProvider(Bootstrapper.Settings.TestIdentity);
            string containerName = TestContainerPrefix + Path.GetRandomFileName();
            string objectName = Path.GetRandomFileName();
            string fileContents = "File contents!";

            ObjectStore result = provider.CreateContainer(containerName);
            Assert.AreEqual(ObjectStore.ContainerCreated, result);

            Stream stream = new MemoryStream(Encoding.UTF8.GetBytes(fileContents));
            provider.CreateObject(containerName, stream, objectName);

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

            provider.DeleteContainer(containerName, true);
        }

        private static IEnumerable<Container> ListAllContainers(IObjectStorageProvider provider, int? blockSize = null, string region = null, bool useInternalUrl = false, CloudIdentity identity = null)
        {
            if (blockSize <= 0)
                throw new ArgumentOutOfRangeException("blockSize");

            Container lastContainer = null;

            do
            {
                string marker = lastContainer != null ? lastContainer.Name : null;
                IEnumerable<Container> containers = provider.ListContainers(blockSize, marker, null, region, useInternalUrl, identity);
                lastContainer = null;
                foreach (Container container in containers)
                {
                    lastContainer = container;
                    yield return container;
                }
            } while (lastContainer != null);
        }

        private static IEnumerable<ContainerCDN> ListAllCDNContainers(IObjectStorageProvider provider, int? blockSize = null, bool cdnEnabled = false, string region = null, CloudIdentity identity = null)
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

        private static Dictionary<string, string> GetContainerMetadataWithPrefix(IObjectStorageProvider provider, Container container, string prefix)
        {
            Dictionary<string, string> metadata = provider.GetContainerMetaData(container.Name);
            return FilterMetadataPrefix(metadata, prefix);
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
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.ObjectStorage)]
        [Ignore]
        public void CleanupAllAccountMetadata()
        {
            IObjectStorageProvider provider = new CloudFilesProvider(Bootstrapper.Settings.TestIdentity);
            Dictionary<string, string> metadata = provider.GetAccountMetaData();
            Dictionary<string, string> removedMetadata = metadata.ToDictionary(i => i.Key, i => string.Empty);
            provider.UpdateAccountMetadata(removedMetadata);
        }

        /// <summary>
        /// This unit test clears the metadata associated with the account which is
        /// created by the unit tests in this class.
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.ObjectStorage)]
        public void CleanupTestAccountMetadata()
        {
            IObjectStorageProvider provider = new CloudFilesProvider(Bootstrapper.Settings.TestIdentity);
            Dictionary<string, string> metadata = GetAccountMetadataWithPrefix(provider, TestKeyPrefix);
            Dictionary<string, string> removedMetadata = metadata.ToDictionary(i => i.Key, i => string.Empty);
            provider.UpdateAccountMetadata(removedMetadata);
        }

        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.ObjectStorage)]
        public void TestGetAccountHeaders()
        {
            IObjectStorageProvider provider = new CloudFilesProvider(Bootstrapper.Settings.TestIdentity);
            Dictionary<string, string> headers = provider.GetAccountHeaders();
            Assert.IsNotNull(headers);

            Console.WriteLine("Account Headers:");
            foreach (var pair in headers)
            {
                Assert.IsNotNull(pair.Key);
                Assert.IsNotNull(pair.Value);
                Assert.IsFalse(string.IsNullOrEmpty(pair.Key));
                Console.WriteLine("    {0}: {1}", pair.Key, pair.Value);
            }

            Assert.AreEqual(StringComparer.OrdinalIgnoreCase, headers.Comparer);

            string containerCountText;
            Assert.IsTrue(headers.TryGetValue(CloudFilesProvider.AccountContainerCount, out containerCountText));
            long containerCount;
            Assert.IsTrue(long.TryParse(containerCountText, out containerCount));
            Assert.IsTrue(containerCount >= 0);

            string accountBytesText;
            Assert.IsTrue(headers.TryGetValue(CloudFilesProvider.AccountBytesUsed, out accountBytesText));
            long accountBytes;
            Assert.IsTrue(long.TryParse(accountBytesText, out accountBytes));
            Assert.IsTrue(accountBytes >= 0);

            string objectCountText;
            if (headers.TryGetValue(CloudFilesProvider.AccountObjectCount, out objectCountText))
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
        public void TestGetAccountMetaData()
        {
            IObjectStorageProvider provider = new CloudFilesProvider(Bootstrapper.Settings.TestIdentity);
            Dictionary<string, string> metadata = provider.GetAccountMetaData();
            Assert.IsNotNull(metadata);
            Assert.AreEqual(StringComparer.OrdinalIgnoreCase, metadata.Comparer);

            Console.WriteLine("Account MetaData:");
            foreach (var pair in metadata)
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
        public void TestAccountHeaderKeyCharacters()
        {
            IObjectStorageProvider provider = new CloudFilesProvider(Bootstrapper.Settings.TestIdentity);

            List<char> keyCharList = new List<char>();
            for (char i = MinHeaderKeyCharacter; i <= MaxHeaderKeyCharacter; i++)
            {
                if (!SeparatorCharacters.Contains(i) && !NotSupportedCharacters.Contains(i))
                    keyCharList.Add(i);
            }

            string key = TestKeyPrefix + new string(keyCharList.ToArray());
            Console.WriteLine("Expected key: {0}", key);

            provider.UpdateAccountMetadata(
                new Dictionary<string, string>
                {
                    { key, "Value" }
                });

            Dictionary<string, string> metadata = provider.GetAccountMetaData();
            Assert.IsNotNull(metadata);

            string value;
            Assert.IsTrue(metadata.TryGetValue(key, out value));
            Assert.AreEqual("Value", value);

            provider.UpdateAccountMetadata(
                new Dictionary<string, string>
                {
                    { key, null }
                });

            metadata = provider.GetAccountMetaData();
            Assert.IsNotNull(metadata);
            Assert.IsFalse(metadata.TryGetValue(key, out value));
        }

        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.ObjectStorage)]
        public void TestAccountInvalidHeaderKeyCharacters()
        {
            IObjectStorageProvider provider = new CloudFilesProvider(Bootstrapper.Settings.TestIdentity);

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
                    provider.UpdateAccountMetadata(
                        new Dictionary<string, string>
                        {
                            { invalidKey, "Value" }
                        });
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
        }

        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.ObjectStorage)]
        public void TestUpdateAccountMetadata()
        {
            IObjectStorageProvider provider = new CloudFilesProvider(Bootstrapper.Settings.TestIdentity);
            Dictionary<string, string> metadata = provider.GetAccountMetaData();
            if (metadata.Any(i => i.Key.StartsWith(TestKeyPrefix, StringComparison.OrdinalIgnoreCase)))
            {
                Assert.Inconclusive("The account contains metadata from a previous unit test run. Run CleanupTestAccountMetadata and try again.");
                return;
            }

            // test add metadata
            TestGetAccountMetaData();
            provider.UpdateAccountMetadata(
                new Dictionary<string, string>
            {
                { TestKeyPrefix + "1", "Value ĳ" },
                { TestKeyPrefix + "2", "Value ²" },
            });
            TestGetAccountMetaData();

            Dictionary<string, string> expected =
                new Dictionary<string, string>
            {
                { TestKeyPrefix + "1", "Value ĳ" },
                { TestKeyPrefix + "2", "Value ²" },
            };
            CheckMetadataCollections(expected, GetAccountMetadataWithPrefix(provider, TestKeyPrefix));

            // test update metadata
            provider.UpdateAccountMetadata(
                new Dictionary<string, string>
            {
                { TestKeyPrefix + "1", "Value 1" },
            });

            expected = new Dictionary<string, string>
            {
                { TestKeyPrefix + "1", "Value 1" },
                { TestKeyPrefix + "2", "Value ²" },
            };
            CheckMetadataCollections(expected, GetAccountMetadataWithPrefix(provider, TestKeyPrefix));

            // test remove metadata
            provider.UpdateAccountMetadata(
                new Dictionary<string, string>
            {
                { TestKeyPrefix + "1", null },
                { TestKeyPrefix + "2", string.Empty },
            });

            expected = new Dictionary<string, string>();
            CheckMetadataCollections(expected, GetAccountMetadataWithPrefix(provider, TestKeyPrefix));
        }

        private static Dictionary<string, string> GetAccountMetadataWithPrefix(IObjectStorageProvider provider, string prefix)
        {
            Dictionary<string, string> metadata = provider.GetAccountMetaData();
            return FilterMetadataPrefix(metadata, prefix);
        }

        #endregion

        private static Dictionary<string, string> FilterMetadataPrefix(Dictionary<string, string> metadata, string prefix)
        {
            Dictionary<string, string> result = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            foreach (KeyValuePair<string, string> pair in metadata)
            {
                if (pair.Key.StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
                    result.Add(pair.Key, pair.Value);
            }

            return result;
        }

        private static void CheckMetadataCollections(Dictionary<string, string> expected, Dictionary<string, string> actual)
        {
            Assert.AreEqual(expected.Count, actual.Count);
            CheckMetadataSubset(expected, actual);
        }

        private static void CheckMetadataSubset(Dictionary<string, string> expected, Dictionary<string, string> actual)
        {
            foreach (var pair in expected)
                Assert.IsTrue(actual.Contains(pair), "Expected metadata item {{ {0} : {1} }} not found.", pair.Key, pair.Value);
        }
    }
}
