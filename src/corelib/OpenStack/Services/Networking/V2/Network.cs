namespace OpenStack.Services.Networking.V2
{
    using System.Collections.ObjectModel;
    using Newtonsoft.Json;

    [JsonObject(MemberSerialization.OptIn)]
    public class Network : NetworkData
    {
        [JsonProperty("id", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private NetworkId _id;

        [JsonProperty("status", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private NetworkStatus _status;

        [JsonProperty("subnets", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private SubnetId[] _subnets;

        /// <summary>
        /// Initializes a new instance of the <see cref="Network"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected Network()
        {
        }

        public NetworkId Id
        {
            get
            {
                return _id;
            }
        }

        public NetworkStatus Status
        {
            get
            {
                return _status;
            }
        }

        public ReadOnlyCollection<SubnetId> Subnets
        {
            get
            {
                if (_subnets == null)
                    return null;

                return new ReadOnlyCollection<SubnetId>(_subnets);
            }
        }
    }
}
