namespace net.openstack.Core.Domain
{
    using System;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    [JsonObject(MemberSerialization.OptIn)]
    public class Message
    {
        [JsonProperty("ttl")]
        private long _timeToLive;

        [JsonProperty("body")]
        private JObject _body;

        [JsonConstructor]
        private Message()
        {
        }

        public Message(TimeSpan timeToLive, JObject body)
        {
            if (timeToLive < TimeSpan.Zero)
                throw new ArgumentOutOfRangeException("timeToLive");

            _timeToLive = (long)timeToLive.TotalSeconds;
            _body = body;
        }

        public TimeSpan TimeToLive
        {
            get
            {
                return TimeSpan.FromSeconds(_timeToLive);
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
