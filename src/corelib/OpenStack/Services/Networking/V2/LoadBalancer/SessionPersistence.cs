namespace OpenStack.Services.Networking.V2.LoadBalancer
{
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using OpenStack.ObjectModel;

    [JsonObject(MemberSerialization.OptIn)]
    public class SessionPersistence : ExtensibleJsonObject
    {
        [JsonProperty("type", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private SessionPersistenceType _type;

        [JsonProperty("cookie_name", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private string _cookieName;

        /// <summary>
        /// Initializes a new instance of the <see cref="SessionPersistence"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected SessionPersistence()
        {
        }

        public SessionPersistence(SessionPersistenceType type)
        {
            _type = type;
        }

        public SessionPersistence(SessionPersistenceType type, string cookieName, params JProperty[] extensionData)
            : base(extensionData)
        {
            _type = type;
            _cookieName = cookieName;
        }

        public SessionPersistence(SessionPersistenceType type, string cookieName, IDictionary<string, JToken> extensionData)
            : base(extensionData)
        {
            _type = type;
            _cookieName = cookieName;
        }

        public SessionPersistenceType Type
        {
            get
            {
                return _type;
            }
        }

        public string CookieName
        {
            get
            {
                return _cookieName;
            }
        }
    }
}
