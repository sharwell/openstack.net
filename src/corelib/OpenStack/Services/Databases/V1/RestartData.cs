namespace OpenStack.Services.Databases.V1
{
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using OpenStack.ObjectModel;

    [JsonObject(MemberSerialization.OptIn)]
    public class RestartData : ExtensibleJsonObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RestartData"/> class.
        /// </summary>
        [JsonConstructor]
        public RestartData()
        {
        }

        public RestartData(params JProperty[] extensionData)
            : base(extensionData)
        {
        }

        public RestartData(IDictionary<string, JToken> extensionData)
            : base(extensionData)
        {
        }
    }
}