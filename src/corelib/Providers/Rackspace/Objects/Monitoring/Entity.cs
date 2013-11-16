namespace net.openstack.Providers.Rackspace.Objects.Monitoring
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Newtonsoft.Json;

    [JsonObject(MemberSerialization.OptIn)]
    public class Entity : EntityConfiguration
    {
        [JsonProperty("id")]
        private EntityId _id;

        /// <summary>
        /// Initializes a new instance of the <see cref="Entity"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected Entity()
        {
        }

        public EntityId Id
        {
            get
            {
                return _id;
            }
        }
    }
}
