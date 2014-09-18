namespace OpenStack.Services.Orchestration.V1
{
    using System;
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using OpenStack.ObjectModel;

    /// <summary>
    /// This class models the JSON representation of the request body for the <see cref="ValidateTemplateApiCall"/> HTTP
    /// API call in the OpenStack Orchestration Service.
    /// </summary>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    [JsonObject(MemberSerialization.OptIn)]
    public class ValidateTemplateRequest : ExtensibleJsonObject
    {
        /// <summary>
        /// This is the backing field for the <see cref="TemplateUri"/> property.
        /// </summary>
        [JsonProperty("template_url", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private string _templateUri;

        /// <summary>
        /// This is the backing field for the <see cref="Template"/> property.
        /// </summary>
        [JsonProperty("template", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private StackTemplate _template;

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidateTemplateRequest"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected ValidateTemplateRequest()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidateTemplateRequest"/> class using the specified template
        /// URI.
        /// </summary>
        /// <param name="templateUri">The URI of the stack template to validate.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="templateUri"/> is
        /// <see langword="null"/>.</exception>
        public ValidateTemplateRequest(Uri templateUri)
        {
            if (templateUri == null)
                throw new ArgumentNullException("templateUri");

            _templateUri = templateUri.OriginalString;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidateTemplateRequest"/> class using the specified stack
        /// template.
        /// </summary>
        /// <param name="template">The stack template to validate.</param>
        public ValidateTemplateRequest(StackTemplate template)
        {
            _template = template;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidateTemplateRequest"/> class using the specified stack
        /// template and extension data.
        /// </summary>
        /// <param name="templateUri">The URI of the stack template to validate.</param>
        /// <param name="template">The stack template to validate.</param>
        /// <param name="extensionData">The extension data.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="extensionData"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">If <paramref name="extensionData"/> contains any <see langword="null"/> values.</exception>
        public ValidateTemplateRequest(Uri templateUri, StackTemplate template, params JProperty[] extensionData)
            : base(extensionData)
        {
            _templateUri = templateUri != null ? templateUri.AbsoluteUri : null;
            _template = template;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidateTemplateRequest"/> class using the specified stack
        /// template and extension data.
        /// </summary>
        /// <param name="templateUri">The URI of the stack template to validate.</param>
        /// <param name="template">The stack template to validate.</param>
        /// <param name="extensionData">The extension data.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="extensionData"/> is <see langword="null"/>.</exception>
        public ValidateTemplateRequest(Uri templateUri, StackTemplate template, IDictionary<string, JToken> extensionData)
            : base(extensionData)
        {
            _templateUri = templateUri != null ? templateUri.AbsoluteUri : null;
            _template = template;
        }

        /// <summary>
        /// Gets the URI of the stack template to validate.
        /// </summary>
        /// <value>
        /// <para>The URI of the stack template to validate.</para>
        /// <token>NullIfNotIncluded</token>
        /// </value>
        public Uri TemplateUri
        {
            get
            {
                if (_templateUri == null)
                    return null;

                return new Uri(_templateUri);
            }
        }

        /// <summary>
        /// Gets the stack template to validate.
        /// </summary>
        /// <value>
        /// <para>The stack template to validate.</para>
        /// <token>NullIfNotIncluded</token>
        /// </value>
        public StackTemplate Template
        {
            get
            {
                return _template;
            }
        }
    }
}
