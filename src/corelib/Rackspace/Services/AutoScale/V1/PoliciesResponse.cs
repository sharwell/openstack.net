namespace Rackspace.Services.AutoScale.V1
{
    using System.Collections.ObjectModel;
    using Newtonsoft.Json;
    using OpenStack.ObjectModel;

    [JsonObject(MemberSerialization.OptIn)]
    public class PoliciesResponse : ExtensibleJsonObject
    {
        [JsonProperty("policies", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private Policy[] _policies;

        /// <summary>
        /// Initializes a new instance of the <see cref="PoliciesResponse"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected PoliciesResponse()
        {
        }

        public ReadOnlyCollection<Policy> Policies
        {
            get
            {
                if (_policies == null)
                    return null;

                return new ReadOnlyCollection<Policy>(_policies);
            }
        }
    }
}
