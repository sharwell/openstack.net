namespace OpenStack.Services.Networking.V2.Metering
{
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using OpenStack.ObjectModel;

    [JsonObject(MemberSerialization.OptIn)]
    public class MeteringLabelRequest : ExtensibleJsonObject
    {
        [JsonProperty("metering_label", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private MeteringLabelData _meteringLabel;

        /// <summary>
        /// Initializes a new instance of the <see cref="MeteringLabelRequest"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected MeteringLabelRequest()
        {
        }

        public MeteringLabelRequest(MeteringLabelData meteringLabel)
        {
            _meteringLabel = meteringLabel;
        }

        public MeteringLabelRequest(MeteringLabelData meteringLabel, params JProperty[] extensionData)
            : base(extensionData)
        {
            _meteringLabel = meteringLabel;
        }

        public MeteringLabelRequest(MeteringLabelData meteringLabel, IDictionary<string, JToken> extensionData)
            : base(extensionData)
        {
            _meteringLabel = meteringLabel;
        }

        public MeteringLabelData MeteringLabel
        {
            get
            {
                return _meteringLabel;
            }
        }
    }
}
