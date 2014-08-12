namespace Rackspace.Services.AutoScale.V1
{
    using System.Collections.ObjectModel;
    using Newtonsoft.Json;
    using OpenStack.ObjectModel;

    [JsonObject(MemberSerialization.OptIn)]
    public class WebhooksResponse : ExtensibleJsonObject
    {
        [JsonProperty("webhooks", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private Webhook[] _webhooks;

        /// <summary>
        /// Initializes a new instance of the <see cref="WebhooksResponse"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected WebhooksResponse()
        {
        }

        public ReadOnlyCollection<Webhook> Webhooks
        {
            get
            {
                if (_webhooks == null)
                    return null;

                return new ReadOnlyCollection<Webhook>(_webhooks);
            }
        }
    }
}
