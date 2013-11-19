namespace net.openstack.Providers.Rackspace.Objects.Monitoring
{
    using Newtonsoft.Json;

    [JsonObject(MemberSerialization.OptIn)]
    public class TelnetBannerCheckDetails : SecureConnectionCheckDetails
    {
        [JsonProperty("banner_match", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private string _bannerMatch;

        /// <summary>
        /// Initializes a new instance of the <see cref="TelnetBannerCheckDetails"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected TelnetBannerCheckDetails()
        {
        }

        public TelnetBannerCheckDetails(int port, bool? enableSsl = null, string bannerMatch = null)
            : base(port, enableSsl)
        {
            _bannerMatch = bannerMatch;
        }

        public string BannerMatch
        {
            get
            {
                return _bannerMatch;
            }
        }

        protected internal override bool SupportsCheckType(CheckTypeId checkTypeId)
        {
            return checkTypeId == CheckTypeId.RemoteTelnetBanner;
        }
    }
}
