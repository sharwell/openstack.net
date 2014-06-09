namespace OpenStack.Services.Compute.V2
{
    using System;
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using OpenStack.ObjectModel;

    /// <summary>
    /// This class models the JSON representation of the body of a request to reboot
    /// a server resource.
    /// </summary>
    /// <seealso cref="IComputeService.PrepareRebootServerAsync"/>
    /// <seealso cref="ComputeServiceExtensions.RebootServerAsync"/>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    [JsonObject(MemberSerialization.OptIn)]
    public class RebootData : ExtensibleJsonObject
    {
        /// <summary>
        /// This is the backing field for the <see cref="Type"/> property.
        /// </summary>
        [JsonProperty("type", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private RebootType _type;

        /// <summary>
        /// Initializes a new instance of the <see cref="RebootData"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected RebootData()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RebootData"/> class with
        /// the specified reboot type.
        /// </summary>
        /// <param name="type">The type of reboot to perform on a server resource.</param>
        public RebootData(RebootType type)
        {
            _type = type;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RebootData"/> class with
        /// the specified reboot type and extension data.
        /// </summary>
        /// <param name="type">The type of reboot to perform on a server resource.</param>
        /// <param name="extensionData">The extension data.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="extensionData"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">If <paramref name="extensionData"/> contains any <see langword="null"/> values.</exception>
        public RebootData(RebootType type, params JProperty[] extensionData)
            : base(extensionData)
        {
            _type = type;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RebootData"/> class with
        /// the specified reboot type and extension data.
        /// </summary>
        /// <param name="type">The type of reboot to perform on a server resource.</param>
        /// <param name="extensionData">The extension data.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="extensionData"/> is <see langword="null"/>.</exception>
        public RebootData(RebootType type, IDictionary<string, JToken> extensionData)
            : base(extensionData)
        {
            _type = type;
        }

        /// <summary>
        /// Gets the type of reboot to perform on a server resource.
        /// </summary>
        /// <value>
        /// The type of reboot to perform on a server resource.
        /// <para>-or-</para>
        /// <para><see langword="null"/> if the JSON representation did not include the underlying property.</para>
        /// </value>
        public RebootType Type
        {
            get
            {
                return _type;
            }
        }
    }
}
