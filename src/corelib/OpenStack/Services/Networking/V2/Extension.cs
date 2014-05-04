namespace OpenStack.Services.Networking.V2
{
    using System;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using OpenStack.ObjectModel;

    [JsonObject(MemberSerialization.OptIn)]
    public class Extension : ExtensibleJsonObject
    {
        [JsonProperty("alias", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private ExtensionAlias _alias;

        [JsonProperty("name", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private string _name;

        [JsonProperty("description", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private string _description;

        [JsonProperty("namespace", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private string _namespace;

        [JsonProperty("updated", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private DateTimeOffset? _updated;

        [JsonProperty("links", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private JToken _links; // seriously, what is this?

        /// <summary>
        /// Initializes a new instance of the <see cref="Extension"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected Extension()
        {
        }

        public ExtensionAlias Alias
        {
            get
            {
                return _alias;
            }
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

        public string Namespace
        {
            get
            {
                return _namespace;
            }
        }

        public DateTimeOffset? LastModified
        {
            get
            {
                return _updated;
            }
        }

        public JToken Links
        {
            get
            {
                return _links;
            }
        }
    }
}
