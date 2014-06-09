namespace OpenStack.Services.Compute.V2
{
    using System;
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using OpenStack.ObjectModel;

    /// <summary>
    /// This class models the JSON representation of the request body used for
    /// API calls to create an image from a server resource in the
    /// <see cref="IComputeService"/>.
    /// </summary>
    /// <seealso cref="IComputeService.PrepareCreateImageAsync"/>
    /// <seealso cref="ComputeServiceExtensions.CreateImageAsync"/>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    [JsonObject(MemberSerialization.OptIn)]
    public class CreateImageRequest : ExtensibleJsonObject
    {
        /// <summary>
        /// This is the backing field for the <see cref="Arguments"/> property.
        /// </summary>
        [JsonProperty("createImage", DefaultValueHandling = DefaultValueHandling.Include)]
        private CreateImageData _arguments;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateImageRequest"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected CreateImageRequest()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateImageRequest"/> class with the specified
        /// arguments.
        /// </summary>
        /// <param name="arguments">A <see cref="CreateImageData"/> object containing the arguments for the action.</param>
        public CreateImageRequest(CreateImageData arguments)
        {
            _arguments = arguments;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateImageData"/> class with the specified
        /// arguments and extension data.
        /// </summary>
        /// <param name="arguments">A <see cref="CreateImageData"/> object containing the arguments for the action.</param>
        /// <param name="extensionData">The extension data.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="extensionData"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">If <paramref name="extensionData"/> contains any <see langword="null"/> values.</exception>
        public CreateImageRequest(CreateImageData arguments, params JProperty[] extensionData)
            : base(extensionData)
        {
            _arguments = arguments;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateImageData"/> class with the specified
        /// arguments and extension data.
        /// </summary>
        /// <param name="arguments">A <see cref="CreateImageData"/> object containing the arguments for the action.</param>
        /// <param name="extensionData">The extension data.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="extensionData"/> is <see langword="null"/>.</exception>
        public CreateImageRequest(CreateImageData arguments, IDictionary<string, JToken> extensionData)
            : base(extensionData)
        {
            _arguments = arguments;
        }

        /// <summary>
        /// Gets a <see cref="CreateImageData"/> instance containing arguments for the action.
        /// </summary>
        /// <value>
        /// A <see cref="CreateImageData"/> instance containing arguments for the action.
        /// <para>-or-</para>
        /// <para><see langword="null"/> if the JSON representation did not include the underlying property.</para>
        /// </value>
        public CreateImageData Arguments
        {
            get
            {
                return _arguments;
            }
        }
    }
}
