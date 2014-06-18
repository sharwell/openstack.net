namespace OpenStack.Services.BlockStorage.V1.QuotaSets
{
    using Newtonsoft.Json;
    using OpenStack.ObjectModel;

    [JsonObject(MemberSerialization.OptIn)]
    public class QuotaSetDetails : ExtensibleJsonObject
    {
        [JsonProperty("cores", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private QuotaDetail _cores;

        [JsonProperty("fixed_ips", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private QuotaDetail _fixedAddresses;

        [JsonProperty("floating_ips", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private QuotaDetail _floatingAddresses;

        [JsonProperty("instances", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private QuotaDetail _instances;

        [JsonProperty("key_pairs", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private QuotaDetail _keyPairs;

        [JsonProperty("metadata_items", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private QuotaDetail _metadataItems;

        [JsonProperty("ram", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private QuotaDetail _memory;

        [JsonProperty("injected_files", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private QuotaDetail _injectedFiles;

        [JsonProperty("security_groups", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private QuotaDetail _injectedFileContentSize;

        [JsonProperty("injected_file_content_bytes", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private QuotaDetail _injectedFilePathSize;

        [JsonProperty("injected_file_path_bytes", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private QuotaDetail _securityGroups;

        [JsonProperty("security_group_rules", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private QuotaDetail _securityGroupRules;

        /// <summary>
        /// Initializes a new instance of the <see cref="QuotaSetDetails"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected QuotaSetDetails()
        {
        }

        public QuotaDetail Cores
        {
            get
            {
                return _cores;
            }
        }

        public QuotaDetail FixedAddresses
        {
            get
            {
                return _fixedAddresses;
            }
        }

        public QuotaDetail FloatingAddresses
        {
            get
            {
                return _floatingAddresses;
            }
        }

        public QuotaDetail Instances
        {
            get
            {
                return _instances;
            }
        }

        public QuotaDetail KeyPairs
        {
            get
            {
                return _keyPairs;
            }
        }

        public QuotaDetail MetadataItems
        {
            get
            {
                return _metadataItems;
            }
        }

        public QuotaDetail Memory
        {
            get
            {
                return _memory;
            }
        }

        public QuotaDetail InjectedFiles
        {
            get
            {
                return _injectedFiles;
            }
        }

        public QuotaDetail InjectedFileContentSize
        {
            get
            {
                return _injectedFileContentSize;
            }
        }

        public QuotaDetail InjectedFilePathSize
        {
            get
            {
                return _injectedFilePathSize;
            }
        }

        public QuotaDetail SecurityGroups
        {
            get
            {
                return _securityGroups;
            }
        }

        public QuotaDetail SecurityGroupRules
        {
            get
            {
                return _securityGroupRules;
            }
        }
    }
}