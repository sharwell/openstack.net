namespace OpenStack.Services.Orchestration.V1
{
    using System;
    using Newtonsoft.Json;
    using OpenStack.ObjectModel;

    /// <summary>
    /// Represents the unique identifier of a stack.
    /// </summary>
    /// <seealso cref="Stack.Id"/>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    [JsonConverter(typeof(StackId.Converter))]
    public sealed class StackId : ResourceIdentifier<StackId>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StackId"/> class
        /// with the specified identifier value.
        /// </summary>
        /// <param name="id">The identifier value.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="id"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">If <paramref name="id"/> is empty.</exception>
        public StackId(string id)
            : base(id)
        {
        }

        /// <summary>
        /// Provides support for serializing and deserializing <see cref="StackId"/>
        /// objects to JSON string values.
        /// </summary>
        /// <threadsafety static="true" instance="false"/>
        /// <preliminary/>
        private sealed class Converter : ConverterBase
        {
            /// <inheritdoc/>
            protected override StackId FromValue(string id)
            {
                return new StackId(id);
            }
        }
    }
}
