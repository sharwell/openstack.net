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
    public class AllocationPool
    {
        [JsonProperty("start")]
        [JsonConverter(typeof(IPAddressSimpleConverter))]
        private IPAddress _start;

        [JsonProperty("end")]
        [JsonConverter(typeof(IPAddressSimpleConverter))]
        private IPAddress _end;

        /// <summary>
        /// Initializes a new instance of the <see cref="AllocationPool"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected AllocationPool()
        {
        }

        public IPAddress Start
        {
            get
            {
                return _start;
            }
        }

        public IPAddress End
        {
            get
            {
                return _end;
            }
        }
    }
}
