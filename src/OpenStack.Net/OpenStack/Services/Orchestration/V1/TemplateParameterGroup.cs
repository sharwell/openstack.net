namespace OpenStack.Services.Orchestration.V1
{
    using System.Collections.ObjectModel;
    using Newtonsoft.Json;
    using OpenStack.ObjectModel;

    /// <summary>
    /// This class models the JSON representation of a parameter group within a stack template.
    /// </summary>
    /// <seealso cref="StackTemplate.ParameterGroups"/>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    [JsonObject(MemberSerialization.OptIn)]
    public class TemplateParameterGroup : ExtensibleJsonObject, ITemplateParameterGroup
    {
#pragma warning disable 649 // Field 'fieldName' is never assigned to, and will always have its default value {value}
        /// <summary>
        /// This is the backing field for the <see cref="Label"/> property.
        /// </summary>
        [JsonProperty("label", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private string _label;

        /// <summary>
        /// This is the backing field for the <see cref="Description"/> property.
        /// </summary>
        [JsonProperty("description", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private string _description;

        /// <summary>
        /// This is the backing field for the <see cref="Parameters"/> property.
        /// </summary>
        [JsonProperty("properties", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private TemplateParameterName[] _parameters;
#pragma warning restore 649

        /// <summary>
        /// Initializes a new instance of the <see cref="TemplateParameterGroup"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected TemplateParameterGroup()
        {
        }

        /// <summary>
        /// Gets the display name of the parameter group.
        /// </summary>
        /// <value>
        /// <para>The display name of the parameter group.</para>
        /// <token>NullIfNotIncluded</token>
        /// </value>
        public string Label
        {
            get
            {
                return _label;
            }
        }

        /// <summary>
        /// Gets a description of the parameter group.
        /// </summary>
        /// <value>
        /// <para>The display name of the parameter group.</para>
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
        /// Gets the names of the template parameters which are included in the group.
        /// </summary>
        /// <value>
        /// <para>A collection of <see cref="TemplateParameterName"/> instances identifying the parameters which are
        /// included in this group.</para>
        /// <token>NullIfNotIncluded</token>
        /// </value>
        public ReadOnlyCollection<TemplateParameterName> Parameters
        {
            get
            {
                if (_parameters == null)
                    return null;

                return new ReadOnlyCollection<TemplateParameterName>(_parameters);
            }
        }
    }
}