namespace net.openstack.Providers.Rackspace.Objects.AutoScale
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;
    using net.openstack.Core.Domain;
    using Newtonsoft.Json;

    [JsonObject(MemberSerialization.OptIn)]
    public class ServerArgument
    {
        [JsonProperty("flavorRef", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private FlavorId _flavorRef;

        [JsonProperty("imageRef", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private ImageId _imageId;

        [JsonProperty("name", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private string _name;

        [JsonProperty("networks", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private ServerNetworkArgument[] _networks;

        [JsonProperty("personality", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private Personality[] _personality;

        /// <summary>
        /// Initializes a new instance of the <see cref="ServerArgument"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected ServerArgument()
        {
        }

        public ServerArgument(FlavorId flavorId, ImageId imageId, string name, IEnumerable<ServerNetworkArgument> networks, IEnumerable<Personality> personality)
        {
            _flavorRef = flavorId;
            _imageId = imageId;
            _name = name;

            if (networks != null)
            {
                _networks = networks.ToArray();
                if (_networks.Contains(null))
                    throw new ArgumentException("networks cannot contain any null values", "networks");
            }

            if (personality != null)
            {
                _personality = personality.ToArray();
                if (_personality.Contains(null))
                    throw new ArgumentException("personality cannot contain any null values", "personality");
            }
        }

        public FlavorId FlavorId
        {
            get
            {
                return _flavorRef;
            }
        }

        public ImageId ImageId
        {
            get
            {
                return _imageId;
            }
        }

        public string Name
        {
            get
            {
                return _name;
            }
        }

        public ReadOnlyCollection<ServerNetworkArgument> Networks
        {
            get
            {
                if (_networks == null)
                    return null;

                return new ReadOnlyCollection<ServerNetworkArgument>(_networks);
            }
        }

        public ReadOnlyCollection<Personality> Personality
        {
            get
            {
                if (_personality == null)
                    return null;

                return new ReadOnlyCollection<Personality>(_personality);
            }
        }
    }
}
