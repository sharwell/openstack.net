namespace net.openstack.Providers.Rackspace.Objects.Images
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    [JsonObject(MemberSerialization.OptIn)]
    public class GenericImageTask : ImageTask<JObject, JObject>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GenericImageTask"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected GenericImageTask()
        {
        }
    }
}
