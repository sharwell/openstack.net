namespace Rackspace.Services.AutoScale.V1
{
    using Newtonsoft.Json;
    using OpenStack.ObjectModel;

    [JsonObject(MemberSerialization.OptIn)]
    public class PolicyResponse : ExtensibleJsonObject
    {
        /// <summary>
        /// This is the backing field for the <see cref="Policy"/> property.
        /// </summary>
        [JsonProperty("policy", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private Policy _policy;

        /// <summary>
        /// Initializes a new instance of the <see cref="PolicyResponse"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected PolicyResponse()
        {
        }

        public Policy Policy
        {
            get
            {
                return _policy;
            }
        }
    }
}
