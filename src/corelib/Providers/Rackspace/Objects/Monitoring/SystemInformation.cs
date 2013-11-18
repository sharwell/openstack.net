namespace net.openstack.Providers.Rackspace.Objects.Monitoring
{
    using Newtonsoft.Json;

    [JsonObject(MemberSerialization.OptIn)]
    public class SystemInformation
    {
#pragma warning disable 649 // Field 'fieldName' is never assigned to, and will always have its default value {value}
        [JsonProperty("arch")]
        private string _arch;

        [JsonProperty("name")]
        private string _name;

        [JsonProperty("vendor")]
        private string _vendor;

        [JsonProperty("vendor_version")]
        private string _vendorVersion;

        [JsonProperty("version")]
        private string _version;
#pragma warning restore 649

        /// <summary>
        /// Initializes a new instance of the <see cref="SystemInformation"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected SystemInformation()
        {
        }

        /// <summary>
        /// Gets the CPU architecture.
        /// </summary>
        public string Architecture
        {
            get
            {
                return _arch;
            }
        }

        /// <summary>
        /// Gets the generic name of the operating system.
        /// </summary>
        public string Name
        {
            get
            {
                return _name;
            }
        }

        /// <summary>
        /// Gets the name of the vendor.
        /// </summary>
        public string Vendor
        {
            get
            {
                return _vendor;
            }
        }

        /// <summary>
        /// Gets the vendor version name.
        /// </summary>
        public string VendorVersion
        {
            get
            {
                return _vendorVersion;
            }
        }

        /// <summary>
        /// Gets the version.
        /// </summary>
        public string Version
        {
            get
            {
                return _version;
            }
        }
    }
}
