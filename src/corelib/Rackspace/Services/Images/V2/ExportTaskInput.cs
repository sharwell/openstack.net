namespace Rackspace.Services.Images.V2
{
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using OpenStack.ObjectModel;
    using OpenStack.Services.Images.V2;
    using OpenStack.Services.ObjectStorage.V1;

    [JsonObject(MemberSerialization.OptIn)]
    public class ExportTaskInput : ExtensibleJsonObject
    {
        [JsonProperty("image_uuid", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private ImageId _imageId;

        [JsonProperty("receiving_swift_container", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private ContainerName _containerName;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExportTaskInput"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected ExportTaskInput()
        {
        }

        public ExportTaskInput(ImageId imageId, ContainerName containerName)
        {
            _imageId = imageId;
            _containerName = containerName;
        }

        public ExportTaskInput(ImageId imageId, ContainerName containerName, params JProperty[] extensionData)
            : base(extensionData)
        {
            _imageId = imageId;
            _containerName = containerName;
        }

        public ExportTaskInput(ImageId imageId, ContainerName containerName, IDictionary<string, JToken> extensionData)
            : base(extensionData)
        {
            _imageId = imageId;
            _containerName = containerName;
        }

        public ImageId ImageId
        {
            get
            {
                return _imageId;
            }
        }

        public ContainerName ContainerName
        {
            get
            {
                return _containerName;
            }
        }
    }
}
