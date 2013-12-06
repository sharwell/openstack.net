namespace net.openstack.Core.Domain.Networking
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Newtonsoft.Json;

    [JsonObject(MemberSerialization.OptIn)]
    public class Port : PortConfiguration
    {
        [JsonProperty("id")]
        private PortId _id;

        [JsonProperty("status")]
        private NetworkStatus _status;

        public PortId Id
        {
            get
            {
                return _id;
            }
        }
    }
}
