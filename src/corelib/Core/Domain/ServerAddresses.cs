namespace net.openstack.Core.Domain
{
    using System.Collections.Generic;
    using System.Net;
    using Newtonsoft.Json;

    [JsonDictionary(ItemConverterType = typeof(IPAddressConverter))]
    public class ServerAddresses : Dictionary<string, IPAddress[]>
    {
        [JsonIgnore]
        public IPAddress[] Private { get { return this["private"]; } }

        [JsonIgnore]
        public IPAddress[] Public { get { return this["public"]; } }
    }
}
