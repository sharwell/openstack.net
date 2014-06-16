namespace OpenStack.Services.ObjectStorage.V1
{
    using System;
    using Newtonsoft.Json;
    using OpenStack.ObjectModel;

    /// <summary>
    /// Provides the details of an object stored in the Object Storage Service.
    /// </summary>
    /// <seealso href="http://docs.openstack.org/api/openstack-object-storage/1.0/content/GET_showContainerDetails__v1__account___container__storage_container_services.html">Show container details and list objects (OpenStack Object Storage API V1 Reference)</seealso>
    /// <threadsafety static="true" instance="false"/>
    [JsonObject(MemberSerialization.OptIn)]
    public class ContainerObject : ExtensibleJsonObject
    {
        /// <summary>
        /// This is the backing field for the <see cref="Name"/> property.
        /// </summary>
        [JsonProperty("name", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private ObjectName _name;

        /// <summary>
        /// This is the backing field for the <see cref="Hash"/> property.
        /// </summary>
        [JsonProperty("hash", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private string _hash;

        /// <summary>
        /// This is the backing field for the <see cref="Size"/> property.
        /// </summary>
        [JsonProperty("bytes", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private long? _bytes;

        /// <summary>
        /// This is the backing field for the <see cref="ContentType"/> property.
        /// </summary>
        [JsonProperty("content_type", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private string _contentType;

        /// <summary>
        /// This is the backing field for the <see cref="LastModified"/> property.
        /// </summary>
        [JsonProperty("last_modified", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private DateTimeOffset? _lastModified;

        /// <summary>
        /// Initializes a new instance of the <see cref="ContainerObject"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected ContainerObject()
        {
        }

        /// <summary>
        /// Gets the name of the object.
        /// <note type="warning">The value of this property is not defined by OpenStack, and may not be consistent across vendors.</note>
        /// </summary>
        /// <value>
        /// The name of the object.
        /// <para>-or-</para>
        /// <para><see langword="null"/> if the JSON representation did not include the underlying property.</para>
        /// </value>
        /// <seealso href="http://docs.openstack.org/api/openstack-object-storage/1.0/content/GET_showContainerDetails__v1__account___container__storage_container_services.html">Show container details and list objects (OpenStack Object Storage API V1 Reference)</seealso>
        public ObjectName Name
        {
            get
            {
                return _name;
            }
        }

        /// <summary>
        /// Gets the hash code of the object.
        /// <note type="warning">The value of this property is not defined by OpenStack, and may not be consistent across vendors.</note>
        /// </summary>
        /// <value>
        /// The hash code of the object.
        /// <para>-or-</para>
        /// <para><see langword="null"/> if the JSON representation did not include the underlying property.</para>
        /// </value>
        /// <seealso href="http://docs.openstack.org/api/openstack-object-storage/1.0/content/GET_showContainerDetails__v1__account___container__storage_container_services.html">Show container details and list objects (OpenStack Object Storage API V1 Reference)</seealso>
        public string Hash
        {
            get
            {
                return _hash;
            }
        }

        /// <summary>
        /// Gets the size of the object in bytes.
        /// <note type="warning">The value of this property is not defined by OpenStack, and may not be consistent across vendors.</note>
        /// </summary>
        /// <value>
        /// The size of the object in bytes.
        /// <para>-or-</para>
        /// <para><see langword="null"/> if the JSON representation did not include the underlying property.</para>
        /// </value>
        /// <seealso href="http://docs.openstack.org/api/openstack-object-storage/1.0/content/GET_showContainerDetails__v1__account___container__storage_container_services.html">Show container details and list objects (OpenStack Object Storage API V1 Reference)</seealso>
        public long? Size
        {
            get
            {
                return _bytes;
            }
        }

        /// <summary>
        /// Gets the content type of the object.
        /// <note type="warning">The value of this property is not defined by OpenStack, and may not be consistent across vendors.</note>
        /// </summary>
        /// <value>
        /// The content type of the object.
        /// <para>-or-</para>
        /// <para><see langword="null"/> if the JSON representation did not include the underlying property.</para>
        /// </value>
        /// <seealso href="http://docs.openstack.org/api/openstack-object-storage/1.0/content/GET_showContainerDetails__v1__account___container__storage_container_services.html">Show container details and list objects (OpenStack Object Storage API V1 Reference)</seealso>
        public string ContentType
        {
            get
            {
                return _contentType;
            }
        }

        /// <summary>
        /// Gets a timestamp indicating when the object was last updated.
        /// <note type="warning">The value of this property is not defined by OpenStack, and may not be consistent across vendors.</note>
        /// </summary>
        /// <value>
        /// A timestamp indicating when the object was last updated.
        /// <para>-or-</para>
        /// <para><see langword="null"/> if the JSON representation did not include the underlying property.</para>
        /// </value>
        /// <seealso href="http://docs.openstack.org/api/openstack-object-storage/1.0/content/GET_showContainerDetails__v1__account___container__storage_container_services.html">Show container details and list objects (OpenStack Object Storage API V1 Reference)</seealso>
        public DateTimeOffset? LastModified
        {
            get
            {
                return _lastModified;
            }
        }
    }
}
