namespace OpenStack.Services.Compute.V2
{
    using Newtonsoft.Json;
    using OpenStack.ObjectModel;

    /// <summary>
    /// This class models the JSON representation used for the result of the API calls
    /// which return a <seealso cref="V2.Server"/> resource.
    /// </summary>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    [JsonObject(MemberSerialization.OptIn)]
    public class ServerResponse : ExtensibleJsonObject
    {
#pragma warning disable 649 // Field 'fieldName' is never assigned to, and will always have its default value {value}
        /// <summary>
        /// This is the backing field for the <see cref="Server"/> property.
        /// </summary>
        [JsonProperty("server", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private Server _server;
#pragma warning restore 649

        /// <summary>
        /// Initializes a new instance of the <see cref="ServerResponse"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected ServerResponse()
        {
        }

        /// <summary>
        /// Gets a <see cref="V2.Server"/> instance describing the server resource.
        /// </summary>
        /// <value>
        /// A <see cref="V2.Server"/> instance describing the server resource.
        /// <para>-or-</para>
        /// <para><see langword="null"/> if the JSON representation did not include the underlying property.</para>
        /// </value>
        public Server Server
        {
            get
            {
                return _server;
            }
        }
    }
}
