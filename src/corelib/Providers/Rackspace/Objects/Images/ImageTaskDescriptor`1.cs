namespace net.openstack.Providers.Rackspace.Objects.Images
{
    using Newtonsoft.Json;

    [JsonObject(MemberSerialization.OptIn)]
    public class ImageTaskDescriptor<TInput>
    {
        [JsonProperty("type")]
        private ImageTaskType _type;

        [JsonProperty("input")]
        private TInput _input;

        /// <summary>
        /// Initializes a new instance of the <see cref="ImageTaskDescriptor"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected ImageTaskDescriptor()
        {
        }

        protected ImageTaskDescriptor(ImageTaskType type, TInput input)
        {
            _type = type;
            _input = input;
        }
    }
}
