namespace net.openstack.Providers.Rackspace.Objects.Monitoring
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Newtonsoft.Json;

    [JsonObject(MemberSerialization.OptIn)]
    public class AgentTokenConfiguration
    {
        /// <summary>
        /// This is the backing field for the <see cref="Label"/> property.
        /// </summary>
        [JsonProperty("label")]
        private string _label;

        /// <summary>
        /// Initializes a new instance of the <see cref="AgentTokenConfiguration"/> class.
        /// </summary>
        [JsonConstructor]
        protected AgentTokenConfiguration()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AgentTokenConfiguration"/> class
        /// with the specified label.
        /// </summary>
        /// <param name="label">The token label.</param>
        public AgentTokenConfiguration(string label)
        {
            _label = label;
        }

        public string Label
        {
            get
            {
                return _label;
            }
        }
    }
}
