namespace net.openstack.Providers.Rackspace.Objects.Images
{
    using Newtonsoft.Json;

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
