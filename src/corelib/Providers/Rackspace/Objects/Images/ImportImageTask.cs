namespace net.openstack.Providers.Rackspace.Objects.Images
{
    using Newtonsoft.Json;

    /// <summary>
    /// This class models the JSON representation of an <see cref="ImageTaskType.Import"/> task in the
    /// <see cref="IImageService"/>.
    /// </summary>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    [JsonObject(MemberSerialization.OptIn)]
    public class ImportImageTask : ImageTask<ImportTaskInput, ImportTaskResult>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ImportImageTask"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected ImportImageTask()
        {
        }
    }
}
