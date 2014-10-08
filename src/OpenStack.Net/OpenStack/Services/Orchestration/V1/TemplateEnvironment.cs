namespace OpenStack.Services.Orchestration.V1
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using OpenStack.ObjectModel;

    [JsonObject(MemberSerialization.OptIn)]
    public class TemplateEnvironment : ExtensibleJsonObject
    {
        [JsonProperty("parameters", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private IDictionary<string, JToken> _parameters;

        [JsonProperty("resource_registry", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private ResourceRegistry _resourceRegistry;

        /// <summary>
        /// Initializes a new instance of the <see cref="TemplateEnvironment"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected TemplateEnvironment()
        {
        }

        public TemplateEnvironment(IDictionary<string, JToken> parameters, ResourceRegistry resourceRegistry)
        {
            _parameters = parameters;
            _resourceRegistry = resourceRegistry;
        }

        public ResourceRegistry ResourceRegistry
        {
            get
            {
                return _resourceRegistry;
            }
        }

        public ReadOnlyDictionary<string, JToken> Parameters
        {
            get
            {
                if (_parameters == null)
                    return null;

                return new ReadOnlyDictionary<string, JToken>(_parameters);
            }
        }
    }
}