namespace Net.OpenStack.Testing.Integration.Providers.Rackspace
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using net.openstack.Providers.Rackspace;
    using net.openstack.Core.Providers;
    using System.Globalization;

    /// <summary>
    /// This class contains integration tests for the Rackspace Object Storage Provider
    /// (Cloud Files) that can be run with user (non-admin) credentials.
    /// </summary>
    /// <seealso cref="CloudFilesProvider"/>
    [TestClass]
    public class UserObjectStorageTests
    {
        #region Container

        #endregion

        #region Objects

        #endregion

        #region Accounts

        /// <summary>
        /// This test can be used to clear the metadata associated with an account.
        /// </summary>
        /// <remarks>
        /// This test is normally disabled. To run the cleanup method, comment out or remove the
        /// <see cref="IgnoreAttribute"/>.
        /// </remarks>
        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.ObjectStorage)]
        [Ignore]
        public void CleanupAccountMetadata()
        {
            IObjectStorageProvider provider = new CloudFilesProvider(Bootstrapper.Settings.TestIdentity);
            Dictionary<string, string> emptyMetadata = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            provider.UpdateAccountMetadata(emptyMetadata);
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
        }

        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.ObjectStorage)]
        public void TestGetAccountMetaData()
        {
            IObjectStorageProvider provider = new CloudFilesProvider(Bootstrapper.Settings.TestIdentity);
            Dictionary<string, string> metadata = provider.GetAccountMetaData();
            Assert.IsNotNull(metadata);

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
        public void TestUpdateAccountMetadata()
        {
            IObjectStorageProvider provider = new CloudFilesProvider(Bootstrapper.Settings.TestIdentity);
            Dictionary<string, string> metadata = provider.GetAccountMetaData();
            if (metadata.Count > 0)
            {
                Assert.Inconclusive("Cannot run test on an account that already contains metadata.");
                return;
            }

            Dictionary<string, string> updatedMetadata = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            updatedMetadata.Add("CustomKey", "CustomValue");
            provider.UpdateAccountMetadata(updatedMetadata);

            metadata = provider.GetAccountMetaData();
            Assert.IsNotNull(metadata);
            Assert.AreEqual(1, metadata.Count);
            Assert.AreEqual("CustomKey", metadata.First().Key, true, CultureInfo.InvariantCulture);
            Assert.AreEqual("CustomValue", metadata.First().Value);

            CleanupAccountMetadata();
        }

        public void TestUpdateAccountHeaders()
        {
        }

        #endregion
    }
}
