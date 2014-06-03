namespace OpenStack.Services.Compute.V2
{
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using OpenStack.ObjectModel;

    /// <summary>
    /// This class models the JSON representation of the request body used for
    /// API calls to create or update server resources in the <see cref="IComputeService"/>.
    /// </summary>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    [JsonObject(MemberSerialization.OptIn)]
    public class ServerRequest : ExtensibleJsonObject
    {
        /// <summary>
        /// This is the backing field for the <see cref="Server"/> property.
        /// </summary>
        [JsonProperty("server", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private ServerData _server;

        /// <summary>
        /// Initializes a new instance of the <see cref="ServerRequest"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected ServerRequest()
        {
        }

        public ServerRequest(ServerData server)
        {
            _server = server;
        }

        public ServerRequest(ServerData server, params JProperty[] extensionData)
            : base(extensionData)
        {
            _server = server;
        }

        public ServerRequest(ServerData server, IDictionary<string, JToken> extensionData)
            : base(extensionData)
        {
            _server = server;
        }

        public ServerData Server
        {
            get
            {
                return _server;
            }
        }
    }
}
