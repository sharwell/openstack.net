namespace OpenStack.Services.Compute.V2
{
    using Newtonsoft.Json;
    using OpenStack.ObjectModel;

    [JsonObject(MemberSerialization.OptIn)]
    public class FlavorResponse : ExtensibleJsonObject
    {
#pragma warning disable 649 // Field 'fieldName' is never assigned to, and will always have its default value {value}
        /// <summary>
        /// This is the backing field for the <see cref="Flavor"/> property.
        /// </summary>
        [JsonProperty("flavor", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private Flavor _flavor;
#pragma warning restore 649

        /// <summary>
        /// Initializes a new instance of the <see cref="FlavorResponse"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected FlavorResponse()
        {
        }

        public Flavor Flavor
        {
            get
            {
                return _flavor;
            }
        }
    }
}
