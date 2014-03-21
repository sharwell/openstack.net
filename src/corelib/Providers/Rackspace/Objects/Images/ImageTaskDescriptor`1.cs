namespace net.openstack.Providers.Rackspace.Objects.Images
{
    using System;
    using Newtonsoft.Json;

    /// <summary>
    /// This is the base class for objects modeling the JSON data in requests to create a
    /// new <see cref="ImageTask"/> in the the <see cref="IImageService"/>.
    /// </summary>
    /// <typeparam name="TInput">The type modeling the JSON representation of the input parameters for a specific type of task.</typeparam>
    /// <seealso cref="ImportTaskDescriptor"/>
    /// <seealso cref="ExportTaskDescriptor"/>
    /// <seealso cref="ImageTask{TInput, TResult}.Input"/>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    [JsonObject(MemberSerialization.OptIn)]
    public class ImageTaskDescriptor<TInput>
        where TInput : class
    {
        /// <summary>
        /// This is the backing field for the <see cref="Type"/> property.
        /// </summary>
        [JsonProperty("type")]
        private ImageTaskType _type;

        /// <summary>
        /// This is the backing field for the <see cref="Input"/> property.
        /// </summary>
        [JsonProperty("input", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private TInput _input;

        /// <summary>
        /// Initializes a new instance of the <see cref="ImageTaskDescriptor{TInput}"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected ImageTaskDescriptor()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ImageTaskDescriptor{TInput}"/> class
        /// with the specified <see cref="ImageTaskType"/> and input data.
        /// </summary>
        /// <param name="type">The type of task described by this instance.</param>
        /// <param name="input">An instance of <typeparamref name="TInput"/> containing the arguments for creating the task, or <see langword="null"/> if no input data is required for creating tasks of the specified <paramref name="type"/>.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="type"/> is <see langword="null"/>.</exception>
        protected ImageTaskDescriptor(ImageTaskType type, TInput input)
        {
            _type = type;
            _input = input;
        }

        /// <summary>
        /// Gets the type of task described by this instance.
        /// </summary>
        /// <value>
        /// An instance of <see cref="ImageTaskType"/> indicating the type of task, or <see langword="null"/> if the
        /// server did not provide the underlying property in a task descriptor.
        /// </value>
        public ImageTaskType Type
        {
            get
            {
                return _type;
            }
        }

        /// <summary>
        /// Gets the input arguments for creating the task.
        /// </summary>
        /// <value>
        /// An instance of <typeparamref name="TInput"/> containing the arguments for creating the task,
        /// or <see langword="null"/> if no arguments were specified for the task.
        /// </value>
        public TInput Input
        {
            get
            {
                return _input;
            }
        }
    }
}
