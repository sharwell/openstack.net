namespace net.openstack.Core.Domain.Networking
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Text;
    using net.openstack.Core.Domain.Converters;
    using Newtonsoft.Json;

    [JsonObject(MemberSerialization.OptIn)]
    public abstract class SubnetConfiguration
    {
        [JsonProperty("name")]
        private string _name;

        [JsonProperty("network_id")]
        private NetworkId _networkId;

        [JsonProperty("gateway_ip")]
        [JsonConverter(typeof(IPAddressSimpleConverter))]
        private IPAddress _gatewayAddress;

        [JsonProperty("ip_version")]
        private string _ipVersion;

        [JsonProperty("cidr")]
        private string _cidr;

        [JsonProperty("allocation_pools")]
        private AllocationPool[] _allocationPools;

        [JsonProperty("enable_dhcp")]
        private bool? _enableDhcp;
    }
}
