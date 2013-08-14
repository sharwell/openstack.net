namespace net.openstack.Core.Domain
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    [JsonDictionary]
    public class ServerAddresses : Dictionary<string, AddressDetails[]>
    {
        [JsonIgnore]
        public AddressDetails[] Private { get { return this["private"]; } }

        [JsonIgnore]
        public AddressDetails[] Public { get { return this["public"]; } }
    }
}
