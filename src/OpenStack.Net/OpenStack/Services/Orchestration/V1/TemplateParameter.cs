namespace OpenStack.Services.Orchestration.V1
{
    using System;
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using OpenStack.ObjectModel;

    /// <summary>
    /// This class models the JSON representation of a parameter for a stack template.
    /// </summary>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    [JsonObject(MemberSerialization.OptIn)]
    public class TemplateParameter : ExtensibleJsonObject, ITemplateParameter
    {
        /// <summary>
        /// This is the backing field for the <see cref="Type"/> property.
        /// </summary>
        [JsonProperty("type", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private TemplateParameterType _type;

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
        /// This is the backing field for the <see cref="DefaultValue"/> property.
        /// </summary>
        [JsonProperty("default", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private JToken _defaultValue;

        /// <summary>
        /// This is the backing field for the <see cref="Hidden"/> property.
        /// </summary>
        [JsonProperty("hidden", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private bool? _hidden;

        /// <summary>
        /// This is the backing field for the <see cref="Constraints"/> property.
        /// </summary>
        [JsonProperty("constraints", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private JToken _constraints;

        /// <summary>
        /// Initializes a new instance of the <see cref="TemplateParameter"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected TemplateParameter()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TemplateParameter"/> class with the specified values.
        /// </summary>
        /// <param name="type">The type of the template parameter.</param>
        /// <param name="label">The display name of the template parameter.</param>
        /// <param name="description">A description of the template parameter.</param>
        /// <param name="defaultValue">The default value of the template parameter.</param>
        /// <param name="hidden"><see langword="true"/> if the parameter value should be hidden when returning details
        /// about an instantiated stack; otherwise, <see langword="false"/>.</param>
        /// <param name="constraints">A collection of additional constraints which apply to the template
        /// parameter.</param>
        public TemplateParameter(TemplateParameterType type, string label, string description, JToken defaultValue, bool? hidden, JToken constraints)
        {
            _type = type;
            _label = label;
            _description = description;
            _defaultValue = defaultValue;
            _hidden = hidden;
            _constraints = constraints;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TemplateParameter"/> class with the specified values and
        /// extension data.
        /// </summary>
        /// <param name="type">The type of the template parameter.</param>
        /// <param name="label">The display name of the template parameter.</param>
        /// <param name="description">A description of the template parameter.</param>
        /// <param name="defaultValue">The default value of the template parameter.</param>
        /// <param name="hidden"><see langword="true"/> if the parameter value should be hidden when returning details
        /// about an instantiated stack; otherwise, <see langword="false"/>.</param>
        /// <param name="constraints">A collection of additional constraints which apply to the template
        /// parameter.</param>
        /// <param name="extensionData">The extension data.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="extensionData"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">If <paramref name="extensionData"/> contains any <see langword="null"/> values.</exception>
        public TemplateParameter(TemplateParameterType type, string label, string description, JToken defaultValue, bool? hidden, JToken constraints, params JProperty[] extensionData)
            : base(extensionData)
        {
            _type = type;
            _label = label;
            _description = description;
            _defaultValue = defaultValue;
            _hidden = hidden;
            _constraints = constraints;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TemplateParameter"/> class with the specified values and
        /// extension data.
        /// </summary>
        /// <param name="type">The type of the template parameter.</param>
        /// <param name="label">The display name of the template parameter.</param>
        /// <param name="description">A description of the template parameter.</param>
        /// <param name="defaultValue">The default value of the template parameter.</param>
        /// <param name="hidden"><see langword="true"/> if the parameter value should be hidden when returning details
        /// about an instantiated stack; otherwise, <see langword="false"/>.</param>
        /// <param name="constraints">A collection of additional constraints which apply to the template
        /// parameter.</param>
        /// <param name="extensionData">The extension data.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="extensionData"/> is <see langword="null"/>.</exception>
        public TemplateParameter(TemplateParameterType type, string label, string description, JToken defaultValue, bool? hidden, JToken constraints, IDictionary<string, JToken> extensionData)
            : base(extensionData)
        {
            _type = type;
            _label = label;
            _description = description;
            _defaultValue = defaultValue;
            _hidden = hidden;
            _constraints = constraints;
        }

        /// <summary>
        /// Gets the type of the template parameter.
        /// </summary>
        /// <value>
        /// <para>The type of the template parameter.</para>
        /// <token>NullIfNotIncluded</token>
        /// </value>
        public TemplateParameterType Type
        {
            get
            {
                return _type;
            }
        }

        ITemplateParameterType ITemplateParameter.Type
        {
            get
            {
                return Type;
            }
        }

        /// <summary>
        /// Gets the display name of the template parameter.
        /// </summary>
        /// <value>
        /// <para>The name of the template parameter.</para>
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
        /// Gets a description of the template parameter.
        /// </summary>
        /// <value>
        /// <para>A description of the template parameter.</para>
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
        /// Gets the default value of the template parameter.
        /// </summary>
        /// <value>
        /// <para>The default value of the template parameter.</para>
        /// <token>NullIfNotIncluded</token>
        /// </value>
        public JToken DefaultValue
        {
            get
            {
                return _defaultValue;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the template parameter value should be hidden when showing information about
        /// an instantiated stack.
        /// </summary>
        /// <remarks>
        /// <para>This property may be used to hide sensitive information, such as passwords, which was specified as an
        /// argument while instantiating a stack template.</para>
        /// </remarks>
        /// <value>
        /// <para><see langword="true"/> if the parameter value should be hidden when returning information about an
        /// instantiated stack.</para>
        /// <para>-or-</para>
        /// <para><see langword="false"/> if the parameter value should be included when returning information about an
        /// instantiated stack.</para>
        /// <token>NullIfNotIncluded</token>
        /// </value>
        public bool? Hidden
        {
            get
            {
                return _hidden;
            }
        }

        /// <summary>
        /// Gets a collection of additional constraints which apply to the template parameter.
        /// </summary>
        /// <value>
        /// <para>A collection of additional constraints which apply to the template parameter.</para>
        /// <token>NullIfNotIncluded</token>
        /// </value>
        public JToken Constraints
        {
            get
            {
                return _constraints;
            }
        }
    }
}
