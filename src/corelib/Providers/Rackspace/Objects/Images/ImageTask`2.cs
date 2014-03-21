namespace net.openstack.Providers.Rackspace.Objects.Images
{
    using Newtonsoft.Json;

    [JsonObject(MemberSerialization.OptIn)]
    public abstract class ImageTask<TInput, TResult> : ImageTask
        where TInput : class
        where TResult : class
    {
        /// <summary>
        /// This is the backing field for the <see cref="Input"/> property.
        /// </summary>
        [JsonProperty("input")]
        private TInput _input;

        /// <summary>
        /// This is the backing field for the <see cref="Result"/> property.
        /// </summary>
        [JsonProperty("result")]
        private TResult _result;

        /// <summary>
        /// Initializes a new instance of the <see cref="ImageTask{TInput, TResult}"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected ImageTask()
        {
        }

        public TInput Input
        {
            get
            {
                return _input;
            }
        }

        public TResult Result
        {
            get
            {
                return _result;
            }
        }
    }
}
