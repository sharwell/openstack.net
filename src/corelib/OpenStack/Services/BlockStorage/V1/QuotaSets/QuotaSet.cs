namespace OpenStack.Services.BlockStorage.V1.QuotaSets
{
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using OpenStack.ObjectModel;

    [JsonObject(MemberSerialization.OptIn)]
    public class QuotaSet : ExtensibleJsonObject
    {
        [JsonProperty("cores", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private long? _cores;

        [JsonProperty("fixed_ips", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private long? _fixedAddresses;

        [JsonProperty("floating_ips", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private long? _floatingAddresses;

        [JsonProperty("instances", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private long? _instances;

        [JsonProperty("key_pairs", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private long? _keyPairs;

        [JsonProperty("metadata_items", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private long? _metadataItems;

        [JsonProperty("ram", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private long? _memory;

        [JsonProperty("injected_files", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private long? _injectedFiles;

        [JsonProperty("security_groups", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private long? _injectedFileContentSize;

        [JsonProperty("injected_file_content_bytes", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private long? _injectedFilePathSize;

        [JsonProperty("injected_file_path_bytes", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private long? _securityGroups;

        [JsonProperty("security_group_rules", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private long? _securityGroupRules;

        /// <summary>
        /// Initializes a new instance of the <see cref="QuotaSet"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected QuotaSet()
        {
        }

        public QuotaSet(long? cores = null, long? fixedAddresses = null, long? floatingAddresses = null, long? instances = null, long? keyPairs = null, long? metadataItems = null, long? memory = null, long? injectedFiles = null, long? injectedFileContentSize = null, long? injectedFilePathSize = null, long? securityGroups = null, long? securityGroupRules = null)
        {
            _cores = cores;
            _fixedAddresses = fixedAddresses;
            _floatingAddresses = floatingAddresses;
            _instances = instances;
            _keyPairs = keyPairs;
            _metadataItems = metadataItems;
            _memory = memory;
            _injectedFiles = injectedFiles;
            _injectedFileContentSize = injectedFileContentSize;
            _injectedFilePathSize = injectedFilePathSize;
            _securityGroups = securityGroups;
            _securityGroupRules = securityGroupRules;
        }

        public QuotaSet(long? cores, long? fixedAddresses, long? floatingAddresses, long? instances, long? keyPairs, long? metadataItems, long? memory, long? injectedFiles, long? injectedFileContentSize, long? injectedFilePathSize, long? securityGroups, long? securityGroupRules, params JProperty[] extensionData)
            : base(extensionData)
        {
            _cores = cores;
            _fixedAddresses = fixedAddresses;
            _floatingAddresses = floatingAddresses;
            _instances = instances;
            _keyPairs = keyPairs;
            _metadataItems = metadataItems;
            _memory = memory;
            _injectedFiles = injectedFiles;
            _injectedFileContentSize = injectedFileContentSize;
            _injectedFilePathSize = injectedFilePathSize;
            _securityGroups = securityGroups;
            _securityGroupRules = securityGroupRules;
        }

        public QuotaSet(long? cores, long? fixedAddresses, long? floatingAddresses, long? instances, long? keyPairs, long? metadataItems, long? memory, long? injectedFiles, long? injectedFileContentSize, long? injectedFilePathSize, long? securityGroups, long? securityGroupRules, IDictionary<string, JToken> extensionData)
            : base(extensionData)
        {
            _cores = cores;
            _fixedAddresses = fixedAddresses;
            _floatingAddresses = floatingAddresses;
            _instances = instances;
            _keyPairs = keyPairs;
            _metadataItems = metadataItems;
            _memory = memory;
            _injectedFiles = injectedFiles;
            _injectedFileContentSize = injectedFileContentSize;
            _injectedFilePathSize = injectedFilePathSize;
            _securityGroups = securityGroups;
            _securityGroupRules = securityGroupRules;
        }

        public long? Cores
        {
            get
            {
                return _cores;
            }
        }

        public long? FixedAddresses
        {
            get
            {
                return _fixedAddresses;
            }
        }

        public long? FloatingAddresses
        {
            get
            {
                return _floatingAddresses;
            }
        }

        public long? Instances
        {
            get
            {
                return _instances;
            }
        }

        public long? KeyPairs
        {
            get
            {
                return _keyPairs;
            }
        }

        public long? MetadataItems
        {
            get
            {
                return _metadataItems;
            }
        }

        public long? Memory
        {
            get
            {
                return _memory;
            }
        }

        public long? InjectedFiles
        {
            get
            {
                return _injectedFiles;
            }
        }

        public long? InjectedFileContentSize
        {
            get
            {
                return _injectedFileContentSize;
            }
        }

        public long? InjectedFilePathSize
        {
            get
            {
                return _injectedFilePathSize;
            }
        }

        public long? SecurityGroups
        {
            get
            {
                return _securityGroups;
            }
        }

        public long? SecurityGroupRules
        {
            get
            {
                return _securityGroupRules;
            }
        }
    }
}