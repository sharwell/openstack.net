namespace OpenStack.Services.Orchestration.V1
{
    using System;
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using OpenStack.ObjectModel;

    /// <summary>
    /// This class models the JSON representation of the description of an output within a <see cref="StackTemplate"/>
    /// in the OpenStack Orchestration Service.
    /// </summary>
    /// <remarks>
    /// <para>This class only contains information about the <em>form</em> an output will take. The actual name of the
    /// output(s) are defined by the keys of the <see cref="StackTemplate.Outputs"/> dictionary.</para>
    /// </remarks>
    /// <seealso cref="StackTemplate.Outputs"/>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    [JsonObject(MemberSerialization.OptIn)]
    public class TemplateOutput : ExtensibleJsonObject
    {
        /// <summary>
        /// This is the backing field for the <see cref="Description"/> property.
        /// </summary>
        [JsonProperty("description", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private string _description;

        /// <summary>
        /// This is the backing field for the <see cref="Value"/> property.
        /// </summary>
        [JsonProperty("value", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private JToken _value;

        /// <summary>
        /// Initializes a new instance of the <see cref="TemplateOutput"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected TemplateOutput()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TemplateOutput"/> class with the specified description and
        /// value.
        /// </summary>
        /// <param name="description">A description of the template output.</param>
        /// <param name="value">The value of the template output.</param>
        public TemplateOutput(string description, JToken value)
        {
            _description = description;
            _value = value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TemplateOutput"/> class with the specified description, value,
        /// and extension data.
        /// </summary>
        /// <param name="description">A description of the template output.</param>
        /// <param name="value">The value of the template output.</param>
        /// <param name="extensionData">The extension data.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="extensionData"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">If <paramref name="extensionData"/> contains any <see langword="null"/> values.</exception>
        public TemplateOutput(string description, JToken value, params JProperty[] extensionData)
            : base(extensionData)
        {
            _description = description;
            _value = value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TemplateOutput"/> class with the specified description, value,
        /// and extension data.
        /// </summary>
        /// <param name="description">A description of the template output.</param>
        /// <param name="value">The value of the template output.</param>
        /// <param name="extensionData">The extension data.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="extensionData"/> is <see langword="null"/>.</exception>
        public TemplateOutput(string description, JToken value, IDictionary<string, JToken> extensionData)
            : base(extensionData)
        {
            _description = description;
            _value = value;
        }

        /// <summary>
        /// Gets a description of the template output.
        /// </summary>
        /// <value>
        /// <para>A description of the template output.</para>
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
        /// Gets the value of the template output.
        /// </summary>
        /// <remarks>
        /// <para>Typically, this will be resolved by means of a function, for example getting an attribute value of one
        /// of the stack's resources.</para>
        /// </remarks>
        /// <value>
        /// <para>The value of the template output.</para>
        /// <token>NullIfNotIncluded</token>
        /// </value>
        public JToken Value
        {
            get
            {
                return _value;
            }
        }
    }
}
