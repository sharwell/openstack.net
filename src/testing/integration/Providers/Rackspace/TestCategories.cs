namespace Net.OpenStack.Testing.Integration.Providers.Rackspace
{
    using net.openstack.Core.Providers;

    /// <summary>
    /// Provides predefined categories for use with the <see cref="TestCategoryAttribute"/>.
    /// </summary>
    public static class TestCategories
    {
#if PORTABLE
        private const string CategorySuffix = " (Portable)";
#else
        private const string CategorySuffix = "";
#endif

        /// <summary>
        /// Identity service tests.
        /// </summary>
        /// <seealso cref="IIdentityProvider"/>
        public const string Identity = "Identity" + CategorySuffix;

        /// <summary>
        /// Block storage service tests.
        /// </summary>
        /// <seealso cref="IBlockStorageProvider"/>
        public const string BlockStorage = "Block Storage" + CategorySuffix;

        /// <summary>
        /// Object storage service tests.
        /// </summary>
        /// <seealso cref="IObjectStorageProvider"/>
        public const string ObjectStorage = "Object Storage" + CategorySuffix;

        /// <summary>
        /// Networks service tests.
        /// </summary>
        /// <seealso cref="INetworksProvider"/>
        public const string Networks = "Networks" + CategorySuffix;

        /// <summary>
        /// Compute service tests.
        /// </summary>
        /// <seealso cref="IComputeProvider"/>
        public const string Compute = "Compute" + CategorySuffix;

        /// <summary>
        /// Auto Scale service tests.
        /// </summary>
        /// <seealso cref="IAutoScaleService"/>
        public const string AutoScale = "Auto Scale" + CategorySuffix;

        /// <summary>
        /// DNS service tests.
        /// </summary>
        /// <seealso cref="IDnsService"/>
        public const string Dns = "DNS" + CategorySuffix;

        /// <summary>
        /// Database service tests.
        /// </summary>
        /// <seealso cref="IDatabaseService"/>
        public const string Database = "Database" + CategorySuffix;

        /// <summary>
        /// Load balancer service tests.
        /// </summary>
        /// <seealso cref="ILoadBalancerProvider"/>
        /// <preliminary/>
        public const string LoadBalancers = "Load Balancers" + CategorySuffix;

        /// <summary>
        /// Monitoring service tests.
        /// </summary>
        /// <seealso cref="IMonitoringProvider"/>
        /// <preliminary/>
        public const string Monitoring = "Monitoring" + CategorySuffix;

        /// <summary>
        /// Queueing service tests.
        /// </summary>
        /// <seealso cref="IQueuesService"/>
        public const string Queues = "Queues" + CategorySuffix;

        /// <summary>
        /// Unit tests designed to remove resources from an account which were created
        /// by previous unit test runs which were cancelled, failed, or designed in such
        /// a way that resources were not deleted automatically at the end of the test.
        /// </summary>
        public const string Cleanup = "Cleanup" + CategorySuffix;

        /// <summary>
        /// Tests which require user credentials.
        /// </summary>
        public const string User = "User" + CategorySuffix;

        /// <summary>
        /// Tests which require administrator credentials.
        /// </summary>
        public const string Admin = "Admin" + CategorySuffix;
    }
}
