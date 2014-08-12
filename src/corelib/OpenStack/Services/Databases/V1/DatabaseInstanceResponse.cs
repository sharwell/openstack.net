namespace OpenStack.Services.Databases.V1
{
    using Newtonsoft.Json;
    using OpenStack.ObjectModel;

    [JsonObject(MemberSerialization.OptIn)]
    public class DatabaseInstanceResponse : ExtensibleJsonObject
    {
        [JsonProperty("instance", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private DatabaseInstance _instance;

        /// <summary>
        /// Initializes a new instance of the <see cref="DatabaseInstanceResponse"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected DatabaseInstanceResponse()
        {
        }

        public DatabaseInstance Instance
        {
            get
            {
                return _instance;
            }
        }
    }
}
