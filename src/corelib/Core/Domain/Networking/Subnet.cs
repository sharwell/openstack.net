namespace net.openstack.Core.Domain.Networking
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Newtonsoft.Json;

    [JsonObject(MemberSerialization.OptIn)]
    public class Subnet : SubnetConfiguration
    {
        [JsonProperty("id")]
        private SubnetId _id;

        [JsonProperty("tenant_id")]
        private ProjectId _tenantId;
    }
}
