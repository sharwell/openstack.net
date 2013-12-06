namespace net.openstack.Providers.Rackspace.Objects.AutoScale
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Newtonsoft.Json;

    [JsonObject(MemberSerialization.OptIn)]
    public class UpdateWebhookConfiguration : WebhookConfiguration
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateWebhookConfiguration"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected UpdateWebhookConfiguration()
        {
        }

        public UpdateWebhookConfiguration(string name)
            : base(name, null)
        {
        }

        public UpdateWebhookConfiguration(IDictionary<string, string> metadata)
            : base(null, metadata)
        {
        }

        public UpdateWebhookConfiguration(string name, IDictionary<string, string> metadata)
            : base(name, metadata)
        {
        }
    }
}
