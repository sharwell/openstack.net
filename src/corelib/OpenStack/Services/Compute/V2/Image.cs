namespace OpenStack.Services.Compute.V2
{
    using System;
    using System.Collections.ObjectModel;
    using Newtonsoft.Json;
    using OpenStack.ObjectModel;

    /// <summary>
    /// This class models the JSON representation of an image resource in the <see cref="IComputeService"/>.
    /// </summary>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    [JsonObject(MemberSerialization.OptIn)]
    public class Image : ExtensibleJsonObject
    {
#pragma warning disable 649 // Field 'fieldName' is never assigned to, and will always have its default value {value}
        /// <summary>
        /// This is the backing field for the <see cref="Id"/> property.
        /// </summary>
        [JsonProperty("id", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private ImageId _id;

        /// <summary>
        /// This is the backing field for the <see cref="Name"/> property.
        /// </summary>
        [JsonProperty("name", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private string _name;

        /// <summary>
        /// This is the backing field for the <see cref="Status"/> property.
        /// </summary>
        [JsonProperty("status", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private ImageStatus _status;

        /// <summary>
        /// This is the backing field for the <see cref="Progress"/> property.
        /// </summary>
        [JsonProperty("progress", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private int? _progress;

        /// <summary>
        /// This is the backing field for the <see cref="MinDisk"/> property.
        /// </summary>
        [JsonProperty("minDisk", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private int? _minDisk;

        /// <summary>
        /// This is the backing field for the <see cref="MinRam"/> property.
        /// </summary>
        [JsonProperty("minRam", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private int? _minRam;

        /// <summary>
        /// This is the backing field for the <see cref="Created"/> property.
        /// </summary>
        [JsonProperty("created", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private DateTimeOffset? _created;

        /// <summary>
        /// This is the backing field for the <see cref="LastModified"/> property.
        /// </summary>
        [JsonProperty("updated", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private DateTimeOffset? _updated;

        /// <summary>
        /// This is the backing field for the <see cref="Links"/> property.
        /// </summary>
        [JsonProperty("links", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private Link[] _links;
#pragma warning restore 649

        /// <summary>
        /// Initializes a new instance of the <see cref="Image"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected Image()
        {
        }

        /// <summary>
        /// Gets the unique identifier of the image resource.
        /// </summary>
        /// <value>
        /// A <see cref="ServerId"/> containing the unique identifier of the image resource.
        /// <para>-or-</para>
        /// <para><see langword="null"/> if the JSON representation did not include the underlying property.</para>
        /// </value>
        public ImageId Id
        {
            get
            {
                return _id;
            }
        }

        /// <summary>
        /// Gets the name of the image resource.
        /// </summary>
        /// <value>
        /// The name of the image resource.
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
        /// Gets the current status of the image resource.
        /// </summary>
        /// <value>
        /// An <see cref="ImageStatus"/> value indicating the current status of the image resource.
        /// <para>-or-</para>
        /// <para><see langword="null"/> if the JSON representation did not include the underlying property.</para>
        /// </value>
        public ImageStatus Status
        {
            get
            {
                return _status;
            }
        }

        /// <summary>
        /// Gets a value indicating the current progress of an ongoing operation on the image resource.
        /// </summary>
        /// <value>
        /// A value indicating the current progress of an ongoing operation on the image resource.
        /// <para>-or-</para>
        /// <para><see langword="null"/> if the JSON representation did not include the underlying property.</para>
        /// </value>
        public int? Progress
        {
            get
            {
                return _progress;
            }
        }

        /// <summary>
        /// Gets the minimum disk size required to use this image with a server resource (in GB).
        /// </summary>
        /// <remarks>
        /// <note>
        /// This value should only be used in comparisons to <see cref="Flavor.Disk"/>, as the service
        /// does not define the number of bytes in a GB.
        /// </note>
        /// </remarks>
        /// <value>
        /// The minimum disk size required to use this image with a server resource (in GB).
        /// <para>-or-</para>
        /// <para><see langword="null"/> if the JSON representation did not include the underlying property.</para>
        /// </value>
        /// <seealso cref="Flavor.Disk"/>
        public int? MinDisk
        {
            get
            {
                return _minDisk;
            }
        }

        /// <summary>
        /// Gets the minimum amount of memory required to use this image with a server resource (in MB).
        /// </summary>
        /// <remarks>
        /// <note>
        /// This value should only be used in comparisons to <see cref="Flavor.Ram"/>, as the service
        /// does not define the number of bytes in a MB.
        /// </note>
        /// </remarks>
        /// <value>
        /// The minimum amount of memory required to use this image with a server resource (in MB).
        /// <para>-or-</para>
        /// <para><see langword="null"/> if the JSON representation did not include the underlying property.</para>
        /// </value>
        /// <seealso cref="Flavor.Ram"/>
        public int? MinRam
        {
            get
            {
                return _minRam;
            }
        }

        /// <summary>
        /// Gets a timestamp indicating when the image resource was created.
        /// </summary>
        /// <value>
        /// A timestamp indicating when the image resource was created.
        /// <para>-or-</para>
        /// <para><see langword="null"/> if the JSON representation did not include the underlying property.</para>
        /// </value>
        public DateTimeOffset? Created
        {
            get
            {
                return _created;
            }
        }

        /// <summary>
        /// Gets a timestamp indicating when the image resource was last modified.
        /// </summary>
        /// <value>
        /// A timestamp indicating when the image resource was last modified.
        /// <para>-or-</para>
        /// <para><see langword="null"/> if the JSON representation did not include the underlying property.</para>
        /// </value>
        public DateTimeOffset? LastModified
        {
            get
            {
                return _updated;
            }
        }

        /// <summary>
        /// Gets a collection of links to other resources associated with the image resource.
        /// </summary>
        /// <value>
        /// A collection of <see cref="Link"/> instances describing resources associated with the image resource.
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
