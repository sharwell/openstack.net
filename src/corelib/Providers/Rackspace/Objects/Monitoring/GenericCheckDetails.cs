namespace net.openstack.Providers.Rackspace.Objects.Monitoring
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using net.openstack.Core.Collections;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    [JsonObject(MemberSerialization.OptIn)]
    public class GenericCheckDetails : CheckDetails
    {
        [JsonExtensionData]
        private IDictionary<string, JToken> _properties;

        /// <summary>
        /// Initializes a new instance of the <see cref="GenericCheckDetails"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected GenericCheckDetails()
        {
        }

        public GenericCheckDetails(IDictionary<string, JToken> properties)
        {
            _properties = properties;
        }

        public ReadOnlyDictionary<string, JToken> Properties
        {
            get
            {
                return new ReadOnlyDictionary<string, JToken>(_properties);
            }
        }

        /// <inheritdoc/>
        protected internal override bool SupportsCheckType(CheckTypeId checkTypeId)
        {
            return true;
        }
    }
}
