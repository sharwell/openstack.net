namespace net.openstack.Providers.Rackspace.Objects.Monitoring
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using net.openstack.Core.Collections;
    using Newtonsoft.Json;

    [JsonObject(MemberSerialization.OptIn)]
    public class UpdateNotificationPlanConfiguration
    {
        /// <summary>
        /// This is the backing field for the <see cref="Label"/> property.
        /// </summary>
        [JsonProperty("label", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private string _label;

        /// <summary>
        /// This is the backing field for the <see cref="CriticalState"/> property.
        /// </summary>
        [JsonProperty("critical_state", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private NotificationId[] _criticalState;

        /// <summary>
        /// This is the backing field for the <see cref="WarningState"/> property.
        /// </summary>
        [JsonProperty("warning_state", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private NotificationId[] _warningState;

        /// <summary>
        /// This is the backing field for the <see cref="OkState"/> property.
        /// </summary>
        [JsonProperty("ok_state", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private NotificationId[] _okState;

        /// <summary>
        /// This is the backing field for the <see cref="Metadata"/> property.
        /// </summary>
        [JsonProperty("metadata", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private IDictionary<string, string> _metadata;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateNotificationPlanConfiguration"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected UpdateNotificationPlanConfiguration()
        {
        }

        public UpdateNotificationPlanConfiguration(string label = null, IEnumerable<NotificationId> criticalState = null, IEnumerable<NotificationId> warningState = null, IEnumerable<NotificationId> okState = null, IDictionary<string, string> metadata = null)
        {
            _label = label;
            _metadata = metadata;

            if (criticalState != null)
            {
                _criticalState = criticalState.ToArray();
                if (_criticalState.Contains(null))
                    throw new ArgumentException("criticalState cannot contain any null values", "criticalState");
            }

            if (warningState != null)
            {
                _warningState = warningState.ToArray();
                if (_warningState.Contains(null))
                    throw new ArgumentException("warningState cannot contain any null values", "warningState");
            }

            if (okState != null)
            {
                _okState = okState.ToArray();
                if (_okState.Contains(null))
                    throw new ArgumentException("okState cannot contain any null values", "okState");
            }
        }

        /// <summary>
        /// Gets the friendly name for the notification plan.
        /// </summary>
        public string Label
        {
            get
            {
                return _label;
            }
        }

        /// <summary>
        /// The notification list to send to when the state is CRITICAL.
        /// </summary>
        public ReadOnlyCollection<NotificationId> CriticalState
        {
            get
            {
                if (_criticalState == null)
                    return null;

                return new ReadOnlyCollection<NotificationId>(_criticalState);
            }
        }

        /// <summary>
        /// The notification list to send to when the state is WARNING.
        /// </summary>
        public ReadOnlyCollection<NotificationId> WarningState
        {
            get
            {
                if (_warningState == null)
                    return null;

                return new ReadOnlyCollection<NotificationId>(_warningState);
            }
        }

        /// <summary>
        /// The notification list to send to when the state is OK.
        /// </summary>
        public ReadOnlyCollection<NotificationId> OkState
        {
            get
            {
                if (_okState == null)
                    return null;

                return new ReadOnlyCollection<NotificationId>(_okState);
            }
        }

        /// <summary>
        /// Gets a collection of metadata associated with the notification plan.
        /// </summary>
        public ReadOnlyDictionary<string, string> Metadata
        {
            get
            {
                if (_metadata == null)
                    return null;

                return new ReadOnlyDictionary<string, string>(_metadata);
            }
        }
    }
}
