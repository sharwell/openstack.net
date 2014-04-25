namespace OpenStack.Services.Identity.V2
{
    using System;
    using System.Collections.ObjectModel;
    using Newtonsoft.Json;
    using OpenStack.ObjectModel;
    using Link = net.openstack.Core.Domain.Link;

    [JsonObject(MemberSerialization.OptIn)]
    public class ExtensionData : ExtensibleJsonObject
    {
        /// <summary>
        /// This is the backing field for the <see cref="Name"/> property.
        /// </summary>
        [JsonProperty("name", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private string _name;

        /// <summary>
        /// This is the backing field for the <see cref="Namespace"/> property.
        /// </summary>
        [JsonProperty("namespace", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private string _namespace;

        /// <summary>
        /// This is the backing field for the <see cref="Alias"/> property.
        /// </summary>
        [JsonProperty("alias", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private string _alias;

        /// <summary>
        /// This is the backing field for the <see cref="LastModified"/> property.
        /// </summary>
        [JsonProperty("updated", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private DateTimeOffset? _updated;

        /// <summary>
        /// This is the backing field for the <see cref="Description"/> property.
        /// </summary>
        [JsonProperty("description", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private string _description;

        /// <summary>
        /// This is the backing field for the <see cref="Links"/> property.
        /// </summary>
        [JsonProperty("links", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private Link[] _links;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExtensionData"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected ExtensionData()
        {
        }

        public string Name
        {
            get
            {
                return _name;
            }
        }

        public string Namespace
        {
            get
            {
                return _namespace;
            }
        }

        public string Alias
        {
            get
            {
                return _alias;
            }
        }

        public DateTimeOffset? LastModified
        {
            get
            {
                return _updated;
            }
        }

        public string Description
        {
            get
            {
                return _description;
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
    }
}
