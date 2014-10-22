namespace OpenStack.Services.Orchestration.V1
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using OpenStack.ObjectModel;

    [JsonObject(MemberSerialization.OptIn)]
    public class StackTemplate : ExtensibleJsonObject, ITemplate
    {
        /// <summary>
        /// This is the backing field for the <see cref="TemplateVersion"/> property.
        /// </summary>
        [JsonProperty("heat_template_version", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private TemplateVersion _templateVersion;

        /// <summary>
        /// This is the backing field for the <see cref="Description"/> property.
        /// </summary>
        [JsonProperty("description", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private string _description;

        /// <summary>
        /// This is the backing field for the <see cref="ParameterGroups"/> property.
        /// </summary>
        [JsonProperty("parameter_groups", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private TemplateParameterGroup[] _parameterGroups;

        /// <summary>
        /// This is the backing field for the <see cref="Parameters"/> property.
        /// </summary>
        [JsonProperty("parameters", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private IDictionary<TemplateParameterName, TemplateParameter> _parameters;

        /// <summary>
        /// This is the backing field for the <see cref="Resources"/> property.
        /// </summary>
        [JsonProperty("resources", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private IDictionary<string, TemplateResource> _resources;

        /// <summary>
        /// This is the backing field for the <see cref="Outputs"/> property.
        /// </summary>
        [JsonProperty("outputs", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private IDictionary<string, TemplateOutput> _outputs;

        /// <summary>
        /// Initializes a new instance of the <see cref="StackTemplate"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected StackTemplate()
        {
        }

        public StackTemplate(TemplateVersion templateVersion, string description, IDictionary<string, JToken> parameters, IDictionary<string, JToken> resources, IDictionary<string, JToken> outputs, params JProperty[] extensionData)
            : base(extensionData)
        {
            _templateVersion = templateVersion;
            _description = description;
        }

        public StackTemplate(TemplateVersion templateVersion, string description, IDictionary<string, JToken> parameters, IDictionary<string, JToken> resources, IDictionary<string, JToken> outputs, IDictionary<string, JToken> extensionData)
            : base(extensionData)
        {
            _templateVersion = templateVersion;
            _description = description;
        }

        public TemplateVersion TemplateVersion
        {
            get
            {
                return _templateVersion;
            }
        }

        public string Description
        {
            get
            {
                return _description;
            }
        }

        public ReadOnlyCollection<TemplateParameterGroup> ParameterGroups
        {
            get
            {
                if (_parameterGroups == null)
                    return null;

                return new ReadOnlyCollection<TemplateParameterGroup>(_parameterGroups);
            }
        }

        /// <inheritdoc/>
        ReadOnlyCollection<ITemplateParameterGroup> ITemplate.ParameterGroups
        {
            get
            {
                if (_parameterGroups == null)
                    return null;

                return new ReadOnlyCollection<ITemplateParameterGroup>(_parameterGroups);
            }
        }

        public ReadOnlyDictionary<TemplateParameterName, TemplateParameter> Parameters
        {
            get
            {
                if (_parameters == null)
                    return null;

                return new ReadOnlyDictionary<TemplateParameterName, TemplateParameter>(_parameters);
            }
        }

        /// <inheritdoc/>
        ReadOnlyDictionary<TemplateParameterName, ITemplateParameter> ITemplate.Parameters
        {
            get
            {
                if (_parameters == null)
                    return null;

                return new ReadOnlyDictionary<TemplateParameterName, ITemplateParameter>(_parameters.ToDictionary<KeyValuePair<TemplateParameterName, TemplateParameter>, TemplateParameterName, ITemplateParameter>(i => i.Key, i => i.Value));
            }
        }

        public ReadOnlyDictionary<string, TemplateResource> Resources
        {
            get
            {
                if (_resources == null)
                    return null;

                return new ReadOnlyDictionary<string, TemplateResource>(_resources);
            }
        }

        /// <inheritdoc/>
        ReadOnlyDictionary<string, ITemplateResource> ITemplate.Resources
        {
            get
            {
                if (_resources == null)
                    return null;

                return new ReadOnlyDictionary<string, ITemplateResource>(_resources.ToDictionary<KeyValuePair<string, TemplateResource>, string, ITemplateResource>(i => i.Key, i => i.Value));
            }
        }

        public ReadOnlyDictionary<string, TemplateOutput> Outputs
        {
            get
            {
                if (_outputs == null)
                    return null;

                return new ReadOnlyDictionary<string, TemplateOutput>(_outputs);
            }
        }

        /// <inheritdoc/>
        ReadOnlyDictionary<string, ITemplateOutput> ITemplate.Outputs
        {
            get
            {
                if (_outputs == null)
                    return null;

                return new ReadOnlyDictionary<string, ITemplateOutput>(_outputs.ToDictionary<KeyValuePair<string, TemplateOutput>, string, ITemplateOutput>(i => i.Key, i => i.Value));
            }
        }
    }
}
