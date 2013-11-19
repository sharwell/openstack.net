namespace net.openstack.Providers.Rackspace.Objects.Monitoring
{
    using System;
    using System.Collections.Generic;
    using net.openstack.Core.Collections;
    using Newtonsoft.Json;

    [JsonObject(MemberSerialization.OptIn)]
    public class AlarmConfiguration
    {
        [JsonProperty("check_id", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private CheckId _checkId;

        [JsonProperty("notification_plan_id", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private NotificationPlanId _notificationPlanId;

        [JsonProperty("criteria", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private string _criteria;

        [JsonProperty("disabled", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private bool? _disabled;

        [JsonProperty("label", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private string _label;

        [JsonProperty("metadata", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private IDictionary<string, string> _metadata;

        /// <summary>
        /// Initializes a new instance of the <see cref="AlarmConfiguration"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected AlarmConfiguration()
        {
        }

        public AlarmConfiguration(CheckId checkId, NotificationPlanId notificationPlanId, string criteria = null, bool? enabled = null, string label = null, IDictionary<string, string> metadata = null)
        {
            if (checkId == null)
                throw new ArgumentNullException("checkId");
            if (notificationPlanId == null)
                throw new ArgumentNullException("notificationPlanId");

            _checkId = checkId;
            _notificationPlanId = notificationPlanId;
            _criteria = criteria;
            _disabled = !enabled;
            _label = label;
            _metadata = metadata;
        }

        /// <summary>
        /// Gets the ID of the check to alert on.
        /// </summary>
        /// <seealso cref="Check.Id"/>
        public CheckId CheckId
        {
            get
            {
                return _checkId;
            }
        }

        /// <summary>
        /// Gets the ID of the notification plan to execute when the state changes.
        /// </summary>
        /// <seealso cref="NotificationPlan.Label"/>
        public NotificationPlanId NotificationPlanId
        {
            get
            {
                return _notificationPlanId;
            }
        }

        /// <summary>
        /// Gets the alarm DSL for describing alerting conditions and their output states.
        /// </summary>
        public string Criteria
        {
            get
            {
                return _criteria;
            }
        }

        public bool? Enabled
        {
            get
            {
                if (!_disabled.HasValue)
                    return null;

                return !_disabled.Value;
            }
        }

        public string Label
        {
            get
            {
                return _label;
            }
        }

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
