namespace OpenStack.Services.Databases.V1
{
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using OpenStack.ObjectModel;

    [JsonObject(MemberSerialization.OptIn)]
    public class ResizeData : ExtensibleJsonObject
    {
        [JsonProperty("flavorRef", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private FlavorRef _flavor;

        /// <summary>
        /// Initializes a new instance of the <see cref="ResizeData"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected ResizeData()
        {
        }

        public ResizeData(FlavorRef flavor)
        {
            _flavor = flavor;
        }

        public ResizeData(FlavorRef flavor, params JProperty[] extensionData)
            : base(extensionData)
        {
            _flavor = flavor;
        }

        public ResizeData(FlavorRef flavor, IDictionary<string, JToken> extensionData)
            : base(extensionData)
        {
            _flavor = flavor;
        }

        public FlavorRef Flavor
        {
            get
            {
                return _flavor;
            }
        }
    }
}
