namespace OpenStack.Services.Compute.V2
{
    using System.Collections.ObjectModel;
    using Newtonsoft.Json;
    using OpenStack.ObjectModel;

    [JsonObject(MemberSerialization.OptIn)]
    public class Flavor : ExtensibleJsonObject
    {
#pragma warning disable 649 // Field 'fieldName' is never assigned to, and will always have its default value {value}
        /// <summary>
        /// This is the backing field for the <see cref="Id"/> property.
        /// </summary>
        [JsonProperty("id", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private FlavorId _id;

        /// <summary>
        /// This is the backing field for the <see cref="Name"/> property.
        /// </summary>
        [JsonProperty("name", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private string _name;

        /// <summary>
        /// This is the backing field for the <see cref="Disk"/> property.
        /// </summary>
        [JsonProperty("disk", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private int? _disk;

        /// <summary>
        /// This is the backing field for the <see cref="Ram"/> property.
        /// </summary>
        [JsonProperty("ram", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private int? _ram;

        /// <summary>
        /// This is the backing field for the <see cref="ProcessorCount"/> property.
        /// </summary>
        [JsonProperty("vcpus", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private int? _vcpu;

        /// <summary>
        /// This is the backing field for the <see cref="Links"/> property.
        /// </summary>
        [JsonProperty("id", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private Link[] _links;
#pragma warning restore 649

        /// <summary>
        /// Initializes a new instance of the <see cref="Flavor"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected Flavor()
        {
        }

        public FlavorId Id
        {
            get
            {
                return _id;
            }
        }

        public string Name
        {
            get
            {
                return _name;
            }
        }

        public int? Disk
        {
            get
            {
                return _disk;
            }
        }

        public int? Ram
        {
            get
            {
                return _ram;
            }
        }

        public int? ProcessorCount
        {
            get
            {
                return _vcpu;
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
