namespace OpenStack.Services.Orchestration.V1
{
    using System;
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using OpenStack.ObjectModel;

    [JsonObject(MemberSerialization.OptIn)]
    public class StackActionRequest : ExtensibleJsonObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StackActionRequest"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        public StackActionRequest()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StackActionRequest"/> class
        /// with the specified extension data.
        /// </summary>
        /// <param name="extensionData">The extension data.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="extensionData"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">If <paramref name="extensionData"/> contains any <see langword="null"/> values.</exception>
        protected StackActionRequest(params JProperty[] extensionData)
            : base(extensionData)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StackActionRequest"/> class
        /// with the specified extension data.
        /// </summary>
        /// <param name="extensionData">The extension data.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="extensionData"/> is <see langword="null"/>.</exception>
        public StackActionRequest(IDictionary<string, JToken> extensionData)
            : base(extensionData)
        {
        }
    }
}
