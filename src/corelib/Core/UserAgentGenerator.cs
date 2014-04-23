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
            _productName = AssemblyInfo.AssemblyProduct;
            _productVersion = AssemblyInfo.AssemblyFileVersion;
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
