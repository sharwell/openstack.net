namespace OpenStack.Services.Orchestration.V1
{
    using System;
    using Newtonsoft.Json;
    using OpenStack.ObjectModel;

    /// <summary>
    /// Represents the unique identifier of an <see cref="Event"/> resource in the OpenStack Orchestration Service.
    /// </summary>
    /// <seealso cref="Event.Id"/>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    [JsonConverter(typeof(EventId.Converter))]
    public sealed class EventId : ResourceIdentifier<EventId>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EventId"/> class
        /// with the specified identifier value.
        /// </summary>
        /// <param name="id">The identifier value.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="id"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">If <paramref name="id"/> is empty.</exception>
        public EventId(string id)
            : base(id)
        {
        }

        /// <summary>
        /// Provides support for serializing and deserializing <see cref="EventId"/>
        /// objects to JSON string values.
        /// </summary>
        /// <threadsafety static="true" instance="false"/>
        /// <preliminary/>
        private sealed class Converter : ConverterBase
        {
            /// <inheritdoc/>
            protected override EventId FromValue(string id)
            {
                return new EventId(id);
            }
        }
    }
}
