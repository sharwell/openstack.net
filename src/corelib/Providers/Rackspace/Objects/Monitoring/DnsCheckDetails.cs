namespace net.openstack.Providers.Rackspace.Objects.Monitoring
{
    using System;
    using Newtonsoft.Json;
    using DnsRecordType = net.openstack.Providers.Rackspace.Objects.Dns.DnsRecordType;

    [JsonObject(MemberSerialization.OptIn)]
    public class DnsCheckDetails : ConnectionCheckDetails
    {
        [JsonProperty("query")]
        private string _query;

        [JsonProperty("record_type")]
        private DnsRecordType _recordType;

        /// <summary>
        /// Initializes a new instance of the <see cref="DnsCheckDetails"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected DnsCheckDetails()
        {
        }

        public DnsCheckDetails(string query, DnsRecordType recordType, int? port = null)
            : base(port)
        {
            if (query == null)
                throw new ArgumentNullException("query");
            if (string.IsNullOrEmpty(query))
                throw new ArgumentException("query cannot be empty");
            if (recordType == null)
                throw new ArgumentNullException("recordType");

            _query = query;
            _recordType = recordType;
        }

        public string Query
        {
            get
            {
                return _query;
            }
        }

        public DnsRecordType RecordType
        {
            get
            {
                return _recordType;
            }
        }

        protected internal override bool SupportsCheckType(CheckTypeId checkTypeId)
        {
            return checkTypeId == CheckTypeId.RemoteDns;
        }
    }
}
