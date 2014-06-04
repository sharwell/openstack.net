namespace OpenStack.Services.Compute.V2
{
    using System;
    using System.Diagnostics;
    using Newtonsoft.Json;
    using OpenStack.ObjectModel;

    /// <summary>
    /// Represents a link associated with a resource.
    /// </summary>
    /// <seealso href="http://docs.openstack.org/api/openstack-compute/2/content/LinksReferences.html">Links and References (OpenStack Compute API v2 and Extensions Reference)</seealso>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    [JsonObject(MemberSerialization.OptIn)]
    [DebuggerDisplay("{Rel,nq}: {Href,nq}")]
    public class Link : ExtensibleJsonObject
    {
#pragma warning disable 649 // Field 'fieldName' is never assigned to, and will always have its default value {value}
        /// <summary>
        /// This is the backing field for the <see cref="Relation"/> property.
        /// </summary>
        [JsonProperty("rel", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private string _rel;

        /// <summary>
        /// This is the backing field for the <see cref="Target"/> property.
        /// </summary>
        [JsonProperty("href", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private string _href;
#pragma warning restore 649

        /// <summary>
        /// Initializes a new instance of the <see cref="Link"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected Link()
        {
        }

        /// <summary>
        /// Gets the link relation.
        /// </summary>
        /// <remarks>
        /// <list type="bullet">
        /// <item>A <c>self</c> link contains a versioned link to the resource. Use these links when the link will be followed immediately.</item>
        /// <item>A <c>bookmark</c> link provides a permanent link to a resource that is appropriate for long-term storage.</item>
        /// <item>An <c>alternate</c> link can contain an alternative representation of the resource. For example, an OpenStack Compute image might have an alternate representation in the OpenStack Image service.</item>
        /// </list>
        /// </remarks>
        /// <seealso href="http://docs.openstack.org/api/openstack-compute/2/content/LinksReferences.html">Links and References (OpenStack Compute API v2 and Extensions Reference)</seealso>
        public string Relation
        {
            get
            {
                return _rel;
            }
        }

        /// <summary>
        /// Gets the link target.
        /// </summary>
        /// <seealso href="http://docs.openstack.org/api/openstack-compute/2/content/LinksReferences.html">Links and References (OpenStack Compute API v2 and Extensions Reference)</seealso>
        public Uri Target
        {
            get
            {
                if (string.IsNullOrEmpty(_href))
                    return null;

                return new Uri(_href, UriKind.RelativeOrAbsolute);
            }
        }
    }
}
