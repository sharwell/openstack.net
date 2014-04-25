namespace OpenStack.Services.Identity.V2
{
    using System.Diagnostics;
    using Newtonsoft.Json;
    using OpenStack.ObjectModel;

    /// <summary>
    /// A personality that a user assumes when performing a specific set of operations. A role
    /// includes a set of right and privileges. A user assuming that role inherits those rights
    /// and privileges.
    /// </summary>
    /// <remarks>
    /// In OpenStack Identity Service, a token that is issued to a user includes the list of
    /// roles that user can assume. Services that are being called by that user determine how
    /// they interpret the set of roles a user has and to which operations or resources each
    /// role grants access.
    /// </remarks>
    /// <threadsafety static="true" instance="false"/>
    [JsonObject(MemberSerialization.OptIn)]
    [DebuggerDisplay("{Name,nq} ({Id, nq})")]
    public class Role : ExtensibleJsonObject
    {
        [JsonProperty("id", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private RoleId _id;

        [JsonProperty("name", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private string _name;

        [JsonProperty("tenantId", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private ProjectId _tenantId;

        /// <summary>
        /// Initializes a new instance of the <see cref="Role"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected Role()
        {
        }

        /// <summary>
        /// Gets the unique identifier for the role.
        /// </summary>
        public RoleId Id
        {
            get
            {
                return _id;
            }
        }

        /// <summary>
        /// Gets the name of the role.
        /// </summary>
        public string Name
        {
            get
            {
                return _name;
            }
        }

        public ProjectId TenantId
        {
            get
            {
                return _tenantId;
            }
        }
    }
}
