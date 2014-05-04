namespace OpenStack.Services.Networking.V2
{
    using System.Collections.ObjectModel;
    using Newtonsoft.Json;
    using OpenStack.ObjectModel;

    [JsonObject(MemberSerialization.OptIn)]
    public class ApiDetails : ExtensibleJsonObject
    {
        [JsonProperty("resources", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private Resource[] _resources;

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiDetails"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected ApiDetails()
        {
        }

        public ReadOnlyCollection<Resource> Resources
        {
            get
            {
                if (_resources == null)
                    return null;

                return new ReadOnlyCollection<Resource>(_resources);
            }
        }
    }
}
