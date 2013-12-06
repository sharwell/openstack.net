namespace net.openstack.Providers.Rackspace.Objects.AutoScale
{
    using System;
    using System.Collections.Generic;
    using net.openstack.Core.Collections;
    using Newtonsoft.Json;

    [JsonObject(MemberSerialization.OptIn)]
    public abstract class WebhookConfiguration
    {
        /// <summary>
        /// This is the backing field for the <see cref="Name"/> property.
        /// </summary>
        [JsonProperty("name", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private string _name;

        /// <summary>
        /// This is the backing field for the <see cref="Metadata"/> property.
        /// </summary>
        [JsonProperty("metadata", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private Dictionary<string, string> _metadata;

        /// <summary>
        /// Initializes a new instance of the <see cref="WebhookConfiguration"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected WebhookConfiguration()
        {
        }

        protected WebhookConfiguration(string name, IDictionary<string, string> metadata)
        {
            if (name == string.Empty)
                throw new ArgumentException("name cannot be empty");

            _name = name;
            if (metadata != null)
                _metadata = new Dictionary<string, string>(metadata);
        }

        public string Name
        {
            get
            {
                return _name;
            }
        }

        public ReadOnlyDictionary<string, string> Metadata
        {
            get
            {
                if (_metadata == null)
                    return null;

                return new ReadOnlyDictionary<string, string>(_metadata);
            }
        }
    }
}
