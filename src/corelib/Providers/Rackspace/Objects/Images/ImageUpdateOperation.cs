namespace net.openstack.Providers.Rackspace.Objects.Images
{
    using System;
    using Newtonsoft.Json;

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
        /// 
        /// </summary>
        /// <param name="operation"></param>
        /// <param name="path">The property to apply the update operation to. This may be one of the values in <see cref="PredefinedImageProperties"/>, or a path to an application-specific custom metadata.</param>
        /// <param name="value"></param>
        public ImageUpdateOperation(UpdateOperation operation, string path, object value)
        {
            if (operation == null)
                throw new ArgumentNullException("operation");
            if (path == null)
                throw new ArgumentNullException("path");
            if (string.IsNullOrEmpty(path))
                throw new ArgumentException("path cannot be empty");

            _operation = operation;
            _path = path;
            _value = value;
        }

        public UpdateOperation Operation
        {
            get
            {
                return _operation;
            }
        }

        public string Path
        {
            get
            {
                return _path;
            }
        }

        public object Value
        {
            get
            {
                return _value;
            }
        }
    }
}
