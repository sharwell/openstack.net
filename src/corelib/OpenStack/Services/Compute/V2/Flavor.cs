namespace OpenStack.Services.Compute.V2
{
    using System.Collections.ObjectModel;
    using Newtonsoft.Json;
    using OpenStack.ObjectModel;

    /// <summary>
    /// This class models the JSON representation of a flavor resource in the <see cref="IComputeService"/>.
    /// </summary>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
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

        /// <summary>
        /// Gets the unique identifier of the flavor resource.
        /// </summary>
        /// <value>
        /// A <see cref="ServerId"/> containing the unique identifier of the flavor resource.
        /// <para>-or-</para>
        /// <para><see langword="null"/> if the JSON representation did not include the underlying property.</para>
        /// </value>
        public FlavorId Id
        {
            get
            {
                return _id;
            }
        }

        /// <summary>
        /// Gets the name of the flavor resource.
        /// </summary>
        /// <value>
        /// The name of the flavor resource.
        /// <para>-or-</para>
        /// <para><see langword="null"/> if the JSON representation did not include the underlying property.</para>
        /// </value>
        public string Name
        {
            get
            {
                return _name;
            }
        }

        /// <summary>
        /// Gets the size of the disk included with this flavor, in GB.
        /// </summary>
        /// <remarks>
        /// <note>
        /// This value should only be used in comparisons to <see cref="Image.MinDisk"/>, as the service
        /// does not define the number of bytes in a GB.
        /// </note>
        /// </remarks>
        /// <value>
        /// The size of the disk included with this flavor, in GB.
        /// <para>-or-</para>
        /// <para><see langword="null"/> if the JSON representation did not include the underlying property.</para>
        /// </value>
        /// <seealso cref="Image.MinDisk"/>
        public int? Disk
        {
            get
            {
                return _disk;
            }
        }

        /// <summary>
        /// Gets the amount of memory included with this flavor, in MB.
        /// </summary>
        /// <remarks>
        /// <note>
        /// This value should only be used in comparisons to <see cref="Image.MinRam"/>, as the service
        /// does not define the number of bytes in a MB.
        /// </note>
        /// </remarks>
        /// <value>
        /// The amount of memory included with this flavor, in MB.
        /// <para>-or-</para>
        /// <para><see langword="null"/> if the JSON representation did not include the underlying property.</para>
        /// </value>
        /// <seealso cref="Image.MinRam"/>
        public int? Ram
        {
            get
            {
                return _ram;
            }
        }

        /// <summary>
        /// Gets the number of virtual processors included with this flavor.
        /// </summary>
        /// <value>
        /// The number of virtual processors included with this flavor.
        /// <para>-or-</para>
        /// <para><see langword="null"/> if the JSON representation did not include the underlying property.</para>
        /// </value>
        public int? ProcessorCount
        {
            get
            {
                return _vcpu;
            }
        }

        /// <summary>
        /// Gets a collection of links to other resources associated with the flavor resource.
        /// </summary>
        /// <value>
        /// A collection of <see cref="Link"/> instances describing resources associated with the flavor resource.
        /// <para>-or-</para>
        /// <para><see langword="null"/> if the JSON representation did not include the underlying property.</para>
        /// </value>
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
