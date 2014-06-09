namespace OpenStack.Services.Compute.V2
{
    using System;
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using OpenStack.ObjectModel;

    /// <summary>
    /// This class models the JSON representation of the request body used for
    /// API calls to change the password of a server resource in the
    /// <see cref="IComputeService"/>.
    /// </summary>
    /// <seealso cref="IComputeService.PrepareChangePasswordAsync"/>
    /// <seealso cref="ComputeServiceExtensions.ChangePasswordAsync"/>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    [JsonObject(MemberSerialization.OptIn)]
    public class ChangePasswordRequest : ExtensibleJsonObject
    {
        /// <summary>
        /// This is the backing field for the <see cref="Arguments"/> property.
        /// </summary>
        [JsonProperty("changePassword", DefaultValueHandling = DefaultValueHandling.Include)]
        private ChangePasswordData _arguments;

        /// <summary>
        /// Initializes a new instance of the <see cref="ChangePasswordRequest"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected ChangePasswordRequest()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ChangePasswordRequest"/> class with the specified
        /// arguments.
        /// </summary>
        /// <param name="arguments">A <see cref="ChangePasswordData"/> object containing the arguments for the action.</param>
        public ChangePasswordRequest(ChangePasswordData arguments)
        {
            _arguments = arguments;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ChangePasswordData"/> class with the specified
        /// arguments and extension data.
        /// </summary>
        /// <param name="arguments">A <see cref="ChangePasswordData"/> object containing the arguments for the action.</param>
        /// <param name="extensionData">The extension data.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="extensionData"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">If <paramref name="extensionData"/> contains any <see langword="null"/> values.</exception>
        public ChangePasswordRequest(ChangePasswordData arguments, params JProperty[] extensionData)
            : base(extensionData)
        {
            _arguments = arguments;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ChangePasswordData"/> class with the specified
        /// arguments and extension data.
        /// </summary>
        /// <param name="arguments">A <see cref="ChangePasswordData"/> object containing the arguments for the action.</param>
        /// <param name="extensionData">The extension data.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="extensionData"/> is <see langword="null"/>.</exception>
        public ChangePasswordRequest(ChangePasswordData arguments, IDictionary<string, JToken> extensionData)
            : base(extensionData)
        {
            _arguments = arguments;
        }

        /// <summary>
        /// Gets a <see cref="ChangePasswordData"/> instance containing arguments for the action.
        /// </summary>
        /// <value>
        /// A <see cref="ChangePasswordData"/> instance containing arguments for the action.
        /// <para>-or-</para>
        /// <para><see langword="null"/> if the JSON representation did not include the underlying property.</para>
        /// </value>
        public ChangePasswordData Arguments
        {
            get
            {
                return _arguments;
            }
        }
    }
}
