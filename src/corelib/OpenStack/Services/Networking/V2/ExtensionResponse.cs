namespace OpenStack.Services.Networking.V2
{
    using Newtonsoft.Json;
    using OpenStack.ObjectModel;

    [JsonObject(MemberSerialization.OptIn)]
    public class ExtensionResponse : ExtensibleJsonObject
    {
        [JsonProperty("extension", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private Extension _extension;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExtensionResponse"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected ExtensionResponse()
        {
        }

        public Extension Extension
        {
            get
            {
                return _extension;
            }
        }
    }
}
