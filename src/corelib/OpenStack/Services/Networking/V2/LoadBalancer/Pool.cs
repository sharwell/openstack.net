namespace OpenStack.Services.Networking.V2.LoadBalancer
{
    using System.Collections.ObjectModel;
    using Newtonsoft.Json;

    [JsonObject(MemberSerialization.OptIn)]
    public class Pool : PoolData
    {
        [JsonProperty("id", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private PoolId _id;

        [JsonProperty("vip_id", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private VirtualAddressId _virtualAddressId;

        [JsonProperty("members", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private MemberId[] _members;

        [JsonProperty("status", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private PoolStatus _status;

        /// <summary>
        /// Initializes a new instance of the <see cref="Pool"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected Pool()
        {
        }

        public PoolId Id
        {
            get
            {
                return _id;
            }
        }

        public VirtualAddressId VirtualAddressId
        {
            get
            {
                return _virtualAddressId;
            }
        }

        public ReadOnlyCollection<MemberId> Members
        {
            get
            {
                if (_members == null)
                    return null;

                return new ReadOnlyCollection<MemberId>(_members);
            }
        }

        public PoolStatus Status
        {
            get
            {
                return _status;
            }
        }
    }
}
