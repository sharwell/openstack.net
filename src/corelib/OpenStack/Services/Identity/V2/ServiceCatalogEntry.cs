namespace OpenStack.Services.Identity.V2
{
    using System.Collections.ObjectModel;
    using System.Diagnostics;
    using Newtonsoft.Json;
    using OpenStack.ObjectModel;

    /// <summary>
    /// Represents a single service provided to an authenticated user. Each service
    /// has one or more <see cref="Endpoints"/> providing access information for the
    /// service.
    /// </summary>
    /// <seealso href="http://docs.openstack.org/api/openstack-identity-service/2.0/content/POST_authenticate_v2.0_tokens_.html">Authenticate (OpenStack Identity Service API v2.0 Reference)</seealso>
    /// <threadsafety static="true" instance="false"/>
    [JsonObject(MemberSerialization.OptIn)]
    [DebuggerDisplay("{Name,nq} ({Type,nq})")]
    public class ServiceCatalogEntry : ExtensibleJsonObject
    {
        /// <summary>
        /// This is the backing field for the <see cref="Type"/> property.
        /// </summary>
        [JsonProperty("type", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private string _type;

        /// <summary>
        /// This is the backing field for the <see cref="Name"/> property.
        /// </summary>
        [JsonProperty("name", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private string _name;

        /// <summary>
        /// This is the backing field for the <see cref="Endpoints"/> property.
        /// </summary>
        [JsonProperty("endpoints", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private Endpoint[] _endpoints;

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceCatalogEntry"/> class.
        /// </summary>
        /// <remarks>
        /// This constructor is used by the JSON deserializer.
        /// </remarks>
        [JsonConstructor]
        protected ServiceCatalogEntry()
        {
        }

        /// <summary>
        /// Gets the canonical name of the specification implemented by this service.
        /// </summary>
        /// <seealso href="http://docs.openstack.org/api/openstack-identity-service/2.0/content/POST_authenticate_v2.0_tokens_.html">Authenticate (OpenStack Identity Service API v2.0 Reference)</seealso>
        public string Type
        {
            get
            {
                return _type;
            }
        }

        /// <summary>
        /// Gets the display name of the service, which may be a vendor-specific
        /// product name.
        /// </summary>
        /// <seealso href="http://docs.openstack.org/api/openstack-identity-service/2.0/content/POST_authenticate_v2.0_tokens_.html">Authenticate (OpenStack Identity Service API v2.0 Reference)</seealso>
        public string Name
        {
            get
            {
                return _name;
            }
        }

        /// <summary>
        /// Gets the endpoints for the service.
        /// </summary>
        /// <seealso href="http://docs.openstack.org/api/openstack-identity-service/2.0/content/POST_authenticate_v2.0_tokens_.html">Authenticate (OpenStack Identity Service API v2.0 Reference)</seealso>
        public ReadOnlyCollection<Endpoint> Endpoints
        {
            get
            {
                if (_endpoints == null)
                    return null;

                return new ReadOnlyCollection<Endpoint>(_endpoints);
            }
        }
    }
}