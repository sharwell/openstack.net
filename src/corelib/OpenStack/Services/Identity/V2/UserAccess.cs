namespace OpenStack.Services.Identity.V2
{
    using System.Collections.ObjectModel;
    using Newtonsoft.Json;
    using OpenStack.ObjectModel;

    /// <summary>
    /// Represents the response to a user authentication.
    /// </summary>
    /// <seealso href="http://docs.openstack.org/api/openstack-identity-service/2.0/content/POST_authenticate_v2.0_tokens_.html">Authenticate (OpenStack Identity Service API v2.0 Reference)</seealso>
    /// <threadsafety static="true" instance="false"/>
    [JsonObject(MemberSerialization.OptIn)]
    public class UserAccess : ExtensibleJsonObject
    {
        /// <summary>
        /// This is the backing field for the <see cref="Token"/> property.
        /// </summary>
        [JsonProperty("token", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private IdentityToken _token;

        /// <summary>
        /// This is the backing field for the <see cref="User"/> property.
        /// </summary>
        [JsonProperty("user", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private UserDetails _user;

        /// <summary>
        /// This is the backing field for the <see cref="ServiceCatalog"/> property.
        /// </summary>
        [JsonProperty("serviceCatalog", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private ServiceCatalogEntry[] _serviceCatalog;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserAccess"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected UserAccess()
        {
        }

        /// <summary>
        /// Gets the <see cref="IdentityToken"/> which allows providers to make authenticated
        /// calls to API methods.
        /// </summary>
        /// <remarks>
        /// The specific manner in which the token is used is provider-specific. Some implementations
        /// pass the token's <see cref="IdentityToken.Id"/> as an HTTP header when requesting a
        /// resource.
        /// </remarks>
        /// <seealso href="http://docs.openstack.org/api/openstack-identity-service/2.0/content/POST_authenticate_v2.0_tokens_.html">Authenticate (OpenStack Identity Service API v2.0 Reference)</seealso>
        public IdentityToken Token
        {
            get
            {
                return _token;
            }
        }

        /// <summary>
        /// Gets the details for the authenticated user, such as the username and roles.
        /// </summary>
        /// <seealso href="http://docs.openstack.org/api/openstack-identity-service/2.0/content/POST_authenticate_v2.0_tokens_.html">Authenticate (OpenStack Identity Service API v2.0 Reference)</seealso>
        public UserDetails User
        {
            get
            {
                return _user;
            }
        }

        /// <summary>
        /// Gets the services which may be accessed by this user.
        /// </summary>
        /// <seealso href="http://docs.openstack.org/api/openstack-identity-service/2.0/content/POST_authenticate_v2.0_tokens_.html">Authenticate (OpenStack Identity Service API v2.0 Reference)</seealso>
        public ReadOnlyCollection<ServiceCatalogEntry> ServiceCatalog
        {
            get
            {
                if (_serviceCatalog == null)
                    return null;

                return new ReadOnlyCollection<ServiceCatalogEntry>(_serviceCatalog);
            }
        }
    }
}
