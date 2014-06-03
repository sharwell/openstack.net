namespace OpenStack.Services.Compute.V2
{
    using System;
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using OpenStack.ObjectModel;

    /// <summary>
    /// This is the base class for types modeling the JSON representation of a server resource in the <see cref="IComputeService"/>.
    /// </summary>
    /// <remarks>
    /// The base class defines standard properties which are used when creating and/or updating server resources.
    /// </remarks>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    [JsonObject(MemberSerialization.OptIn)]
    public class ServerData : ExtensibleJsonObject
    {
        /// <summary>
        /// This is the backing field for the <see cref="Name"/> property.
        /// </summary>
        [JsonProperty("name", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private string _name;

        /// <summary>
        /// This is the backing field for the <see cref="FlavorId"/> property.
        /// </summary>
        [JsonProperty("flavorRef", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private FlavorId _flavorRef;

        /// <summary>
        /// This is the backing field for the <see cref="ImageId"/> property.
        /// </summary>
        [JsonProperty("imageRef", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private ImageId _imageRef;

        /// <summary>
        /// Initializes a new instance of the <see cref="ServerData"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected ServerData()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ServerData"/> class
        /// with the specified properties.
        /// </summary>
        /// <param name="name">The name of the server resource.</param>
        /// <param name="flavorId">A <see cref="V2.FlavorId"/> identifying the flavor used for configuring the server resource.</param>
        /// <param name="imageId">A <see cref="V2.ImageId"/> identifying the image used to create the server resource.</param>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="name"/> is <see langword="null"/>.
        /// <para>-or-</para>
        /// <para>If <paramref name="flavorId"/> is <see langword="null"/>.</para>
        /// <para>-or-</para>
        /// <para>If <paramref name="imageId"/> is <see langword="null"/>.</para>
        /// </exception>
        /// <exception cref="ArgumentException">
        /// If <paramref name="name"/> is empty.
        /// </exception>
        public ServerData(string name, FlavorId flavorId, ImageId imageId)
        {
            Initialize(name, flavorId, imageId);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ServerData"/> class
        /// with the specified properties and extension data.
        /// </summary>
        /// <param name="name">The name of the server resource.</param>
        /// <param name="flavorId">A <see cref="V2.FlavorId"/> identifying the flavor used for configuring the server resource.</param>
        /// <param name="imageId">A <see cref="V2.ImageId"/> identifying the image used to create the server resource.</param>
        /// <param name="extensionData">The extension data.</param>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="name"/> is <see langword="null"/>.
        /// <para>-or-</para>
        /// <para>If <paramref name="flavorId"/> is <see langword="null"/>.</para>
        /// <para>-or-</para>
        /// <para>If <paramref name="imageId"/> is <see langword="null"/>.</para>
        /// <para>-or-</para>
        /// <para>If <paramref name="extensionData"/> is <see langword="null"/>.</para>
        /// </exception>
        /// <exception cref="ArgumentException">
        /// If <paramref name="name"/> is empty.
        /// <para>-or-</para>
        /// If <paramref name="extensionData"/> contains any <see langword="null"/> values.
        /// </exception>
        public ServerData(string name, FlavorId flavorId, ImageId imageId, params JProperty[] extensionData)
            : base(extensionData)
        {
            Initialize(name, flavorId, imageId);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ServerData"/> class
        /// with the specified properties.
        /// </summary>
        /// <param name="name">The name of the server resource.</param>
        /// <param name="flavorId">A <see cref="V2.FlavorId"/> identifying the flavor used for configuring the server resource.</param>
        /// <param name="imageId">A <see cref="V2.ImageId"/> identifying the image used to create the server resource.</param>
        /// <param name="extensionData">The extension data.</param>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="name"/> is <see langword="null"/>.
        /// <para>-or-</para>
        /// <para>If <paramref name="flavorId"/> is <see langword="null"/>.</para>
        /// <para>-or-</para>
        /// <para>If <paramref name="imageId"/> is <see langword="null"/>.</para>
        /// <para>-or-</para>
        /// <para>If <paramref name="extensionData"/> is <see langword="null"/>.</para>
        /// </exception>
        /// <exception cref="ArgumentException">
        /// If <paramref name="name"/> is empty.
        /// </exception>
        public ServerData(string name, FlavorId flavorId, ImageId imageId, IDictionary<string, JToken> extensionData)
            : base(extensionData)
        {
            Initialize(name, flavorId, imageId);
        }

        /// <summary>
        /// Gets the name of the server resource.
        /// </summary>
        /// <value>
        /// The name of the server resource.
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
        /// Gets the unique identifier of the <see cref="Flavor"/> of the server resource.
        /// </summary>
        /// <value>
        /// A <see cref="V2.FlavorId"/> containing the unique identifier of the flavor of the server resource.
        /// <para>-or-</para>
        /// <para><see langword="null"/> if the JSON representation did not include the underlying property.</para>
        /// </value>
        public FlavorId FlavorId
        {
            get
            {
                return _flavorRef;
            }
        }

        /// <summary>
        /// Gets the unique identifier of the <see cref="Image"/> used to create the server resource.
        /// </summary>
        /// <value>
        /// A <see cref="V2.ImageId"/> containing the unique identifier of the image used to create the server resource.
        /// <para>-or-</para>
        /// <para><see langword="null"/> if the JSON representation did not include the underlying property.</para>
        /// </value>
        public ImageId ImageId
        {
            get
            {
                return _imageRef;
            }
        }

        /// <summary>
        /// This method provides common parameter validation and initialization code to support the
        /// multiple constructors provided by this class.
        /// </summary>
        /// <param name="name">The name of the server resource.</param>
        /// <param name="flavorId">A <see cref="V2.FlavorId"/> identifying the flavor used for configuring the server resource.</param>
        /// <param name="imageId">A <see cref="V2.ImageId"/> identifying the image used to create the server resource.</param>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="name"/> is <see langword="null"/>.
        /// <para>-or-</para>
        /// <para>If <paramref name="flavorId"/> is <see langword="null"/>.</para>
        /// <para>-or-</para>
        /// <para>If <paramref name="imageId"/> is <see langword="null"/>.</para>
        /// </exception>
        /// <exception cref="ArgumentException">
        /// If <paramref name="name"/> is empty.
        /// </exception>
        private void Initialize(string name, FlavorId flavorId, ImageId imageId)
        {
            if (name == null)
                throw new ArgumentNullException("name");
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("name cannot be empty", "name");

            _name = name;
            _flavorRef = flavorId;
            _imageRef = imageId;
        }
    }
}
