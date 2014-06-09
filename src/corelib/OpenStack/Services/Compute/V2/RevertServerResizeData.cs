namespace OpenStack.Services.Compute.V2
{
    using System;
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using OpenStack.ObjectModel;

    /// <summary>
    /// This class models the JSON representation of the body of a request to revert a
    /// completed resize operation on a server resource.
    /// </summary>
    /// <seealso cref="IComputeService.PrepareRevertServerResizeAsync"/>
    /// <seealso cref="ComputeServiceExtensions.RevertServerResizeAsync"/>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    [JsonObject(MemberSerialization.OptIn)]
    public class RevertServerResizeData : ExtensibleJsonObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RevertServerResizeData"/> class.
        /// </summary>
        [JsonConstructor]
        public RevertServerResizeData()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RevertServerResizeData"/> class with the specified
        /// extension data.
        /// </summary>
        /// <param name="extensionData">The extension data.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="extensionData"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">If <paramref name="extensionData"/> contains any <see langword="null"/> values.</exception>
        public RevertServerResizeData(params JProperty[] extensionData)
            : base(extensionData)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RevertServerResizeData"/> class with the specified
        /// extension data.
        /// </summary>
        /// <param name="extensionData">The extension data.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="extensionData"/> is <see langword="null"/>.</exception>
        public RevertServerResizeData(IDictionary<string, JToken> extensionData)
            : base(extensionData)
        {
        }
    }
}