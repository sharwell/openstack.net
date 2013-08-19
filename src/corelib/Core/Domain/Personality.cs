﻿namespace net.openstack.Core.Domain
{
    using Newtonsoft.Json;

    [JsonObject(MemberSerialization.OptIn)]
    public class Personality
    {
        [JsonProperty("path")]
        public string Path { get; private set; }

        [JsonProperty("contents")]
        public string Content { get; private set; }
    }
}
