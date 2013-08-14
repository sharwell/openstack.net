namespace net.openstack.Core.Domain
{
    using System.Runtime.Serialization;

    /// <summary>
    /// Represents a single service provided to an authenticated user. Each service
    /// has one or more <see cref="Endpoints"/> providing access information for the
    /// service.
    /// </summary>
    /// <seealso href="http://docs.openstack.org/api/openstack-identity-service/2.0/content/POST_authenticate_v2.0_tokens_.html">Authenticate (OpenStack Identity Service API v2.0 Reference)</seealso>
    [DataContract]
    public class ServiceCatalog
    {
        /// <summary>
        /// Gets the endpoints for the service.
        /// </summary>
        /// <seealso href="http://docs.openstack.org/api/openstack-identity-service/2.0/content/POST_authenticate_v2.0_tokens_.html">Authenticate (OpenStack Identity Service API v2.0 Reference)</seealso>
        [DataMember(Name = "endpoints")]
        public Endpoint[] Endpoints { get; private set; }

        /// <summary>
        /// Gets the display name of the service, which may be a vendor-specific
        /// product name.
        /// </summary>
        /// <seealso href="http://docs.openstack.org/api/openstack-identity-service/2.0/content/POST_authenticate_v2.0_tokens_.html">Authenticate (OpenStack Identity Service API v2.0 Reference)</seealso>
        [DataMember(Name = "name")]
        public string Name { get; private set; }

        /// <summary>
        /// Gets the canonical name of the specification implemented by this service.
        /// </summary>
        /// <seealso href="http://docs.openstack.org/api/openstack-identity-service/2.0/content/POST_authenticate_v2.0_tokens_.html">Authenticate (OpenStack Identity Service API v2.0 Reference)</seealso>
        [DataMember(Name = "type")]
        public string Type { get; private set; }
    }
}