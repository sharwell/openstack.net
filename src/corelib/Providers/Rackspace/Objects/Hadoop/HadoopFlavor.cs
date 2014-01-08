namespace net.openstack.Providers.Rackspace.Objects.Hadoop
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Newtonsoft.Json;

    [JsonObject(MemberSerialization.OptIn)]
    public class HadoopFlavor : Databases.DatabaseFlavor
    {
        [JsonProperty("disk", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private int? _disk;

        [JsonProperty("vcpus", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private int? _vcpus;

        /// <summary>
        /// Initializes a new instance of the <see cref="HadoopFlavor"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected HadoopFlavor()
        {
        }

        public int? Disk
        {
            get
            {
                return _disk;
            }
        }

        public int? VirtualProcessorCount
        {
            get
            {
                return _vcpus;
            }
        }
    }
}
