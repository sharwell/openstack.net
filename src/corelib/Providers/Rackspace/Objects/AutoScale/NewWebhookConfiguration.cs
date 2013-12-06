namespace net.openstack.Providers.Rackspace.Objects.AutoScale
{
    using System;
    using System.Collections.Generic;
    using Newtonsoft.Json;

    [JsonObject(MemberSerialization.OptIn)]
    public class NewWebhookConfiguration : WebhookConfiguration
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NewWebhookConfiguration"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected NewWebhookConfiguration()
        {
        }

        public NewWebhookConfiguration(string name)
            : base(name, null)
        {
            if (name == null)
                throw new ArgumentNullException("name");
        }

        public NewWebhookConfiguration(string name, IDictionary<string, string> metadata)
            : base(name, metadata)
        {
            if (name == null)
                throw new ArgumentNullException("name");
        }
    }
}
