namespace net.openstack.Providers.Rackspace.Objects
{
    using Newtonsoft.Json;

    [JsonObject(MemberSerialization.OptIn)]
    internal class AuthDetails
    {
        [JsonProperty("passwordCredentials", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public Credentials PasswordCredentials { get; set; }

        [JsonProperty("RAX-KSKEY:apiKeyCredentials", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public Credentials APIKeyCredentials { get; set; }

        [JsonProperty("RAX-AUTH:domain", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public Domain Domain { get; set; }
    }
}
