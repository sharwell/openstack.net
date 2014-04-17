namespace net.openstack.Core
{
    using System.Net.Http.Headers;
    using System.Reflection;

    /// <summary>
    /// Generates the User-Agent value which identifies this SDK in REST requests.
    /// </summary>
    /// <threadsafety static="true" instance="false"/>
    public static class UserAgentGenerator
    {
        private static readonly string _productName;
        private static readonly string _productVersion;
        private static readonly string _userAgent;

        static UserAgentGenerator()
        {
#if !PORTABLE
            var productAttribute = typeof(UserAgentGenerator).Assembly.GetCustomAttribute<AssemblyProductAttribute>();
            _productName = productAttribute.Product;

            var informationalVersionAttribute = typeof(UserAgentGenerator).Assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>();
            _productVersion = informationalVersionAttribute.InformationalVersion;
#else
#warning this should get the information from the assembly...
            _productName = "openstack.net";
            _productVersion = "1.3.3.0-dev";
#endif

            _userAgent = string.Format("{0}/{1}", _productName, _productVersion);
        }

        /// <summary>
        /// Gets the User-Agent value for this SDK.
        /// </summary>
        public static string UserAgent
        {
            get
            {
                return _userAgent;
            }
        }

        /// <summary>
        /// Gets the product name for this SDK, for use with the <see cref="ProductInfoHeaderValue"/> class.
        /// </summary>
        public static string ProductName
        {
            get
            {
                return _productName;
            }
        }

        /// <summary>
        /// Gets the product version for this SDK, for use with the <see cref="ProductInfoHeaderValue"/> class.
        /// </summary>
        public static string ProductVersion
        {
            get
            {
                return _productVersion;
            }
        }
    }
}
