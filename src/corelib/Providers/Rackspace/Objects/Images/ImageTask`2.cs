namespace net.openstack.Providers.Rackspace.Objects.Images
{
    using Newtonsoft.Json;

    /// <summary>
    /// This is the base class for objects modeling specific types of tasks in the
    /// <see cref="IImageService"/>.
    /// </summary>
    /// <typeparam name="TInput">The type modeling the JSON representation of the input arguments to the image task.</typeparam>
    /// <typeparam name="TResult">The type modeling the JSON representation of the result of the image task.</typeparam>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    [JsonObject(MemberSerialization.OptIn)]
    public abstract class ImageTask<TInput, TResult> : ImageTask
        where TInput : class
        where TResult : class
    {
#pragma warning disable 649 // Field 'fieldName' is never assigned to, and will always have its default value {value}
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
#pragma warning restore 649

        /// <summary>
        /// Initializes a new instance of the <see cref="ImageTask{TInput, TResult}"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected ImageTask()
        {
        }

        /// <summary>
        /// Gets an instance of <typeparamref name="TInput"/> describing the input arguments
        /// used to created the task.
        /// </summary>
        public TInput Input
        {
            get
            {
                return _input;
            }
        }

        /// <summary>
        /// Gets an instance of <typeparamref name="TResult"/> describing the result of the task.
        /// The value is unspecified if the <see cref="ImageTask.Status"/> is not
        /// <see cref="ImageTaskStatus.Success"/>.
        /// </summary>
        public TResult Result
        {
            get
            {
                return _result;
            }
        }
    }
}
