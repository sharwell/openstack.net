namespace OpenStack.Services.Networking.V2.SecurityGroups
{
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using OpenStack.ObjectModel;

    [JsonObject(MemberSerialization.OptIn)]
    public class SecurityGroupRuleRequest : ExtensibleJsonObject
    {
        [JsonProperty("security_group_rule", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private SecurityGroupRuleData _securityGroupRule;

        /// <summary>
        /// Initializes a new instance of the <see cref="SecurityGroupRuleRequest"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected SecurityGroupRuleRequest()
        {
        }

        public SecurityGroupRuleRequest(SecurityGroupRuleData securityGroupRule)
        {
            _securityGroupRule = securityGroupRule;
        }

        public SecurityGroupRuleRequest(SecurityGroupRuleData securityGroupRule, params JProperty[] extensionData)
            : base(extensionData)
        {
            _securityGroupRule = securityGroupRule;
        }

        public SecurityGroupRuleRequest(SecurityGroupRuleData securityGroupRule, IDictionary<string, JToken> extensionData)
            : base(extensionData)
        {
            _securityGroupRule = securityGroupRule;
        }

        public SecurityGroupRuleData SecurityGroupRule
        {
            get
            {
                return _securityGroupRule;
            }
        }
    }
}
