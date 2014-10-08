namespace OpenStack.Services.Orchestration.V1
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using OpenStack.ObjectModel;

    [JsonObject(MemberSerialization.OptIn)]
    public class ResourceTypeTemplate : ExtensibleJsonObject
    {
#pragma warning disable 649 // Field 'fieldName' is never assigned to, and will always have its default value {value}
        /// <summary>
        /// This is the backing field for the <see cref="HeatTemplateFormatVersion"/> property.
        /// </summary>
        [JsonProperty("HeatTemplateFormatVersion", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private string _heatTemplateFormatVersion;

        /// <summary>
        /// This is the backing field for the <see cref="Outputs"/> property.
        /// </summary>
        [JsonProperty("Outputs", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private IDictionary<string, ResourceTypeTemplateOutput> _outputs;

        /// <summary>
        /// This is the backing field for the <see cref="Parameters"/> property.
        /// </summary>
        [JsonProperty("Parameters", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private IDictionary<string, ResourceTypeTemplateParameter> _parameters;

        /// <summary>
        /// This is the backing field for the <see cref="Resources"/> property.
        /// </summary>
        [JsonProperty("Resources", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private IDictionary<string, ResourceTypeTemplateResource> _resources;
#pragma warning restore 649

        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceTypeTemplate"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected ResourceTypeTemplate()
        {
        }

        public string HeatTemplateFormatVersion
        {
            get
            {
                return _heatTemplateFormatVersion;
            }
        }

        public ReadOnlyDictionary<string, ResourceTypeTemplateOutput> Outputs
        {
            get
            {
                if (_outputs == null)
                    return null;

                return new ReadOnlyDictionary<string, ResourceTypeTemplateOutput>(_outputs);
            }
        }

        public ReadOnlyDictionary<string, ResourceTypeTemplateParameter> Parameters
        {
            get
            {
                if (_parameters == null)
                    return null;

                return new ReadOnlyDictionary<string, ResourceTypeTemplateParameter>(_parameters);
            }
        }

        public ReadOnlyDictionary<string, ResourceTypeTemplateResource> Resources
        {
            get
            {
                if (_resources == null)
                    return null;

                return new ReadOnlyDictionary<string, ResourceTypeTemplateResource>(_resources);
            }
        }
    }
}
