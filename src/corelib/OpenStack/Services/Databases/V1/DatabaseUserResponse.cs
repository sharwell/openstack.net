namespace OpenStack.Services.Databases.V1
{
    using Newtonsoft.Json;
    using OpenStack.ObjectModel;

    [JsonObject(MemberSerialization.OptIn)]
    public class DatabaseUserResponse : ExtensibleJsonObject
    {
        [JsonProperty("user", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private DatabaseUser _user;

        /// <summary>
        /// Initializes a new instance of the <see cref="DatabaseUserResponse"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected DatabaseUserResponse()
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