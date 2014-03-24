namespace net.openstack.Providers.Rackspace.Objects.Images
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    /// <summary>
    /// This class models the JSON representation of a generic task in the <see cref="IImageService"/>.
    /// The input and result objects are provided as a <see cref="JObject"/>.
    /// </summary>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
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
