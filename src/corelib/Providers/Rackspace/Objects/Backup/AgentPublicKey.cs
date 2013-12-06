namespace net.openstack.Providers.Rackspace.Objects.Backup
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Newtonsoft.Json;

    [JsonObject(MemberSerialization.OptIn)]
    public class AgentPublicKey
    {
        /// <summary>
        /// This is the backing field for the <see cref="ModulusHex"/> property.
        /// </summary>
        [JsonProperty("ModulusHex")]
        private string _modulusHex;

        /// <summary>
        /// This is the backing field for the <see cref="ExponentHex"/> property.
        /// </summary>
        [JsonProperty("ExponentHex")]
        private string _exponentHex;

        /// <summary>
        /// Initializes a new instance of the <see cref="AgentPublicKey"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected AgentPublicKey()
        {
        }
    }
}
