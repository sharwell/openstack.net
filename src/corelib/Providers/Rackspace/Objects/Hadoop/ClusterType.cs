namespace net.openstack.Providers.Rackspace.Objects.Hadoop
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;
    using net.openstack.Core.Domain;
    using Newtonsoft.Json;

    [JsonObject(MemberSerialization.OptIn)]
    public class ClusterType
    {
#pragma warning disable 649 // Field 'fieldName' is never assigned to, and will always have its default value {value}
        /// <summary>
        /// This is the backing field for the <see cref="Id"/> property.
        /// </summary>
        [JsonProperty("id")]
        private ClusterTypeId _id;

        /// <summary>
        /// This is the backing field for the <see cref="Links"/> property.
        /// </summary>
        [JsonProperty("links")]
        private Link[] _links;

        /// <summary>
        /// This is the backing field for the <see cref="Name"/> property.
        /// </summary>
        [JsonProperty("name")]
        private string _name;
#pragma warning restore 649

        /// <summary>
        /// Gets the unique identifier for the cluster type.
        /// </summary>
        public ClusterTypeId Id
        {
            get
            {
                return _id;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ClusterType"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected ClusterType()
        {
        }

        /// <summary>
        /// Gets a collection of <see cref="Link"/> objects describing resources related
        /// to this cluster type resource.
        /// </summary>
        public ReadOnlyCollection<Link> Links
        {
            get
            {
                if (_links == null)
                    return null;

                return new ReadOnlyCollection<Link>(_links);
            }
        }

        /// <summary>
        /// Gets the name of the cluster type.
        /// </summary>
        public string Name
        {
            get
            {
                return _name;
            }
        }
    }
}
