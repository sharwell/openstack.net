namespace OpenStack.Services.Networking.V2.LoadBalancer
{
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using OpenStack.ObjectModel;

    [JsonObject(MemberSerialization.OptIn)]
    public class PoolRequest : ExtensibleJsonObject
    {
        [JsonProperty("pool", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private PoolData _pool;

        /// <summary>
        /// Initializes a new instance of the <see cref="PoolRequest"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected PoolRequest()
        {
        }

        public PoolRequest(PoolData pool)
        {
            _pool = pool;
        }

        public PoolRequest(PoolData pool, params JProperty[] extensionData)
            : base(extensionData)
        {
            _pool = pool;
        }

        public PoolRequest(PoolData pool, IDictionary<string, JToken> extensionData)
            : base(extensionData)
        {
            _pool = pool;
        }

        public PoolData Pool
        {
            get
            {
                return _pool;
            }
        }
    }
}
