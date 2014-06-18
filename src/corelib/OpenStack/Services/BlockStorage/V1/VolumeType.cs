namespace OpenStack.Services.BlockStorage.V1
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using OpenStack.ObjectModel;

#if !NET45PLUS
    using OpenStack.Collections;
#endif

    [JsonObject(MemberSerialization.OptIn)]
    public class VolumeType : ExtensibleJsonObject
    {
        [JsonProperty("id", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private VolumeTypeId _id;

        [JsonProperty("name", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private string _name;

        [JsonProperty("extra_specs", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private IDictionary<string, JToken> _extraSpecs;

        /// <summary>
        /// Initializes a new instance of the <see cref="VolumeType"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected VolumeType()
        {
        }

        public VolumeTypeId Id
        {
            get
            {
                return _id;
            }
        }

        public string Name
        {
            get
            {
                return _name;
            }
        }

        public ReadOnlyDictionary<string, JToken> ExtraSpecs
        {
            get
            {
                if (_extraSpecs == null)
                    return null;

                return new ReadOnlyDictionary<string, JToken>(_extraSpecs);
            }
        }
    }
}
