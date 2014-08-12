namespace OpenStack.Services.Databases.V1
{
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using OpenStack.ObjectModel;

    [JsonObject(MemberSerialization.OptIn)]
    public class DatabaseInstanceRequest : ExtensibleJsonObject
    {
        [JsonProperty("instance", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private DatabaseInstanceData _instance;

        /// <summary>
        /// Initializes a new instance of the <see cref="DatabaseInstanceRequest"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected DatabaseInstanceRequest()
        {
        }

        public DatabaseInstanceRequest(DatabaseInstanceData instance)
        {
            _instance = instance;
        }

        public DatabaseInstanceRequest(DatabaseInstanceData instance, params JProperty[] extensionData)
            : base(extensionData)
        {
            _instance = instance;
        }

        public DatabaseInstanceRequest(DatabaseInstanceData instance, IDictionary<string, JToken> extensionData)
            : base(extensionData)
        {
            _instance = instance;
        }

        public DatabaseInstanceData Instance
        {
            get
            {
                return _instance;
            }
        }
    }
}
