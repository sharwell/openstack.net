﻿namespace OpenStack.ObjectModel.JsonHome
{
    using System;
    using System.Collections.Immutable;
    using System.Diagnostics.CodeAnalysis;
    using Newtonsoft.Json;

    /// <summary>
    /// This class models the Resource Object of the home document described by
    /// <strong>Home Documents for HTTP APIs</strong>.
    /// </summary>
    /// <seealso href="http://tools.ietf.org/html/draft-nottingham-json-home-03#section-3">Resource Objects (Home Documents for HTTP APIs - draft-nottingham-json-home-03)</seealso>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    [JsonObject(MemberSerialization.OptIn)]
    public class ResourceObject : ExtensibleJsonObject
    {
#pragma warning disable 649 // Field 'fieldName' is never assigned to, and will always have its default value {value}
        /// <summary>
        /// This is the backing field for the <see cref="Href"/> property.
        /// </summary>
        [JsonProperty("href", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private string _href;

        /// <summary>
        /// This is the backing field for the <see cref="HrefTemplate"/> property.
        /// </summary>
        [JsonProperty("href-template", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private string _hrefTemplate;

        /// <summary>
        /// This is the backing field for the <see cref="HrefVars"/> property.
        /// </summary>
        [JsonProperty("href-vars", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private ImmutableDictionary<string, Uri> _hrefVars;

        /// <summary>
        /// This is the backing field for the <see cref="Hints"/> property.
        /// </summary>
        [JsonProperty("hints", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private ResourceHints _hints;
#pragma warning restore 649

        /// <summary>
        /// Gets the URI of the resource, if the resource is a direct link. The value
        /// may be a relative URI whose base URI is that of the JSON Home Document itself.
        /// </summary>
        /// <value>
        /// The direct URI of the resource, or <see langword="null"/> if this resource is
        /// a templated link.
        /// </value>
        /// <seealso href="http://tools.ietf.org/html/rfc3986">RFC3986 (Uniform Resource Identifier (URI): Generic Syntax)</seealso>
        public Uri Href
        {
            get
            {
                if (_href == null)
                    return null;

                return new Uri(_href);
            }
        }

        /// <summary>
        /// Gets the URI Template of the resource. The value may be a relative URI
        /// whose base URI is that of the JSON Home Document itself.
        /// </summary>
        /// <value>
        /// The URI Template of the resource, or <see langword="null"/> if this resource is
        /// a direct link.
        /// </value>
        /// <seealso href="http://tools.ietf.org/html/rfc6570">RFC6570 (URI Template)</seealso>
        [SuppressMessage("OpenStack.Documentation", "DocumentNullJsonValue", Justification = "The meaning of null is documented.")]
        public string HrefTemplate
        {
            get
            {
                return _hrefTemplate;
            }
        }

        /// <summary>
        /// Gets the template variables used to fill the template returned by
        /// <see cref="HrefTemplate"/>.
        /// </summary>
        /// <remarks>
        /// This is a mapping between variable names available to the template and
        /// absolute URIs that are used as global identifiers for the semantics and
        /// syntax of those variables.
        /// </remarks>
        /// <value>
        /// The template variable mapping, or <see langword="null"/> if this is a direct link.
        /// </value>
        [SuppressMessage("OpenStack.Documentation", "DocumentNullJsonValue", Justification = "The meaning of null is documented.")]
        public ImmutableDictionary<string, Uri> HrefVars
        {
            get
            {
                return _hrefVars;
            }
        }

        /// <summary>
        /// Gets the additional Resource Hints describing the resource.
        /// </summary>
        /// <value>
        /// <para>The additional Resource Hints describing the resource.</para>
        /// <token>NullIfNotIncluded</token>
        /// </value>
        /// <seealso cref="ResourceHints"/>
        public ResourceHints Hints
        {
            get
            {
                return _hints;
            }
        }
    }
}
