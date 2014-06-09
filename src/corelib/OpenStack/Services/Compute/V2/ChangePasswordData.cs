namespace OpenStack.Services.Compute.V2
{
    using System;
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using OpenStack.ObjectModel;

    /// <summary>
    /// This class models the JSON representation of the body of a request to change the password
    /// associated with a server resource.
    /// </summary>
    /// <seealso cref="IComputeService.PrepareChangePasswordAsync"/>
    /// <seealso cref="ComputeServiceExtensions.ChangePasswordAsync"/>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    [JsonObject]
    public class ChangePasswordData : ExtensibleJsonObject
    {
        /// <summary>
        /// This is the backing field for the <see cref="AdminPassword"/> property.
        /// </summary>
        [JsonProperty("adminPass", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private string _adminPass;

        /// <summary>
        /// Initializes a new instance of the <see cref="ChangePasswordData"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected ChangePasswordData()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ChangePasswordData"/> class with
        /// the specified admin password.
        /// </summary>
        /// <param name="adminPass">The new administrator password.</param>
        public ChangePasswordData(string adminPass)
        {
            _adminPass = adminPass;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ChangePasswordData"/> class with
        /// the specified admin password and extension data.
        /// </summary>
        /// <param name="adminPass">The new administrator password.</param>
        /// <param name="extensionData">The extension data.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="extensionData"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">If <paramref name="extensionData"/> contains any <see langword="null"/> values.</exception>
        public ChangePasswordData(string adminPass, params JProperty[] extensionData)
            : base(extensionData)
        {
            _adminPass = adminPass;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ChangePasswordData"/> class with
        /// the specified admin password and extension data.
        /// </summary>
        /// <param name="adminPass">The new administrator password.</param>
        /// <param name="extensionData">The extension data.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="extensionData"/> is <see langword="null"/>.</exception>
        public ChangePasswordData(string adminPass, IDictionary<string, JToken> extensionData)
            : base(extensionData)
        {
            _adminPass = adminPass;
        }

        /// <summary>
        /// Gets the new administrator password.
        /// </summary>
        /// <value>
        /// The new administrator password.
        /// <para>-or-</para>
        /// <para><see langword="null"/> if the JSON representation did not include the underlying property.</para>
        /// </value>
        public string AdminPassword
        {
            get
            {
                return _adminPass;
            }
        }
    }
}
