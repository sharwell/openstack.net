namespace OpenStack.Services.Databases.V1
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using OpenStack.ObjectModel;

    [JsonObject(MemberSerialization.OptIn)]
    public class UsersRequest : ExtensibleJsonObject
    {
        [JsonProperty("users", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private DatabaseUserData[] _users;

        /// <summary>
        /// Initializes a new instance of the <see cref="UsersRequest"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected UsersRequest()
        {
        }

        public UsersRequest(IEnumerable<DatabaseUserData> users)
        {
            Initialize(users);
        }

        public UsersRequest(params DatabaseUserData[] users)
        {
            Initialize(users);
        }

        public UsersRequest(IEnumerable<DatabaseUserData> users, params JProperty[] extensionData)
            : base(extensionData)
        {
            Initialize(users);
        }

        public UsersRequest(IEnumerable<DatabaseUserData> users, IDictionary<string, JToken> extensionData)
            : base(extensionData)
        {
            Initialize(users);
        }

        public ReadOnlyCollection<DatabaseUserData> Users
        {
            get
            {
                if (_users == null)
                    return null;

                return new ReadOnlyCollection<DatabaseUserData>(_users);
            }
        }

        private void Initialize(IEnumerable<DatabaseUserData> users)
        {
            if (users != null)
                _users = users.ToArray();
        }
    }
}
