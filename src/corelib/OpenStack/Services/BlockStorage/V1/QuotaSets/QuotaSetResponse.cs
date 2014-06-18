namespace OpenStack.Services.BlockStorage.V1.QuotaSets
{
    using Newtonsoft.Json;
    using OpenStack.ObjectModel;

    [JsonObject(MemberSerialization.OptIn)]
    public class QuotaSetResponse : ExtensibleJsonObject
    {
        [JsonProperty("quota_set", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private QuotaSet _quotaSet;

        /// <summary>
        /// Initializes a new instance of the <see cref="QuotaSetResponse"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected QuotaSetResponse()
        {
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