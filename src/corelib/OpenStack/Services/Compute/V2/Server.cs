namespace OpenStack.Services.Compute.V2
{
    using System;
    using System.Collections.ObjectModel;
    using Newtonsoft.Json;
    using OpenStack.Services.Identity;

    /// <summary>
    /// This class models the JSON representation of a server resource in the <see cref="IComputeService"/>.
    /// It extends <see cref="ServerData"/> by defining additional properties which are provided by the
    /// standard API calls, but are not used in the creation and/or updates to server resources by users.
    /// </summary>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    [JsonObject(MemberSerialization.OptIn)]
    public class Server : ServerData
    {
#pragma warning disable 649 // Field 'fieldName' is never assigned to, and will always have its default value {value}
        /// <summary>
        /// This is the backing field for the <see cref="Id"/> property.
        /// </summary>
        [JsonProperty("id", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private ServerId _id;

        /// <summary>
        /// This is the backing field for the <see cref="Created"/> property.
        /// </summary>
        [JsonProperty("created", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private DateTimeOffset? _created;

        /// <summary>
        /// This is the backing field for the <see cref="LastModified"/> property.
        /// </summary>
        [JsonProperty("updated", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private DateTimeOffset? _updated;

        /// <summary>
        /// This is the backing field for the <see cref="Status"/> property.
        /// </summary>
        [JsonProperty("status", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private ServerStatus _status;

        /// <summary>
        /// This is the backing field for the <see cref="Progress"/> property.
        /// </summary>
        [JsonProperty("progress", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private int? _progress;

        /// <summary>
        /// This is the backing field for the <see cref="ProjectId"/> property.
        /// </summary>
        [JsonProperty("tenant_id", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private ProjectId _tenantId;

        /// <summary>
        /// This is the backing field for the <see cref="Links"/> property.
        /// </summary>
        [JsonProperty("links", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private Link[] _links;
#pragma warning restore 649

        /// <summary>
        /// Initializes a new instance of the <see cref="Server"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected Server()
        {
        }

        /// <summary>
        /// Gets the unique identifier of the server resource.
        /// </summary>
        /// <value>
        /// A <see cref="ServerId"/> containing the unique identifier of the server resource.
        /// <para>-or-</para>
        /// <para><see langword="null"/> if the JSON representation did not include the underlying property.</para>
        /// </value>
        public ServerId Id
        {
            get
            {
                return _id;
            }
        }

        /// <summary>
        /// Gets a timestamp indicating when the server resource was created.
        /// </summary>
        /// <value>
        /// A timestamp indicating when the server resource was created.
        /// <para>-or-</para>
        /// <para><see langword="null"/> if the JSON representation did not include the underlying property.</para>
        /// </value>
        public DateTimeOffset? Created
        {
            get
            {
                return _created;
            }
        }

        /// <summary>
        /// Gets a timestamp indicating when the server resource was last modified.
        /// </summary>
        /// <value>
        /// A timestamp indicating when the server resource was last modified.
        /// <para>-or-</para>
        /// <para><see langword="null"/> if the JSON representation did not include the underlying property.</para>
        /// </value>
        public DateTimeOffset? LastModified
        {
            get
            {
                return _updated;
            }
        }

        /// <summary>
        /// Gets the current status of the server resource.
        /// </summary>
        /// <value>
        /// A <see cref="ServerStatus"/> value indicating the current status of the server resource.
        /// <para>-or-</para>
        /// <para><see langword="null"/> if the JSON representation did not include the underlying property.</para>
        /// </value>
        public ServerStatus Status
        {
            get
            {
                return _status;
            }
        }

        /// <summary>
        /// Gets a value indicating the current progress of an ongoing operation on the server resource.
        /// </summary>
        /// <value>
        /// A value indicating the current progress of an ongoing operation on the server resource.
        /// <para>-or-</para>
        /// <para><see langword="null"/> if the JSON representation did not include the underlying property.</para>
        /// </value>
        public int? Progress
        {
            get
            {
                return _progress;
            }
        }

        /// <summary>
        /// Gets the unique identifier of the project the server resource is associated with.
        /// </summary>
        /// <value>
        /// A <see cref="Identity.ProjectId"/> instance indicating the project the server resource is associated with.
        /// <para>-or-</para>
        /// <para><see langword="null"/> if the JSON representation did not include the underlying property.</para>
        /// </value>
        public ProjectId ProjectId
        {
            get
            {
                return _tenantId;
            }
        }

        /// <summary>
        /// Gets a collection of links to other resources associated with the server resource.
        /// </summary>
        /// <value>
        /// A collection of <see cref="Link"/> instances describing resources associated with the server resource.
        /// <para>-or-</para>
        /// <para><see langword="null"/> if the JSON representation did not include the underlying property.</para>
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
    }
}
