namespace Rackspace.Services.Images.V2
{
    using System;
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using OpenStack.ObjectModel;
    using OpenStack.Services.Images.V2;
    using OpenStack.Services.ObjectStorage.V1;

    [JsonObject(MemberSerialization.OptIn)]
    public class ImportTaskInput : ExtensibleJsonObject
    {
        /// <summary>
        /// This is the backing field for the <see cref="ContainerName"/> and <see cref="ObjectName"/> properties.
        /// </summary>
        [JsonProperty("import_from", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private string _importFrom;

        /// <summary>
        /// This is the backing field for the <see cref="Properties"/> property.
        /// </summary>
        [JsonProperty("image_properties", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private ImageData _imageProperties;

        /// <summary>
        /// Initializes a new instance of the <see cref="ImportTaskInput"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected ImportTaskInput()
        {
        }

        public ImportTaskInput(ContainerName containerName, ObjectName objectName, ImageData properties)
        {
            Initialize(containerName, objectName, properties);
        }

        public ImportTaskInput(ContainerName containerName, ObjectName objectName, ImageData properties, params JProperty[] extensionData)
            : base(extensionData)
        {
            Initialize(containerName, objectName, properties);
        }

        public ImportTaskInput(ContainerName containerName, ObjectName objectName, ImageData properties, IDictionary<string, JToken> extensionData)
            : base(extensionData)
        {
            Initialize(containerName, objectName, properties);
        }

        private void Initialize(ContainerName containerName, ObjectName objectName, ImageData properties)
        {
            if (containerName != null)
            {
                if (objectName != null)
                    _importFrom = containerName.Value + "/" + objectName.Value;
                else
                    _importFrom = containerName.Value;
            }
            else if (objectName != null)
            {
                _importFrom = "/" + objectName.Value;
            }

            _imageProperties = properties;
        }

        public ContainerName ContainerName
        {
            get
            {
                if (string.IsNullOrEmpty(_importFrom) || _importFrom[0] == '/')
                    return null;

                return new ContainerName(_importFrom.Substring(0, _importFrom.IndexOf('/')));
            }
        }

        public ObjectName ObjectName
        {
            get
            {
                if (string.IsNullOrEmpty(_importFrom))
                    return null;

                int slash = _importFrom.IndexOf('/');
                if (slash < 0)
                    return null;

                return new ObjectName(_importFrom.Substring(slash + 1));
            }
        }

        public ImageData Properties
        {
            get
            {
                return _imageProperties;
            }
        }
    }
}
