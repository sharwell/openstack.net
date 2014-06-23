namespace OpenStack.Services.Queues.V1
{
    using System;
    using System.Collections.ObjectModel;
    using Newtonsoft.Json;
    using OpenStack.ObjectModel;

    [JsonObject(MemberSerialization.OptIn)]
    public class PostMessagesResponse : ExtensibleJsonObject
    {
        /// <summary>
        /// This is the backing field for the <see cref="Resources"/> property.
        /// </summary>
        [JsonProperty("resources", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private Uri[] _resources;

        /// <summary>
        /// This is the backing field for the <see cref="Partial"/> property.
        /// </summary>
        private bool? _partial;

        /// <summary>
        /// Initializes a new instance of the <see cref="PostMessagesResponse"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected PostMessagesResponse()
        {
        }

        public ReadOnlyCollection<Uri> Resources
        {
            get
            {
                if (_resources == null)
                    return null;

                return new ReadOnlyCollection<Uri>(_resources);
            }
        }

        public bool? Partial
        {
            get
            {
                return _partial;
            }
        }
    }
}
