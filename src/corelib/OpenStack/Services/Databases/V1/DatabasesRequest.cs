namespace OpenStack.Services.Databases.V1
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using OpenStack.ObjectModel;

    [JsonObject(MemberSerialization.OptIn)]
    public class DatabasesRequest : ExtensibleJsonObject
    {
        [JsonProperty("databases", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private DatabaseData[] _databases;

        /// <summary>
        /// Initializes a new instance of the <see cref="DatabasesRequest"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected DatabasesRequest()
        {
        }

        public DatabasesRequest(IEnumerable<DatabaseName> databases)
        {
            Initialize(databases);
        }

        public DatabasesRequest(params DatabaseName[] databases)
        {
            Initialize(databases);
        }

        public DatabasesRequest(IEnumerable<DatabaseData> databases)
        {
            Initialize(databases);
        }

        public DatabasesRequest(params DatabaseData[] databases)
        {
            Initialize(databases);
        }

        public DatabasesRequest(IEnumerable<DatabaseData> databases, params JProperty[] extensionData)
            : base(extensionData)
        {
            Initialize(databases);
        }

        public DatabasesRequest(IEnumerable<DatabaseData> databases, IDictionary<string, JToken> extensionData)
            : base(extensionData)
        {
            Initialize(databases);
        }

        public ReadOnlyCollection<DatabaseData> Databases
        {
            get
            {
                if (_databases == null)
                    return null;

                return new ReadOnlyCollection<DatabaseData>(_databases);
            }
        }

        private void Initialize(IEnumerable<DatabaseName> databases)
        {
            if (databases != null)
                _databases = databases.Select(name => new DatabaseData(name)).ToArray();
        }

        private void Initialize(IEnumerable<DatabaseData> databases)
        {
            if (databases != null)
                _databases = databases.ToArray();
        }
    }
}
