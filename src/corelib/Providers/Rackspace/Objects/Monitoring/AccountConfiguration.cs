namespace net.openstack.Providers.Rackspace.Objects.Monitoring
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using net.openstack.Core.Collections;
    using Newtonsoft.Json;

    [JsonObject(MemberSerialization.OptIn)]
    public class AccountConfiguration
    {
        /// <summary>
        /// This is the backing field for the <see cref="Metadata"/> property.
        /// </summary>
        [JsonProperty("metadata", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private Dictionary<string, string> _metadata;

        /// <summary>
        /// This is the backing field for the <see cref="WebhookToken"/> property.
        /// </summary>
        [JsonProperty("webhook_token", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private WebhookToken _webhookToken;

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountConfiguration"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected AccountConfiguration()
        {
        }

        public AccountConfiguration(IDictionary<string, string> metadata)
        {
            if (metadata == null)
                throw new ArgumentNullException("metadata");
            if (metadata.ContainsKey(null) || metadata.ContainsKey(string.Empty))
                throw new ArgumentException("metadata cannot contain any null or empty keys", "metadata");

            _metadata = new Dictionary<string,string>(metadata);
        }

        public AccountConfiguration(WebhookToken webhookToken)
        {
            if (webhookToken == null)
                throw new ArgumentNullException("webhookToken");

            _webhookToken = webhookToken;
        }

        public AccountConfiguration(IDictionary<string, string> metadata, WebhookToken webhookToken)
        {
            if (metadata != null)
            {
                if (metadata.ContainsKey(null) || metadata.ContainsKey(string.Empty))
                    throw new ArgumentException("metadata cannot contain any null or empty keys", "metadata");

                _metadata = new Dictionary<string,string>(metadata);
            }

            _webhookToken = webhookToken;
        }

        public ReadOnlyDictionary<string, string> Metadata
        {
            get
            {
                return new ReadOnlyDictionary<string,string>(_metadata);
            }
        }

        public WebhookToken WebhookToken
        {
            get
            {
                return _webhookToken;
            }
        }
    }
}
