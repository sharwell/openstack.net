namespace net.openstack.Core.Domain.Orchestration
{
    using Newtonsoft.Json;

    [JsonObject(MemberSerialization.OptIn)]
    public class BuildInfo
    {
        [JsonProperty("api", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private BuildRevision _api;

        [JsonProperty("engine", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private BuildRevision _engine;

        /// <summary>
        /// Initializes a new instance of the <see cref="BuildInfo"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected BuildInfo()
        {
        }

        public string ApiRevision
        {
            get
            {
                return _api == null ? null : _api.Revision;
            }
        }

        public string EngineRevision
        {
            get
            {
                return _engine == null ? null : _engine.Revision;
            }
        }

        protected class BuildRevision
        {
            [JsonProperty("Revision", DefaultValueHandling = DefaultValueHandling.Ignore)]
            private string _revision;

            /// <summary>
            /// Initializes a new instance of the <see cref="Revision"/> class
            /// during JSON deserialization.
            /// </summary>
            [JsonConstructor]
            protected BuildRevision()
            {
            }

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
