namespace Rackspace.Services.Compute.V2
{
    using System;
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using OpenStack.ObjectModel;

    /// <summary>
    /// This class models the JSON representation of the request body used for the
    /// Rackspace-specific API call to place a server resource into rescue mode.
    /// </summary>
    /// <seealso cref="RescueExtensions.PrepareRescueServerAsync"/>
    /// <seealso cref="RescueExtensions.RescueServerAsync"/>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    [JsonObject(MemberSerialization.OptIn)]
    public class RescueServerRequest : ExtensibleJsonObject
    {
        /// <summary>
        /// The arguments for the Rescue Server action are just a simple fixed string.
        /// </summary>
        [JsonProperty("rescue", DefaultValueHandling = DefaultValueHandling.Include)]
        private string _arguments = "none";

        /// <summary>
        /// Initializes a new instance of the <see cref="RescueServerRequest"/> class.
        /// </summary>
        [JsonConstructor]
        public RescueServerRequest()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RescueServerRequest"/> class with the specified
        /// extension data.
        /// </summary>
        /// <param name="extensionData">The extension data.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="extensionData"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">If <paramref name="extensionData"/> contains any <see langword="null"/> values.</exception>
        public RescueServerRequest(params JProperty[] extensionData)
            : base(extensionData)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RescueServerRequest"/> class with the specified
        /// arguments and extension data.
        /// </summary>
        /// <param name="extensionData">The extension data.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="extensionData"/> is <see langword="null"/>.</exception>
        public RescueServerRequest(IDictionary<string, JToken> extensionData)
            : base(extensionData)
        {
        }
    }
}
