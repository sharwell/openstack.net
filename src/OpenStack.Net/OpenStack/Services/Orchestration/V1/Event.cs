namespace OpenStack.Services.Orchestration.V1
{
    using System;
    using System.Collections.ObjectModel;
    using Newtonsoft.Json;
    using OpenStack.ObjectModel;
    using OpenStack.Services.Identity;

    /// <summary>
    /// This class models the JSON representation of an event resource associated with a particular stack resource in
    /// the OpenStack Orchestration Service.
    /// </summary>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    [JsonObject(MemberSerialization.OptIn)]
    public class Event : ExtensibleJsonObject
    {
#pragma warning disable 649 // Field 'fieldName' is never assigned to, and will always have its default value {value}
        /// <summary>
        /// This is the backing field for the <see cref="Id"/> property.
        /// </summary>
        [JsonProperty("id", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private EventId _id;

        /// <summary>
        /// This is the backing field for the <see cref="EventTime"/> property.
        /// </summary>
        [JsonProperty("event_time", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private DateTimeOffset? _eventTime;

        /// <summary>
        /// This is the backing field for the <see cref="Links"/> property.
        /// </summary>
        [JsonProperty("links", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private Link[] _links;

        /// <summary>
        /// This is the backing field for the <see cref="LogicalResourceId"/> property.
        /// </summary>
        [JsonProperty("logical_resource_id", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private string _logicalResourceId;

        /// <summary>
        /// This is the backing field for the <see cref="PhysicalResourceId"/> property.
        /// </summary>
        [JsonProperty("physical_resource_id", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private string _physicalResourceId;

        /// <summary>
        /// This is the backing field for the <see cref="ResourceName"/> property.
        /// </summary>
        [JsonProperty("resource_name", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private ResourceName _resourceName;

        /// <summary>
        /// This is the backing field for the <see cref="ResourceStatus"/> property.
        /// </summary>
        [JsonProperty("resource_status", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private ResourceStatus _resourceStatus;

        /// <summary>
        /// This is the backing field for the <see cref="ResourceStatusReason"/> property.
        /// </summary>
        [JsonProperty("resource_status_reason", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private string _resourceStatusReason;
#pragma warning restore 649

        /// <summary>
        /// Initializes a new instance of the <see cref="Event"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected Event()
        {
        }

        /// <summary>
        /// Gets the unique identifier for the event resource.
        /// </summary>
        /// <value>
        /// <para>The unique identifier for the event resource.</para>
        /// <token>NullIfNotIncluded</token>
        /// </value>
        public EventId Id
        {
            get
            {
                return _id;
            }
        }

        /// <summary>
        /// Gets a timestamp indicating when the event occurred.
        /// </summary>
        /// <value>
        /// <para>A timestamp indicating when the event occurred.</para>
        /// <token>NullIfNotIncluded</token>
        /// </value>
        public DateTimeOffset? EventTime
        {
            get
            {
                return _eventTime;
            }
        }

        /// <summary>
        /// Gets a collection of links to external resources associated with the event.
        /// </summary>
        /// <value>
        /// <para>A collection of <seealso cref="Link"/> objects describing external resources associated with the
        /// event.</para>
        /// <token>NullIfNotIncluded</token>
        /// </value>
        public ReadOnlyCollection<Link> Links
        {
            get
            {
                if (_links == null)
                    return null;

                return new ReadOnlyCollection<Link>(_links);
            }
        }

        /// <summary>
        /// Gets the logical resource ID of the event.
        /// <token>OpenStackNotDefined</token>
        /// </summary>
        /// <value>
        /// <para>The logical resource ID of the event.</para>
        /// <token>NullIfNotIncluded</token>
        /// </value>
        public string LogicalResourceId
        {
            get
            {
                return _logicalResourceId;
            }
        }

        /// <summary>
        /// Gets the physical resource ID of the event.
        /// <token>OpenStackNotDefined</token>
        /// </summary>
        /// <value>
        /// <para>The physical resource ID of the event.</para>
        /// <token>NullIfNotIncluded</token>
        /// </value>
        public string PhysicalResourceId
        {
            get
            {
                return _physicalResourceId;
            }
        }

        /// <summary>
        /// Gets the name of the <see cref="Resource"/> affected by the event.
        /// </summary>
        /// <value>
        /// <para>The name of the <see cref="Resource"/> affected by the event.</para>
        /// <token>NullIfNotIncluded</token>
        /// </value>
        public ResourceName ResourceName
        {
            get
            {
                return _resourceName;
            }
        }

        /// <summary>
        /// Gets the status of the <see cref="Resource"/> following the event.
        /// </summary>
        /// <value>
        /// <para>The status of the <see cref="Resource"/> following the event.</para>
        /// <token>NullIfNotIncluded</token>
        /// </value>
        public ResourceStatus ResourceStatus
        {
            get
            {
                return _resourceStatus;
            }
        }

        /// <summary>
        /// Gets an additional message describing the reason for the <see cref="ResourceStatus"/> value.
        /// </summary>
        /// <value>
        /// <para>An additional message describing the reason for the <see cref="ResourceStatus"/> value.</para>
        /// <token>NullIfNotIncluded</token>
        /// </value>
        public string ResourceStatusReason
        {
            get
            {
                return _resourceStatusReason;
            }
        }
    }
}
