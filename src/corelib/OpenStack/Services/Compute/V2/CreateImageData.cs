namespace OpenStack.Services.Compute.V2
{
    using System;
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using OpenStack.ObjectModel;

    /// <summary>
    /// This class models the JSON representation of the body of a request to create an
    /// image of a server resource.
    /// </summary>
    /// <seealso cref="IComputeService.PrepareCreateImageAsync"/>
    /// <seealso cref="ComputeServiceExtensions.CreateImageAsync"/>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    [JsonObject(MemberSerialization.OptIn)]
    public class CreateImageData : ExtensibleJsonObject
    {
        /// <summary>
        /// This is the backing field for the <see cref="Name"/> property.
        /// </summary>
        private string _name;

        /// <summary>
        /// This is the backing field for the <see cref="Metadata"/> property.
        /// </summary>
        private IDictionary<string, string> _metadata;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateImageData"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected CreateImageData()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateImageData"/> class
        /// with the specified name and initial metadata.
        /// </summary>
        /// <param name="name">The name of the image to create from a server resource.</param>
        /// <param name="metadata">The initial metadata to associate with the created image.</param>
        public CreateImageData(string name, IDictionary<string, string> metadata)
        {
            _name = name;
            _metadata = metadata;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateImageData"/> class
        /// with the specified name, initial metadata, and extension data.
        /// </summary>
        /// <param name="name">The name of the image to create from a server resource.</param>
        /// <param name="metadata">The initial metadata to associate with the created image.</param>
        /// <param name="extensionData">The extension data.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="extensionData"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">If <paramref name="extensionData"/> contains any <see langword="null"/> values.</exception>
        public CreateImageData(string name, IDictionary<string, string> metadata, params JProperty[] extensionData)
            : base(extensionData)
        {
            _name = name;
            _metadata = metadata;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateImageData"/> class
        /// with the specified name, initial metadata, and extension data.
        /// </summary>
        /// <param name="name">The name of the image to create from a server resource.</param>
        /// <param name="metadata">The initial metadata to associate with the created image.</param>
        /// <param name="extensionData">The extension data.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="extensionData"/> is <see langword="null"/>.</exception>
        public CreateImageData(string name, IDictionary<string, string> metadata, IDictionary<string, JToken> extensionData)
            : base(extensionData)
        {
            _name = name;
            _metadata = metadata;
        }

        /// <summary>
        /// Gets the name of the image to create from a server resource.
        /// </summary>
        /// <value>
        /// The name of the image to create from a server resource.
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
        /// Gets the initial metadata to associate with the created image.
        /// </summary>
        /// <value>
        /// The initial metadata to associate with the created image.
        /// <para>-or-</para>
        /// <para><see langword="null"/> if the JSON representation did not include the underlying property.</para>
        /// </value>
        public IDictionary<string, string> Metadata
        {
            get
            {
                return _metadata;
            }
        }
    }
}
