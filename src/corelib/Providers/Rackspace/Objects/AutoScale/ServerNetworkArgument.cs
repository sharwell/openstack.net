namespace net.openstack.Providers.Rackspace.Objects.AutoScale
{
    using System;
    using net.openstack.Core.Domain;
    using net.openstack.Core.Domain.Networking;
    using Newtonsoft.Json;

    [JsonObject(MemberSerialization.OptIn)]
    public class ServerNetworkArgument
    {
        [JsonProperty("uuid")]
        private NetworkId _uuid;

        /// <summary>
        /// Initializes a new instance of the <see cref="ServerNetworkArgument"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected ServerNetworkArgument()
        {
        }

        public ServerNetworkArgument(NetworkId networkId)
        {
            if (networkId == null)
                throw new ArgumentNullException("networkId");

            _uuid = networkId;
        }

        public NetworkId NetworkId
        {
            get
            {
                return _uuid;
            }
        }
    }
}
