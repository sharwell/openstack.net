namespace OpenStack.Services.Identity.V2
{
    using System;
    using Newtonsoft.Json;
    using OpenStack.ObjectModel;

    [JsonObject(MemberSerialization.OptIn)]
    public class AdminEndpoint : ExtensibleJsonObject
    {
        [JsonProperty("id", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private EndpointId _id;

        [JsonProperty("name", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private string _name;

        [JsonProperty("adminURL", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private string _adminUrl;

        [JsonProperty("type", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private string _type;

        /// <summary>
        /// Initializes a new instance of the <see cref="AdminEndpoint"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected AdminEndpoint()
        {
        }

        public EndpointId Id
        {
            get
            {
                return _id;
            }
        }

        public string Name
        {
            get
            {
                return _name;
            }
        }

        public string Type
        {
            get
            {
                return _type;
            }
        }

        public Uri AdminUri
        {
            get
            {
                if (_adminUrl == null)
                    return null;

                return new Uri(_adminUrl, UriKind.Absolute);
            }
        }
    }
}
