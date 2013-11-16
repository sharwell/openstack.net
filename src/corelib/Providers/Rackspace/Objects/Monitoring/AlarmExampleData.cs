namespace net.openstack.Providers.Rackspace.Objects.Monitoring
{
    using Newtonsoft.Json;

    [JsonObject(MemberSerialization.OptIn)]
    public class AlarmExampleData
    {
        [JsonProperty("bound_criteria")]
        private string _boundCriteria;
    }
}
