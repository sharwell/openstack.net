namespace net.openstack.Core.Domain
{
    /// <summary>
    /// Represents an endpoint for a service provided in the <see cref="ServiceCatalog"/>.
    /// </summary>
    public class Endpoint
    {
        /// <summary>
        /// Gets the public URL of the service.
        /// </summary>
        public string PublicURL { get; set; }

        /// <summary>
        /// Gets the region where this service endpoint is located. If this is <c>null</c>
        /// or empty, the region is not specified.
        /// </summary>
        public string Region { get; set; }

        /// <summary>
        /// Gets the tenant (or account) ID which this endpoint operates on.
        /// </summary>
        public string TenantId { get; set; }

        /// <summary>
        /// Gets the "versionId" property associated with the endpoint.
        /// <note type="warning">The value of this property is not defined. Do not use.</note>
        /// </summary>
        /// <seealso href="http://docs.openstack.org/api/openstack-identity-service/2.0/content/POST_authenticate_v2.0_tokens_.html">Authenticate (OpenStack Identity Service API v2.0 Reference)</seealso>
        public string VersionId { get; set; }

        /// <summary>
        /// Gets the "versionInfo" property associated with the endpoint.
        /// <note type="warning">The value of this property is not defined. Do not use.</note>
        /// </summary>
        /// <seealso href="http://docs.openstack.org/api/openstack-identity-service/2.0/content/POST_authenticate_v2.0_tokens_.html">Authenticate (OpenStack Identity Service API v2.0 Reference)</seealso>
        public string VersionInfo { get; set; }

        /// <summary>
        /// Gets the "versionList" property associated with the endpoint.
        /// <note type="warning">The value of this property is not defined. Do not use.</note>
        /// </summary>
        /// <seealso href="http://docs.openstack.org/api/openstack-identity-service/2.0/content/POST_authenticate_v2.0_tokens_.html">Authenticate (OpenStack Identity Service API v2.0 Reference)</seealso>
        public string VersionList { get; set; }

        /// <summary>
        /// Gets the internal URL of the service. If this is <c>null</c> or empty,
        /// the service should be accessed using the <see cref="PublicURL"/>.
        /// </summary>
        public string InternalURL { get; set; }
    }
}
