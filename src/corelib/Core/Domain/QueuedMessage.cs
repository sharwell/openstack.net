namespace net.openstack.Core.Domain
{
    using System;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    [JsonObject(MemberSerialization.OptIn)]
    public class QueuedMessage
    {
#pragma warning disable 649 // Field 'fieldName' is never assigned to, and will always have its default value {value}
        [JsonProperty("href")]
        private string _href;

        [JsonProperty("ttl")]
        private long _ttl;

        [JsonProperty("age")]
        private long _age;

        [JsonProperty("body")]
        private JObject _body;
#pragma warning restore 649

        [JsonConstructor]
        private QueuedMessage()
        {
        }

        public QueuedMessage(TimeSpan timeToLive, JObject body)
        {
            if (body == null)
                throw new ArgumentNullException("body");
            if (timeToLive <= TimeSpan.Zero)
                throw new ArgumentOutOfRangeException("timeToLive");
        }

        public string Id
        {
            get
            {
                string path = Href;
                path = path.Substring(path.LastIndexOf('/') + 1);
                if (path.Contains("?"))
                    path = path.Substring(0, path.IndexOf('?'));

                return path;
            }
        }

        public string Href
        {
            get
            {
                return _href;
            }
        }

        public TimeSpan TimeToLive
        {
            get
            {
                return TimeSpan.FromSeconds(_ttl);
            }
        }

        public TimeSpan Age
        {
            get
            {
                return TimeSpan.FromSeconds(_age);
            }
        }

        public JObject Body
        {
            get
            {
                return _body;
            }
        }
    }
}
