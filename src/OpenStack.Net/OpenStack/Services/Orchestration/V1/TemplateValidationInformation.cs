namespace OpenStack.Services.Orchestration.V1
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using OpenStack.ObjectModel;

    /// <summary>
    /// This class models the JSON representation of the stack template validation information returned by the
    /// <see cref="ValidateTemplateApiCall"/> HTTP API call in the OpenStack Orchestration Service.
    /// </summary>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    [JsonObject(MemberSerialization.OptIn)]
    public class TemplateValidationInformation : ExtensibleJsonObject
    {
#pragma warning disable 649 // Field 'fieldName' is never assigned to, and will always have its default value {value}
        /// <summary>
        /// This is the backing field for the <see cref="Description"/> property.
        /// </summary>
        [JsonProperty("Description", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private string _description;

        /// <summary>
        /// This is the backing field for the <see cref="Parameters"/> property.
        /// </summary>
        [JsonProperty("Parameters", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private IDictionary<TemplateParameterName, JToken> _parameters;
#pragma warning restore 649

        /// <summary>
        /// Initializes a new instance of the <see cref="TemplateValidationInformation"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected TemplateValidationInformation()
        {
        }

        /// <summary>
        /// Gets a description of the stack template.
        /// </summary>
        /// <value>
        /// <para>A description of the stack template.</para>
        /// <token>NullIfNotIncluded</token>
        /// </value>
        public string Description
        {
            get
            {
                return _description;
            }
        }

        /// <summary>
        /// Gets a collection of parameters defined by a stack template.
        /// </summary>
        /// <value>
        /// <para>A collection of parameters defined by a stack template.</para>
        /// <token>NullIfNotIncluded</token>
        /// </value>
        public ReadOnlyDictionary<TemplateParameterName, JToken> Parameters
        {
            get
            {
                if (_parameters == null)
                    return null;

                return new ReadOnlyDictionary<TemplateParameterName, JToken>(_parameters);
            }
        }
    }
}
