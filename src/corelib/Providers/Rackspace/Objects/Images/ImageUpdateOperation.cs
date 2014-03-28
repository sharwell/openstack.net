namespace net.openstack.Providers.Rackspace.Objects.Images
{
    using System;
    using Newtonsoft.Json;

    /// <summary>
    /// Defines a single update operation to apply to an image resource during the
    /// <see cref="IImageService.UpdateImageAsync"/> operation.
    /// </summary>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    [JsonObject(MemberSerialization.OptIn)]
    public class ImageUpdateOperation
    {
        /// <summary>
        /// This is the backing field for the <see cref="Operation"/> property.
        /// </summary>
        [JsonProperty("op")]
        private UpdateOperation _operation;

        /// <summary>
        /// This is the backing field for the <see cref="Path"/> property.
        /// </summary>
        [JsonProperty("path")]
        private string _path;

        /// <summary>
        /// This is the backing field for the <see cref="Value"/> property.
        /// </summary>
        [JsonProperty("value", NullValueHandling = NullValueHandling.Ignore)]
        private object _value;

        /// <summary>
        /// Initializes a new instance of the <see cref="ImageUpdateOperation"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected ImageUpdateOperation()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ImageUpdateOperation"/> class
        /// using the specified values.
        /// </summary>
        /// <remarks>
        /// If the <paramref name="path"/> parameter does not start with a leading <c>/</c> character,
        /// one will be automatically prepended. For example, if the property <c>name</c> is specified
        /// as the <paramref name="path"/>, the effective <see cref="Path"/> for the update operation
        /// will be <c>/name</c>.
        /// </remarks>
        /// <param name="operation">An <see cref="UpdateOperation"/> instance describing the type of update operation to apply.</param>
        /// <param name="path">The property to apply the update operation to. This may be one of the values in <see cref="PredefinedImageProperties"/>, or a path to an application-specific custom metadata.</param>
        /// <param name="value">The value for the update operation. The image service may restrict the allowed values based on the <paramref name="operation"/> and/or <paramref name="path"/>.</param>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="operation"/> is <see langword="null"/>.
        /// <para>-or-</para>
        /// <para>If <paramref name="path"/> is <see langword="null"/>.</para>
        /// </exception>
        /// <exception cref="ArgumentException">If <paramref name="path"/> is empty.</exception>
        public ImageUpdateOperation(UpdateOperation operation, string path, object value)
        {
            if (operation == null)
                throw new ArgumentNullException("operation");
            if (path == null)
                throw new ArgumentNullException("path");
            if (string.IsNullOrEmpty(path))
                throw new ArgumentException("path cannot be empty");

            if (!path.StartsWith("/"))
                path = "/" + path;

            _operation = operation;
            _path = path;
            _value = value;
        }

        /// <summary>
        /// Gets the <see cref="UpdateOperation"/> instance describing the operation to apply.
        /// </summary>
        public UpdateOperation Operation
        {
            get
            {
                return _operation;
            }
        }

        /// <summary>
        /// Gets the path to the property to apply the update operation to.
        /// </summary>
        public string Path
        {
            get
            {
                return _path;
            }
        }

        /// <summary>
        /// Gets the value for the property update operation.
        /// </summary>
        /// <value>
        /// The new value for the property, if <see cref="Operation"/> is <see cref="UpdateOperation.Add"/> or  <see cref="UpdateOperation.Replace"/>.
        /// <para>-or-</para>
        /// <para><see langword="null"/>, if <see cref="Operation"/> is <see cref="UpdateOperation.Remove"/>.</para>
        /// <para>-or-</para>
        /// <para>A value specific to the <see cref="Operation"/> for any other <see cref="UpdateOperation"/>.</para>
        /// </value>
        public object Value
        {
            get
            {
                return _value;
            }
        }
    }
}
