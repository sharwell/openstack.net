namespace Net.OpenStack.Testing.Integration.Providers.Rackspace
{
    using net.openstack.Core.Providers;

    /// <summary>
    /// Provides predefined categories for use with the <see cref="TestCategoryAttribute"/>.
    /// </summary>
    public static class TestCategories
    {
        /// <summary>
        /// Identity service tests.
        /// </summary>
        /// <seealso cref="IIdentityProvider"/>
        public const string Identity = "Identity";

        /// <summary>
        /// Object storage service tests.
        /// </summary>
        /// <seealso cref="IObjectStorageProvider"/>
        public const string ObjectStorage = "ObjectStorage";

        /// <summary>
        /// Tests which require user credentials.
        /// </summary>
        public const string User = "User";

        /// <summary>
        /// Tests which require administrator credentials.
        /// </summary>
        public const string Admin = "Admin";
    }
}
