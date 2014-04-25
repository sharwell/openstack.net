namespace OpenStack.Services.Identity.V2
{
    using System;
    using Newtonsoft.Json;
    using OpenStack.ObjectModel;

    /// <summary>
    /// Represents the authentication token used for making authenticated calls to
    /// multiple APIs.
    /// </summary>
    /// <threadsafety static="true" instance="false"/>
    [JsonObject(MemberSerialization.OptIn)]
    public class IdentityToken : ExtensibleJsonObject
    {
        /// <summary>
        /// This is the backing field for the <see cref="Id"/> property.
        /// </summary>
        [JsonProperty("id", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private TokenId _id;

        /// <summary>
        /// This is the backing field for the <see cref="Expiration"/> property.
        /// </summary>
        [JsonProperty("expires", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private DateTimeOffset? _expires;

        /// <summary>
        /// This is the backing field for the <see cref="Tenant"/> property.
        /// </summary>
        [JsonProperty("tenant", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private Tenant _tenant;

        /// <summary>
        /// Initializes a new instance of the <see cref="IdentityToken"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected IdentityToken()
        {
        }

        /// <summary>
        /// Gets the token ID which can be used to make authenticated API calls.
        /// </summary>
        public TokenId Id
        {
            get
            {
                return _id;
            }
        }

        /// <summary>
        /// Gets the token expiration time in the format originally returned by the
        /// authentication response.
        /// </summary>
        /// <seealso cref="IIdentityProvider.GetToken"/>
        public DateTimeOffset? Expiration
        {
            get
            {
                return _expires;
            }
        }

        /// <summary>
        /// Gets a <see cref="Tenant"/> object containing the name and ID of the
        /// tenant (or account) for the authenticated credentials.
        /// </summary>
        public Tenant Tenant
        {
            get
            {
                return _tenant;
            }
        }
    }
}
