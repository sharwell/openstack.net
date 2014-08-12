namespace OpenStack.Services.Databases.V1
{
    using Newtonsoft.Json;
    using OpenStack.ObjectModel;

    [JsonObject(MemberSerialization.OptIn)]
    public class RootEnabledResponse : ExtensibleJsonObject
    {
        [JsonProperty("rootEnabled", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private bool? _rootEnabled;

        /// <summary>
        /// Initializes a new instance of the <see cref="RootEnabledResponse"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected RootEnabledResponse()
        {
        }

        public bool? RootEnabled
        {
            get
            {
                return _rootEnabled;
            }
        }
    }
}
