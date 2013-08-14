namespace net.openstack.Core.Domain
{
    using Newtonsoft.Json;

    [JsonObject(MemberSerialization.OptIn)]
    public class User
    {
        [JsonProperty("RAX-AUTH:defaultRegion")]
        public string DefaultRegion { get; private set; }

        [JsonProperty("id", DefaultValueHandling = DefaultValueHandling.Include)]
        public string Id { get; private set; }

        [JsonProperty("username")]
        public string Username { get; private set; }

        [JsonProperty("email")]
        public string Email { get; private set; }

        [JsonProperty("enabled")]
        public bool Enabled { get; private set; }
    }
}
