namespace net.openstack.Providers.Rackspace.Objects.Monitoring
{
    using Newtonsoft.Json;

    [JsonObject(MemberSerialization.OptIn)]
    public abstract class SecureConnectionCheckDetails : ConnectionCheckDetails
    {
        [JsonProperty("ssl", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private bool? _enableSsl;

        /// <summary>
        /// Initializes a new instance of the <see cref="SecureConnectionCheckDetails"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected SecureConnectionCheckDetails()
        {
        }

        protected SecureConnectionCheckDetails(int? port, bool? enableSsl)
            : base(port)
        {
            _enableSsl = enableSsl;
        }

        public bool? EnableSsl
        {
            get
            {
                return _enableSsl;
            }
        }
    }
}
