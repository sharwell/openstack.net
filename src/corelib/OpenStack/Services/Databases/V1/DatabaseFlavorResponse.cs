namespace OpenStack.Services.Databases.V1
{
    using Newtonsoft.Json;
    using OpenStack.ObjectModel;

    [JsonObject(MemberSerialization.OptIn)]
    public class DatabaseFlavorResponse : ExtensibleJsonObject
    {
        [JsonProperty("flavor", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private DatabaseFlavor _flavor;

        /// <summary>
        /// Initializes a new instance of the <see cref="DatabaseFlavorResponse"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected DatabaseFlavorResponse()
        {
        }

        public DatabaseFlavor Flavor
        {
            get
            {
                return _flavor;
            }
        }
    }
}