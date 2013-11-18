namespace net.openstack.Providers.Rackspace.Objects.Monitoring
{
    using System.Collections.ObjectModel;
    using Newtonsoft.Json;

    [JsonObject(MemberSerialization.OptIn)]
    public class AlarmExample
    {
#pragma warning disable 649 // Field 'fieldName' is never assigned to, and will always have its default value {value}
        [JsonProperty("id")]
        private AlarmExampleId _id;

        [JsonProperty("label")]
        private string _label;

        [JsonProperty("description")]
        private string _description;

        [JsonProperty("check_type")]
        private CheckTypeId _checkTypeId;

        [JsonProperty("criteria")]
        private string _criteria;

        [JsonProperty("fields")]
        private AlarmExampleField[] _fields;
#pragma warning restore 649

        /// <summary>
        /// Initializes a new instance of the <see cref="AlarmExample"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected AlarmExample()
        {
        }

        public AlarmExampleId Id
        {
            get
            {
                return _id;
            }
        }

        public string Label
        {
            get
            {
                return _label;
            }
        }

        public string Description
        {
            get
            {
                return _description;
            }
        }

        public CheckTypeId CheckTypeId
        {
            get
            {
                return _checkTypeId;
            }
        }

        public string Criteria
        {
            get
            {
                return _criteria;
            }
        }

        public ReadOnlyCollection<AlarmExampleField> Fields
        {
            get
            {
                if (_fields == null)
                    return null;

                return new ReadOnlyCollection<AlarmExampleField>(_fields);
            }
        }
    }
}
