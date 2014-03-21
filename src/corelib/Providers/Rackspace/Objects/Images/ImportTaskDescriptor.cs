namespace net.openstack.Providers.Rackspace.Objects.Images
{
    using Newtonsoft.Json;

    [JsonObject(MemberSerialization.OptIn)]
    public class ImportTaskDescriptor : ImageTaskDescriptor<ImportTaskInput>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ImportTaskDescriptor"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected ImportTaskDescriptor()
        {
        }

        public ImportTaskDescriptor(ImportTaskInput input)
            : base(ImageTaskType.Import, input)
        {
        }
    }
}
