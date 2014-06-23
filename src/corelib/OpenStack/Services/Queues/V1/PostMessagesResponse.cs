namespace OpenStack.Services.Queues.V1
{
    using System;
    using System.Collections.ObjectModel;
    using Newtonsoft.Json;
    using OpenStack.ObjectModel;

    /// <summary>
    /// This class models the JSON response to an HTTP API call prepared by <see cref="IQueuesService.PreparePostMessagesAsync"/>
    /// or <see cref="IQueuesService.PreparePostMessagesAsync{T}"/>.
    /// </summary>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    [JsonObject(MemberSerialization.OptIn)]
    public class PostMessagesResponse : ExtensibleJsonObject
    {
#pragma warning disable 649 // Field 'fieldName' is never assigned to, and will always have its default value {value}
        /// <summary>
        /// This is the backing field for the <see cref="Resources"/> property.
        /// </summary>
        [JsonProperty("resources", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private Uri[] _resources;

        /// <summary>
        /// This is the backing field for the <see cref="Partial"/> property.
        /// </summary>
        [JsonProperty("partial", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private bool? _partial;
#pragma warning restore 649

        /// <summary>
        /// Initializes a new instance of the <see cref="PostMessagesResponse"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected PostMessagesResponse()
        {
        }

        /// <summary>
        /// Gets a collection of addresses to individual message resources created by a Post Messages operation.
        /// </summary>
        /// <value>
        /// A collection of <see cref="Uri"/> instances containing links to message resources created by the operation.
        /// <para>-or-</para>
        /// <para><see langword="null"/> if the JSON representation did not include the underlying property.</para>
        /// </value>
        public ReadOnlyCollection<Uri> Resources
        {
            get
            {
                if (_resources == null)
                    return null;

                return new ReadOnlyCollection<Uri>(_resources);
            }
        }

        /// <summary>
        /// Gets a value indicating whether the API call successfully posted all requested messages to the queue.
        /// </summary>
        /// <value>
        /// <see langword="false"/> if all requested messages were posted to the queue.
        /// <para>-or-</para>
        /// <para><see langword="true"/> if some, but not all, messages were posted to the queue.</para>
        /// <para>-or-</para>
        /// <para><see langword="null"/> if the JSON representation did not include the underlying property.</para>
        /// </value>
        public bool? Partial
        {
            get
            {
                return _partial;
            }
        }
    }
}
