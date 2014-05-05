namespace OpenStack.Services.Networking.V2.Metering
{
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using OpenStack.ObjectModel;

    [JsonObject(MemberSerialization.OptIn)]
    public class MeteringLabelRuleRequest : ExtensibleJsonObject
    {
        [JsonProperty("metering_label_rule", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private MeteringLabelRuleData _meteringLabelRule;

        /// <summary>
        /// Initializes a new instance of the <see cref="MeteringLabelRuleRequest"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected MeteringLabelRuleRequest()
        {
        }

        public MeteringLabelRuleRequest(MeteringLabelRuleData meteringLabelRule)
        {
            _meteringLabelRule = meteringLabelRule;
        }

        public MeteringLabelRuleRequest(MeteringLabelRuleData meteringLabelRule, params JProperty[] extensionData)
            : base(extensionData)
        {
            _meteringLabelRule = meteringLabelRule;
        }

        public MeteringLabelRuleRequest(MeteringLabelRuleData meteringLabelRule, IDictionary<string, JToken> extensionData)
            : base(extensionData)
        {
            _meteringLabelRule = meteringLabelRule;
        }

        public MeteringLabelRuleData MeteringLabelRule
        {
            get
            {
                return _meteringLabelRule;
            }
        }
    }
}
