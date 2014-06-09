namespace OpenStack.Services.Compute.V2
{
    using System;
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using OpenStack.ObjectModel;

    /// <summary>
    /// This class models the JSON representation of the body of a request to resize
    /// a server resource.
    /// </summary>
    /// <seealso cref="IComputeService.PrepareResizeServerAsync"/>
    /// <seealso cref="ComputeServiceExtensions.ResizeServerAsync"/>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    [JsonObject(MemberSerialization.OptIn)]
    public class ResizeData : ExtensibleJsonObject
    {
        /// <summary>
        /// This is the backing field for the <see cref="FlavorId"/> property.
        /// </summary>
        [JsonProperty("flavorRef", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private FlavorId _flavorId;

        /// <summary>
        /// Initializes a new instance of the <see cref="ResizeData"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected ResizeData()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ResizeData"/> class with
        /// the specified flavor.
        /// </summary>
        /// <param name="flavorId">The identifier of the new flavor to apply to the server resource.</param>
        public ResizeData(FlavorId flavorId)
        {
            _flavorId = flavorId;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ResizeData"/> class with
        /// the specified flavor and extension data.
        /// </summary>
        /// <param name="flavorId">The identifier of the new flavor to apply to the server resource.</param>
        /// <param name="extensionData">The extension data.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="extensionData"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">If <paramref name="extensionData"/> contains any <see langword="null"/> values.</exception>
        public ResizeData(FlavorId flavorId, params JProperty[] extensionData)
            : base(extensionData)
        {
            _flavorId = flavorId;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ResizeData"/> class with
        /// the specified flavor and extension data.
        /// </summary>
        /// <param name="flavorId">The identifier of the new flavor to apply to the server resource.</param>
        /// <param name="extensionData">The extension data.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="extensionData"/> is <see langword="null"/>.</exception>
        public ResizeData(FlavorId flavorId, IDictionary<string, JToken> extensionData)
            : base(extensionData)
        {
            _flavorId = flavorId;
        }

        /// <summary>
        /// Gets the identifier of the new flavor to apply to the server resource.
        /// </summary>
        /// <value>
        /// The identifier of the new flavor to apply to the server resource.
        /// <para>-or-</para>
        /// <para><see langword="null"/> if the JSON representation did not include the underlying property.</para>
        /// </value>
        public FlavorId FlavorId
        {
            get
            {
                return _flavorId;
            }
        }
    }
}
