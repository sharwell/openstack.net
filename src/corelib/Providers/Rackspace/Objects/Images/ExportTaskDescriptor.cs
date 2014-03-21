namespace net.openstack.Providers.Rackspace.Objects.Images
{
    using Newtonsoft.Json;

    [JsonObject(MemberSerialization.OptIn)]
    public class ExportTaskDescriptor : ImageTaskDescriptor<ExportTaskInput>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExportTaskDescriptor"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected ExportTaskDescriptor()
        {
        }

        public ExportTaskDescriptor(ExportTaskInput input)
            : base(ImageTaskType.Export, input)
        {
        }
    }
}
