namespace OpenStack.Services.Networking.V2.SecurityGroups
{
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using OpenStack.ObjectModel;

    [JsonObject(MemberSerialization.OptIn)]
    public class SecurityGroupData : ExtensibleJsonObject
    {
        [JsonProperty("name", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private string _name;

        [JsonProperty("description", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private string _description;

        /// <summary>
        /// Initializes a new instance of the <see cref="SecurityGroupData"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected SecurityGroupData()
        {
        }

        public SecurityGroupData(string name)
        {
            _name = name;
        }

        public SecurityGroupData(string name, string description, params JProperty[] extensionData)
            : base(extensionData)
        {
            _name = name;
            _description = description;
        }

        public SecurityGroupData(string name, string description, IDictionary<string, JToken> extensionData)
            : base(extensionData)
        {
            _name = name;
            _description = description;
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
