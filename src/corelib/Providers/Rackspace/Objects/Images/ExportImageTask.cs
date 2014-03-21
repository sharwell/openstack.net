namespace net.openstack.Providers.Rackspace.Objects.Images
{
    using Newtonsoft.Json;

    [JsonObject(MemberSerialization.OptIn)]
    public class ExportImageTask : ImageTask<ExportTaskInput, ExportTaskResult>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExportImageTask"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected ExportImageTask()
        {
        }
    }
}
