namespace net.openstack.Providers.Rackspace.Objects.Monitoring
{
    using System;
    using Newtonsoft.Json;

    [JsonObject(MemberSerialization.OptIn)]
    public class PingCheckDetails : CheckDetails
    {
        [JsonProperty("count", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private int? _count;

        /// <summary>
        /// Initializes a new instance of the <see cref="PingCheckDetails"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected PingCheckDetails()
        {
        }

        public PingCheckDetails(int? count = null)
        {
            if (count <= 0)
                throw new ArgumentOutOfRangeException("count");

            _count = count;
        }

        protected internal override bool SupportsCheckType(CheckTypeId checkTypeId)
        {
            return checkTypeId == CheckTypeId.RemotePing;
        }
    }
}
