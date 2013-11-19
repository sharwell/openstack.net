namespace net.openstack.Providers.Rackspace.Objects.Monitoring
{
    using Newtonsoft.Json;

    [JsonObject(MemberSerialization.OptIn)]
    public class SmtpCheckDetails : ConnectionCheckDetails
    {
        [JsonProperty("ehlo", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private string _ehlo;

        [JsonProperty("from", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private string _from;

        [JsonProperty("to", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private string _to;

        [JsonProperty("payload", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private string _payload;

        [JsonProperty("starttls", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private bool? _startTls;

        /// <summary>
        /// Initializes a new instance of the <see cref="SmtpCheckDetails"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected SmtpCheckDetails()
        {
        }

        public SmtpCheckDetails(int? port, string ehlo, string from, string to, string payload, bool? startTls)
            : base(port)
        {
            _ehlo = ehlo;
            _from = from;
            _to = to;
            _payload = payload;
            _startTls = startTls;
        }

        public string Ehlo
        {
            get
            {
                return _ehlo;
            }
        }

        public string From
        {
            get
            {
                return _from;
            }
        }

        public string To
        {
            get
            {
                return _to;
            }
        }

        public string Payload
        {
            get
            {
                return _payload;
            }
        }

        public bool? StartTls
        {
            get
            {
                return _startTls;
            }
        }

        protected internal override bool SupportsCheckType(CheckTypeId checkTypeId)
        {
            return checkTypeId == CheckTypeId.RemoteSmtp;
        }
    }
}
