namespace OpenStack.Services.Identity.V2
{
    using System.Collections.ObjectModel;
    using net.openstack.Core.Providers;
    using Newtonsoft.Json;
    using OpenStack.ObjectModel;
    using Link = OpenStack.Services.Compute.V2.Link;

    /// <summary>
    /// Contains additional information about an authenticated user.
    /// </summary>
    /// <seealso cref="UserAccess.User"/>
    /// <seealso cref="IIdentityProvider.Authenticate"/>
    /// <threadsafety static="true" instance="false"/>
    [JsonObject(MemberSerialization.OptIn)]
    public class UserDetails : ExtensibleJsonObject
    {
        [JsonProperty("id", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private UserId _id;

        [JsonProperty("name", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private string _name;

        [JsonProperty("roles", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private Role[] _roles;

        [JsonProperty("roles_links", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private Link[] _rolesLinks;

        /// <summary>
        /// Gets the unique identifier for the user.
        /// <note type="warning">The value of this property is not defined. Do not use.</note>
        /// </summary>
        public UserId Id
        {
            get
            {
                return _id;
            }
        }

        /// <summary>
        /// Gets the "name" property for the user.
        /// <note type="warning">The value of this property is not defined. Do not use.</note>
        /// </summary>
        public string Name
        {
            get
            {
                return _name;
            }
        }

        /// <summary>
        /// Gets the "roles" property for the user.
        /// <note type="warning">The value of this property is not defined. Do not use.</note>
        /// </summary>
        public ReadOnlyCollection<Role> Roles
        {
            get
            {
                if (_roles == null)
                    return null;

                return new ReadOnlyCollection<Role>(_roles);
            }
        }

        public ReadOnlyCollection<Link> RolesLinks
        {
            get
            {
                if (_rolesLinks == null)
                    return null;

                return new ReadOnlyCollection<Link>(_rolesLinks);
            }
        }
    }
}
