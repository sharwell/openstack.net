namespace OpenStack.Services.Compute.V2
{
    using System;
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

        /// <summary>
        /// Initializes a new instance of the <see cref="ServerRequest"/> class with the specified
        /// server data.
        /// </summary>
        /// <param name="server">A <see cref="ServerData"/> object describing the server resource.</param>
        public ServerRequest(ServerData server)
        {
            _server = server;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ServerRequest"/> class with the specified
        /// server and extension data.
        /// </summary>
        /// <param name="server">A <see cref="ServerData"/> object describing the server resource.</param>
        /// <param name="extensionData">The extension data.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="extensionData"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">If <paramref name="extensionData"/> contains any <see langword="null"/> values.</exception>
        public ServerRequest(ServerData server, params JProperty[] extensionData)
            : base(extensionData)
        {
            _server = server;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ServerRequest"/> class with the specified
        /// server and extension data.
        /// </summary>
        /// <param name="server">A <see cref="ServerData"/> object describing the server resource.</param>
        /// <param name="extensionData">The extension data.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="extensionData"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">If <paramref name="extensionData"/> contains any <see langword="null"/> values.</exception>
        public ServerRequest(ServerData server, IDictionary<string, JToken> extensionData)
            : base(extensionData)
        {
            _server = server;
        }

        /// <summary>
        /// Gets a <see cref="ServerData"/> instance containing details about the server resource.
        /// </summary>
        /// <value>
        /// A <see cref="ServerData"/> instance containing details about the server resource.
        /// <para>-or-</para>
        /// <para><see langword="null"/> if the JSON representation did not include the underlying property.</para>
        /// </value>
        public ServerData Server
        {
            get
            {
                return _server;
            }
        }
    }
}
