namespace net.openstack.Providers.Rackspace.Objects.Images
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using net.openstack.Core.Collections;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    /// <summary>
    /// This class models the JSON representation of an image in the <see cref="IImageService"/>.
    /// </summary>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    [JsonObject(MemberSerialization.OptIn)]
    public class Image
    {
#pragma warning disable 649 // Field 'fieldName' is never assigned to, and will always have its default value {value}
        /// <summary>
        /// This is the backing field for the <see cref="Id"/> property.
        /// </summary>
        [JsonProperty("id")]
        private ImageId _id;

        /// <summary>
        /// This is the backing field for the <see cref="Name"/> property.
        /// </summary>
        [JsonProperty("name")]
        private string _name;

        /// <summary>
        /// This is the backing field for the <see cref="Status"/> property.
        /// </summary>
        [JsonProperty("status")]
        private ImageStatus _status;

        /// <summary>
        /// This is the backing field for the <see cref="Visibility"/> property.
        /// </summary>
        [JsonProperty("visibility")]
        private ImageVisibility _visibility;

        /// <summary>
        /// This is the backing field for the <see cref="Size"/> property.
        /// </summary>
        [JsonProperty("size")]
        private long? _size;

        /// <summary>
        /// This is the backing field for the <see cref="Checksum"/> property.
        /// </summary>
        [JsonProperty("checksum")]
        private string _checksum;

        /// <summary>
        /// This is the backing field for the <see cref="Tags"/> property.
        /// </summary>
        [JsonProperty("tags")]
        private ImageTag[] _tags;

        /// <summary>
        /// This is the backing field for the <see cref="Created"/> property.
        /// </summary>
        [JsonProperty("created_at")]
        private DateTimeOffset? _created;

        /// <summary>
        /// This is the backing field for the <see cref="LastModified"/> property.
        /// </summary>
        [JsonProperty("updated_at")]
        private DateTimeOffset? _updated;

        /// <summary>
        /// This is the backing field for the <see cref="Self"/> property.
        /// </summary>
        [JsonProperty("self")]
        private string _self;

        /// <summary>
        /// This is the backing field for the <see cref="File"/> property.
        /// </summary>
        [JsonProperty("file")]
        private string _file;

        /// <summary>
        /// This is the backing field for the <see cref="Schema"/> property.
        /// </summary>
        [JsonProperty("schema")]
        private string _schema;

        /// <summary>
        /// This is the backing field for the <see cref="ExtensionData"/> property.
        /// </summary>
        [JsonExtensionData]
        private Dictionary<string, JToken> _extensionData;
#pragma warning restore 649

        /// <summary>
        /// Initializes a new instance of the <see cref="Image"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected Image()
        {
        }

        /// <summary>
        /// Gets the unique identifier of the image resource in the <see cref="IImageService"/>.
        /// </summary>
        public ImageId Id
        {
            get
            {
                return _id;
            }
        }

        /// <summary>
        /// Gets the name of the image.
        /// </summary>
        public string Name
        {
            get
            {
                return _name;
            }
        }

        /// <summary>
        /// Gets an <see cref="ImageStatus"/> instance indicating the current status of the image.
        /// </summary>
        public ImageStatus Status
        {
            get
            {
                return _status;
            }
        }

        /// <summary>
        /// Gets an <see cref="ImageVisibility"/> instance indicating the visibility of the image.
        /// </summary>
        public ImageVisibility Visibility
        {
            get
            {
                return _visibility;
            }
        }

        /// <summary>
        /// Gets the size (in bytes) of the image.
        /// </summary>
        public long? Size
        {
            get
            {
                return _size;
            }
        }

        /// <summary>
        /// Gets the checksum for the image.
        /// </summary>
        public string Checksum
        {
            get
            {
                return _checksum;
            }
        }

        /// <summary>
        /// Gets a collection of tags associated with the image.
        /// </summary>
        /// <seealso cref="IImageService.AddImageTagAsync"/>
        /// <seealso cref="IImageService.RemoveImageTagAsync"/>
        public ReadOnlyCollection<ImageTag> Tags
        {
            get
            {
                if (_tags == null)
                    return null;

                return new ReadOnlyCollection<ImageTag>(_tags);
            }
        }

        /// <summary>
        /// Gets a timestamp indicating when the image was created.
        /// </summary>
        public DateTimeOffset? Created
        {
            get
            {
                return _created;
            }
        }

        /// <summary>
        /// Gets a timestamp indicating when the image was last modified.
        /// </summary>
        public DateTimeOffset? LastModified
        {
            get
            {
                return _updated;
            }
        }

        /// <summary>
        /// Gets a <see cref="Uri"/> for the location of this object.
        /// </summary>
        public Uri Self
        {
            get
            {
                if (_self == null)
                    return null;

                return new Uri(_self, UriKind.RelativeOrAbsolute);
            }
        }

        /// <summary>
        /// Gets the <see cref="Uri"/> of the image file itself.
        /// </summary>
        public Uri File
        {
            get
            {
                if (_file == null)
                    return null;

                return new Uri(_file, UriKind.RelativeOrAbsolute);
            }
        }

        /// <summary>
        /// Gets a <see cref="Uri"/> for the location of the json-schema representation of this object.
        /// </summary>
        public Uri Schema
        {
            get
            {
                if (_schema == null)
                    return null;

                return new Uri(_schema, UriKind.RelativeOrAbsolute);
            }
        }

        /// <summary>
        /// Gets the JSON properties returned by the server which are not explicitly handled
        /// by another property in the <see cref="Image"/> model class.
        /// </summary>
        public ReadOnlyDictionary<string, JToken> ExtensionData
        {
            get
            {
                if (_extensionData == null)
                    return new ReadOnlyDictionary<string, JToken>(new Dictionary<string, JToken>());

                return new ReadOnlyDictionary<string, JToken>(_extensionData);
            }
        }
    }
}
