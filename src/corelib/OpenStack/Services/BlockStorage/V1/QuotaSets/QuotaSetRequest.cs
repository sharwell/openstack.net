namespace OpenStack.Services.BlockStorage.V1.QuotaSets
{
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using OpenStack.ObjectModel;

    [JsonObject(MemberSerialization.OptIn)]
    public class QuotaSetRequest : ExtensibleJsonObject
    {
        [JsonProperty("quota_set", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private QuotaSet _quotaSet;

        /// <summary>
        /// Initializes a new instance of the <see cref="QuotaSetRequest"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected QuotaSetRequest()
        {
        }

        public QuotaSetRequest(QuotaSet quotaSet)
        {
            _quotaSet = quotaSet;
        }

        public QuotaSetRequest(QuotaSet quotaSet, params JProperty[] extensionData)
            : base(extensionData)
        {
            _quotaSet = quotaSet;
        }

        public QuotaSetRequest(QuotaSet quotaSet, IDictionary<string, JToken> extensionData)
            : base(extensionData)
        {
            _quotaSet = quotaSet;
        }

        public QuotaSet QuotaSet
        {
            get
            {
                return _quotaSet;
            }
        }
    }
}