namespace net.openstack.Core.Domain.Networking
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;
    using Newtonsoft.Json;

    [JsonObject(MemberSerialization.OptIn)]
    public class NetworkingExtension
    {
        [JsonProperty("alias")]
        private string _alias;

        [JsonProperty("name")]
        private string _name;

        [JsonProperty("description")]
        private string _description;

        [JsonProperty("namespace")]
        private string _namespace;

        [JsonProperty("links")]
        private Link[] _links;

        [JsonProperty("updated")]
        private DateTimeOffset? _updated;

        /// <summary>
        /// Initializes a new instance of the <see cref="NetworkingExtension"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected NetworkingExtension()
        {
        }

        public string Alias
        {
            get
            {
                return _alias;
            }
        }

        public string Name
        {
            get
            {
                return _name;
            }
        }

        public string Description
        {
            get
            {
                return _description;
            }
        }

        public string Namespace
        {
            get
            {
                return _namespace;
            }
        }

        public ReadOnlyCollection<Link> Links
        {
            get
            {
                if (_links == null)
                    return null;

                return new ReadOnlyCollection<Link>(_links);
            }
        }

        public DateTimeOffset? Updated
        {
            get
            {
                return _updated;
            }
        }
    }
}
