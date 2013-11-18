﻿namespace net.openstack.Providers.Rackspace.Objects.Monitoring
{
    using System.Collections.ObjectModel;
    using Newtonsoft.Json;

    [JsonObject(MemberSerialization.OptIn)]
    public class CheckType
    {
#pragma warning disable 649 // Field 'fieldName' is never assigned to, and will always have its default value {value}
        [JsonProperty("id")]
        private CheckTypeId _id;

        [JsonProperty("type")]
        private CheckTypeType _type;

        [JsonProperty("fields")]
        private NotificationTypeField[] _fields;
#pragma warning restore 649

        /// <summary>
        /// Initializes a new instance of the <see cref="CheckType"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected CheckType()
        {
        }

        public CheckTypeId Id
        {
            get
            {
                return _id;
            }
        }

        public CheckTypeType Type
        {
            get
            {
                return _type;
            }
        }

        public ReadOnlyCollection<NotificationTypeField> Fields
        {
            get
            {
                if (_fields == null)
                    return null;

                return new ReadOnlyCollection<NotificationTypeField>(_fields);
            }
        }
    }
}
