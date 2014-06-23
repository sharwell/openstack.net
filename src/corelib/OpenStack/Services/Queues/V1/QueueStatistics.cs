namespace OpenStack.Services.Queues.V1
{
    using Newtonsoft.Json;
    using OpenStack.ObjectModel;

    /// <summary>
    /// This models the JSON object used to represent statistics returned by the
    /// Get Queue Statistics API call.
    /// </summary>
    /// <seealso href="https://wiki.openstack.org/w/index.php?title=Marconi/specs/api/v1#Get_Queue_Stats">Get Queue Statistics (Marconi API v1 Blueprint)</seealso>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    [JsonObject(MemberSerialization.OptIn)]
    public class QueueStatistics : ExtensibleJsonObject
    {
#pragma warning disable 649 // Field 'fieldName' is never assigned to, and will always have its default value {value}
        /// <summary>
        /// The backing field for the <see cref="MessageStatistics"/> property.
        /// </summary>
        [JsonProperty("messages", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private QueueMessagesStatistics _messageStatistics;
#pragma warning restore 649

        /// <summary>
        /// Initializes a new instance of the <see cref="QueueStatistics"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected QueueStatistics()
        {
        }

        /// <summary>
        /// Gets statistics about messages contained in the queue.
        /// </summary>
        /// <value>
        /// A <see cref="QueueMessagesStatistics"/> object containing statistics about messages contained in the queue.
        /// <para>-or-</para>
        /// <para><see langword="null"/> if the JSON representation did not include the underlying property.</para>
        /// </value>
        public QueueMessagesStatistics MessageStatistics
        {
            get
            {
                return _messageStatistics;
            }
        }
    }
}
