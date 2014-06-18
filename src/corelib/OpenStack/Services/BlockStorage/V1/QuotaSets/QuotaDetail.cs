namespace OpenStack.Services.BlockStorage.V1.QuotaSets
{
    using Newtonsoft.Json;
    using OpenStack.ObjectModel;

    [JsonObject(MemberSerialization.OptIn)]
    public class QuotaDetail : ExtensibleJsonObject
    {
        [JsonProperty("in_use", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private long? _inUse;

        [JsonProperty("limit", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private long? _limit;

        [JsonProperty("reserved", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private long? _reserved;

        /// <summary>
        /// Initializes a new instance of the <see cref="QuotaDetail"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected QuotaDetail()
        {
        }

        public long? InUse
        {
            get
            {
                return _inUse;
            }
        }

        public long? Limit
        {
            get
            {
                return _limit;
            }
        }

        public long? Reserved
        {
            get
            {
                return _reserved;
            }
        }
    }
}