namespace OpenStack.Services.Compute.V2
{
    using System;
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using OpenStack.ObjectModel;

    /// <summary>
    /// This class models the JSON representation of the body of a request to confirm a
    /// completed resize operation on a server resource.
    /// </summary>
    /// <seealso cref="IComputeService.PrepareConfirmServerResizeAsync"/>
    /// <seealso cref="ComputeServiceExtensions.ConfirmServerResizeAsync"/>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    [JsonObject(MemberSerialization.OptIn)]
    public class ConfirmServerResizeData : ExtensibleJsonObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConfirmServerResizeData"/> class.
        /// </summary>
        [JsonConstructor]
        public ConfirmServerResizeData()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfirmServerResizeData"/> class with the specified
        /// extension data.
        /// </summary>
        /// <param name="extensionData">The extension data.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="extensionData"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">If <paramref name="extensionData"/> contains any <see langword="null"/> values.</exception>
        public ConfirmServerResizeData(params JProperty[] extensionData)
            : base(extensionData)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfirmServerResizeData"/> class with the specified
        /// extension data.
        /// </summary>
        /// <param name="extensionData">The extension data.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="extensionData"/> is <see langword="null"/>.</exception>
        public ConfirmServerResizeData(IDictionary<string, JToken> extensionData)
            : base(extensionData)
        {
        }
    }
}