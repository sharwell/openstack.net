namespace OpenStack.Services.Orchestration.V1
{
    using System;
    using System.Collections.ObjectModel;
    using Newtonsoft.Json;
    using OpenStack.ObjectModel;
    using OpenStack.Services.Identity;

    /// <summary>
    /// This class models the JSON representation of a resource associated with a particular stack resource in the
    /// OpenStack Orchestration Service.
    /// </summary>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    [JsonObject(MemberSerialization.OptIn)]
    public class Resource : ExtensibleJsonObject
    {
#pragma warning disable 649 // Field 'fieldName' is never assigned to, and will always have its default value {value}
        /// <summary>
        /// This is the backing field for the <see cref="Name"/> property.
        /// </summary>
        [JsonProperty("resource_name", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private ResourceName _resourceName;

        /// <summary>
        /// This is the backing field for the <see cref="Description"/> property.
        /// </summary>
        [JsonProperty("description", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private string _description;

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
        /// This is the backing field for the <see cref="Status"/> property.
        /// </summary>
        [JsonProperty("resource_status", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private ResourceStatus _resourceStatus;

        /// <summary>
        /// This is the backing field for the <see cref="LastModified"/> property.
        /// </summary>
        [JsonProperty("updated_time", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private DateTimeOffset? _updatedTime;

        /// <summary>
        /// This is the backing field for the <see cref="RequiredBy"/> property.
        /// </summary>
        [JsonProperty("required_by", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private ResourceName[] _requiredBy;

        /// <summary>
        /// This is the backing field for the <see cref="StatusReason"/> property.
        /// </summary>
        [JsonProperty("resource_status_reason", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private string _resourceStatusReason;

        /// <summary>
        /// This is the backing field for the <see cref="PhysicalResourceId"/> property.
        /// </summary>
        [JsonProperty("physical_resource_id", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private string _physicalResourceId;

        /// <summary>
        /// This is the backing field for the <see cref="Type"/> property.
        /// </summary>
        [JsonProperty("resource_type", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private ResourceTypeName _resourceType;
#pragma warning restore 649

        /// <summary>
        /// Initializes a new instance of the <see cref="Resource"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected Resource()
        {
        }

        /// <summary>
        /// Gets the unique name which identifies the resource in a <see cref="Stack"/>.
        /// </summary>
        /// <value>
        /// <para>The unique name which identifies the resource in a <see cref="Stack"/>.</para>
        /// <token>NullIfNotIncluded</token>
        /// </value>
        public ResourceName Name
        {
            get
            {
                return _resourceName;
            }
        }

        /// <summary>
        /// Gets the description of the resource.
        /// </summary>
        /// <value>
        /// <para>The description of the resource.</para>
        /// <token>NullIfNotIncluded</token>
        /// </value>
        public string Description
        {
            get
            {
                return _description;
            }
        }

        /// <summary>
        /// Gets a collection of links to external resources associated with the resource.
        /// </summary>
        /// <value>
        /// <para>A collection of <seealso cref="Link"/> objects describing external resources associated with the
        /// resource.</para>
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
        /// Gets the logical resource ID of the resource.
        /// </summary>
        /// <value>
        /// <para>The logical resource ID of the resource.</para>
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
        /// Gets the physical resource ID of the resource.
        /// </summary>
        /// <value>
        /// <para>The physical resource ID of the resource.</para>
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
        /// Gets a timestamp indicating when the resource was last modified.
        /// </summary>
        /// <value>
        /// <para>A timestamp indicating when the resource was last modified.</para>
        /// <token>NullIfNotIncluded</token>
        /// </value>
        public DateTimeOffset? LastModified
        {
            get
            {
                return _updatedTime;
            }
        }

        /// <summary>
        /// Gets a collection of names of resources which depend on this resource.
        /// <token>OpenStackNotDefined</token>
        /// </summary>
        /// <value>
        /// <token>OpenStackNotDefined</token>
        /// <token>NullIfNotIncluded</token>
        /// </value>
        public ReadOnlyCollection<ResourceName> RequiredBy
        {
            get
            {
                if (_requiredBy == null)
                    return null;

                return new ReadOnlyCollection<ResourceName>(_requiredBy);
            }
        }

        /// <summary>
        /// Gets the status of the resource.
        /// </summary>
        /// <value>
        /// <para>The status of the resource.</para>
        /// <token>NullIfNotIncluded</token>
        /// </value>
        public ResourceStatus Status
        {
            get
            {
                return _resourceStatus;
            }
        }

        /// <summary>
        /// Gets a message describing the reason for the <see cref="Status"/> value.
        /// </summary>
        /// <value>
        /// <para>A message describing the reason for the <see cref="Status"/> value.</para>
        /// <token>NullIfNotIncluded</token>
        /// </value>
        public string StatusReason
        {
            get
            {
                return _resourceStatusReason;
            }
        }

        /// <summary>
        /// Gets the name of the resource type for the resource.
        /// </summary>
        /// <value>
        /// <para>A <see cref="ResourceTypeName"/> identifying the type of the resource.</para>
        /// <token>NullIfNotIncluded</token>
        /// </value>
        public ResourceTypeName Type
        {
            get
            {
                return _resourceType;
            }
        }
    }
}
