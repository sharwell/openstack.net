namespace OpenStack.Services.Compute.V2
{
    using System;
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using OpenStack.ObjectModel;

    /// <summary>
    /// This class models the JSON representation of the request body used for
    /// API calls to reboot a server resource in the <see cref="IComputeService"/>.
    /// </summary>
    /// <seealso cref="IComputeService.PrepareRebootServerAsync"/>
    /// <seealso cref="ComputeServiceExtensions.RebootServerAsync"/>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    [JsonObject(MemberSerialization.OptIn)]
    public class RebootRequest : ExtensibleJsonObject
    {
        /// <summary>
        /// This is the backing field for the <see cref="Arguments"/> property.
        /// </summary>
        [JsonProperty("reboot", DefaultValueHandling = DefaultValueHandling.Include)]
        private RebootData _arguments;

        /// <summary>
        /// Initializes a new instance of the <see cref="RebootRequest"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected RebootRequest()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RebootRequest"/> class with the specified
        /// arguments.
        /// </summary>
        /// <param name="arguments">A <see cref="RebootData"/> object containing the arguments for the action.</param>
        public RebootRequest(RebootData arguments)
        {
            _arguments = arguments;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RebootData"/> class with the specified
        /// arguments and extension data.
        /// </summary>
        /// <param name="arguments">A <see cref="RebootData"/> object containing the arguments for the action.</param>
        /// <param name="extensionData">The extension data.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="extensionData"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">If <paramref name="extensionData"/> contains any <see langword="null"/> values.</exception>
        public RebootRequest(RebootData arguments, params JProperty[] extensionData)
            : base(extensionData)
        {
            _arguments = arguments;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RebootData"/> class with the specified
        /// arguments and extension data.
        /// </summary>
        /// <param name="arguments">A <see cref="RebootData"/> object containing the arguments for the action.</param>
        /// <param name="extensionData">The extension data.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="extensionData"/> is <see langword="null"/>.</exception>
        public RebootRequest(RebootData arguments, IDictionary<string, JToken> extensionData)
            : base(extensionData)
        {
            _arguments = arguments;
        }

        /// <summary>
        /// Gets a <see cref="RebootData"/> instance containing arguments for the action.
        /// </summary>
        /// <value>
        /// A <see cref="RebootData"/> instance containing arguments for the action.
        /// <para>-or-</para>
        /// <para><see langword="null"/> if the JSON representation did not include the underlying property.</para>
        /// </value>
        public RebootData Arguments
        {
            get
            {
                return _arguments;
            }
        }
    }
}
