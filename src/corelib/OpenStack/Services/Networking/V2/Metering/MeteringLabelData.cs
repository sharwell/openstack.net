namespace OpenStack.Services.Networking.V2.Metering
{
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using OpenStack.ObjectModel;

    [JsonObject(MemberSerialization.OptIn)]
    public class MeteringLabelData : ExtensibleJsonObject
    {
        [JsonProperty("name", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private string _name;

        [JsonProperty("description", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private string _description;

        /// <summary>
        /// Initializes a new instance of the <see cref="MeteringLabelData"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected MeteringLabelData()
        {
        }

        public MeteringLabelData(string name)
        {
            _name = name;
        }

        public MeteringLabelData(string name, string description, params JProperty[] extensionData)
            : base(extensionData)
        {
            _name = name;
        }

        public MeteringLabelData(string name, string description, IDictionary<string, JToken> extensionData)
            : base(extensionData)
        {
            _name = name;
        }

        public string Name
        {
            get
            {
                return _name;
            }
        }

        public string Description
        {
            get
            {
                return _description;
            }
        }
    }
}
