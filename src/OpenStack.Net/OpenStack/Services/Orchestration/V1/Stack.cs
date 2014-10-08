namespace OpenStack.Services.Orchestration.V1
{
    using System;
    using Newtonsoft.Json;
    using OpenStack.ObjectModel;

#warning not all properties have been added here

    [JsonObject(MemberSerialization.OptIn)]
    public class Stack : StackData
    {
#pragma warning disable 649 // Field 'fieldName' is never assigned to, and will always have its default value {value}
        /// <summary>
        /// This is the backing field for the <see cref="Id"/> property.
        /// </summary>
        [JsonProperty("id", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private StackId _id;

        /// <summary>
        /// This is the backing field for the <see cref="Status"/> property.
        /// </summary>
        [JsonProperty("stack_status", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private StackStatus _stackStatus;

        /// <summary>
        /// This is the backing field for the <see cref="CreationTime"/> property.
        /// </summary>
        [JsonProperty("creation_time", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private DateTimeOffset? _creationTime;
#pragma warning restore 649

        /// <summary>
        /// Initializes a new instance of the <see cref="Stack"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected Stack()
        {
        }

        /// <summary>
        /// Gets the unique identifier of the stack resource within the OpenStack Orchestration Service.
        /// </summary>
        /// <value>
        /// <para>The unique identifier of the stack resource within the OpenStack Orchestration Service.</para>
        /// <token>NullIfNotIncluded</token>
        /// </value>
        public StackId Id
        {
            get
            {
                return _id;
            }
        }

        public StackStatus Status
        {
            get
            {
                return _stackStatus;
            }
        }

        public DateTimeOffset? CreationTime
        {
            get
            {
                return _creationTime;
            }
        }
    }
}
