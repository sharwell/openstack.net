namespace OpenStack.Services.Orchestration.V1
{
    using Newtonsoft.Json;
    using OpenStack.ObjectModel;

    /// <summary>
    /// This class models the JSON representation of service build information with the OpenStack Orchestration Service.
    /// </summary>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    [JsonObject(MemberSerialization.OptIn)]
    public class BuildInformation : ExtensibleJsonObject
    {
#pragma warning disable 649 // Field 'fieldName' is never assigned to, and will always have its default value {value}
        /// <summary>
        /// This is the backing field for the <see cref="Api"/> property.
        /// </summary>
        [JsonProperty("api", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private BuildRevision _api;

        /// <summary>
        /// This is the backing field for the <see cref="Engine"/> property.
        /// </summary>
        [JsonProperty("engine", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private BuildRevision _engine;
#pragma warning restore 649

        /// <summary>
        /// Initializes a new instance of the <see cref="BuildInformation"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected BuildInformation()
        {
        }

        /// <summary>
        /// Gets the API build information.
        /// </summary>
        /// <value>
        /// <para>A <see cref="BuildRevision"/> object containing the API revision information.</para>
        /// <token>NullIfNotIncluded</token>
        /// </value>
        public BuildRevision Api
        {
            get
            {
                return _api;
            }
        }

        /// <summary>
        /// Gets the orchestration engine build information.
        /// </summary>
        /// <value>
        /// <para>A <see cref="BuildRevision"/> object containing the orchestration engine revision information.</para>
        /// <token>NullIfNotIncluded</token>
        /// </value>
        public BuildRevision Engine
        {
            get
            {
                return _engine;
            }
        }

        /// <summary>
        /// This class models the JSON representation of revision information for a specific component within the
        /// OpenStack Orchestration Service.
        /// </summary>
        /// <threadsafety static="true" instance="false"/>
        /// <preliminary/>
        [JsonObject(MemberSerialization.OptIn)]
        public class BuildRevision : ExtensibleJsonObject
        {
#pragma warning disable 649 // Field 'fieldName' is never assigned to, and will always have its default value {value}
            /// <summary>
            /// This is the backing field for the <see cref="Revision"/> property.
            /// </summary>
            [JsonProperty("revision", DefaultValueHandling = DefaultValueHandling.Ignore)]
            private string _revision;
#pragma warning restore 649

            /// <summary>
            /// Initializes a new instance of the <see cref="BuildRevision"/> class
            /// during JSON deserialization.
            /// </summary>
            [JsonConstructor]
            protected BuildRevision()
            {
            }

            /// <summary>
            /// Gets the revision of the component.
            /// </summary>
            /// <value>
            /// <para>A <see cref="string"/> representing the revision of the component.</para>
            /// <token>NullIfNotIncluded</token>
            /// </value>
            public string Revision
            {
                get
                {
                    return _revision;
                }
            }
        }
    }
}
