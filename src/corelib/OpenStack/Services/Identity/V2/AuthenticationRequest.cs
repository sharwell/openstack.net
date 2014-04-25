namespace OpenStack.Services.Identity.V2
{
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using OpenStack.ObjectModel;

    [JsonObject(MemberSerialization.OptIn)]
    public class AuthenticationRequest : ExtensibleJsonObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AuthenticationRequest"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected AuthenticationRequest()
        {
        }

        public AuthenticationRequest(params JProperty[] extensionData)
            : base(extensionData)
        {
        }

        public AuthenticationRequest(IDictionary<string, JToken> extensionData)
            : base(extensionData)
        {
        }
    }
}
