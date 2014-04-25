namespace OpenStack.Services.Identity.V2
{
    using System;
    using Newtonsoft.Json;
    using OpenStack.ObjectModel;

    /// <summary>
    /// Represents an endpoint for a service provided in the <see cref="ServiceCatalogEntry"/>.
    /// </summary>
    /// <threadsafety static="true" instance="false"/>
    [JsonObject(MemberSerialization.OptIn)]
    public class Endpoint : ExtensibleJsonObject
    {
        /// <summary>
        /// This is the backing field for the <see cref="PublicUri"/> property.
        /// </summary>
        [JsonProperty("publicURL", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private string _publicUrl;

        /// <summary>
        /// This is the backing field for the <see cref="InternalUri"/> property.
        /// </summary>
        [JsonProperty("internalURL", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private string _internalUrl;

        /// <summary>
        /// This is the backing field for the <see cref="Region"/> property.
        /// </summary>
        [JsonProperty("region", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private string _region;

        /// <summary>
        /// This is the backing field for the <see cref="TenantId"/> property.
        /// </summary>
        [JsonProperty("tenantId", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private ProjectId _tenantId;

        /// <summary>
        /// This is the backing field for the <see cref="VersionId"/> property.
        /// </summary>
        [JsonProperty("versionId", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private string _versionId;

        /// <summary>
        /// This is the backing field for the <see cref="VersionInfo"/> property.
        /// </summary>
        [JsonProperty("versionInfo", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private string _versionInfo;

        /// <summary>
        /// This is the backing field for the <see cref="VersionList"/> property.
        /// </summary>
        [JsonProperty("versionList", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private string _versionList;

        /// <summary>
        /// Initializes a new instance of the <see cref="Endpoint"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected Endpoint()
        {
        }

        /// <summary>
        /// Gets the public URL of the service.
        /// </summary>
        public Uri PublicUri
        {
            get
            {
                if (_publicUrl == null)
                    return null;

                return new Uri(_publicUrl, UriKind.Absolute);
            }
        }

        /// <summary>
        /// Gets the internal URL of the service. If this is <see langword="null"/> or empty,
        /// the service should be accessed using the <see cref="PublicUri"/>.
        /// </summary>
        public Uri InternalUri
        {
            get
            {
                if (_internalUrl == null)
                    return null;

                return new Uri(_internalUrl, UriKind.Absolute);
            }
        }

        /// <summary>
        /// Gets the region where this service endpoint is located. If this is <see langword="null"/>
        /// or empty, the region is not specified.
        /// </summary>
        public string Region
        {
            get
            {
                return _region;
            }
        }

        /// <summary>
        /// Gets the tenant (or account) ID which this endpoint operates on.
        /// </summary>
        public ProjectId TenantId
        {
            get
            {
                return _tenantId;
            }
        }

        /// <summary>
        /// Gets the "versionId" property associated with the endpoint.
        /// <note type="warning">The value of this property is not defined. Do not use.</note>
        /// </summary>
        /// <seealso href="http://docs.openstack.org/api/openstack-identity-service/2.0/content/POST_authenticate_v2.0_tokens_.html">Authenticate (OpenStack Identity Service API v2.0 Reference)</seealso>
        public string VersionId
        {
            get
            {
                return _versionId;
            }
        }

        /// <summary>
        /// Gets the "versionInfo" property associated with the endpoint.
        /// <note type="warning">The value of this property is not defined. Do not use.</note>
        /// </summary>
        /// <seealso href="http://docs.openstack.org/api/openstack-identity-service/2.0/content/POST_authenticate_v2.0_tokens_.html">Authenticate (OpenStack Identity Service API v2.0 Reference)</seealso>
        public string VersionInfo
        {
            get
            {
                return _versionInfo;
            }
        }

        /// <summary>
        /// Gets the "versionList" property associated with the endpoint.
        /// <note type="warning">The value of this property is not defined. Do not use.</note>
        /// </summary>
        /// <seealso href="http://docs.openstack.org/api/openstack-identity-service/2.0/content/POST_authenticate_v2.0_tokens_.html">Authenticate (OpenStack Identity Service API v2.0 Reference)</seealso>
        public string VersionList
        {
            get
            {
                return _versionList;
            }
        }
    }
}
