namespace Rackspace.Services.Databases.V1
{
    using Newtonsoft.Json;
    using OpenStack.ObjectModel;
    using OpenStack.Services.Databases.V1;

    [JsonObject(MemberSerialization.OptIn)]
    public class UserResponse : ExtensibleJsonObject
    {
        [JsonProperty("user", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private DatabaseUser _user;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserResponse"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected UserResponse()
        {
        }

        public DatabaseUser User
        {
            get
            {
                return _user;
            }
        }
    }
}