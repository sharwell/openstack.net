namespace OpenStack.Services.Databases.V1
{
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using OpenStack.ObjectModel;

    [JsonObject(MemberSerialization.OptIn)]
    public class RestartRequest : ExtensibleJsonObject
    {
        [JsonProperty("restart", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private RestartData _restartData;

        /// <summary>
        /// Initializes a new instance of the <see cref="RestartRequest"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected RestartRequest()
        {
        }

        public RestartRequest(RestartData restartData)
        {
            _restartData = restartData;
        }

        public RestartRequest(RestartData restartData, params JProperty[] extensionData)
            : base(extensionData)
        {
            _restartData = restartData;
        }

        public RestartRequest(RestartData restartData, IDictionary<string, JToken> extensionData)
            : base(extensionData)
        {
            _restartData = restartData;
        }

        public RestartData RestartData
        {
            get
            {
                return _restartData;
            }
        }
    }
}
