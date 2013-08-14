namespace net.openstack.Core.Domain
{
    using System.Runtime.Serialization;

    /// <summary>
    /// Represents an endpoint for a service provided in the <see cref="ServiceCatalog"/>.
    /// </summary>
    [DataContract]
    public class Endpoint
    {
        /// <summary>
        /// Gets the public URL of the service.
        /// </summary>
        [DataMember(Name = "publicURL")]
        public string PublicURL { get; private set; }

        /// <summary>
        /// Gets the region where this service endpoint is located. If this is <c>null</c>
        /// or empty, the region is not specified.
        /// </summary>
        [DataMember(Name = "region")]
        public string Region { get; private set; }

        /// <summary>
        /// Gets the tenant (or account) ID which this endpoint operates on.
        /// </summary>
        [DataMember(Name = "tenantId")]
        public string TenantId { get; private set; }

        /// <summary>
        /// Gets the "versionId" property associated with the endpoint.
        /// <note type="warning">The value of this property is not defined. Do not use.</note>
        /// </summary>
        /// <seealso href="http://docs.openstack.org/api/openstack-identity-service/2.0/content/POST_authenticate_v2.0_tokens_.html">Authenticate (OpenStack Identity Service API v2.0 Reference)</seealso>
        [DataMember(Name = "versionId")]
        public string VersionId { get; private set; }

        /// <summary>
        /// Gets the "versionInfo" property associated with the endpoint.
        /// <note type="warning">The value of this property is not defined. Do not use.</note>
        /// </summary>
        /// <seealso href="http://docs.openstack.org/api/openstack-identity-service/2.0/content/POST_authenticate_v2.0_tokens_.html">Authenticate (OpenStack Identity Service API v2.0 Reference)</seealso>
        [DataMember(Name = "versionInfo")]
        public string VersionInfo { get; private set; }

        /// <summary>
        /// Gets the "versionList" property associated with the endpoint.
        /// <note type="warning">The value of this property is not defined. Do not use.</note>
        /// </summary>
        /// <seealso href="http://docs.openstack.org/api/openstack-identity-service/2.0/content/POST_authenticate_v2.0_tokens_.html">Authenticate (OpenStack Identity Service API v2.0 Reference)</seealso>
        [DataMember(Name = "versionList")]
        public string VersionList { get; private set; }

        /// <summary>
        /// Gets the internal URL of the service. If this is <c>null</c> or empty,
        /// the service should be accessed using the <see cref="PublicURL"/>.
        /// </summary>
        [DataMember(Name = "internalURL")]
        public string InternalURL { get; private set; }
    }
}
