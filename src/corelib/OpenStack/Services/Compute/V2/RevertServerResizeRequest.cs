namespace OpenStack.Services.Compute.V2
{
    using System;
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using OpenStack.ObjectModel;

    /// <summary>
    /// This class models the JSON representation of the request body used for
    /// API calls to revert a completed resize operation on a server resource
    /// in the <see cref="IComputeService"/>.
    /// </summary>
    /// <seealso cref="IComputeService.PrepareRevertServerResizeAsync"/>
    /// <seealso cref="ComputeServiceExtensions.RevertServerResizeAsync"/>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    [JsonObject(MemberSerialization.OptIn)]
    public class RevertServerResizeRequest : ExtensibleJsonObject
    {
        /// <summary>
        /// This is the backing field for the <see cref="Arguments"/> property.
        /// </summary>
        [JsonProperty("revertResize", DefaultValueHandling = DefaultValueHandling.Include)]
        private RevertServerResizeData _arguments;

        /// <summary>
        /// Initializes a new instance of the <see cref="RevertServerResizeRequest"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected RevertServerResizeRequest()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RevertServerResizeRequest"/> class with the specified
        /// arguments.
        /// </summary>
        /// <param name="arguments">A <see cref="RevertServerResizeData"/> object containing the arguments for the action.</param>
        public RevertServerResizeRequest(RevertServerResizeData arguments)
        {
            _arguments = arguments;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RevertServerResizeData"/> class with the specified
        /// arguments and extension data.
        /// </summary>
        /// <param name="arguments">A <see cref="RevertServerResizeData"/> object containing the arguments for the action.</param>
        /// <param name="extensionData">The extension data.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="extensionData"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">If <paramref name="extensionData"/> contains any <see langword="null"/> values.</exception>
        public RevertServerResizeRequest(RevertServerResizeData arguments, params JProperty[] extensionData)
            : base(extensionData)
        {
            _arguments = arguments;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RevertServerResizeData"/> class with the specified
        /// arguments and extension data.
        /// </summary>
        /// <param name="arguments">A <see cref="RevertServerResizeData"/> object containing the arguments for the action.</param>
        /// <param name="extensionData">The extension data.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="extensionData"/> is <see langword="null"/>.</exception>
        public RevertServerResizeRequest(RevertServerResizeData arguments, IDictionary<string, JToken> extensionData)
            : base(extensionData)
        {
            _arguments = arguments;
        }

        /// <summary>
        /// Gets a <see cref="RevertServerResizeData"/> instance containing arguments for the action.
        /// </summary>
        /// <value>
        /// A <see cref="RevertServerResizeData"/> instance containing arguments for the action.
        /// <para>-or-</para>
        /// <para><see langword="null"/> if the JSON representation did not include the underlying property.</para>
        /// </value>
        public RevertServerResizeData Arguments
        {
            get
            {
                return _arguments;
            }
        }
    }
}
