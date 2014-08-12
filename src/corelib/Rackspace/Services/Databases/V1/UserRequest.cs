namespace Rackspace.Services.Databases.V1
{
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using OpenStack.ObjectModel;
    using OpenStack.Services.Databases.V1;

    [JsonObject(MemberSerialization.OptIn)]
    public class UserRequest : ExtensibleJsonObject
    {
        [JsonProperty("user", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private DatabaseUserData _user;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserRequest"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected UserRequest()
        {
        }

        public UserRequest(DatabaseUserData userData)
        {
            _user = userData;
        }

        public UserRequest(DatabaseUserData userData, params JProperty[] extensionData)
            : base(extensionData)
        {
            _user = userData;
        }

        public UserRequest(DatabaseUserData userData, IDictionary<string, JToken> extensionData)
            : base(extensionData)
        {
            _user = userData;
        }

        public DatabaseUserData User
        {
            get
            {
                return _user;
            }
        }
    }
}
