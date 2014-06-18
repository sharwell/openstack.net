namespace OpenStack.Services.BlockStorage.V1.QuotaSets
{
    using Newtonsoft.Json;
    using OpenStack.ObjectModel;

    [JsonObject(MemberSerialization.OptIn)]
    public class QuotaSetDetailsResponse : ExtensibleJsonObject
    {
        [JsonProperty("quota_set", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private QuotaSetDetails _quotaSetDetails;

        /// <summary>
        /// Initializes a new instance of the <see cref="QuotaSetDetailsResponse"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected QuotaSetDetailsResponse()
        {
        }

        public QuotaSetDetails QuotaSetDetails
        {
            get
            {
                return _quotaSetDetails;
            }
        }
    }
}