namespace OpenStack.Services.Identity.V2
{
    using Newtonsoft.Json;
    using OpenStack.ObjectModel;

    /// <summary>
    /// This models the basic JSON description of a tenant.
    /// </summary>
    /// <seealso cref="IIdentityProvider.ListTenants"/>
    /// <threadsafety static="true" instance="false"/>
    [JsonObject(MemberSerialization.OptIn)]
    public class Tenant : ExtensibleJsonObject
    {
        /// <summary>
        /// This is the backing field for the <see cref="Id"/> property.
        /// </summary>
        [JsonProperty("id", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private ProjectId _id;

        /// <summary>
        /// This is the backing field for the <see cref="Name"/> property.
        /// </summary>
        [JsonProperty("name", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private string _name;

        /// <summary>
        /// Initializes a new instance of the <see cref="Tenant"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected Tenant()
        {
        }

        /// <summary>
        /// Gets the unique identifier for the tenant.
        /// <note type="warning">The value of this property is not defined. Do not use.</note>
        /// </summary>
        public ProjectId Id
        {
            get
            {
                return _id;
            }
        }

        /// <summary>
        /// Gets the "name" property for the tenant.
        /// <note type="warning">The value of this property is not defined. Do not use.</note>
        /// </summary>
        public string Name
        {
            get
            {
                return _name;
            }
        }
    }
}
