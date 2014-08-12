namespace Rackspace.Services.AutoScale.V1
{
    using Newtonsoft.Json;
    using OpenStack.ObjectModel;

    [JsonObject(MemberSerialization.OptIn)]
    public class GroupConfigurationResponse : ExtensibleJsonObject
    {
        [JsonProperty("groupConfiguration", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private GroupConfiguration _groupConfiguration;

        /// <summary>
        /// Initializes a new instance of the <see cref="GroupConfigurationResponse"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected GroupConfigurationResponse()
        {
        }

        public GroupConfiguration GroupConfiguration
        {
            get
            {
                return _groupConfiguration;
            }
        }
    }
}
