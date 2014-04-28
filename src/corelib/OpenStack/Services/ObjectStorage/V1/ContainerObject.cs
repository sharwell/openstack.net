namespace OpenStack.Services.ObjectStorage.V1
{
    using System;
    using Newtonsoft.Json;
    using OpenStack.ObjectModel;

    /// <summary>
    /// Provides the details of an object stored in an Object Storage provider.
    /// </summary>
    /// <seealso href="http://docs.openstack.org/api/openstack-object-storage/1.0/content/GET_showContainerDetails_v1__account___container__storage_container_services.html">Show container details and list objects (OpenStack Object Storage API v1 Reference)</seealso>
    /// <threadsafety static="true" instance="false"/>
    [JsonObject(MemberSerialization.OptIn)]
    public class ContainerObject : ExtensibleJsonObject
    {
        [JsonProperty("name", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private ObjectName _name;

        [JsonProperty("hash", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private string _hash;

        [JsonProperty("bytes", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private long? _bytes;

        [JsonProperty("content_type", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private string _contentType;

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
        /// Gets a "name" associated with the object.
        /// <note type="warning">The value of this property is not defined. Do not use.</note>
        /// </summary>
        /// <seealso href="http://docs.openstack.org/api/openstack-object-storage/1.0/content/GET_showContainerDetails_v1__account___container__storage_container_services.html">Show container details and list objects (OpenStack Object Storage API v1 Reference)</seealso>
        public ObjectName Name
        {
            get
            {
                return _name;
            }
        }

        /// <summary>
        /// Gets the "hash" value associated with the object.
        /// <note type="warning">The value of this property is not defined. Do not use.</note>
        /// </summary>
        /// <seealso href="http://docs.openstack.org/api/openstack-object-storage/1.0/content/GET_showContainerDetails_v1__account___container__storage_container_services.html">Show container details and list objects (OpenStack Object Storage API v1 Reference)</seealso>
        public string Hash
        {
            get
            {
                return _hash;
            }
        }

        /// <summary>
        /// Gets the "bytes" value associated with the object.
        /// <note type="warning">The value of this property is not defined. Do not use.</note>
        /// </summary>
        /// <seealso href="http://docs.openstack.org/api/openstack-object-storage/1.0/content/GET_showContainerDetails_v1__account___container__storage_container_services.html">Show container details and list objects (OpenStack Object Storage API v1 Reference)</seealso>
        public long? Size
        {
            get
            {
                return _bytes;
            }
        }

        /// <summary>
        /// Gets the "content type" value associated with the object.
        /// <note type="warning">The value of this property is not defined. Do not use.</note>
        /// </summary>
        /// <seealso href="http://docs.openstack.org/api/openstack-object-storage/1.0/content/GET_showContainerDetails_v1__account___container__storage_container_services.html">Show container details and list objects (OpenStack Object Storage API v1 Reference)</seealso>
        public string ContentType
        {
            get
            {
                return _contentType;
            }
        }

        /// <summary>
        /// Gets the "last modified" value associated with the object.
        /// <note type="warning">The value of this property is not defined. Do not use.</note>
        /// </summary>
        /// <seealso href="http://docs.openstack.org/api/openstack-object-storage/1.0/content/GET_showContainerDetails_v1__account___container__storage_container_services.html">Show container details and list objects (OpenStack Object Storage API v1 Reference)</seealso>
        public DateTimeOffset? LastModified
        {
            get
            {
                return _lastModified;
            }
        }
    }
}
