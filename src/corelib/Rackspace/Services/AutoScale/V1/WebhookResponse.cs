namespace Rackspace.Services.AutoScale.V1
{
    using Newtonsoft.Json;
    using OpenStack.ObjectModel;

    [JsonObject(MemberSerialization.OptIn)]
    public class WebhookResponse : ExtensibleJsonObject
    {
        [JsonProperty("webhook", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private Webhook _webhook;

        /// <summary>
        /// Initializes a new instance of the <see cref="WebhookResponse"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected WebhookResponse()
        {
        }

        public Webhook Webhook
        {
            get
            {
                return _webhook;
            }
        }
    }
}
