namespace OpenStack.Services.Compute.V2
{
    using System;
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using OpenStack.ObjectModel;

    /// <summary>
    /// This class models the JSON representation of the request body used for
    /// API calls to confirm a completed resize opeartion on a server resource
    /// in the <see cref="IComputeService"/>.
    /// </summary>
    /// <seealso cref="IComputeService.PrepareConfirmServerResizeAsync"/>
    /// <seealso cref="ComputeServiceExtensions.ConfirmServerResizeAsync"/>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    [JsonObject(MemberSerialization.OptIn)]
    public class ConfirmServerResizeRequest : ExtensibleJsonObject
    {
        /// <summary>
        /// This is the backing field for the <see cref="Arguments"/> property.
        /// </summary>
        [JsonProperty("confirmResize", DefaultValueHandling = DefaultValueHandling.Include)]
        private ConfirmServerResizeData _arguments;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfirmServerResizeRequest"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected ConfirmServerResizeRequest()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfirmServerResizeRequest"/> class with the specified
        /// arguments.
        /// </summary>
        /// <param name="arguments">A <see cref="ConfirmServerResizeData"/> object containing the arguments for the action.</param>
        public ConfirmServerResizeRequest(ConfirmServerResizeData arguments)
        {
            _arguments = arguments;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfirmServerResizeData"/> class with the specified
        /// arguments and extension data.
        /// </summary>
        /// <param name="arguments">A <see cref="ConfirmServerResizeData"/> object containing the arguments for the action.</param>
        /// <param name="extensionData">The extension data.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="extensionData"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">If <paramref name="extensionData"/> contains any <see langword="null"/> values.</exception>
        public ConfirmServerResizeRequest(ConfirmServerResizeData arguments, params JProperty[] extensionData)
            : base(extensionData)
        {
            _arguments = arguments;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfirmServerResizeData"/> class with the specified
        /// arguments and extension data.
        /// </summary>
        /// <param name="arguments">A <see cref="ConfirmServerResizeData"/> object containing the arguments for the action.</param>
        /// <param name="extensionData">The extension data.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="extensionData"/> is <see langword="null"/>.</exception>
        public ConfirmServerResizeRequest(ConfirmServerResizeData arguments, IDictionary<string, JToken> extensionData)
            : base(extensionData)
        {
            _arguments = arguments;
        }

        /// <summary>
        /// Gets a <see cref="ConfirmServerResizeData"/> instance containing arguments for the action.
        /// </summary>
        /// <value>
        /// A <see cref="ConfirmServerResizeData"/> instance containing arguments for the action.
        /// <para>-or-</para>
        /// <para><see langword="null"/> if the JSON representation did not include the underlying property.</para>
        /// </value>
        public ConfirmServerResizeData Arguments
        {
            get
            {
                return _arguments;
            }
        }
    }
}
