namespace Net.OpenStack.Testing.Integration.Providers.Rackspace
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using net.openstack.Core.Providers;
    using net.openstack.Providers.Rackspace;

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

        private Dictionary<string, string> GetAccountMetadataWithPrefix(IObjectStorageProvider provider, string prefix)
        {
            Dictionary<string, string> metadata = provider.GetAccountMetaData();
            Dictionary<string, string> result = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            foreach (KeyValuePair<string, string> pair in metadata)
            {
                if (pair.Key.StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
                    result.Add(pair.Key, pair.Value);
            }

            return result;
        }

        private void CheckMetadataCollections(Dictionary<string, string> expected, Dictionary<string, string> actual)
        {
            Assert.AreEqual(expected.Count, actual.Count);
            foreach (var pair in expected)
                Assert.IsTrue(actual.Contains(pair), "Expected metadata item {{ {0} : {1} }} not found.", pair.Key, pair.Value);
        }

        #endregion
    }
}
