namespace net.openstack.Providers.Rackspace.Objects.Monitoring
{
    using Newtonsoft.Json;

    [JsonObject(MemberSerialization.OptIn)]
    public class TcpCheckDetails : SecureConnectionCheckDetails
    {
        [JsonProperty("banner_match", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private string _bannerMatch;

        [JsonProperty("body_match", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private string _bodyMatch;

        [JsonProperty("send_body", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private string _sendBody;

        /// <summary>
        /// Initializes a new instance of the <see cref="TcpCheckDetails"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected TcpCheckDetails()
        {
        }

        public TcpCheckDetails(int port, bool? enableSsl = null, string bannerMatch = null, string bodyMatch = null, string sendBody = null)
            : base(port, enableSsl)
        {
            _bannerMatch = bannerMatch;
            _bodyMatch = bodyMatch;
            _sendBody = sendBody;
        }

        public string BannerMatch
        {
            get
            {
                return _bannerMatch;
            }
        }

        public string BodyMatch
        {
            get
            {
                return _bodyMatch;
            }
        }

        public string SendBody
        {
            get
            {
                return _sendBody;
            }
        }

        protected internal override bool SupportsCheckType(CheckTypeId checkTypeId)
        {
            return checkTypeId == CheckTypeId.RemoteTcp;
        }
    }
}
